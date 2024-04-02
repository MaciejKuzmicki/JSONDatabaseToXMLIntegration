using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JSONDatabaseToXmlIntegration.Models;

public class Product
{
    [Key]
    [JsonPropertyName("ProductId")]
    public Guid ProductId { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Price")]
    public double Price { get; set; }
}