using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x0200006A RID: 106
	public class NullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000C122 File Offset: 0x0000A322
		public virtual void InitializeDatabase(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
		}
	}
}
