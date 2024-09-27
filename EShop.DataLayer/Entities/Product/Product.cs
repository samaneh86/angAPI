using EShop.DataLayer.Entities.Basketcart;
using EShop.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DataLayer.Entities.Product
{
  public class Product : BaseEntity
  {
    public string? ProductName { get; set; }
    public string? ProductImage { get; set; }
    public int Price { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public bool IsSpecial { get; set; }
    public bool IsExists { get; set; }
    public virtual ICollection<ProductGallery>? ProductGalleries{ get;set;}
    public virtual ICollection<ProductSelectedCategory>? ProductSelectedCategories { get; set; }
    public virtual ICollection<ProductVisit>? ProductVisits { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
      public virtual ICollection<CartItem>? CartItems { get; set; }

  }
}
