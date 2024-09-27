using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Product
{
  public class ProductCategory:BaseEntity
  {
    public string? Title { get; set; }
    public string? TitleUrl { get; set; }
    public long ParentId { get; set; }
    public virtual ICollection<ProductSelectedCategory>? ProductSelectedCategories { get; set; }
  }
}
