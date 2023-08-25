namespace mkryuchkov.BaristaBot.DataScrapper.Model;

public class ListResponse<T>
    where T : class
{
    public string Status { get; init; } = null!;
    public IList<T> Value { get; init; } = Array.Empty<T>();
}