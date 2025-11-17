public static class Constants
{
    public static string DeepSeekBaseUrl = "https://api.deepseek.com";
    public static string DeepSeekChatUrl = "https://api.deepseek.com/chat/completions";
    public static string DeepSeekApiKey = Environment.GetEnvironmentVariable("PROFILE_ASSISTANT_API_KEY");



    public static string ProfileModelTunningDataPath = "/var/www/html/dist/rag/about_eduardo_model_tunning.txt";
    public static string ProfileRagDataPath = "/var/www/html/dist/rag/about_eduardo_profile_rag_data.txt";
    public static string ProfileModelTunningData = "";
    public static string ProfileRagData = "";
    async public static Task CollectRagData()
    {
        try
        {
            Constants.ProfileModelTunningData = await File.ReadAllTextAsync(Constants.ProfileModelTunningDataPath);
            Constants.ProfileRagData = await File.ReadAllTextAsync(Constants.ProfileRagDataPath);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: File not found. Make sure the file exists at {ex.FileName}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: CRITICAL_FAILURE. The API service does not have permission to read the RAG files. Check file permissions on the server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: An unexpected error occurred while reading files. {ex.Message}");
        }
    }
}