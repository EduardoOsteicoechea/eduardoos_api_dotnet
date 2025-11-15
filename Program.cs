using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/eduardoos_api.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();



try
{
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.MapGet("/api", () =>
    {
        Log.Information("Received Request");
        return "Thanks Lord";
    });

    app.MapGet("/api/profile/assistant", async () =>
    {
        var handler = new ProfileAssistantHandler();
        return await handler.Response();
    });

    app.Run();
}
catch (System.Exception ex)
{
    Log.Fatal($"{ex.Message} - {ex.StackTrace}");
}
finally
{
    Log.CloseAndFlush();
}