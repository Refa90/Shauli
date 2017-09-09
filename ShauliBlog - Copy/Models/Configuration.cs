using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models
{
    internal sealed class Configuration :  DbMigrationsConfiguration<ShauliBlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ShauliBlogContext context)
        {
        }
    }
}