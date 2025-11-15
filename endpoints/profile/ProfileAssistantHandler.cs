using Serilog;

public class ProfileAssistantHandler
{
    public string ApiKey = Environment.GetEnvironmentVariable("PROFILE_ASSISTANT_API_KEY");
    async public Task<string> Response()
    {
        Log.Information("Log from profile");
        var client = new DeepseekApiClient();
        var response = await client.SendChatRequestAsync("say hello and explain what you do");
        return response;
    }
}