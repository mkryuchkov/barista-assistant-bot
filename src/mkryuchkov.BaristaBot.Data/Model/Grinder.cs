namespace mkryuchkov.BaristaBot.Data.Model;

public class Grinder
{
    public Guid Id { get; init; }
    public string Manufacturer { get; init; } = null!;
    public string Model { get; init; } = null!;
    public bool IsDiscrete { get; init; } 

    public IList<Grind> Grinds { get; set; } = Array.Empty<Grind>();
}