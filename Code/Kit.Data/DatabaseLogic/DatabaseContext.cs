using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kit.Data.DatabaseLogic
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        #region OnModelCreating()
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Main");

            //Register EntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DatabaseContext)));

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Add()
        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                if (entity as IIdentity == null)
                {
                    throw new NotImplementedException($"Entity must implement interface {nameof(IIdentity)}");
                }
                if (entity as IRowVersion == null)
                {
                    throw new NotImplementedException($"Entity must implement interface {nameof(IRowVersion)}");
                }

                ((IIdentity)entity).PublicId = Guid.NewGuid();

                if ((entity as IChangeInfo) != null)
                {
                    ((IChangeInfo)entity).DateCreated = DateTime.UtcNow;
                    ((IChangeInfo)entity).CreatedBy = 1; // TODO CreatedBy
                }

                var result = base.Add(entity);
                SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public override EntityEntry Add(object entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                if (entity as IIdentity == null)
                {
                    throw new NotImplementedException($"Entity must implement interface {nameof(IIdentity)}");
                }
                if (entity as IRowVersion == null)
                {
                    throw new NotImplementedException($"Entity must implement interface {nameof(IRowVersion)}");
                }

                ((IIdentity)entity).PublicId = Guid.NewGuid();

                if (entity as IChangeInfo != null)
                {
                    ((IChangeInfo)entity).DateCreated = DateTime.UtcNow;
                    ((IChangeInfo)entity).CreatedBy = 1; //TODO CreatedBy
                }

                var result = base.Add(entity);
                SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
        #endregion

        #region Update()
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                if ((entity as IIdentity) == null)
                {
                    throw new NotImplementedException($"Entity must implement interface {nameof(IIdentity)}");
                }
                if (entity as IRowVersion == null)
                {
                    throw new NotImplementedException($"Entity must implement interface {nameof(IRowVersion)}");
                }

                var existing = this.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefault(t => ((IIdentity)t).Id == ((IIdentity)entity).Id);

                if (existing == null)
                {
                    throw new Exception("Entity doesn't exist in DB");
                }

                ((IIdentity)entity).PublicId = ((IIdentity)existing).PublicId;
                ((IRowVersion)entity).TimeStamp = ((IRowVersion)existing).TimeStamp;

                if (entity as IChangeInfo != null)
                {
                    ((IChangeInfo)entity).CreatedBy = ((IChangeInfo)existing).CreatedBy;
                    ((IChangeInfo)entity).ModifiedBy = null; // TODO ModifiedBy

                    ((IChangeInfo)entity).DateCreated = ((IChangeInfo)existing).DateCreated;
                    ((IChangeInfo)entity).DateModified = DateTime.UtcNow;
                }

                var result = base.Update(entity);
                SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
        #endregion
    }
}
