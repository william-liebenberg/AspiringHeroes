using Shared;

namespace AspiringHeroes.Web;

public class HeroesApiClient(HttpClient httpClient)
{
    public async Task<SuperHero[]> GetHeroesAsync()
    {
        return await httpClient.GetFromJsonAsync<SuperHero[]>("/heroSchedule") ?? [];
    }
}