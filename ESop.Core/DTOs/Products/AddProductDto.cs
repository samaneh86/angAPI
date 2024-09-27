using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Products
{
  public class AddProductDto
  {
    public string ProductName { get; set; }
    public int ProductPrice { get; set; }
    public string ProductImage { get; set; }
    public string ProductShortDescription { get; set; }
    public string ProductDescription { get; set; }
  }
}
