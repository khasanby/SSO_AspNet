using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Services;
using Notes.Model;

namespace Notes.Api.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;
        private readonly IMapper _mapper;

        public NotesController(
            INotesRepository notesRepository,
            IMapper mapper)
        {
            _notesRepository = notesRepository ??
                throw new ArgumentNullException(nameof(notesRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<Note[]>> GetNotesAsync()
        {
            var notesFromRepo = await _notesRepository.GetNotesAsync();
            var notesToReturn = _mapper.Map<Note[]>(notesFromRepo);

            return Ok(notesToReturn);
        }

        [HttpGet("{id}", Name = "GetNote")]
        public async Task<ActionResult<Note>> GetNoteAsync(Guid id)
        {
            var noteFromRepo = await _notesRepository.GetNoteAsync(id);
            if (noteFromRepo == null)
                return NotFound();

            var noteToReturn = _mapper.Map<Note>(noteFromRepo);
            return Ok(noteToReturn);
        }

        [HttpPost()]
        public async Task<ActionResult<Note>> CreateNoteAsync([FromBody] NoteForCreation noteForCreation)
        {
            var noteEntity = _mapper.Map<Entities.Note>(noteForCreation);
            _notesRepository.AddNote(noteEntity);
            await _notesRepository.SaveChangesAsync();
            var noteToReturn = _mapper.Map<Note>(noteEntity);

            return CreatedAtRoute("GetNote", new { id = noteToReturn.Id }, noteToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(Guid id)
        {
            var noteFromRepo = await _notesRepository.GetNoteAsync(id);
            if (noteFromRepo == null)
                return NotFound();

            _notesRepository.DeleteNote(noteFromRepo);
            await _notesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteAsync(Guid id, [FromBody] NoteForUpdate noteForUpdate)
        {
            var noteFromRepo = await _notesRepository.GetNoteAsync(id);
            if (noteFromRepo == null)
                return NotFound();

            _mapper.Map(noteForUpdate, noteFromRepo);
            _notesRepository.UpdateNote(noteFromRepo);
            await _notesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}