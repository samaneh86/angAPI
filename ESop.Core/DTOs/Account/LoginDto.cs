using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.DTOs.Account
{
  public class LoginDto
  {
  
    [Required(ErrorMessage = "لطفا ایمیل  را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا ایمیل  را کوتاه تر وارد کنید")]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }

    [Required(ErrorMessage = "لطفا کلمه عبور  را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا کلمه عبور  را کوتاه تر وارد کنید")]
    [Display(Name = "کلمه عبور")]
    public string Password { get; set; }
  }
  public enum LoginUserResult
  {
    Success,
    IncorrectData,
    NotActivated,
    NotAdmin,
    NotFound


  }
}
