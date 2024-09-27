using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Slider
{
  public class Slider:BaseEntity
  {
    [Required(ErrorMessage="لطفا یک عکس را انتخاب کنید")]
    public string? ImageName { get; set; }
    [Required(ErrorMessage = "لطفا یک متن را انتخاب کنید")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "لطفا یک عنوان را انتخاب کنید")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "لطفا یک لینک را انتخاب کنید")]
    public string? Link { get; set; }
  }
}
