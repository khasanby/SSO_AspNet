using System.ComponentModel.DataAnnotations;

namespace Notes.Client.ViewModels
{
    public class AddNoteViewModel
    {
        [Required]
        public string NoteText { get; set; }

        public AddNoteViewModel(string noteText)
        {
            NoteText = noteText;
        }

        public AddNoteViewModel()
        {

        }
    }
}
