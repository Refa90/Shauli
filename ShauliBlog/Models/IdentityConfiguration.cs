using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    internal sealed class IdentityConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public IdentityConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            Debug.WriteLine("Seed identity db..");
        }
    }
}