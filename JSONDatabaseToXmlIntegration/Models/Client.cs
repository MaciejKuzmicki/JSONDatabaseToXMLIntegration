using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JSONDatabaseToXmlIntegration.Models;

public class Client
{
    [Key]
    [JsonPropertyName("ClientId")]
    public Guid ClientId { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Email")]
    public string Email { get; set; }
}