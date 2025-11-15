public class DeepseekApiClient
{
    private static readonly HttpClient client = new HttpClient();
    public async Task<string> SendChatRequestAsync(DeepseekMessage[] previousMessages)
    {
        string apiKey = Constants.DeepSeekApiKey;

        if (string.IsNullOrEmpty(apiKey))
        {
            return "Error: CRITICAL_FAILURE. The server environment variable 'PROFILE_ASSISTANT_API_KEY' is NULL or EMPTY. The service must be restarted or the variable is in the wrong location.";
        }

        var messages = new List<DeepseekMessage>();
        messages.Add(new DeepseekMessage { Role = "system", Content = "You are a helpful assistant." });
        foreach (DeepseekMessage item in previousMessages)
        {
            messages.Add(new DeepseekMessage { Role = item.Role, Content = item.Content });
        }

        var requestPayload = new DeepseekChatRequest
        {
            Model = "deepseek-chat",
            Stream = false,
            Messages = messages
        };

        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);
        var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, Constants.DeepSeekChatUrl);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        httpRequest.Content = httpContent;

        try
        {
            HttpResponseMessage response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                return $"Error: {response.StatusCode}\n{errorBody}";
            }
        }
        catch (Exception ex)
        {
            return $"Exception: {ex.Message}";
        }
    }
}