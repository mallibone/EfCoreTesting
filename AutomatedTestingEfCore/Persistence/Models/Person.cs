using System.ComponentModel.DataAnnotations;

namespace AutomatedTestingEfCore.Persistence.Models;

public class Person
{
    [Key]
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<BlogPost>? BlogPosts { get; set; }
}