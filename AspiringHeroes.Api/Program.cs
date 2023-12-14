using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

var heroes = new SuperHero[]
{
    new("Superman", "Clark Kent", ["Flight", "Super strength", "Heat vision"]),
    new("Spider-Man", "Peter Parker", ["Wall-crawling", "Web-slinging", "Spidey sense"]),
    new("Wonder Woman", "Diana Prince", ["Superhuman strength", "Lasso of Truth", "Flight"]),
    new("Batman", "Bruce Wayne", ["Intelligence", "Martial arts", "Gadgets"]),
    new("Captain America", "Steve Rogers", ["Superhuman strength", "Shield", "Leadership"])
};

app.MapGet("/heroSchedule", () =>
{
    List<SuperHero> schedule = new();
    foreach(var hero in heroes)
    {
        var newhero = hero with
        {
            LastSave = DateTime.Now.Subtract(TimeSpan.FromMinutes(Random.Shared.NextDouble() * 240))
        };
        schedule.Add(newhero);
    }
    return schedule;
})
.WithName("GetHeroes")
.WithOpenApi();

app.Run();
