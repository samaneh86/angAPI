using System.Security.Claims;

namespace ESop.Utility.Identification
{
  public static class GetIdentification
  {
    public static long GetId(this ClaimsPrincipal user)
    {
      if (user != null)
      {
        Claim result = user.FindFirst(ClaimTypes.NameIdentifier.ToString());
        return Convert.ToInt64(result.Value);
      }
      else
      {
        return default(long);
      }
    }
  }
}
