using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Products
{
  public class ProductFilterDto
  {
    public int PageId { get; set; }
    
    public string? Title { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public ProductOrder? ProductOrder { get; set; }
    public ICollection<long>? Categories { get; set; }
  }

  public enum ProductOrder
  {
    Asc=1,
    Desc=2
  }
}
