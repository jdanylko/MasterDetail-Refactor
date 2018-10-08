using System.Collections.Generic;
using MasterDetailExample.Models;

namespace MasterDetailExample.ViewModel
{
    public class CartViewModel
    {
        public Customer Customer { get; set; }
        public Cart Cart { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
