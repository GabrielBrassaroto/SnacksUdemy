using Microsoft.AspNetCore.Identity;

namespace SnacksUdemy.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            //identify if the created profile already exists
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }

            //identify if the created profile already exists
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUser()
        {
            //try to locate the user by email if not found create
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false ;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "G@briel280296").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user,"Member").Wait();  
                }
            }
           // try to locate the user by email if not found create
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "G@briel280296").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }
        }
    }
}
