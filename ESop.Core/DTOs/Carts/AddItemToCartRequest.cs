using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Carts
{
  public class AddItemToCartRequest
  {

    public long ProductId { get; set; }
    
 
    public int Count { get; set; }

  }
}
