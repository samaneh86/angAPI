using EShop.DataLayer.Repository;
using EshopWebApi.Utility.ExtentionMethods;
using ESop.Core.Services.Implementations;
using ESop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy("SitePolicy", builder =>
  {
    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build();
  });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
  AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = false,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = "https://localhost:7018",
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEShopJwtBearerAngularEShopJwtBearer"))
    };
  });


// Add services to the container.
builder.Services.AddRazorPages();
IConfiguration configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
builder.Services.AddAngularEntityFramework(configuration)  ;

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IRenderView, RenderView>();
builder.Services.AddScoped<IAccessService, AccessService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
 
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.MapRazorPages();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("SitePolicy");


app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
  endpoints.MapControllerRoute("default", "{ controller=Home}/{ action=index}/{id?}");
});
app.Run();

