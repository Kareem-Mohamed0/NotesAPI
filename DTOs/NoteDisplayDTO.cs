namespace NotesAPI.DTOs
{
    public class NoteDisplayDTO
    {
            public int id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string? Content { get; set; }
            public string Color { get; set; } = "#ffffff";
            public bool IsPinned { get; set; }
            public bool IsArchived { get; set; }
            public string? DrawingData { get; set; }
        
    }
}
