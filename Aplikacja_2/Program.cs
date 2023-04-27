// connect
using var client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

// get content
var content = await client.GetStringAsync("https://konni.delthas.fr/games");

Console.WriteLine(content);

Console.ReadLine();

// fetch list
content = await client.GetStringAsync("https://konni.delthas.fr/games");
Console.WriteLine(content);
//