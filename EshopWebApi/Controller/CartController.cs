using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Entities.Basketcart;
using EshopWebApi.Utility.Common;
using EshopWebApi.Utility.Identification;
using ESop.Core.DTOs.Carts;
using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebApi.Controller
{
  public class CartController : SiteBaseController
  {
    private readonly ICartService cartService;
    public CartController(ICartService _cartService)
    {
      cartService = _cartService;
    }
    [HttpPost("add-item-to-cart")]
    public async Task<IActionResult>  AddItemToCart(AddItemToCartRequest item)
    {
      if (User.Identity.IsAuthenticated)
      {
        var userId = User.GetId();
        var result = await cartService.AddItemToCart(userId, item);
        return JsonResponse.Success(new
        {
          message = "با موفقیت به سبد خرید اضافه شد",
         res = await cartService.GetAllItemsOfCart(userId)
        });
      }
      else
      {
        return JsonResponse.Error(); ;
      }
    }

    [HttpGet("get-items-cart")]
    public async Task<IActionResult> GetCart()

    {
      if (User.Identity.IsAuthenticated)
      {
        var userId = User.GetId();
        var result = await cartService.GetAllItemsOfCart(userId);
        return JsonResponse.Success(result);

      }
      else
      {
        return JsonResponse.Error(); ;
      }


    }

    [HttpGet("remove-item/{id}")]
    public async Task<IActionResult> RemoveItemFromCart(long id)
    {
      if (User.Identity.IsAuthenticated)
      {
        var userId = User.GetId();
        var result = await cartService.RemoveItemFromCart(userId, id);
        if (result)
        {
          return JsonResponse.Success(new
          {
            message = "با موفقیت از سبد خرید حذف شد",
            res = await cartService.GetAllItemsOfCart(userId)
          });
        }
        else
        {
          return JsonResponse.Error(); ;
        }
      }


      else
      {
        return JsonResponse.Error(); ;
      }
    }
  }
}
