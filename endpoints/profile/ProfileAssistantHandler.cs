using Serilog;

public class ProfileAssistantHandler
{
    public string ApiKey = Environment.GetEnvironmentVariable("PROFILE_ASSISTANT_API_KEY");
    public string Response()
    {
        Log.Information("Log from profile");
        return "This is the response";

    }
}