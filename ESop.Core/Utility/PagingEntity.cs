using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Utility
{
  public class PagingEntity
  {
    public int ActivePage{get;set;}
    public int PageId { get; set; }
    public int CountOfProductsInEachPage { get; set; }
    public int CountOfTotalProducts { get; set; }
   
  }
}
