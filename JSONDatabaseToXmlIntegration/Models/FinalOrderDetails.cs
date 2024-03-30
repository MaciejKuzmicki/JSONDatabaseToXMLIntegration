using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class FinalOrderDetails
{
    [Key]
    public Guid FinalOrderDetailsId { get; set; }
    public Client Client { get; set; }
    public Invoice Invoice { get; set; }
    public DateTime OrderDate { get; set; }
    public IList<ProductQuantity> Products { get; set; }
    
}