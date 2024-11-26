using Lab2.Models;
using Lab2.Repositories;
using Xunit;

public class RepositoryTests
{
    [Fact]
    public void LoadDataFromFile_ShouldLoadCorrectly()
    {
        // Arrange
        var filePath = "test_data.txt";
        File.WriteAllLines(filePath, new[] { "1 = TestValue", "2 = AnotherValue" });
        var repository = new Repository(filePath);

        // Act
        var items = repository.Items;

        // Assert
        Assert.Equal(2, items.Count);
        Assert.Equal("TestValue", items[0].Value);
        Assert.Equal("AnotherValue", items[1].Value);

        // Cleanup
        File.Delete(filePath);
    }

    [Fact]
    public void SaveDataToFile_ShouldSaveCorrectly()
    {
        // Arrange
        var filePath = "test_save.txt";
        var repository = new Repository(filePath);
        repository.Items.Add(new Item { Id = 1, Value = "TestValue" });
        repository.Items.Add(new Item { Id = 2, Value = "AnotherValue" });

        // Act
        repository.SaveDataToFile();
        var lines = File.ReadAllLines(filePath);

        // Assert
        Assert.Equal("1 = TestValue", lines[0]);
        Assert.Equal("2 = AnotherValue", lines[1]);

        // Cleanup
        File.Delete(filePath);
    }
}