using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Product
{
  public class ProductVisit:BaseEntity
  {
    public int UserIp { get; set; }
    public long ProductId { get; set; }
    public virtual Product? Product { get; set; }
  }
}
