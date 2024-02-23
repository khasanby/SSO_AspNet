using Notes.Api.Entities;

namespace Notes.Api.Services
{
    public interface INotesRepository
    {
        Task<Note[]> GetNotesAsync();
        Task<bool> IsNoteOwnerAsync(Guid id, string ownerId);
        Task<Note?> GetNoteAsync(Guid id);
        Task<bool> NoteExistsAsync(Guid id);
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(Note note);
        Task<bool> SaveChangesAsync();
    }
}
