using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShauliBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliBlog.Utils
{
    public class IdentityManager
    {
        // Swap ApplicationRole for IdentityRole:
        RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        ApplicationDbContext _db = new ApplicationDbContext();


        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name, string description = "")
        {
            // Swap ApplicationRole for IdentityRole:
            IdentityRole identityRole = new IdentityRole
            {
                Name = name
            };
            var idResult = _roleManager.Create(identityRole);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRoleAsync(userId, roleName).Result;
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            
            return idResult.Succeeded;
        }
    
        public ApplicationUser GetUserByName(string name)
        {
            return _userManager.Users.Where(user => user.UserName == name).SingleOrDefault();
        }
    }
}