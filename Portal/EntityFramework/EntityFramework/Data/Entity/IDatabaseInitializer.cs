using System;

namespace System.Data.Entity
{
	// Token: 0x02000067 RID: 103
	public interface IDatabaseInitializer<in TContext> where TContext : DbContext
	{
		// Token: 0x0600035F RID: 863
		void InitializeDatabase(TContext context);
	}
}
