using Microsoft.AspNetCore.Mvc;

namespace EshopWebApi.Utility.Common
{
  public static class JsonResponse
  {
    public static JsonResult Success()
    {
      return new JsonResult(new { Status = "Success" });
    }
    public static JsonResult Success(Object returnData)
    {
      return new JsonResult(new { Status = "Success",data=returnData });
    }
    public static JsonResult Error()
    {
      return new JsonResult(new { Status = "Error" });
    }
    public static JsonResult Error(Object returnData)
    {
      return new JsonResult(new { Status = "Error", data = returnData });
    }
    public static JsonResult NotFound()
    {
      return new JsonResult(new { Status = "NotFound" });
    }
    public static JsonResult NotFound(Object returnData)
    {
      return new JsonResult(new { Status = "NotFound", data = returnData });
    }

    public static JsonResult UnAuthorized()
    {
      return new JsonResult(new { Status = "UnAuthorized" });
    }
    public static JsonResult UnAuthorized(Object returnData)
    {
      return new JsonResult(new { Status = "UnAuthorized", data = returnData });
    }


    public static JsonResult NoAccess()
    {
      return new JsonResult(new { Status = "NoAccess" });
    }
    public static JsonResult NoAccess(Object returnData)
    {
      return new JsonResult(new { Status = "NoAccess", data = returnData });
    }
  }
}
