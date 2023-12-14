using AspiringHeroes.Web;
using AspiringHeroes.Web.Components;
using static AspiringHeroes.Grpc.HeroSchedule;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<HeroesApiClient>(client=> client.BaseAddress = new("http://heroesRestAPI"));

builder.Services.AddGrpcClient<HeroScheduleClient>(o => o.Address = new("http://heroesGrpcService"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
    
app.MapDefaultEndpoints();

app.Run();
