using EShop.DataLayer.Entities.Account;
using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ITempDataProvider = Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider;
using ViewDataDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using EmptyModelMetadataProvider = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESop.Core.Services.Implementations
{
  public class RenderView:IRenderView
  {
    private readonly IServiceProvider serviceProvider;
    private readonly IRazorViewEngine razorViewEngine;
    private readonly ITempDataProvider tempDataProvider;
    public RenderView(IServiceProvider _serviceProvider, IRazorViewEngine _razorViewEngine, ITempDataProvider _tempDataProvider)
    {
      serviceProvider = _serviceProvider;
      razorViewEngine = _razorViewEngine;
      tempDataProvider = _tempDataProvider;
    }



    public string RenderViewToString(string viewName, object model)
    {
      var httpContext = new DefaultHttpContext { RequestServices = serviceProvider };
      var actionContext = new ActionContext (httpContext, new RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
      using(var sw = new StringWriter())
      {
        var viewResult = razorViewEngine.FindView(actionContext, viewName, false);

        var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model };


      
          var viewContext = new ViewContext(actionContext,
                                     viewResult.View,
                                      viewDataDictionary,
                                      new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                                       sw,
                                       new HtmlHelperOptions()
                                      );
      

    
       
        
      
       viewResult.View.RenderAsync(viewContext);
        var t = sw;
        return sw.ToString();
      }
        

   

    }
  }
}
