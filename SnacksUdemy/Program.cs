using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SnacksUdemy;
using SnacksUdemy.Areas.Services;
using SnacksUdemy.Models;
using SnacksUdemy.Repositories;
using SnacksUdemy.Repositories.Interfaces;
using SnacksUdemy.Repository;
using SnacksUdemy.Services;

var builder = WebApplication.CreateBuilder(args);

//add services to the container 

//Additional AppDBContext 
builder.Services.AddDbContext<AppDbContext>(options => options.
UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<ConfigurationImagens>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

//IdentityUser - gerencia o usuario
//IdentityRole fornece informacoes sobre o usuario
//AddEntityFrameworkStores - aramazenar e recueperar informacoes do perfil do usuario com ef core para o sql
//AddDefaultTokenProviders para geradacao de token dois fatores redefinacao de senha email e outros dados do usuarios
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// alter policy password identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddTransient<ISnackRepository, SnackRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IRequestRepository, RequestRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();   //register service 
                                                                           //metodo que foi criado statico para ser invocado na statup
                                                                           // e ter carrinho com list de itens e context e session
                                                                           // e addscope cria cada request uma session e carrinhos diferentese tempo de vida

builder.Services.AddScoped<ReportSalesService>();
builder.Services.AddScoped<SalesChartService>();



//add policy admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.AddScoped(sp => ShoppingCart.GetCart(sp));


builder.Services.AddPaging(
    options =>
    {
        options.ViewName = "Bootstrap4";
        options.PageParameterName = "pageindex";
    }
    );


builder.Services.AddMemoryCache();
builder.Services.AddSession();


builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();

var app = builder.Build();



if (app.Environment.IsDevelopment())
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

CreatePerfilsUser(app);


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


void CreatePerfilsUser( WebApplication app)
{
    //create user and assign perfil
    //create perfils
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using ( var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedRoles();

        service.SeedUser();
    }
}

app.Run();