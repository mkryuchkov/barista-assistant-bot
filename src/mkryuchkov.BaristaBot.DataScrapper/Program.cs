using System.Text.Json;
using mkryuchkov.BaristaBot.DataScrapper;
using mkryuchkov.BaristaBot.DataScrapper.Model;
using File = System.IO.File;

Console.WriteLine("Start");

async Task<GrinderDto> ReadFileAsync(string path, CancellationToken token = default)
{
    await using var file = File.Open(path, FileMode.Open);
    using var reader = new StreamReader(file);
    return JsonSerializer.Deserialize<GrinderDto>(await reader.ReadToEndAsync(token))
           ?? throw new InvalidOperationException();
}

float ConvertFromPreset(GrinderDto from, GrinderDto to, Guid grindId)
{
    var source = from.Grinds.First(g => g.Id == grindId);
    return FindNearestByDistance(to, source).Value();
}

GrindDto FindNearestByDistance(GrinderDto from, GrindDto grind)
    => from.Grinds.MinBy(g => g.DistanceTo(grind))!;

GrindDto FindNearestByValue(GrinderDto from, float value)
    => from.Grinds.MinBy(g => Math.Abs(g.Value() - value))!;

GrindDto GetApproximated(GrinderDto from, float value)
{
    var source = FindNearestByValue(from, value);

    var percent = source.Value() / value;

    return new GrindDto
    {
        Step = (float)Math.Truncate(value),
        SubStep = (float)(value - Math.Truncate(value)),
        Amount = source.Amount,
        Coarse = source.Coarse * percent,
        HighAvg = source.HighAvg * percent,
        LowAvg = source.LowAvg * percent,
        Fine = source.Fine * percent
    };
}

float ConvertFromValue(GrinderDto from, GrinderDto to, float value)
{
    var approxGrind = GetApproximated(from, value);

    var target = FindNearestByDistance(to, approxGrind);

    var percentB = (
        target.Coarse / approxGrind.Coarse +
        target.HighAvg / approxGrind.HighAvg +
        target.LowAvg / approxGrind.LowAvg +
        target.Fine / approxGrind.Fine
    ) / 4;

    var result = target.Value() * percentB;

    return result;
}

var c40 = await ReadFileAsync("Data/data.C40.json");
var ditting = await ReadFileAsync("Data/data.ditting.json");

const float dittingValue = 7.0f;

var nearest = FindNearestByValue(ditting, dittingValue);
Console.WriteLine($"Ditting nearest to {dittingValue:F1} is {nearest.Value():F1}:\n{JsonSerializer.Serialize(nearest)}");

var fromPreset = ConvertFromPreset(ditting, c40, nearest.Id);
Console.WriteLine($"Ditting from nearest {nearest.Value():F1} = C40 {fromPreset:F1}");

var approx = GetApproximated(ditting, dittingValue);
Console.WriteLine($"Ditting approximated to {dittingValue:F1} is\n{JsonSerializer.Serialize(approx)}\n{approx.HighAvg + approx.LowAvg + approx.Coarse + approx.Fine:F}");

var fromApprox = FindNearestByDistance(c40, approx).Value();
Console.WriteLine($"Ditting from approximated {approx.Value():F1} = C40 {fromApprox:F1}");

var fromValue = ConvertFromValue(ditting, c40, dittingValue);
Console.WriteLine($"Ditting from value {dittingValue:F1} = C40 {fromValue}");

Console.WriteLine("Done!");