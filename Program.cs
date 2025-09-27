var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/api", () =>
{
    Console.WriteLine("Received Request");
    return "Thanks Lord";
});

app.Run();