using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Product;
using ESop.Core.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Interfaces
{
   public interface IProductService:IDisposable
  {
    
   Task AddProduct(AddProductDto productDto);
    Task DeleteProducts(long productId);
    void UpdateProducts(Product product);
    Task<GetProductResult> GetAllProducts(ProductFilterDto filter);
    Task<ICollection<GetProductCategoriesResult>> GetAllProductCategories();
    Task<Product> GetProductById(long id);
    Task<ICollection<Product>> GetRelatedProducts(long id);
    Task<ICollection<GetProductGalleryResult>> GetProductGallery(long productId);
    Task<ICollection<GetProductCommentResult>> GetAllCommentsOfProduct(long productId,long userId);
    Task<bool> AddProductComment(long userId,AddCommentDto comment);

    Task  EditProduct(EditProductDto product);
    Task DeleteProduct(long id);
  }
}
