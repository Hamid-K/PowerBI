using System;

namespace Microsoft.PowerBI.ExploreHost.Contracts
{
	// Token: 0x0200000A RID: 10
	public abstract class QueryExecutionOptionsBase
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27
		public abstract int ConnectionAttempts { get; }

		// Token: 0x0600001C RID: 28 RVA: 0x0000242F File Offset: 0x0000062F
		public virtual bool IsRetriableFailure(uint asErrorCode)
		{
			return false;
		}
	}
}
