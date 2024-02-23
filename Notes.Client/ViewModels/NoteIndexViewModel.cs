using Notes.Model;

namespace Notes.Client.ViewModels
{
    public class NoteIndexViewModel(Note[] notes)
    {
        public Note[] Notes { get; private set; } = notes;
    }
}
