using System;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D4 RID: 212
	public abstract class MigrationsLogger : MarshalByRefObject
	{
		// Token: 0x06001072 RID: 4210
		public abstract void Info(string message);

		// Token: 0x06001073 RID: 4211
		public abstract void Warning(string message);

		// Token: 0x06001074 RID: 4212
		public abstract void Verbose(string message);
	}
}
