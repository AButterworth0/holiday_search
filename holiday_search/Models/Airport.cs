// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;

public class Airport
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
}

