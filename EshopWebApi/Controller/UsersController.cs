using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebApi.Controller
{

  public class UsersController : SiteBaseController
  {
    private readonly IUserService userService;
    public UsersController(IUserService _userService)
    {
      userService = _userService;
    }
    public IActionResult GetUsers()
    {
     return new ObjectResult( userService.GetAllUsers());
    }
  }
}
