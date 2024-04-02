using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JSONDatabaseToXmlIntegration.Models;

public class Invoice
{
    [Key]
    [JsonPropertyName("InvoiceId")]
    public Guid InvoiceId { get; set; }
    [JsonPropertyName("OrderId")]
    public Guid OrderId { get; set; }
    [JsonPropertyName("AmountDue")]
    public double AmountDue { get; set; }
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }
}