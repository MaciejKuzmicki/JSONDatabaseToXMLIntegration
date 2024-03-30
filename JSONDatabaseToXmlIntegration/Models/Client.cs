using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class Client
{
    [Key]
    public Guid ClientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}