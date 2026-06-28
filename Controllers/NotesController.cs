using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.DTOs;
using NotesAPI.Models;
using NotesAPI.Repositories;
using System.Security.Claims;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("api/notes")]
    [Authorize]

    public class NotesController : ControllerBase
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IMapper mapper;

        public NotesController(IUnitOfwork unitOfwork,IMapper mapper)
        {
            this.unitOfwork = unitOfwork;
            this.mapper = mapper;
        }

        private int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDisplayDTO>>> GetNotes()
        {
            var notes = await unitOfwork.Notes.GetActiveNoteAsync(CurrentUserId);
            var notesDto = mapper.Map<IEnumerable<NoteDisplayDTO>>(notes);
            return Ok(notesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            var note = await unitOfwork.Notes.GetByIdAsync(id);
            if (note == null || note.UserId != CurrentUserId) return NotFound();
            var noteDto = mapper.Map<NoteDto>(note);
            return Ok(noteDto);
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> SearchNotes([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                var all = await unitOfwork.Notes.GetActiveNoteAsync(CurrentUserId);
                return Ok(all);
            }
            var notes = await unitOfwork.Notes.SearchAsync(q, CurrentUserId);
            var notesDto = mapper.Map<IEnumerable<NoteDto>>(notes);
            return Ok(notesDto);
        }
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote(NoteDto dto)
        {
            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                Color = dto.Color,
                IsPinned = dto.IsPinned,
                IsArchived = dto.IsArchived,
                DrawingData = dto.DrawingData,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = CurrentUserId
            };
            await unitOfwork.Notes.AddAsync(note);
            await unitOfwork.CompleteAsync();
            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, NoteDto dto)
        {
            var note = await unitOfwork.Notes.GetByIdAsync(id);
            if (note == null || note.UserId != CurrentUserId) return NotFound();
            note.Title = dto.Title;
            note.Content = dto.Content;
            note.Color = dto.Color;
            note.IsPinned = dto.IsPinned;
            note.IsArchived = dto.IsArchived;
            note.DrawingData = dto.DrawingData;
            note.UpdatedAt = DateTime.UtcNow;
            
            unitOfwork.Notes.Update(note);
            await unitOfwork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await unitOfwork.Notes.GetByIdAsync(id);
            if (note == null || note.UserId != CurrentUserId) return NotFound();
            unitOfwork.Notes.Remove(note);
            await unitOfwork.CompleteAsync();
            return NoContent();
        }
    }
}
