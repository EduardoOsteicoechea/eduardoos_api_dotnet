try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/api", () =>
    {
        return "Thanks Lord";
    });

    app.MapPost("/api/profile/assistant", async (DeepseekMessage[] messages) =>
    { 
        var client = new DeepseekApiClient();
        var response = await client.SendChatRequestAsync(messages);
        return response;
    });

    app.Run();
}
catch (System.Exception ex)
{
    Console.WriteLine($"{ex.Message} - {ex.StackTrace}");
}