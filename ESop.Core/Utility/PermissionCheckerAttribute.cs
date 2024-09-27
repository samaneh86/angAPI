using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ESop.Utility.Identification;
using ESop.Core.Services.Interfaces;
using ESop.Core.Services.Implementations;
using ESop.Core.Utility.Common;
using Microsoft.AspNetCore.Mvc;


namespace ESop.Core.Utility
{
  public class PermissionCheckerAttribute:AuthorizeAttribute,Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
  {
   public IAccessService? accessService;
    string role;
    public PermissionCheckerAttribute(string _role)
    {
      role = _role;
    }
    public  void OnAuthorization(AuthorizationFilterContext context)
    {
      accessService = (IAccessService) context.HttpContext.RequestServices
        .GetService(typeof(IAccessService));
      if (context.HttpContext.User.Identity.IsAuthenticated)
      {
        var userId = context.HttpContext.User.GetId();

       var result= accessService.CheckRole(userId, role).Result;
        if(result == false)
        {
          context.Result = new UnauthorizedResult();
        }
      }
      else
      {
        context.Result = JsonResponse.NoAccess("برای ذسترسی به این قسمت باید به عنوان ادمین لاگین شوید");
      }
    }
  }
}
