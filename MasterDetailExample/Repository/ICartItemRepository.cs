using System.Collections.Generic;
using MasterDetailExample.Models;

namespace MasterDetailExample.Repository
{
    public interface ICartItemRepository: IRepository<CartItem>
    {
        List<CartItem> GetCartItems(int cartId);
    }
}