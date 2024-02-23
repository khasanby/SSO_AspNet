using Notes.Api.DbContexts;
using Notes.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Notes.Api.Services
{
    public class NotesRepository : INotesRepository 
    {
        private readonly NotesContext _context;

        public NotesRepository(NotesContext notesContext)
        {
            _context = notesContext ?? 
                throw new ArgumentNullException(nameof(notesContext));
        }

        public async Task<bool> NoteExistsAsync(Guid id)
        {
            return await _context.Notes.AnyAsync(i => i.Id == id);
        }       

        public async Task<Note?> GetNoteAsync(Guid id)
        {
            return await _context.Notes.FirstOrDefaultAsync(i => i.Id == id);
        }
  
        public async Task<Note[]> GetNotesAsync()
        {
            return await _context.Notes.OrderBy(i => i.NoteText).ToArrayAsync();
        }

        public async Task<bool> IsNoteOwnerAsync(Guid id, string ownerId)
        {
            return await _context.Notes.AnyAsync(i => i.Id == id && i.OwnerId == ownerId);
        }
        
        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
        }

        public void UpdateNote(Note note)
        {
            // no code in this implementation
        }

        public void DeleteNote(Note note)
        {
            _context.Notes.Remove(note);

            // Note: in a real-life scenario, the image itself potentially should 
            // be removed from disk.  We don't do this in this demo
            // scenario to allow for easier testing / re-running the code
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        } 
    }
}
