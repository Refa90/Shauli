using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShauliBlog.Utils;
using System.Data.Entity;
using System.Diagnostics;

namespace ShauliBlog.Models
{
    public class CustomDropCreateDatabaseAlways : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Debug.WriteLine("Seed CustomDropCreateDatabaseIfModelChanges identity db..");

            var _roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            var _userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));


            IdentityRole identityRole = new IdentityRole
            {
                Name = Consts.ADMIN_ROLE
            };

            var idResult = _roleManager.Create(identityRole);

            if (idResult.Succeeded)
            {
                string auth = Consts.ADMIN_ROLE + "@gmail.com";

                var user = new ApplicationUser
                {
                    UserName = auth,
                    Email = auth
                };

                var result = _userManager.Create(user, "Aa12345!");

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user.Id, Consts.ADMIN_ROLE).Wait();
                }
            }
        }
    }
}