using System.ComponentModel.DataAnnotations;

namespace MasterDetailExample.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public bool InStock { get; set; }
        public Product()
        {
            InStock = true;
            Price = new decimal(0.00);
        }
    }
}