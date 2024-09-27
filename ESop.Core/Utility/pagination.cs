using EShop.DataLayer.Entities.Product;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Utility
{
  public static class pagination
  {
    public static ICollection<Product>  ToPaged(this IQueryable<Product> products,int pageId)
    {
     var CountOfTotalProducts=products.Count();
      var countOfProductsInEachPage =4 ;
      var skip = (pageId==1)?0:CountOfTotalProducts - (countOfProductsInEachPage * (pageId - 1));
      var take = products.Count() < 4 ? CountOfTotalProducts : countOfProductsInEachPage;
    var selectedProducts= products.Skip(skip).Take(take).ToList();
      return selectedProducts;

    }
  }
}
