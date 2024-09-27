using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Product
{
  public class ProductSelectedCategory:BaseEntity
  {
    public long ProductId { get; set; }
    public long ProductCategoryId { get; set; }
    
    public virtual ProductCategory? ProductCategory { get; set; }
    
    public virtual Product? Product { get; set; }
  }
}
