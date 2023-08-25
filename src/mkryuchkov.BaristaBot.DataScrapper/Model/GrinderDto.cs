namespace mkryuchkov.BaristaBot.DataScrapper.Model;

public class GrinderDto
{
    public Guid Id { get; init; }
    public string Manufacturer { get; init; } = null!;
    public string Model { get; init; } = null!;
    public IList<GrindDto> Grinds { get; set; } = Array.Empty<GrindDto>();
}