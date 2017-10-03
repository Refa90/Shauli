using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    internal sealed class AppConfiguration :  DbMigrationsConfiguration<ShauliBlogContext>
    {
        public AppConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ShauliBlogContext context)
        {
            Debug.WriteLine("Seed app db..");
        }
    }
}