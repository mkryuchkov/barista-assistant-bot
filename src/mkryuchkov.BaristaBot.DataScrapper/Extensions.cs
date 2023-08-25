using mkryuchkov.BaristaBot.DataScrapper.Model;

namespace mkryuchkov.BaristaBot.DataScrapper;

public static class Extensions
{
    public static float DistanceTo(this GrindDto from, GrindDto to) =>
        Math.Abs(from.Coarse - to.Coarse) +
        Math.Abs(from.HighAvg - to.HighAvg) +
        Math.Abs(from.LowAvg - to.LowAvg) +
        Math.Abs(from.Fine - to.Fine);

    public static float Value(this GrindDto dto) =>
        float.Parse($"{dto.Step}.{dto.SubStep}");
}