namespace mkryuchkov.BaristaBot.DataScrapper.Model;

public class GrindDto
{
    public Guid Id { get; init; }
    public float Step { get; init; }
    public float SubStep { get; init; }
    public float Amount { get; init; }
    public float Coarse { get; init; }
    public float HighAvg { get; init; }
    public float LowAvg { get; init; }
    public float Fine { get; init; }
}