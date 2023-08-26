using System.Globalization;
using System.Text.Json;
using CsvHelper;
using mkryuchkov.BaristaBot.Data.Model;

Console.WriteLine("Start");

using var reader = new StreamReader(@"Source/Commandante_C40.csv");
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

csv.Read();
csv.ReadHeader();
csv.Read();
var grinder = csv.GetRecord<Grinder>()!;

csv.Read();
csv.ReadHeader();
csv.Read();
grinder.Grinds = csv.GetRecords<Grind>().ToList();

Console.WriteLine($"{JsonSerializer.Serialize(grinder)}");

Console.WriteLine("Done!");