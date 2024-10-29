using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleCacheApp
{
    class Program
    {
        // Используем словарь для хранения данных
        static Dictionary<string, string> cache = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.Write("Введите имя файла со стартовым состоянием: ");
            string filePath = Console.ReadLine();

            // Загрузка начального состояния в кэш
            LoadData(filePath);

            while (true)
            {
                Console.Write("Введите команду (get <objectId> <propertyId> или <objectId> <propertyId> <newValue>): ");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 3 && parts[0].ToLower() == "get")
                {
                    // Команда get для получения значения
                    GetProperty(parts[1], parts[2]);
                }
                else if (parts.Length == 3)
                {
                    // Обновление значения свойства
                    UpdateProperty(parts[0], parts[1], parts[2]);
                }
                else
                {
                    Console.WriteLine("Неверная команда.");
                }
            }
        }

        // Загрузка данных из файла
        static void LoadData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string key = $"{parts[0]}_{parts[1]}";
                    cache[key] = parts[2];
                }
            }

            Console.WriteLine("Данные успешно загружены.");
        }

        // Получение значения свойства
        static void GetProperty(string objectId, string propertyId)
        {
            string key = $"{objectId}_{propertyId}";
            if (cache.TryGetValue(key, out string value))
            {
                Console.WriteLine($"Значение: {value}");
            }
            else
            {
                Console.WriteLine("Значение не найдено.");
            }
        }

        // Обновление значения в кэше
        static void UpdateProperty(string objectId, string propertyId, string newValue)
        {
            string key = $"{objectId}_{propertyId}";
            cache[key] = newValue;
            Console.WriteLine("Данные успешно обновлены.");
        }
    }
}
