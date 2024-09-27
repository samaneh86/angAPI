using EShop.DataLayer.Entities.Account;
using EshopWebApi.Utility.Common;
using EshopWebApi.Utility.Identification;
using ESop.Core.DTOs.Account;
using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EshopWebApi.Controller
{
  public class AdminController : SiteBaseController
  {
    private readonly IUserService userService;
    public AdminController(IUserService _userService)
    {
      userService = _userService;
    }
    [HttpPost("login-user")]
    public async Task<IActionResult> Login(LoginDto login)
    {
      bool checkAdmin = true;
      if (ModelState.IsValid)
      {
        var result = await this.userService.LoginUser(login, checkAdmin);
        var user = await userService.GetUserByEmail(login.Email);
        switch (result)
        {
          case LoginUserResult.IncorrectData:
            return JsonResponse.Error("نام کاربری یا کلمه عبور صحیح نمی باشد");
            break;
          case LoginUserResult.NotActivated:
            return JsonResponse.Error(" حساب کاربری شما فعال نشده است");
            break;
          case LoginUserResult.NotAdmin:
            return JsonResponse.Error(" شما ادمین سایت نیستید");
            break;
          case LoginUserResult.Success:

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEShopJwtBearerAngularEShopJwtBearer"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
              issuer: "https://localhost:7018",

              claims: new List<Claim>
              {
              new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
              new Claim(ClaimTypes.Name,login.Email)
              },
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signingCredentials
              );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return JsonResponse.Success(new
            {
              TokenString = tokenString,
              FirstName = user.FirstName,
              LastName = user.LastName,
              Email = user.Email,
              Expire = 30,
              UserId = user.Id

            });
          default:
            return JsonResponse.Error();
        }
      }
      else
      {
        return JsonResponse.Error();
      }


    }
    [HttpGet("check-authenticate")]
    public async Task<IActionResult> CheckAuthenticate()
    {
      if (User.Identity.IsAuthenticated)
      {
        var userId = User.GetId();
        var user =await userService.GetUserById(userId);
        return JsonResponse.Success(new{
          FirstName=user.FirstName,
          LastName=user.LastName,
          Email=user.Email,
          Id=userId
        });
      }
      else
      {
        return JsonResponse.Error();
      }

    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {

      try
      {
        await HttpContext.SignOutAsync();

      }
      catch (Exception err)
      {
        var x = err.Message;
      }
      return JsonResponse.Success("با موفقیت خارج شدید");
 
    }
  }
}
