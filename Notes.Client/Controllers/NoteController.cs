using Notes.Client.ViewModels;
using Notes.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Text;
using System.Text.Json;

namespace Notes.Client.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<NoteController> _logger;

        public NoteController(IHttpClientFactory httpClientFactory,
            ILogger<NoteController> logger)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            await LogIndentityInformation();
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/notes/");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var notes = await JsonSerializer.DeserializeAsync<Note[]>(responseStream);

            return View(new NoteIndexViewModel(notes ?? []));
        }

        public async Task<IActionResult> EditNote(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/notes/{id}");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var deserializedNote = await JsonSerializer.DeserializeAsync<Note>(responseStream);
            if (deserializedNote == null)
                throw new Exception("Deserialized note must not be null.");

            var editNoteViewModel = new EditNoteViewModel()
            {
                Id = deserializedNote.Id,
                NoteText = deserializedNote.NoteText
            };

            return View(editNoteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNote(EditNoteViewModel editNoteViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            var noteForUpdate = new NoteForUpdate(editNoteViewModel.NoteText);
            var serializedNoteForUpdate = JsonSerializer.Serialize(noteForUpdate);
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/notes/{editNoteViewModel.Id}")
            {
                Content = new StringContent(serializedNoteForUpdate, Encoding.Unicode, "application/json")
            };
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/notes/{id}");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public IActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(AddNoteViewModel addNoteViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            NoteForCreation? noteForCreation = new NoteForCreation(addNoteViewModel.NoteText);
            var serializedNoteForCreation = JsonSerializer.Serialize(noteForCreation);
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/notes")
            {
                Content = new StringContent(serializedNoteForCreation, Encoding.Unicode, "application/json")
            };

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public async Task LogIndentityInformation()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var userClaimsStringBuilder = new StringBuilder();
            foreach(var claim in User.Claims)
                userClaimsStringBuilder.AppendLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            
            _logger.LogInformation($"Identity token & user claims: " + $"\n{identityToken} \n{userClaimsStringBuilder}");
        }
    }
}
