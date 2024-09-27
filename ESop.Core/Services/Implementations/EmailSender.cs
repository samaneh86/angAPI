using ESop.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Implementations
{
  public class EmailSender:IEmailSender
  {
   public void Send(string to, string subject, string body)
    {
     string defaultMailAddress = "samaneh.vafaeenezhad@gmail.com";
      var mail = new MailMessage();
      mail.From = new MailAddress(defaultMailAddress, "ایمل فعالسازی فروشگاه");
      mail.To.Add(to);
      mail.Subject = subject;
      mail.Body = body;
      mail.IsBodyHtml = true;
      var smtp = new SmtpClient();
      smtp.Port = 587;
      smtp.Host = "smtp.gmail.com";
      smtp.Credentials = new NetworkCredential(defaultMailAddress, "vmbphcxyojtdjwpg");
      smtp.EnableSsl = true;
      smtp.Send(mail);

    }
  }
}
