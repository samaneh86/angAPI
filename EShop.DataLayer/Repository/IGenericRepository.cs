using EShop.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Repository
{
 public interface IGenericRepository<TEntity> :IDisposable where TEntity:BaseEntity
  {
      IQueryable<TEntity>  GetAllEntities();
    Task AddEntity(TEntity entity);
     Task<TEntity>  GetEntityById(long entityId);
    Task  RemoveEntity (long entityId);

    public void RemoveEntity(TEntity entity);
    
   
    void UpdateEntity(TEntity entity);
   Task SaveChanges();

  }
}
