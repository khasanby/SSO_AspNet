using System.ComponentModel.DataAnnotations;

namespace Notes.Model
{
    public class NoteForUpdate(string noteText)
    {
        [Required]
        [MaxLength(150)]
        public string NoteText { get; set; } = noteText;
    }
}
