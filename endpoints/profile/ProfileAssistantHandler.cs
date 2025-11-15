public class ProfileAssistantHandler
{
    async public Task<string> Response(string message)
    {
        var client = new DeepseekApiClient();
        var response = await client.SendChatRequestAsync(message);
        return response;
    }
}