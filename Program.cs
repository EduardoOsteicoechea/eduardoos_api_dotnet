try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    await Constants.CollectRagData();

    app.MapGet("/api", () =>
    {
        return "Thanks Lord For all of this";
    });

    app.MapPost("/api/profile/assistant", async (DeepseekChat messages) =>
    {
        var client = new DeepseekApiClient();
        var response = await client.SendChatRequestAsync(messages.Messages);
        return response;
    });

    app.Run();
}
catch (System.Exception ex)
{
    Console.WriteLine($"{ex.Message} - {ex.StackTrace}");
}