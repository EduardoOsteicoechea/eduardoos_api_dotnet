public class DeepseekApiClient
{
    private static readonly HttpClient client = new HttpClient();
    public async Task<string> SendChatRequestAsync(string userMessage)
    {
        var requestPayload = new DeepseekChatRequest
        {
            Model = "deepseek-chat",
            Stream = false,
            Messages = new List<DeepseekMessage>
            {
                new DeepseekMessage { Role = "system", Content = "You are a helpful assistant." },
                new DeepseekMessage { Role = "user", Content = userMessage }
            }
        };

        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);
        var httpContent = new StringContent(jsonPayload);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, Constants.DeepSeekChatUrl);
        httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.DeepSeekApiKey);
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