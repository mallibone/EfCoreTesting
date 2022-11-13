using AutomatedTestingEfCore.Controllers;
using AutomatedTestingEfCore.Persistence;
using AutomatedTestingEfCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AutomatedTestingEfCore.Tests;

public class BloggersControllerShould
{
    private readonly BloggerContext _testDbContext;
    private readonly BloggerContext _setupDbContext;

    public BloggersControllerShould()
    {
        var testDbName = Guid.NewGuid().ToString();
        _testDbContext = CreateInMemoryContext(testDbName);
        _setupDbContext = CreateInMemoryContext(testDbName);
    }
    
    [Fact]
    public async Task BadTestReturnAllPeopleAndTheirBlogposts()
    {
        // Arrange
        SetupTestBlogger(_setupDbContext);
        var sut = new BloggersController(_setupDbContext);
        // Act
        IReadOnlyCollection<Person> bloggers = await sut.Get();
        // Assert
        Assert.Single(bloggers);
        Assert.Equal(2, bloggers.First().BlogPosts?.Count ?? 0);
    }
    
    [Fact]
    public async Task GoodTestReturnAllPeopleAndTheirBlogpostss()
    {
        // Arrange
        SetupTestBlogger(_setupDbContext);
        var sut = new BloggersController(_testDbContext);
        // Act
        IReadOnlyCollection<Person> bloggers = await sut.Get();
        // Assert
        Assert.Single(bloggers);
        Assert.Equal(2, bloggers.First().BlogPosts?.Count ?? 0);
    }

    private static void SetupTestBlogger(BloggerContext dbContext)
    {
        var person = new Person
        {
            PersonId = 1,
            Name = "John Doe",
            BlogPosts = new List<BlogPost>
            {
                new()
                {
                    Title = "First Post",
                    Url = "This is the first post",
                    PersonId = 1
                },
                new()
                {
                    Title = "Second Post",
                    Url = "This is the second post",
                    PersonId = 1
                }
            }
        };

        dbContext.People.Add(person);
        dbContext.SaveChanges();
    }

    private static BloggerContext CreateInMemoryContext(string dbName)
    {
        var dbContext = new BloggerContext(new DbContextOptionsBuilder<BloggerContext>().UseInMemoryDatabase(dbName)
            // don't raise the error warning us that the in memory db doesn't support transactions
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);
        return dbContext;
    }

}
