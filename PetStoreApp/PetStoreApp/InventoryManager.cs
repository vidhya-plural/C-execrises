using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PetStoreApp.Models;

namespace PetStoreApp
{
    public class InventoryManager
    {
        private List<InventoryItem> items = new List<InventoryItem>();

        public void LoadInventoryFromFile(string filePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("Inventory file not found", fullPath);
            }

            using (var reader = new StreamReader(fullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var fields = line.Split(',');

                    int id = int.Parse(fields[0]);
                    string type = fields[1];
                    string name = fields[2];
                    string description = fields[3];
                    double price = double.Parse(fields[4]);
                    int quantity = int.Parse(fields[5]);

                    InventoryItem item = type switch
                    {
                        "Food" => new FoodItem
                        {
                            Id = id,
                            Name = name,
                            Description = description,
                            Price = price,
                            Quantity = quantity,
                            Brand = fields[6],
                            FoodType = Enum.Parse<FoodType>(fields[7]),
                            AnimalType = Enum.Parse<AnimalType>(fields[8])
                        },
                        "Accessory" => new AccessoryItem
                        {
                            Id = id,
                            Name = name,
                            Description = description,
                            Price = price,
                            Quantity = quantity,
                            Size = fields[6],
                            Color = fields[7]
                        },
                        "Cage" => new CageItem
                        {
                            Id = id,
                            Name = name,
                            Description = description,
                            Price = price,
                            Quantity = quantity,
                            Dimensions = fields[6],
                            Material = fields[7]
                        },
                        "Aquarium" => new AquariumItem
                        {
                            Id = id,
                            Name = name,
                            Description = description,
                            Price = price,
                            Quantity = quantity,
                            Capacity = int.Parse(fields[6]),
                            Shape = fields[7]
                        },
                        "Toy" => new ToyItem
                        {
                            Id = id,
                            Name = name,
                            Description = description,
                            Price = price,
                            Quantity = quantity,
                            Material = fields[6],
                            RecommendedAge = fields[7]
                        },
                        _ => throw new ArgumentException($"Unknown item type: {type}")
                    };

                    items.Add(item);
                }
            }
        }

        public void ShowAllItems()
        {
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void ShowItemDetails(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Console.WriteLine($"ID: {item.Id}");
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price:C}");
                Console.WriteLine($"Quantity: {item.Quantity}");

                switch (item)
                {
                    case FoodItem food:
                        Console.WriteLine($"Brand: {food.Brand}");
                        Console.WriteLine($"FoodType: {food.FoodType}");
                        Console.WriteLine($"AnimalType: {food.AnimalType}");
                        break;
                    case AccessoryItem accessory:
                        Console.WriteLine($"Size: {accessory.Size}");
                        Console.WriteLine($"Color: {accessory.Color}");
                        break;
                    case CageItem cage:
                        Console.WriteLine($"Dimensions: {cage.Dimensions}");
                        Console.WriteLine($"Material: {cage.Material}");
                        break;
                    case AquariumItem aquarium:
                        Console.WriteLine($"Capacity: {aquarium.Capacity} gallons");
                        Console.WriteLine($"Shape: {aquarium.Shape}");
                        break;
                    case ToyItem toy:
                        Console.WriteLine($"Material: {toy.Material}");
                        Console.WriteLine($"Recommended Age: {toy.RecommendedAge}");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        public void PurchaseItem(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                if (item.Quantity > 0)
                {
                    item.Quantity--;
                    Console.WriteLine($"Purchased {item.Name}. Amount due: {item.Price:C}. Remaining quantity: {item.Quantity}");
                }
                else
                {
                    Console.WriteLine("Item is out of stock.");
                }
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
    }
}
