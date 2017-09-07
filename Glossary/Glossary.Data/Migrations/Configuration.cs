using System.Security.Principal;
using System.Threading;
using Glossary.Data.Models;

namespace Glossary.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Glossary.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Glossary.Data.DataContext context)
        {
            if (string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Db Admin", "Forms"), new[] { "DBA" });
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Glossaries.Add(new GlossaryEntity
            {
                Term = "abyssal plain",
                Definition = "The ocean floor offshore from the continental margin, usually very flat with a slight slope."
            });

            context.Glossaries.Add(new GlossaryEntity
            {
                Term = "accrete",
                Definition = "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass."
            });

        }
    }
}
