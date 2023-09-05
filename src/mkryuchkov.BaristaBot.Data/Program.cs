using System.Globalization;
using CsvHelper;
using mkryuchkov.BaristaBot.Data.Model;

Console.WriteLine("Start");

var c40 = await GetGrinderAsync("Source/Commandante_C40.csv");
var ditting = await GetGrinderAsync("Source/Ditting_807.csv");
var niche = await GetGrinderAsync("Source/Niche_Zero.csv");

foreach (var dittingGrind in ditting.Grinds)
{
    Console.WriteLine($"Ditting {dittingGrind.Value:F} - c40 {dittingGrind.GetNearest(c40).Value}");
}

foreach (var c40Grind in c40.Grinds)
{
    Console.WriteLine($"C40 {c40Grind.Value:F0} - niche {c40Grind.GetNearest(niche).Value}");
}

foreach (var dittingGrind in ditting.Grinds)
{
    Console.WriteLine($"Ditting {dittingGrind.Value:F} - niche {dittingGrind.GetNearest(niche).Value}");
}

Console.WriteLine("Done!");
return;

async Task<Grinder> GetGrinderAsync(string csvPath)
{
    using var reader = new StreamReader(csvPath);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

    await csv.ReadAsync();
    csv.ReadHeader();
    await csv.ReadAsync();
    var grinderRecord = csv.GetRecord<Grinder>()!;

    await csv.ReadAsync();
    csv.ReadHeader();
    grinderRecord.Grinds = csv.GetRecords<Grind>().ToList();

    return grinderRecord;
}

public static class Extensions
{
    public static Grind GetNearest(this Grind grind, Grinder from)
        => from.Grinds.MinBy(g => g.DistanceTo(grind))!;

    public static float DistanceTo(this Grind from, Grind to) =>
        Math.Abs(from.Coarse - to.Coarse) +
        Math.Abs(from.MidHigh - to.MidHigh) +
        Math.Abs(from.MidLow - to.MidLow) +
        Math.Abs(from.Fine - to.Fine);
}