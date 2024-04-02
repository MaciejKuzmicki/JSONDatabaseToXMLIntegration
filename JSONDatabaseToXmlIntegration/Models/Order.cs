using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSONDatabaseToXmlIntegration.Models;

public class Order
{
    [Key]
    [JsonPropertyName("OrderId")]
    public Guid OrderId { get; set; }
    [JsonPropertyName("ClientId")]
    public Guid ClientId { get; set; }
    [JsonPropertyName("OrderDate")]
    public DateTime OrderDate { get; set; }
    [NotMapped]
    public IList<Guid> Products { get; set; }
    [JsonIgnore]
    public string ProductsJson
    {
        get => JsonSerializer.Serialize(Products);
        set => Products = JsonSerializer.Deserialize<List<Guid>>(value);
    }
}