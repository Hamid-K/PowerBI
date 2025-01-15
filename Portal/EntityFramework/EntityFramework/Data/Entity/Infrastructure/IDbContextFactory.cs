using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200024B RID: 587
	public interface IDbContextFactory<out TContext> where TContext : DbContext
	{
		// Token: 0x06001EB1 RID: 7857
		TContext Create();
	}
}
