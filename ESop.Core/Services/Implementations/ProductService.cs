using EShop.DataLayer.Entities.Product;
using ESop.Core.Utility;

using EShop.DataLayer.Repository;
using ESop.Core.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

using ESop.Core.DTOs.Products;

using EShop.DataLayer.Entities.Account;

using System.Drawing;

namespace ESop.Core.Services.Implementations
{
  public class ProductService : IProductService
  {
    private readonly IGenericRepository<Product> productRepository;
    private readonly IGenericRepository<ProductCategory> productCategoryRepository;
    private readonly IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository;
    private readonly IGenericRepository<ProductGallery> productGalleryRepository;
    private readonly IGenericRepository<Comment> commentRepository;
    private readonly IGenericRepository<User> userRepository;
    public ProductService(IGenericRepository<Product> _productRepository, IGenericRepository<ProductCategory> _productCatigoryRepository,
      IGenericRepository<ProductSelectedCategory> _productSelectedCategoryRepository,
      IGenericRepository<ProductGallery> _productGalleryRepository,
       IGenericRepository<Comment> _commentRepository,
       IGenericRepository<User> _userRepository
      )
    {
      productRepository = _productRepository;
      productCategoryRepository = _productCatigoryRepository;
      productSelectedCategoryRepository = _productSelectedCategoryRepository;
      productGalleryRepository = _productGalleryRepository;
      commentRepository = _commentRepository;
      userRepository = _userRepository;
    }

    //getAllProducts
    
    public async Task<GetProductResult> GetAllProducts(ProductFilterDto filter)
    {
      var products = productRepository.GetAllEntities().Where(x=>x.IsDelete==false);
      if (filter != null)
      {
        if (filter.Title != "" && filter.Title != null)
        {
          products = products.Where(x => x.ProductName.Contains(filter.Title));
        }
        if (filter.ProductOrder != null)
        {
          switch (filter.ProductOrder)
          {
            case ProductOrder.Asc:
              products = products.OrderBy(x => x.Price);
              break;
            case ProductOrder.Desc:
              products = products.OrderByDescending(x => x.Price);
              break;
          }


        }
        if (filter.MinPrice != 0 && filter.MaxPrice == 0)
        {
          products = products.Where(x => x.Price > filter.MinPrice);
        }
        if (filter.MaxPrice != 0 && filter.MinPrice == 0)
        {
          products = products.Where(x => x.Price < filter.MaxPrice);
        }
        if (filter.MaxPrice != 0 && filter.MinPrice != 0)
        {
          products = products.Where(x => x.Price < filter.MaxPrice && x.Price > filter.MinPrice);
        }
        if (filter.Categories.Count != 0 && filter.Categories != null)
        {
          products = products.SelectMany(
           x => x.ProductSelectedCategories
           .Where(x => filter.Categories.Contains(x.ProductCategoryId))
           .Select(x => x.Product)

             );
        }

      }



      var pagedProducts = products.ToPaged(filter.PageId).ToList();
      return new GetProductResult()
      {
        Products = pagedProducts,
        PagingInfo = new PagingEntity
        {
          ActivePage = filter.PageId,
          PageId = filter.PageId == 0 ? 1 : filter.PageId,
          CountOfProductsInEachPage = 4,
          CountOfTotalProducts = products.Count()
        }
      };

    }


    public async Task AddProduct(AddProductDto productDto)
    {
      var product = new Product
      {
        ProductName = productDto.ProductName,
        Price = productDto.ProductPrice,
        ProductImage = productDto.ProductImage,
        Description = productDto.ProductDescription,
        ShortDescription = productDto.ProductShortDescription,
        IsSpecial = false,
        IsDelete = false,
        IsExists = true

      };
      await productRepository.AddEntity(product);
      await productRepository.SaveChanges();
    }

    public async Task DeleteProducts(long productId)
    {
      await productRepository.RemoveEntity(productId);
      await productRepository.SaveChanges();
    }

    public void UpdateProducts(Product product)
    {
      productRepository.UpdateEntity(product);
      productRepository.SaveChanges();
    }


    //getAllProductCategories
    public async Task<ICollection<GetProductCategoriesResult>> GetAllProductCategories()
    {
      var categories = await productCategoryRepository.GetAllEntities().ToListAsync();
      var categoriesList = new List<GetProductCategoriesResult>();
      foreach (var category in categories)
      {
        var getProductCategoriesResult = new GetProductCategoriesResult
        {
          CategoryId = category.Id,
          Title = category.Title
        };
        categoriesList.Add(getProductCategoriesResult);
      }

      return categoriesList;
    }

    //GetProductById
    public async Task<Product> GetProductById(long productId)
    {
      var product = await productRepository.GetEntityById(productId);
      return product;
    }

    //GetRelatedProducts
    public async Task<ICollection<Product>> GetRelatedProducts(long productId)
    {
      var categoriesIdList = await productSelectedCategoryRepository.GetAllEntities()
        .Where(x => x.ProductId == productId).Select(x => x.ProductCategoryId).ToListAsync();

      var relatedProducts = await productRepository.GetAllEntities().SelectMany(
         x => x.ProductSelectedCategories
         .Where(x => categoriesIdList.Contains(x.ProductCategoryId))
         .Select(x => x.Product)
         )
          .Where(s => s.Id != productId)
          .OrderByDescending(s => s.CreationDate).Take(4).ToListAsync();
      return relatedProducts;
    }


    //GetProductGallery
    public async Task<ICollection<GetProductGalleryResult>> GetProductGallery(long productId)
    {
      var galleries = await productGalleryRepository.GetAllEntities().Where(x => x.ProductId == productId).ToListAsync();
      var resultList = new List<GetProductGalleryResult>();

      foreach (var gallery in galleries)
      {
        var result = new GetProductGalleryResult { ImageName = gallery.ImageName };
        resultList.Add(result);
      }
      return resultList;

    }

    //GetCommnet
    public async Task<ICollection<GetProductCommentResult>>? GetAllCommentsOfProduct(long productId, long userId)
    {
      if (userId != 0)
      {
        var user = await userRepository.GetEntityById(userId);
        var commentList = await commentRepository.GetAllEntities().ToListAsync();

        var commentListReult = new List<GetProductCommentResult>();
        foreach (var comment in commentList)
        {
          var resultItem = new GetProductCommentResult()
          {
            FirstName = user.FirstName,
            LastName = user.LastName,

            Text = comment.Text,
            Date = comment.Date,
            Time = comment.Time

          };
          commentListReult.Add(resultItem);
        }
        return commentListReult;
      }

      else
        return null;
    }


    //AddComment
    public async Task<bool> AddProductComment(long userId, AddCommentDto comment)
    {
      if (userId == null)
      {
        return false;
      }
      var newComment = new Comment()
      {
        Date = DateTime.Now.ToShamsi(),
        Time = DateTime.Now.ToPersianTime(),
        Text = comment.Text,
        ProductId = comment.ProductId,
        UserId = userId
      };
      await commentRepository.AddEntity(newComment);
      await commentRepository.SaveChanges();
      return true;



    }
    
    public async Task EditProduct(EditProductDto product)
    {
      var item = await GetProductById(product.id);
      item.ProductName = product.ProductName;
    
      item.Price = product.Price;
      item.Description = product.Description;
      item.ShortDescription = product.ShortDescription;
      if(product.Base64 != null)
      {
        //ایجاد عکس و ذخیره ان در سرور
        var imageFile = ImageExtension.ConvertBase64ToImage(product.Base64);
        var imageName = Guid.NewGuid().ToString() + ".jpg";
        imageFile.SaveImageToServer(imageName, PathTools.productImageServerPath, product.CurrentImage);
        item.ProductImage = imageName;

        //پایان قسمت ایجاد عکس
      }
      else
      {
        item.ProductImage = product.CurrentImage;
      }
      productRepository.UpdateEntity(item);
      await productRepository.SaveChanges();
    }

    public async Task DeleteProduct(long id)
    {
      var product = await GetProductById(id);
      productRepository.RemoveEntity(product);
      await productRepository.SaveChanges();
    }

    public void Dispose()
    {
     productRepository?.Dispose();
    }
  }
}
