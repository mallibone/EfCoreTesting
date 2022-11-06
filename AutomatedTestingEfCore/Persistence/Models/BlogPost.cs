using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomatedTestingEfCore.Persistence.Models;

public class BlogPost
{
    [Key]
    public int BlogPostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int PersonId { get; set; }
    [ForeignKey("PersonId")]
    public Person? Person { get; set; }
}