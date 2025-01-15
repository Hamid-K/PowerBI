using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x02000064 RID: 100
	public class DropCreateDatabaseAlways<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000BF4B File Offset: 0x0000A14B
		static DropCreateDatabaseAlways()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000BF64 File Offset: 0x0000A164
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
			context.Database.Delete();
			context.Database.Create(DatabaseExistenceState.DoesNotExist);
			this.Seed(context);
			context.SaveChanges();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BFB2 File Offset: 0x0000A1B2
		protected virtual void Seed(TContext context)
		{
		}
	}
}
