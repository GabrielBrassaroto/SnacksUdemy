using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnacksUdemy;
using SnacksUdemy.Models;
using SnacksUdemy.Repositories;
using SnacksUdemy.Repositories.Interfaces;
using SnacksUdemy.Repository;
using SnacksUdemy.Services;

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

        // alter policy password identity
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;
        });

        services.AddTransient<ISnackRepository, SnackRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IRequestRepository, RequestRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();   //register service 
        //metodo que foi criado statico para ser invocado na statup
        // e ter carrinho com list de itens e context e session
        // e addscope cria cada request uma session e carrinhos diferentese tempo de vida

        services.AddScoped(sp => ShoppingCart.GetCart(sp));

        services.AddMemoryCache();
        services.AddSession();


        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
        IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
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
        //create perfils
        seedUserRoleInitial.SeedRoles();
        //create user and assign perfil
        seedUserRoleInitial.SeedUser();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();


        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                              name: "areas",
                              pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                            );

            endpoints.MapControllerRoute(
                name: "categoryFilter",
                pattern: "Snack/{action}/{category?}",
                defaults: new { controller = "Snack", Action = "List" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}