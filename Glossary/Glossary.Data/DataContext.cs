using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glossary.Data.Models;

namespace Glossary.Data
{
    public sealed class DataContext : DbContext, IDataContext
    {
        public IDbSet<GlossaryEntity> Glossaries { get; set; }

        public DataContext() : base("Name=GlossaryConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //by this setting tables wont get created with plural names 
            //e.g Instead of Thresholdranges it will be ProgramBankCostThresholdRange table
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            // Incremented for every SaveChanges() call
           
            var principal = Thread.CurrentPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                principal = new GenericPrincipal(new GenericIdentity("Anonymous", "Forms"), new[] { "Anonymous" });
            }

            if (ChangeTracker.HasChanges())
            {
                var changedEntities = ChangeTracker.Entries<BaseEntity>().Where(e => e.State != EntityState.Unchanged).ToArray();
                var userLogin = principal.Identity.Name;

                foreach (var entity in changedEntities)
                {
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            entity.Entity.CreateLogin = userLogin;
                            entity.Entity.DateCreated = DateTime.Now;
                            break;
                        case EntityState.Deleted:
                        case EntityState.Detached:
                        case EntityState.Modified:
                            entity.Entity.UpdateLogin = userLogin;
                            entity.Entity.DateChanged = DateTime.Now;
                            break;
                    }


                }
            }

           

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                // Helper of finding data formatting error in your seeds
                var sb = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    sb.AppendLine(
                        string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name,
                            eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendLine(ve.ErrorMessage);
                    }
                }

                //TODO: Logging
                //_log.Fatal(sb.ToString(), e);
                throw new DbEntityValidationException(sb.ToString(), e);
            }
            
        }
    }
}
