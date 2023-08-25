// using System.Text.Json;
// using mkryuchkov.BaristaBot.DataScrapper;
// using mkryuchkov.BaristaBot.DataScrapper.Model;
// using File = System.IO.File;
//
// Console.WriteLine("Start");
//
// var serializerOptions = new JsonSerializerOptions
// {
//     MaxDepth = 10,
//     PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance // JsonNamingPolicy.SnakeCaseLower
// };
//
// using var httpClient = new HttpClient(new HttpClientHandler
// {
//     ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
// });
// httpClient.BaseAddress = new Uri(@"https://v.theweldercatherine.ru/api/v1/");
// httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("curl/8.1.2");
// httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");
//
// async Task<T?> GetDataAsync<T>(string query, HttpClient httpclient)
// {
//     var responseMessage = await httpclient.GetAsync(query);
//
//     return await JsonSerializer.DeserializeAsync<T>(
//         await responseMessage.Content.ReadAsStreamAsync(),
//         serializerOptions
//     );
// }
//
// var grinders = (await GetDataAsync<ListResponse<GrinderDto>>("grinders", httpClient))?.Value;
//
// Console.WriteLine(string.Join('\n', grinders?.Select(g => $"{g.Manufacturer} {g.Model}") ?? new[] { "Empty" }));
//
// if (grinders is null)
// {
//     return;
// }
//
// foreach (var grinder in grinders)
// {
//     //https://v.theweldercatherine.ru/api/v1/grinds?grinder_id=0473812d-4e43-fc12-73a8-ad11954ec287&no_auto=true
//     var grinds = (await GetDataAsync<ListResponse<GrindDto>>(
//         $"grinds?grinder_id={grinder.Id}&no_auto=true", httpClient))?.Value;
//
//     if (grinds is not null && grinds.Count > 0)
//     {
//         grinder.Grinds = grinds;
//         Console.WriteLine($"Found {grinds.Count} grinds for {grinder.Model} grinder");
//     }
// }
//
// await using var file = File.Open("./data.json", FileMode.Create);
// await using var writer = new StreamWriter(file);
//
// await writer.WriteAsync(JsonSerializer.Serialize(grinders));
//
// Console.WriteLine("Done!");