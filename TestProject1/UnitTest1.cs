using Lab2;
using Lab2.Models;
using Lab2.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class RepositoryTests
{
    [Fact]
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void GetAll_ShouldReturnAllItems()
    {
        // Arrange
        var context = CreateInMemoryDbContext();
        context.Items.Add(new Item { Id = 1, Value = "TestValue" });
        context.Items.Add(new Item { Id = 2, Value = "AnotherValue" });
        context.SaveChanges();

        var repository = new Repository(context);

        // Act
        var items = repository.GetAll();

        // Assert
        Assert.Equal(2, items.Count);
        Assert.Equal("TestValue", items[0].Value);
        Assert.Equal("AnotherValue", items[1].Value);
    }

    [Fact]
    public void Add_ShouldAddItemToDatabase()
    {
        // Arrange
        var context = CreateInMemoryDbContext();
        var repository = new Repository(context);
        var newItem = new Item { Id = 1, Value = "NewValue" };

        // Act
        repository.Add(newItem);

        // Assert
        var item = context.Items.FirstOrDefault(i => i.Id == 1);
        Assert.NotNull(item);
        Assert.Equal("NewValue", item.Value);
    }

    [Fact]
    public void Update_ShouldUpdateExistingItem()
    {
        // Arrange
        var context = CreateInMemoryDbContext();
        var existingItem = new Item { Id = 1, Value = "OldValue" };
        context.Items.Add(existingItem);
        context.SaveChanges();

        var repository = new Repository(context);

        // Act
        existingItem.Value = "UpdatedValue";
        repository.Update(existingItem);

        // Assert
        var item = context.Items.FirstOrDefault(i => i.Id == 1);
        Assert.NotNull(item);
        Assert.Equal("UpdatedValue", item.Value);
    }

    [Fact]
    public void Delete_ShouldRemoveItemFromDatabase()
    {
        // Arrange
        var context = CreateInMemoryDbContext();
        var existingItem = new Item { Id = 1, Value = "ValueToDelete" };
        context.Items.Add(existingItem);
        context.SaveChanges();

        var repository = new Repository(context);

        // Act
        repository.Delete(1);

        // Assert
        var item = context.Items.FirstOrDefault(i => i.Id == 1);
        Assert.Null(item);
    }
}