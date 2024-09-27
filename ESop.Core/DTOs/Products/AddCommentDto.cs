using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Products
{
  public class AddCommentDto
  {
    public long ProductId { get; set; }
    public string Text { get; set; }
  }
}
