using Notes.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Notes.Api.DbContexts
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;

        public NotesContext(DbContextOptions<NotesContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Note>().HasData(
                new Note()
                {
                    Id = Guid.NewGuid(),
                    NoteText = "A note by David",
                    OwnerId = "d860efca-22d9-47fd-8249-791ba61b07c7"
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    NoteText = "Another note by David",
                    OwnerId = "d860efca-22d9-47fd-8249-791ba61b07c7"
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    NoteText = "A note by Emma",
                    OwnerId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7"
                },
                new Note()
                {
                    Id = Guid.NewGuid(),
                    NoteText = "Another note by Emma",
                    OwnerId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
