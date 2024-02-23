using System.ComponentModel.DataAnnotations;

namespace Notes.Model
{
    public class NoteForCreation(string noteText)
    {
        [Required]
        [MaxLength(150)]
        public string NoteText { get; set; } = noteText;
    }
}
