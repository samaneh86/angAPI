using EshopWebApi.Utility.Common;
using ESop.Core.DTOs.Products;
using ESop.Core.Services.Interfaces;
using ESop.Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EshopWebApi.Controller
{

  public class ProductController : SiteBaseController
  {
    private readonly IProductService productService;
    public ProductController(IProductService _productService)
    {
      productService = _productService;
    }

    //getAllProducts
    [HttpPost("get-all-products")]
    public async Task<ActionResult> GetAllProducts(ProductFilterDto filter)
   {

      var result = await productService.GetAllProducts(filter);
      return JsonResponse.Success(result);
    }

    //getAllProductCategories
    [HttpGet("get-all-product-categories")]
    public async Task<ActionResult> GetAllProductCategories()

    {
      var result = await productService.GetAllProductCategories();
      return JsonResponse.Success(result);
    }

    //GetProductById

    [HttpGet("get-product-by-id/{id}")]
    public async Task<ActionResult> GetProductById(long id)
    {
      var product = await productService.GetProductById(id);
      return JsonResponse.Success(product);
    }

    //GetRelatedProducts

    [HttpGet("get-related-products/{id}")]
    public async Task<ActionResult> GetRelatedProducts(long id)
    {
      var product = await productService.GetRelatedProducts(id);
      return JsonResponse.Success(product);
    }


    //GetProductGallery

    [HttpGet("get-product-gallery/{id}")]
    public async Task<ActionResult> GetProductGallery(long id)
    {
      var result = await productService.GetProductGallery(id);
      return JsonResponse.Success(result);
    }

    //GetProductComments
    [HttpGet("get-product-comments/{id}")]
    public async Task<ActionResult> GetProductComments(long id)
    {
      var userId=0;
      if (User.Identity.IsAuthenticated)
      {
        var claim=User.FindFirst(ClaimTypes.NameIdentifier.ToString());
      userId =Convert.ToInt16(claim.Value);

      }
      var result = await productService.GetAllCommentsOfProduct(id, userId);
      return JsonResponse.Success(result);
    }

    //AddProductComments
    [HttpPost("add-product-comment")]
    public async Task<ActionResult> AddProductComment(AddCommentDto comment)
    {
      var userId = 0;
      if (User.Identity.IsAuthenticated)
      {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier.ToString());
        userId = Convert.ToInt16(claim.Value);

      }
      var result = await productService.AddProductComment(userId,comment);

      if(result)
        return JsonResponse.Success(new { info = "نظر شما با موفقیت ثبت گردید" });
      else
        return JsonResponse.Error(new { ifo = "باید لاگین شوید" });
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct(AddProductDto product)
    {
      await productService.AddProduct(product);
      return JsonResponse.Success(new { data = "محصول با موفقیت اضافه شد" });
    }

    [PermissionChecker("admin")]
    [HttpPost("edit-product")]
    public async Task<IActionResult> EditProduct(EditProductDto product)
    {
       await productService.EditProduct(product);
      return JsonResponse.Success(new { data = "محصول با موفقیت ویرایش شد" });

    }


    [HttpGet("delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
      await productService.DeleteProduct(id);
      return JsonResponse.Success(new { data = "محصول با موفقیت حذف شد" });

    }
  }
}
