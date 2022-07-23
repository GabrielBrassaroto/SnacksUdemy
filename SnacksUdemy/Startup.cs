using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnacksUdemy;
using SnacksUdemy.Models;
using SnacksUdemy.Repositories;
using SnacksUdemy.Repositories.Interfaces;
using SnacksUdemy.Repository;

namespace LanchesMac;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //Additional AppDBContext 
        services.AddDbContext<AppDbContext>(options => options.
        UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        //IdentityUser - gerencia o usuario
        //IdentityRole fornece informacoes sobre o usuario
        //AddEntityFrameworkStores - aramazenar e recueperar informacoes do perfil do usuario com ef core para o sql
        //AddDefaultTokenProviders para geradacao de token dois fatores redefinacao de senha email e outros dados do usuarios
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<ISnackRepository, SnackRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IRequestRepository, RequestRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //metodo que foi criado statico para ser invocado na statup
        // e ter carrinho com list de itens e context e session
        // e addscope cria cada request uma session e carrinhos diferentese tempo de vida

        services.AddScoped(sp => ShoppingCart.GetCart(sp));
       
        services.AddMemoryCache();
        services.AddSession();
  

        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();


        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                name: "categoryFilter",
                pattern: "Snack/{action}/{category?}",
                defaults: new  {controller= "Snack", Action="List"});

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}