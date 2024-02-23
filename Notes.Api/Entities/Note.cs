using System.ComponentModel.DataAnnotations;

namespace Notes.Api.Entities
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string NoteText { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string OwnerId { get; set; } = string.Empty;
    }
}
