using api.Data;
using api.Repositories;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Register AppDbContext so it can be handed to any class that asks for it.
//UseSqllite means our database is a single file on disk (app.db)
//the connection string "DatabaseConnection" is read from apsettings.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));


builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IActorServices, ActorServices>();


// Everything above registers services. Build() creates the app
// Everything below sets up what happens with each request
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// send any http:// request over to https://
app.UseHttpsRedirection();

//checks [Authorize] attributes. We don't have any yet, but it comes baked in.
app.UseAuthorization();

// connects each controller's routes, like api/movies.
app.MapControllers();
// Start the server and wait for requests.
app.Run();
