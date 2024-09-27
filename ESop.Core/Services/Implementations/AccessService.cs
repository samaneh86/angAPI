using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Repository;
using ESop.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESop.Core.Services.Implementations
{
  public class AccessService:IAccessService
  {
    public readonly IGenericRepository<UserRole> userRoleRepository;
    public AccessService(IGenericRepository<UserRole> _userRoleRepository)
    {
      userRoleRepository = _userRoleRepository;
    }
    public async Task<bool> CheckRole(long userId, string role)
    {
      return await userRoleRepository.GetAllEntities().Include(x => x.Role).AsQueryable()
        .AnyAsync(x => x.Role.Name == role && x.UserId == userId);
    }
  }
}
