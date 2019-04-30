namespace CMS.Migrations
{
    using Database.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMS.Database.CMSDbContext>
    {
        private IList<string> _roleList = new List<string> { "Admin", "Customer" };
        private IList<Container> _containers = new List<Container> { new Container { Name = "Container 1", MaxTonnage = 40000, CurrentLocation = "Melaka" }, new Container { Name = "Container 2", MaxTonnage = 20000, CurrentLocation = "KL"},new Container { MaxTonnage = 30000, Name = "Container 3", CurrentLocation = "Thailand" }  };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CMS.Database.CMSDbContext context)
        {
            foreach (string role in this._roleList)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)).Create(new IdentityRole { Name = role });
                }
            }

            foreach(Container container in this._containers)
            {
                if(!context.Containers.Any(x=>x.Name == container.Name))
                {
                    context.Containers.Add(container);
                }
            }

            context.SaveChanges();

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "admin", LockoutEnabled = false };

                manager.Create(user, "123123123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "aaron"))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser { UserName = "aaron", LockoutEnabled = false };

                manager.Create(user, "123123123");
                manager.AddToRole(user.Id, "Customer");
            }
        }
    }
}
