namespace Shared;

public record SuperHero(string Name, string RealName, string[] SuperPowers, DateTime? LastSave = null)
{
    public bool ReadyToSaveTheWorld => (LastSave is null || LastSave < DateTime.Now.AddMinutes(-59));
}
