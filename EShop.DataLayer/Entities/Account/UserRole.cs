using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Account
{
  public class UserRole:BaseEntity
  {
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public User? User { get; set; }
    public Role? Role { get; set; }
  }
}
