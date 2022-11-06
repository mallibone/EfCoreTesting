using AutomatedTestingEfCore.Persistence;
using AutomatedTestingEfCore.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingEfCore.Controllers;

[ApiController]
[Route("[controller]")]
public class BloggersController : ControllerBase
{
    private readonly BloggerContext _context;

    public BloggersController(BloggerContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetBloggers")]
    public async Task<IReadOnlyCollection<Person>> Get()
    {
        var bloggers = 
            await _context
            .People
            // .Include(p => p.BlogPosts) // the "bug"
            .ToListAsync();
        return bloggers;
    }
}