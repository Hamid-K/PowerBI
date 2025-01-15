using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x02000057 RID: 87
	public class CreateDatabaseIfNotExists<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x0600021F RID: 543 RVA: 0x000090C1 File Offset: 0x000072C1
		static CreateDatabaseIfNotExists()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000090D8 File Offset: 0x000072D8
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			DatabaseExistenceState databaseExistenceState = new DatabaseTableChecker().AnyModelTableExists(context.InternalContext);
			if (databaseExistenceState == DatabaseExistenceState.Exists)
			{
				if (!context.Database.CompatibleWithModel(false, databaseExistenceState))
				{
					throw Error.DatabaseInitializationStrategy_ModelMismatch(context.GetType().Name);
				}
			}
			else
			{
				context.Database.Create(databaseExistenceState);
				this.Seed(context);
				context.SaveChanges();
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009159 File Offset: 0x00007359
		protected virtual void Seed(TContext context)
		{
		}
	}
}
