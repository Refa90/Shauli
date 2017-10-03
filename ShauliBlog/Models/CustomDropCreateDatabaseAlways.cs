using ShauliBlog.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    public class CustomDropCreateDatabaseAlways : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Debug.WriteLine("Seed CustomDropCreateDatabaseIfModelChanges identity db..");

            IdentityManager identityManager = new IdentityManager();

            if (!identityManager.RoleExists(Consts.ADMIN_ROLE))
            {
                identityManager.CreateRole(Consts.ADMIN_ROLE);
            }

            if (identityManager.GetUserByName(Consts.ADMIN_ROLE) == null)
            {
                var user = new ApplicationUser { UserName = Consts.ADMIN_ROLE, Email = Consts.ADMIN_ROLE + "@gmail.com" };

                var result = identityManager.CreateUser(user, "Aa12345!");

                if (result)
                {
                    identityManager.AddUserToRole(user.Id, Consts.ADMIN_ROLE);
                }
            }
        }
    }
}