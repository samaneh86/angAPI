using EShop.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Interfaces
{
  public interface IRenderView
  {
    public string RenderViewToString(string viewName, object model);

  }
}
