using EShop.DataLayer.Entities.Basketcart;
using ESop.Core.DTOs.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Interfaces
{
  public interface ICartService:IDisposable
  {
    Task<bool> AddItemToCart(long userId, AddItemToCartRequest item);
    Task<Cart> CreateCart(long userId);
    Task<CartItem> CreateCartItem(Cart cart, AddItemToCartRequest item);
    Task<ICollection<GetItemCartRes>>? GetAllItemsOfCart(long userId);
    Task< bool> RemoveItemFromCart(long userId, long productId);
    Task<Cart> GetUserOpenCart(long userId);
  }
}
