try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/api", () =>
    {
        return "Thanks Lord";
    });

    app.MapPost("/api/profile/assistant", async (DeepseekMessage message) =>
    {
        var handler = new ProfileAssistantHandler();
        return await handler.Response(message.Content);
    });

    app.Run();
}
catch (System.Exception ex)
{
    Console.WriteLine($"{ex.Message} - {ex.StackTrace}");
}