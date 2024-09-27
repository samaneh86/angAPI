using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Utility
{
  public static class FarsiCalender
  {
    public static string ToShamsi(this DateTime date)
    {
      var pc = new PersianCalendar();
      var year = pc.GetYear(date);
      var month = pc.GetMonth(date);
      var day = pc.GetDayOfYear(date).ToString();
      return String.Format("{0}/{1}/{2}", year, month, day);
    }

    public static string ToPersianTime(this DateTime date)
    {
      var pc = new PersianCalendar();
      var hour = pc.GetHour(date);
      var minutes = pc.GetMinute(date);
      return String.Format("{0}:{1}",hour, minutes);
    }
  }
}
