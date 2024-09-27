using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Carts
{
  public class GetItemCartRes
  {
  public long ProductId { get; set; }
    public string  ProductName { get; set; }
    public string  ProductImage { get; set; }
    public int  Count { get; set; }
    public int  ProductPrice { get; set; }
    public int  PriceInEachRow { get; set; }

  }
}
