using System.Collections.Generic;
using System.Linq;
using MasterDetailExample.Context;
using MasterDetailExample.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailExample.Repository
{
    public class CartItemRepository: Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(DbContext context) : base(context)
        {
        }

        public List<CartItem> GetCartItems(int cartId)
        {
            return Find(e => e.CartId == cartId).ToList();
        }
    }
}