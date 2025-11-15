using Serilog;

public class ProfileAssistantHandler
{
    async public Task<string> Response(string message)
    {
        Log.Information("Log from profile");
        var client = new DeepseekApiClient();
        var response = await client.SendChatRequestAsync(message);
        return response;
    }
}