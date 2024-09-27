using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Entities.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Basketcart
{
  public class CartItem : BaseEntity
  {
    
    public long CartId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public int? ProductPrice { get; set; }

    public virtual Cart? Cart { get; set; }
    public virtual EShop.DataLayer.Entities.Product.Product? Product { get; set; }
    
    
  }
}
