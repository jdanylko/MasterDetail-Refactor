using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MasterDetailExample.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public List<CartItem> Items { get; set; }

        public decimal CartTotal => Items.Sum(e=> e.Total);
    }
}
