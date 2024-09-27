using EShop.DataLayer.Entities.Slider;
using EShop.DataLayer.Repository;
using ESop.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Implementations
{
  public class SliderService:ISliderService
  {
    private readonly IGenericRepository<Slider> sliderRepository;
   public SliderService(IGenericRepository<Slider> _sliderRepository)
    {
      sliderRepository = _sliderRepository;
    }
    public async Task<List<Slider>>  GetActiveSliders()
     
    {
      
     return await sliderRepository.GetAllEntities().ToListAsync();
    }
    public void Dispose()
    {
      sliderRepository?.Dispose();
    }
  }
}
