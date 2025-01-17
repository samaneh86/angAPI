using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Account
{
 public class Role:BaseEntity
  {
    public string? Name { get; set; }
    public string? Title { get; set; }

    public virtual ICollection<UserRole>? UserRoles { get; set; }
  }
}
