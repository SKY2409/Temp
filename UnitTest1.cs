using Xunit;
using ToDoApp.API.Repositories;
using ToDoApp.API.Models;

public class ToDoRepositoryTests
{
    private const string TestConnectionString =
        "Server=DESKTOP-LSD22MT\\SQLEXPRESS;Database=ToDoDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

    [Fact]
    public void CanAddAndRetrieveToDoItem()
    {
        // Arrange
        var repo = new ToDoRepository(TestConnectionString);
        var newItem = new ToDoItem
        {
            Title = "Test from xUnit",
            SortOrder = 1,
            IsDone = false
        };

        // Act
        var created = repo.Add(newItem);
        var items = repo.GetAll();

        // Assert
        Assert.True(created.Id > 0);
        Assert.Contains(items, i => i.Id == created.Id && i.Title == "Test from xUnit");
    }
}
