using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Products
{
  public class GetProductCommentResult
  {
   public string FirstName { get; set; }
   public string LastName { get; set; }
    public string Text { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
  }
}
