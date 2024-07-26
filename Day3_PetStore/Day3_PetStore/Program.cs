using System;

namespace PetStore
{
    // Enum to represent types of food (Dry or Wet)
    public enum FoodType
    {
        Dry,
        Wet
    }

    // Enum to represent types of animals
    public enum AnimalType
    {
        Dog,
        Cat,
        Bird,
        Fish,
        Reptile,
        SmallAnimal
    }

    // Base class for all inventory items
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // Derived class for Food items
    public class FoodItem : InventoryItem
    {
        public string Brand { get; set; }
        public FoodType FoodType { get; set; }
        public AnimalType AnimalType { get; set; }
    }

    // Derived class for Accessory items
    public class AccessoryItem : InventoryItem
    {
        public string Size { get; set; }
        public string Color { get; set; }
    }

    // Derived class for Cage items
    public class CageItem : InventoryItem
    {
        public string Dimensions { get; set; }
        public string Material { get; set; }
    }

    // Derived class for Aquarium items
    public class AquariumItem : InventoryItem
    {
        public int Capacity { get; set; }
        public string Shape { get; set; }
    }

    // Derived class for Toy items
    public class ToyItem : InventoryItem
    {
        public string Material { get; set; }
        public string RecommendedAge { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test code to create and display sample inventory items
            FoodItem foodItem = new FoodItem
            {
                Id = 1,
                Name = "Premium Dog Food",
                Description = "Healthy dry food for dogs",
                Price = 29.99M,
                Quantity = 50,
                Brand = "Nutro",
                FoodType = FoodType.Dry,
                AnimalType = AnimalType.Dog
            };

            AccessoryItem accessoryItem = new AccessoryItem
            {
                Id = 2,
                Name = "Red Dog Collar",
                Description = "Adjustable collar for medium-sized dogs",
                Price = 12.50M,
                Quantity = 20,
                Size = "Medium",
                Color = "Red"
            };

            // Display the sample items
            Console.WriteLine("Sample Food Item:");
            DisplayItemDetails(foodItem);

            Console.WriteLine("\nSample Accessory Item:");
            DisplayItemDetails(accessoryItem);
        }

        // Method to display details of an inventory item
        static void DisplayItemDetails(InventoryItem item)
        {
            Console.WriteLine($"ID: {item.Id}");
            Console.WriteLine($"Name: {item.Name}");
            Console.WriteLine($"Description: {item.Description}");
            Console.WriteLine($"Price: ${item.Price}");
            Console.WriteLine($"Quantity in stock: {item.Quantity}");

            // Specific details based on item type
            if (item is FoodItem foodItem)
            {
                Console.WriteLine($"Brand: {foodItem.Brand}");
                Console.WriteLine($"Food Type: {foodItem.FoodType}");
                Console.WriteLine($"Animal Type: {foodItem.AnimalType}");
            }
            else if (item is AccessoryItem accessoryItem)
            {
                Console.WriteLine($"Size: {accessoryItem.Size}");
                Console.WriteLine($"Color: {accessoryItem.Color}");
            }
            // Add additional else if blocks for other item types (CageItem, AquariumItem, ToyItem) as needed
            else
            {
                Console.WriteLine("Unknown item type.");
            }
        }
    }
}
