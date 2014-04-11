using System.ComponentModel.DataAnnotations;

namespace MagenicManila.Brownbag.Data.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SKU { get; set; }
    }
}
