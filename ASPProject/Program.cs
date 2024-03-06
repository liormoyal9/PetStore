using ASPProject.Data;
using ASPProject.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();

string connectionString = builder.Configuration["ConnectionString:DefaultConnection"]!;
builder.Services.AddDbContext<PetShopContext>(options => options.UseLazyLoadingProxies().UseSqlite(connectionString ));

builder.Services.AddControllersWithViews();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PetShopContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();

