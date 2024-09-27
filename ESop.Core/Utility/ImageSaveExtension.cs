using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Utility
{
  public static class ImageSaveExtension
  {
    public static void SaveImageToServer(this System.Drawing.Image image, string imageName, string directoryAddress, string? deleteFileName)
    {
      if(image != null)
      {
        if (!Directory.Exists(directoryAddress))
          Directory.CreateDirectory(directoryAddress);
        if (string.IsNullOrEmpty(deleteFileName))
          File.Delete(directoryAddress + deleteFileName);
        using (var fs = new FileStream(directoryAddress + imageName, FileMode.Create))
        {
          if (!Directory.Exists(directoryAddress + imageName))
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
      }


    }
  }
}
