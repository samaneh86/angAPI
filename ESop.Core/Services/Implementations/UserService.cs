using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Repository;
using EshopWebApi.Utility.Common;
using ESop.Core.DTOs.Account;
using ESop.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Web.Mvc;

namespace ESop.Core.Services.Implementations
{
  public class UserService:IUserService
  {
    private readonly IGenericRepository<User> userRepository;
    private readonly IGenericRepository<UserRole> userRoleRepository;
    private readonly IEmailSender emailSender;
    private readonly IRenderView renderView;
    public UserService(IGenericRepository<User> _userRepository, IGenericRepository<UserRole> _userRoleRepository, IEmailSender _emailSender, IRenderView _renderView)
    {
      userRepository = _userRepository;
      emailSender = _emailSender;
      renderView = _renderView;
      userRoleRepository = _userRoleRepository;
    }
    public  async Task<List<User>> GetAllUsers()
    {
     return await userRepository.GetAllEntities().ToListAsync();
    }

    public async Task<RegisterUserResult> RegisterUser(RegisterUserDto register)
    {
      if ( await IsExistsUserByEmail(register.Email))
        return  RegisterUserResult.EmailExists;
      else 
      {
        User user=new User
        {
          FirstName = register.FirstName,
          LastName = register.LastName,
          Email = register.Email,
          Address = register.Address,
          IsConfirmed=false,
          ActivationCode= Guid.NewGuid().ToString(),
          Password = CryptographyHelper.Encrypt(register.Password),
          ConfirmPassword = CryptographyHelper.Encrypt(register.ConfirmPassword)
        };
        await userRepository.AddEntity(user);
        await userRepository.SaveChanges();

        var body = renderView.RenderViewToString("Email/ActivateAccount", user);
        emailSender.Send(user.Email, "ایمیل فعالسازی من", body);
        return RegisterUserResult.Success;
      }
    }
    public async Task<bool>  IsExistsUserByEmail(string email)
    {
      return  await userRepository.GetAllEntities().AnyAsync(x => x.Email == email.ToLower().Trim());
    }

    public async Task<LoginUserResult> LoginUser(LoginDto login,bool checkAdmin=false)
    {
 
      var password =CryptographyHelper.Encrypt(login.Password);
      var user = userRepository.GetAllEntities().Where(x=>x.Email==login.Email && x.Password==password)
        .FirstOrDefault();
      if (user == null)
      {
        return LoginUserResult.IncorrectData;
      }

      else
      {
        if (!user.IsConfirmed)
        {
          return LoginUserResult.NotActivated;
        }
        if (checkAdmin == true)
        {
          var isAdmin = await userRoleRepository.GetAllEntities().Include(x => x.Role).AsQueryable().AnyAsync(x => x.UserId == user.Id && x.Role.Name == "Admin");
          if ((!isAdmin))
          {
            return LoginUserResult.NotAdmin;
          }
          else
          {
            return LoginUserResult.Success;
          }
        }
        else
          return LoginUserResult.Success;
      }

      
    }

   public async Task<User>?  GetUserByEmail(string email)
    {
      var user = await userRepository.GetAllEntities().Where(x => x.Email == email).FirstOrDefaultAsync();
      if (user == null)
        return null;
      return   user;
    }

    public async Task<User>  GetUserById(long id)
    {
      var user = await userRepository.GetAllEntities().Where(x => x.Id == id).FirstAsync();
     
      return user;
    }

    public async Task<Boolean> ActivateUser(string id)
    {
      var user =await userRepository.GetAllEntities().Where(x => x.ActivationCode == id).FirstOrDefaultAsync();
      if (user == null)
        return false;
      user.IsConfirmed = true;
      user.ActivationCode = Guid.NewGuid().ToString();
      user.LastUpdateDate = DateTime.Now;
      userRepository.UpdateEntity(user);
      await userRepository.SaveChanges();
      return true;
    }

    public async Task<bool> EditUser(long userId, EditFormDto edit)
    {
     var user= await GetUserById(userId);
      if(user != null)
      {
        user.FirstName = edit.FirstName;
        user.LastName = edit.LastName;
        user.Email = edit.Email;
        user.Address = edit.Address;

      }
      else
      {
        return false;
      }
      userRepository.UpdateEntity(user);
     await userRepository.SaveChanges();
      return true;
    }
    public void Dispose()
    {
      userRepository?.Dispose();
    }
  }
}
