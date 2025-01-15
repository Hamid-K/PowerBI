using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x02000065 RID: 101
	public class DropCreateDatabaseIfModelChanges<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x0600035C RID: 860 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		static DropCreateDatabaseIfModelChanges()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			DatabaseExistenceState databaseExistenceState = new DatabaseTableChecker().AnyModelTableExists(context.InternalContext);
			if (databaseExistenceState == DatabaseExistenceState.Exists)
			{
				if (context.Database.CompatibleWithModel(true))
				{
					return;
				}
				context.Database.Delete();
				databaseExistenceState = DatabaseExistenceState.DoesNotExist;
			}
			context.Database.Create(databaseExistenceState);
			this.Seed(context);
			context.SaveChanges();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C052 File Offset: 0x0000A252
		protected virtual void Seed(TContext context)
		{
		}
	}
}
