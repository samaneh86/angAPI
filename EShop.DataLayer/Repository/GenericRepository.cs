using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Repository
{
  public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity:BaseEntity
  {
    private readonly EshopContext context;
    private DbSet<TEntity> dbSet;
    public GenericRepository(EshopContext _context)
    {
      context = _context;
      dbSet =   context.Set<TEntity>();
    }
  public IQueryable<TEntity>  GetAllEntities()
    {
      
     return    dbSet.AsQueryable();
    }

    public async Task AddEntity(TEntity entity)
    {
      entity.CreationDate = DateTime.Now;
      entity.LastUpdateDate = entity.CreationDate;
      entity.IsDelete = false;
      
      await dbSet.AddAsync(entity);
    }

    public async Task<TEntity>  GetEntityById(long entityId)
    {
       var entity= await dbSet.Where(eq=>eq.Id==entityId).FirstOrDefaultAsync();
     
     return  entity;
    }
    public async Task RemoveEntity(long entityId)
    {
     var entity =await GetEntityById(entityId);

     entity.IsDelete=true;
     UpdateEntity(entity);
     }
    public void RemoveEntity(TEntity entity)
    {
      entity.IsDelete = true;
      UpdateEntity(entity);
    }

    public void  UpdateEntity(TEntity entity)
    {
      dbSet.Update(entity);
    }
   public async Task SaveChanges()
    {
      await context.SaveChangesAsync();
    }
    public void Dispose()
    {
      context?.Dispose();
    }
  }
  }

