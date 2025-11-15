using Newtonsoft.Json;

public class DeepseekChatRequest
{
    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("messages")]
    public List<DeepseekMessage> Messages { get; set; }

    [JsonProperty("stream")]
    public bool Stream { get; set; }
    
    public DeepseekChatRequest()
    {
        Messages = new List<DeepseekMessage>();
    }
}

// This class represents a single message in the conversation
public class DeepseekMessage
{
    [JsonProperty("role")]
    public string Role { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }
}


public class DeepseekChat
{
    [JsonProperty("messages")]
    public DeepseekMessage[] Messages { get; set; }
}