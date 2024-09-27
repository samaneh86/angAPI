using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Products
{
  public class EditProductDto
  {
    public long id { get; set; }
    public string ProductName { get; set; }
    public int  Price { get; set; }
    public string ProductImage { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string CurrentImage { get; set; }
    public string? Base64 { get; set; }
  }
}
