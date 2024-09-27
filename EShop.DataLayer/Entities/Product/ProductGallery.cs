using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Product
{
  public  class ProductGallery:BaseEntity
  {
    public long ProductId { get; set; }
    public string? ImageName { get; set; }
    public virtual Product? product { get; set; }
  }
}
