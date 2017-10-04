using System.Data.Entity;

namespace ShauliBlog.Models
{
    public class ShauliBlogContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ShauliBlogContext() : base("name=ShauliBlogContext")
        {
           // Database.SetInitializer<ShauliBlogContext>(new DropCreateDatabaseAlways<ShauliBlogContext>());
        }

        public DbSet<Fan> Fans { get; set; }

        public System.Data.Entity.DbSet<ShauliBlog.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<ShauliBlog.Models.Comment> Comments { get; set; }
    }
}
