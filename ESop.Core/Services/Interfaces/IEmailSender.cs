using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Interfaces
{
public interface IEmailSender
  {
    void Send(string to,string subject,string body);
  }
}
