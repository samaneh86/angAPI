using EShop.DataLayer.Entities.Account;
using EShop.DataLayer.Entities.Basketcart;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Entities.Slider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Context
{
  public class EshopContext:DbContext
  {
    public EshopContext(DbContextOptions<EshopContext> options):base(options)
    {
      
    }
 
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Role { get; set; }
    public virtual DbSet<UserRole> UserRole { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductVisit> ProductVisits { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
  
    public virtual DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }
    public virtual DbSet<Slider> Sliders { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
  }
}
