using EShop.DataLayer.Entities.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Interfaces
{
  public interface ISliderService:IDisposable
  {
    Task<List<Slider>>  GetActiveSliders();
  
  }
}
