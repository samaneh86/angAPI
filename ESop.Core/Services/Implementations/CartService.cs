using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Entities.Basketcart;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Repository;
using ESop.Core.DTOs.Carts;
using ESop.Core.Services.Interfaces;
using IronPython.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IronPython.Modules.PythonIterTools;

namespace ESop.Core.Services.Implementations
{
  public class CartService:ICartService
  {
    private readonly IGenericRepository<Cart> cartRepository;
    private readonly IGenericRepository<CartItem> cartItemRepository;
    private readonly IGenericRepository<Product> productRepository;
    private readonly IGenericRepository<User> userRepository;
    public CartService(IGenericRepository<Cart> _cartRepository,
      IGenericRepository<Product> _productRepository,
      IGenericRepository<CartItem> _cartItemRepository,
      IGenericRepository<User> _userRepository)
    {
      cartRepository = _cartRepository;
      productRepository = _productRepository;
      cartItemRepository = _cartItemRepository;
      userRepository = _userRepository;
    }


    /*AddItemToCart*/
    public async Task<bool> AddItemToCart(long userId, AddItemToCartRequest item)
    {
     var user= await userRepository.GetEntityById(userId);
     var product = await productRepository.GetEntityById(item.ProductId);
      if(user !=null && product != null)
      {
        var cart=await GetUserOpenCart(userId);
        var CartItem= await CreateCartItem(cart, item);
        
        return true;
      }


      return false;
       
    
     
        
       
     
      
    }
    /*createCart*/
    public async Task<Cart> CreateCart(long userId)
    {
      var newCart = new Cart
      {
        UserId = userId,

      };
     
     await cartRepository.AddEntity(newCart);
      await cartRepository.SaveChanges();
      return newCart;
    }

    /*createCartItem*/
    public async Task<CartItem> CreateCartItem(Cart cart, AddItemToCartRequest item)
    {
     
      var product = await productRepository.GetEntityById(item.ProductId);
      var isExists = await cartItemRepository.GetAllEntities().AnyAsync(x => x.ProductId == product.Id && x.IsDelete == false && x.CartId==cart.Id) ;
      if (isExists)
      {
       var exCartItem=await cartItemRepository.GetAllEntities().Where(x => x.ProductId == product.Id).FirstOrDefaultAsync();
        exCartItem.Count= exCartItem.Count+item.Count;
        cartItemRepository.UpdateEntity(exCartItem);
        await cartItemRepository.SaveChanges();
        return exCartItem;
      }
      else
      {
        var cartItem = new CartItem()
        {
          CartId = cart.Id,
          ProductId = product.Id,
          ProductPrice = product.Price,
          Count = item.Count == 0 ? 1 : item.Count
        };
          await cartItemRepository.AddEntity(cartItem);
        await cartItemRepository.SaveChanges();
        return cartItem;
      }
      }

    /*getUserOpenCart*/
   public async Task<Cart> GetUserOpenCart(long userId)
    {
      var cart = await cartRepository.GetAllEntities()
        .Include(x => x.CartItems)
       
.Where(x => x.UserId == userId && x.IsPaid == false && x.IsDelete==false)
.FirstOrDefaultAsync();
      if (cart == null)
      {
        var newCart = await CreateCart(userId);
        return newCart;
      }

      return cart;
    }





    /*getAllItemsOfCart*/
    public async Task<ICollection<GetItemCartRes>>? GetAllItemsOfCart(long userId)
    {
     var cart= await cartRepository.GetAllEntities().Include(x=>x.CartItems)
        .Where(x => x.UserId == userId && x.IsPaid==false && x.IsDelete==false)
        .FirstOrDefaultAsync();
      if(cart != null)
      {
        var cartItems = cart.CartItems.Where(x => x.IsDelete == false && x.Count !=0 ).ToList();
        var resultList = new List<GetItemCartRes>();
        foreach (var cartItem in cartItems)
        {
          var product = await productRepository.GetEntityById(cartItem.ProductId);
          var item = new GetItemCartRes
          {
            ProductId = product.Id,
            ProductName = product.ProductName,
            ProductImage = product.ProductImage,
            ProductPrice = product.Price,
            Count = cartItem.Count,

            PriceInEachRow = product.Price * cartItem.Count
          };
          resultList.Add(item);
        }
        return resultList;
      }

      return null;
    }


    /*removeItemOfCart*/
    public async Task<bool> RemoveItemFromCart(long userId,long productId)
    {
      var cart = await GetUserOpenCart(userId);
      var cartItem=cart.CartItems.Where(x => x.ProductId == productId).FirstOrDefault();
      if (cartItem.Count > 0)
      {
        cartItem.Count = cartItem.Count - 1;
        cartItemRepository.UpdateEntity(cartItem);
        await cartItemRepository.SaveChanges();
      }
      else
      {
        cartItemRepository.RemoveEntity(cartItem);
      }
      
      return true;
    }

    public void Dispose()
    {
      cartRepository?.Dispose();
      cartItemRepository?.Dispose();
    }
  }
}
