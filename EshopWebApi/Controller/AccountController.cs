using EshopWebApi.Utility.Common;
using EshopWebApi.Utility.Identification;
using ESop.Core.DTOs.Account;
using ESop.Core.Services.Implementations;
using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EshopWebApi.Controller
{

  public class AccountController : SiteBaseController
  {
    private readonly IUserService userService;

    public AccountController(IUserService _userService)
    {
      userService = _userService;
    }
    [HttpPost("register-user")]
    public async Task<IActionResult> Register([FromBody]RegisterUserDto register)
    {
      if (ModelState.IsValid)
      {
        var result = await userService.RegisterUser(register);
        if (result == RegisterUserResult.Success)
        {
          return JsonResponse.Success();
        }
        else
        {
          return JsonResponse.Error(new { info = "این ایمیل از قبل وجود دارد" });
        }
      }
      else
      {
        return JsonResponse.Error(new { info = "فرمت اطلاعات وارد شده صحیح نیست" });
      }
     
    }
    [HttpPost("login-user")]
    public async Task<IActionResult> Login(LoginDto login)
    {
      if (ModelState.IsValid)
      {
        var result = await userService.LoginUser(login);

        switch (result)
        {
          case LoginUserResult.NotFound:
            return JsonResponse.Error(new { info = "کاربری با این مشخصات پیدا نشد" });

          case LoginUserResult.IncorrectData:
            return JsonResponse.Error(new { info = "نام کاربری یا کلمه عبور صحیح نیست" });


          case LoginUserResult.NotActivated:
            return JsonResponse.Error(new { data = "شما هنوز ایمیل فعالسازی نفرسته اید" });

          case LoginUserResult.Success:

            var user = await userService.GetUserByEmail(login.Email);
 
              var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEShopJwtBearerAngularEShopJwtBearer"));
              var signingCredential = new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256);

              var tokenOptions = new JwtSecurityToken(
              issuer: "https://localhost:7018",
              claims: new List<Claim>{
              new Claim(ClaimTypes.Name,user.Email),
             new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                },
               expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredential
                );
  
        string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
       
      return JsonResponse.Success(new
      {
        token = tokenString,
        firstName = user.FirstName,
        lastName = user.LastName,
        expireTime = 30,
        id = user.Id,
        email=user.Email,
        address = user.Address
      });
      default:
          return JsonResponse.Error();



    };



  }
      else
      {
        return JsonResponse.Error(new { info = "اطلاعات وارد شده فرمت صحیح ندارد" });
      }
    }

    //CheckIfUserIsAuthenticated
    [HttpGet("check-auth-user")]
    public async Task<IActionResult> CheckAuthUser()
    {
      if (User.Identity.IsAuthenticated)
      {
        Claim result = User.FindFirst(ClaimTypes.NameIdentifier.ToString());
        var id = Convert.ToInt64(result.Value);
        var user = await this.userService.GetUserById(id);
        return JsonResponse.Success(new
        {

          firstName = user.FirstName,
          lastName = user.LastName,
          address=user.Address,
          id = user.Id,
          email = user.Email
        });
      }
      else
        return JsonResponse.Error(new { info="شما لاگین نیستید" });
    }

    //logout
    [HttpGet("logout-user")]
    public async Task<ActionResult> Logout()
    {
      if (User.Identity.IsAuthenticated)
      {
        try
        {
          await HttpContext.SignOutAsync();

        }
        catch(Exception err)
        {
        var x= err.Message;
        }
       
        return JsonResponse.Success();
      }
      else
        return JsonResponse.Error();
    }


    //ActivateUser
     [HttpGet("activate-account/{id}")]
    public async Task<IActionResult> ActivateAccount(string id)
    {
      var result=await userService.ActivateUser(id);
      if (result)
       return JsonResponse.Success(new { info = "کاربر با موفقیت فعال شد" });
      else
     return JsonResponse.Error(new { info = "کاربری با این مشخصات یافت نشد" });
    }

   [ HttpPost("edit-user")]
  public async Task<IActionResult> EditUser(EditFormDto edit)
    {
      if (User.Identity.IsAuthenticated)
      {
        var userId = User.GetId();
        var result = await userService.EditUser(userId, edit);
        if (result == true)
        {
          return JsonResponse.Success("اطلاعات شما با موفقیت ویرایش شد");
        }
        else
        {
          return JsonResponse.Success("کاربری با این مشخصات یافت نشد");
        }

      }
      else
      {
        return JsonResponse.Error("ابتدا وارد سایت شوید");
      }
     
    }
  }



}
