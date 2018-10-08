using System;
using System.ComponentModel.DataAnnotations;

namespace MasterDetailExample.Models
{
    public class CartItem
    {
        private decimal _cost;

        public CartItem()
        {
            Quantity = 0;
            Cost = new Decimal(0.00);
        }

        [Key]
        public int CartItemId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public decimal Cost
        {
            get => _cost == 0 && Product != null ? Product.Price : _cost;
            set => _cost = value;
        }

        public decimal Total
        {
            get
            {
                if (Cost == 0 && Product != null)
                {
                    return Product.Price* Quantity;
                }
                return Cost * Quantity;
            }
        }
    }
}