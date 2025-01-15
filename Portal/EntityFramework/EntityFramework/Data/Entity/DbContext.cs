using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x0200005B RID: 91
	public class DbContext : IDisposable, IObjectContextAdapter
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000A11C File Offset: 0x0000831C
		protected DbContext()
		{
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, this.GetType().DatabaseName()), null);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A13C File Offset: 0x0000833C
		protected DbContext(DbCompiledModel model)
		{
			Check.NotNull<DbCompiledModel>(model, "model");
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, this.GetType().DatabaseName()), model);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A168 File Offset: 0x00008368
		public DbContext(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, nameOrConnectionString), null);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A18A File Offset: 0x0000838A
		public DbContext(string nameOrConnectionString, DbCompiledModel model)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			Check.NotNull<DbCompiledModel>(model, "model");
			this.InitializeLazyInternalContext(new LazyInternalConnection(this, nameOrConnectionString), model);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public DbContext(DbConnection existingConnection, bool contextOwnsConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			this.InitializeLazyInternalContext(new EagerInternalConnection(this, existingConnection, contextOwnsConnection), null);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A1DB File Offset: 0x000083DB
		public DbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			Check.NotNull<DbCompiledModel>(model, "model");
			this.InitializeLazyInternalContext(new EagerInternalConnection(this, existingConnection, contextOwnsConnection), model);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A20A File Offset: 0x0000840A
		public DbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
		{
			Check.NotNull<ObjectContext>(objectContext, "objectContext");
			DbConfigurationManager.Instance.EnsureLoadedForContext(this.GetType());
			this._internalContext = new EagerInternalContext(this, objectContext, dbContextOwnsObjectContext);
			this.DiscoverAndInitializeSets();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A242 File Offset: 0x00008442
		internal virtual void InitializeLazyInternalContext(IInternalConnection internalConnection, DbCompiledModel model = null)
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(this.GetType());
			this._internalContext = new LazyInternalContext(this, internalConnection, model, DbConfiguration.DependencyResolver.GetService<Func<DbContext, IDbModelCacheKey>>(), DbConfiguration.DependencyResolver.GetService<AttributeProvider>(), null, null);
			this.DiscoverAndInitializeSets();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A27E File Offset: 0x0000847E
		private void DiscoverAndInitializeSets()
		{
			new DbSetDiscoveryService(this).InitializeSets();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A28B File Offset: 0x0000848B
		protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A28D File Offset: 0x0000848D
		internal void CallOnModelCreating(DbModelBuilder modelBuilder)
		{
			this.OnModelCreating(modelBuilder);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A296 File Offset: 0x00008496
		public Database Database
		{
			get
			{
				if (this._database == null)
				{
					this._database = new Database(this.InternalContext);
				}
				return this._database;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A2B7 File Offset: 0x000084B7
		public virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return (DbSet<TEntity>)this.InternalContext.Set<TEntity>();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A2C9 File Offset: 0x000084C9
		public virtual DbSet Set(Type entityType)
		{
			Check.NotNull<Type>(entityType, "entityType");
			return (DbSet)this.InternalContext.Set(entityType);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A2E8 File Offset: 0x000084E8
		public virtual int SaveChanges()
		{
			return this.InternalContext.SaveChanges();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A2F5 File Offset: 0x000084F5
		public virtual Task<int> SaveChangesAsync()
		{
			return this.SaveChangesAsync(CancellationToken.None);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A302 File Offset: 0x00008502
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return this.InternalContext.SaveChangesAsync(cancellationToken);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A310 File Offset: 0x00008510
		ObjectContext IObjectContextAdapter.ObjectContext
		{
			get
			{
				this.InternalContext.ForceOSpaceLoadingForKnownEntityTypes();
				return this.InternalContext.ObjectContext;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A328 File Offset: 0x00008528
		public IEnumerable<DbEntityValidationResult> GetValidationErrors()
		{
			List<DbEntityValidationResult> list = new List<DbEntityValidationResult>();
			foreach (DbEntityEntry dbEntityEntry in this.ChangeTracker.Entries())
			{
				if (dbEntityEntry.InternalEntry.EntityType != typeof(EdmMetadata) && this.ShouldValidateEntity(dbEntityEntry))
				{
					DbEntityValidationResult dbEntityValidationResult = this.ValidateEntity(dbEntityEntry, new Dictionary<object, object>());
					if (dbEntityValidationResult != null && !dbEntityValidationResult.IsValid)
					{
						list.Add(dbEntityValidationResult);
					}
				}
			}
			return list;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A3C0 File Offset: 0x000085C0
		protected virtual bool ShouldValidateEntity(DbEntityEntry entityEntry)
		{
			Check.NotNull<DbEntityEntry>(entityEntry, "entityEntry");
			return (entityEntry.State & (EntityState.Added | EntityState.Modified)) > (EntityState)0;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A3DA File Offset: 0x000085DA
		protected virtual DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			Check.NotNull<DbEntityEntry>(entityEntry, "entityEntry");
			return entityEntry.InternalEntry.GetValidationResult(items);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A3F4 File Offset: 0x000085F4
		internal virtual DbEntityValidationResult CallValidateEntity(DbEntityEntry entityEntry)
		{
			return this.ValidateEntity(entityEntry, new Dictionary<object, object>());
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A402 File Offset: 0x00008602
		public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
		{
			Check.NotNull<TEntity>(entity, "entity");
			return new DbEntityEntry<TEntity>(new InternalEntityEntry(this.InternalContext, entity));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A426 File Offset: 0x00008626
		public DbEntityEntry Entry(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			return new DbEntityEntry(new InternalEntityEntry(this.InternalContext, entity));
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A445 File Offset: 0x00008645
		public DbChangeTracker ChangeTracker
		{
			get
			{
				return new DbChangeTracker(this.InternalContext);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000A452 File Offset: 0x00008652
		public DbContextConfiguration Configuration
		{
			get
			{
				return new DbContextConfiguration(this.InternalContext);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A45F File Offset: 0x0000865F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A46E File Offset: 0x0000866E
		protected virtual void Dispose(bool disposing)
		{
			this._internalContext.Dispose();
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000A47B File Offset: 0x0000867B
		internal virtual InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A483 File Offset: 0x00008683
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A48B File Offset: 0x0000868B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A494 File Offset: 0x00008694
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A49C File Offset: 0x0000869C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040000B0 RID: 176
		private InternalContext _internalContext;

		// Token: 0x040000B1 RID: 177
		private Database _database;
	}
}
