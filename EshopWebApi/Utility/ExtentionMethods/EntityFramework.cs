using EShop.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace EshopWebApi.Utility.ExtentionMethods
{
  public static class EntityFramework
  {
    public static IServiceCollection AddAngularEntityFramework(this IServiceCollection service,IConfiguration configuration)
    {
      
      var connectionString = "ConnectionStrings:AngularEShop:Development";
      service.AddEntityFrameworkSqlServer().AddDbContext<EshopContext>(options => options.UseSqlServer(configuration[connectionString]));
      return service;
    }
  }
}
