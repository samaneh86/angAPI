using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Product
{
  public class Comment : BaseEntity
  {

    public string Text { get; set; }
    public long ProductId { get; set; }
    public long UserId { get; set; }

    public string Date { get; set; }
    public string Time { get; set; }
    public virtual Product? Product { get; set; }
    public virtual User User { get; set; }
  }
}
