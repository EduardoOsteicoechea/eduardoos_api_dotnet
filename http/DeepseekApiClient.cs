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

        var modelTunningDataPath = "/var/www/html/dist/rag/about_eduardo_model_tunning.txt";
        var profileRagDataPath = "/var/www/html/dist/rag/about_eduardo_profile_rag_data.txt";

        var modelTunningData = "";
        var profileRagData = "";

        try
        {
            modelTunningData = await File.ReadAllTextAsync(modelTunningDataPath);
            profileRagData = await File.ReadAllTextAsync(profileRagDataPath);
        }
        catch (FileNotFoundException ex)
        {
            return $"Error: File not found. Make sure the file exists at {ex.FileName}";
        }
        catch (UnauthorizedAccessException)
        {
            return "Error: CRITICAL_FAILURE. The API service does not have permission to read the RAG files. Check file permissions on the server.";
        }
        catch (Exception ex)
        {
            return $"Error: An unexpected error occurred while reading files. {ex.Message}";
        }

        var messages = new List<DeepseekMessage>();
        
        messages.Add(new DeepseekMessage { Role = "system", Content = modelTunningData });

        messages.Add(new DeepseekMessage { Role = "user", Content = $"Use the following context to answer the user's question: {profileRagData}" });

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
                DeepseekChatResponse serializedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepseekChatResponse>(responseBody);
                return serializedResponse.Choices.FirstOrDefault().Message.Content;
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