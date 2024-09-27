using ESop.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.DataLayer.Entities.Product;

namespace ESop.Core.DTOs.Products
{
  public class GetProductResult
  {
    public ICollection<Product>? Products { get; set; }
    public PagingEntity? PagingInfo { get; set; }
  }
}
