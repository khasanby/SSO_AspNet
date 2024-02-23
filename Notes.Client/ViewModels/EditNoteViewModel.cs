using System.ComponentModel.DataAnnotations;

namespace Notes.Client.ViewModels
{
    public class EditNoteViewModel
    {
        [Required]
        public string NoteText { get; set; } = string.Empty;

        [Required]
        public Guid Id { get; set; }  
    }
}
