using AspiringHeroes.Grpc;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AspiringHeroes.Grpc.Services;

public class HeroScheduleService : HeroSchedule.HeroScheduleBase
{
    private readonly List<GrpcSuperHero> _heroes =
    [
        new()
        {
            Name = "Superman",
            RealName = "Clark Kent",
            Superpowers = { new[] { "Flight", "Super strength", "Heat vision" } },
            LastSave = null
        },
        new()
        {
            Name = "Spider-Man",
            RealName = "Peter Parker",
            Superpowers = { new[] { "Wall-crawling", "Web-slinging", "Spidey sense" } },
            LastSave = null
        },
        new()
        {
            Name = "Wonder Woman",
            RealName = "Diana Prince",
            Superpowers = { new[] { "Superhuman strength", "Lasso of Truth", "Flight" } },
            LastSave = null
        },
        new()
        {
            Name = "Batman",
            RealName = "Bruce Wayne",
            Superpowers = { new[] { "Intelligence", "Martial arts", "Gadgets" } },
            LastSave = null
        },
        new()
        {
            Name = "Captain America",
            RealName = "Steve Rogers",
            Superpowers = { new[] { "Superhuman strength", "Shield", "Leadership" } },
            LastSave = null
        }
    ];

    public override async Task<ScheduleResponse> GetHeroesSchedule(Empty request, ServerCallContext context)
    {
        List<GrpcSuperHero> schedule = new();

        foreach (var hero in _heroes)
        {
            var newhero = hero.Clone();
            newhero.LastSave = Timestamp.FromDateTime(DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(Random.Shared.NextDouble() * 240)));
            schedule.Add(newhero);
        }

        return await Task.FromResult(new ScheduleResponse()
        {
            Schedule = { schedule }
        });
    }
}
