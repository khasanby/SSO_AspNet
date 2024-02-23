using Microsoft.EntityFrameworkCore;
using Notes.Api.DbContexts;
using Notes.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(configure => configure.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddDbContext<NotesContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:NotesDBConnectionString"]);
});

// Register the repository.
builder.Services.AddScoped<INotesRepository, NotesRepository>();

// Register AutoMapper-related services.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
