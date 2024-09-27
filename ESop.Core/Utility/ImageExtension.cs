using System;
using System.Collections.Generic;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Utility
{
  public static class ImageExtension
  {
    public static Image ConvertBase64ToImage(string base64)
    {
      var bytes=ImageExtension.convertBase64ToBytes(base64);
      var ms = new MemoryStream();
      ms.Write(bytes);
      var image=Image.FromStream(ms, true);
      return image;
    }
    public static byte[]  convertBase64ToBytes(string base64)
    {
      return Convert.FromBase64String(base64.Substring(base64.IndexOf(",")+1));
    }


  }
}
