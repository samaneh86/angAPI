using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Basketcart
{
  public class Cart:BaseEntity
  {
    public long? UserId { get; set; }
    public bool IsPaid { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<CartItem>? CartItems { get; set; }
  }
}
