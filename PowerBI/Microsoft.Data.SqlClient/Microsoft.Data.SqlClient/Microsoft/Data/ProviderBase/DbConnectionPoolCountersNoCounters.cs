using System;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000174 RID: 372
	internal sealed class DbConnectionPoolCountersNoCounters : DbConnectionPoolCounters
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x0007132F File Offset: 0x0006F52F
		private DbConnectionPoolCountersNoCounters()
		{
		}

		// Token: 0x04000B44 RID: 2884
		public static readonly DbConnectionPoolCountersNoCounters SingletonInstance = new DbConnectionPoolCountersNoCounters();
	}
}
