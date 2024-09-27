using EShop.DataLayer.Entities.Basketcart;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Account
{
  public class User:BaseEntity
  {
    [Required(ErrorMessage = "لطفا نام را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا نام را کوتاه تر وارد کنید")]
    [Display(Name = "نام")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "لطفا نام خانوادگی را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا نام خانوادگی را کوتاه تر وارد کنید")]
    [Display(Name = "فامیل")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "لطفا ایمیل  را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا ایمیل  را کوتاه تر وارد کنید")]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }
    [Required(ErrorMessage = "لطفا آدرس  را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا آدرس  را کوتاه تر وارد کنید")]
    [Display(Name = "آدرس")]
    public string Address { get; set; }

    [Required(ErrorMessage = "لطفا کلمه عبور  را وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا کلمه عبور  را کوتاه تر وارد کنید")]
   [Display(Name = "کلمه عبور")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "لطفا کلمه عبور  را دوباره وارد کنید")]
   [MaxLength(100, ErrorMessage = "لطفا کلمه عبور  را کوتاه تر وارد کنید")]
    [Display(Name = "تایید کلمه عبور")]
    [Compare("Password",ErrorMessage="کلمه های عبور با هم مغایرت دارند")]
    public string? ConfirmPassword { get; set; }
    public bool IsConfirmed { get; set; }
    [Required(ErrorMessage = "لطفا  کد فعالسازی  را دوباره وارد کنید")]
    [MaxLength(100, ErrorMessage = "لطفا کد فعالسازی  را کوتاه تر وارد کنید")]
    [Display(Name = "  کد فعالسازی")]
    public string? ActivationCode { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<CartItem>? CartItems { get; set; }


  }
}