using Lab2.Models;

namespace Lab2.Repositories
{
    public class Repository
    {
        private readonly string _filePath;
        public List<Item> Items { get; private set; }

        public Repository(string filePath)
        {
            _filePath = filePath;
            Items = LoadDataFromFile();
        }

        private List<Item> LoadDataFromFile()
        {
            var dataItems = new List<Item>();
            if (!File.Exists(_filePath)) return dataItems;

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && int.TryParse(parts[0], out var id))
                {
                    dataItems.Add(new Item { Id = id, Value = parts[1].Trim() });
                }
            }
            return dataItems;
        }

        public void SaveDataToFile()
        {
            var lines = Items.Select(item => $"{item.Id} = {item.Value}").ToList();
            File.WriteAllLines(_filePath, lines);
        }
    }
}
