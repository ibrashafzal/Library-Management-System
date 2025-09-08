using System.Threading;

using var client = new HttpClient();

var endpoint = "https://localhost:7255/api/Student";
using var resp = await client.GetAsync(endpoint, new CancellationTokenSource(TimeSpan.FromSeconds(15)).Token);

var content = await resp.Content.ReadAsStringAsync();

Console.WriteLine(content);