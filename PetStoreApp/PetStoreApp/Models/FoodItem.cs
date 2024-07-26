using System;

namespace PetStoreApp.Models
{
    public enum FoodType
    {
        Dry,
        Wet
    }

    public enum AnimalType
    {
        Dog,
        Cat,
        Fish,
        Bird,
        Rabbit
    }

    public class FoodItem : InventoryItem
    {
        public string Brand { get; set; }
        public FoodType FoodType { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
