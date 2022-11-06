using AutomatedTestingEfCore.Controllers;
using AutomatedTestingEfCore.Persistence;
using AutomatedTestingEfCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AutomatedTestingEfCore.Tests;

public class BloggersControllerShould
{
    [Fact]
    public async Task BadTestReturnAllPeopleAndTheirBlogposts()
    {
        // Arrange
        // Create a new DbContext instance
        var setupDbName = Guid.NewGuid().ToString();
        var setupDb = CreateInMemoryContext(setupDbName);
        // Prepare DB test data
        SetupTestBlogger(setupDb);
        // var sut = new BloggersController(testDb);
        var sut = new BloggersController(setupDb);
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
        // Create a new DbContext instance
        var testDbName = Guid.NewGuid().ToString();
        var setupDbName = Guid.NewGuid().ToString();
        var testDb = CreateInMemoryContext(testDbName);
        var setupDb = CreateInMemoryContext(setupDbName);
        // Prepare DB test data
        SetupTestBlogger(setupDb);
        // var sut = new BloggersController(testDb);
        var sut = new BloggersController(testDb);
        // Act
        IReadOnlyCollection<Person> bloggers = await sut.Get();
        // Assert
        Assert.Single(bloggers);
        Assert.Equal(2, bloggers.First().BlogPosts?.Count ?? 0);
    }

    private static void SetupTestBlogger(BloggerContext setupDb)
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

        setupDb.People.Add(person);
        setupDb.SaveChanges();
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
