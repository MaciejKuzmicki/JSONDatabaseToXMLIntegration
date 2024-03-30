using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class Invoice
{
    [Key]
    public Guid InvoiceId { get; set; }
    public Guid OrderId { get; set; }
    public double AmountDue { get; set; }
    public DateTime Date { get; set; }
}