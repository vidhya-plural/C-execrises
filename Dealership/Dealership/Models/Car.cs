using System.ComponentModel.DataAnnotations;

namespace Dealership.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int OdometerReading { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } // e.g., "available", "sold"

        // Navigation property
        public ICollection<Sale> Sales { get; set; }
    }
}
