using EShop.DataLayer.Entities.Account;
using ESop.Core.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Interfaces
{
     public interface IUserService:IDisposable
    {
   Task< List<User>> GetAllUsers();
    Task<RegisterUserResult> RegisterUser(RegisterUserDto register);
    Task<LoginUserResult> LoginUser(LoginDto login,bool checkAdmin=false);
    Task< bool> IsExistsUserByEmail(string email);
  
      Task<User>  GetUserByEmail(string email);
    Task<User> GetUserById(long id);

    public Task<Boolean> ActivateUser(string id);
    Task<bool> EditUser(long userId, EditFormDto edit);

  }
}
