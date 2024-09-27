using EShop.DataLayer.Entities;
using EshopWebApi.Utility.Common;
using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebApi.Controller
{

  public class SliderController : SiteBaseController
  {
    private readonly ISliderService sliderService;
    public SliderController(ISliderService _sliderService)
    {
      sliderService = _sliderService;
    }
    [HttpGet("get-active-sliders")]
    public async Task<IActionResult> GetActiveSliders()
    {
      
      return  JsonResponse.Success( await sliderService.GetActiveSliders());
    }
  
  }
}
