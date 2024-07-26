namespace PetStoreApp.Models
{
    public abstract class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Name} ({GetType().Name}) - ID: {Id}";
        }
    }
}
