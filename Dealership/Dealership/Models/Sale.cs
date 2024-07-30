using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public DateTime SalesDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string PaymentMethod { get; set; }
        public decimal PaymentAmount { get; set; }

        // Foreign key
        public int CarId { get; set; }

        // Navigation property
        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
