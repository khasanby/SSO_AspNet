namespace Notes.Model
{
    public class Note
    {      
        public Guid Id { get; set; }

        public string NoteText { get; set; } = string.Empty;
    }
}
