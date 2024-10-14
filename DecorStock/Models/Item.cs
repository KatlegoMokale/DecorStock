using System.ComponentModel.DataAnnotations;

namespace DecorStock.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        [Required]
        public string? Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }



    }
}
