using AutoMapper;
using NotesAPI.DTOs;
using NotesAPI.Models;

namespace NotesAPI.HelperTools
{
    public class MappingProfileAutoMapper : Profile
    {
        public MappingProfileAutoMapper()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Note, NoteDisplayDTO>().ReverseMap();
        }
    }
}
