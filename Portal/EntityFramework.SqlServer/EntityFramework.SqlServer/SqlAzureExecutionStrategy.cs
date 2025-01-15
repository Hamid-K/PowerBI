using System;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000A RID: 10
	public class SqlAzureExecutionStrategy : DbExecutionStrategy
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000255D File Offset: 0x0000075D
		public SqlAzureExecutionStrategy()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002565 File Offset: 0x00000765
		public SqlAzureExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
			: base(maxRetryCount, maxDelay)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000256F File Offset: 0x0000076F
		protected override bool ShouldRetryOn(Exception exception)
		{
			return SqlAzureRetriableExceptionDetector.ShouldRetryOn(exception);
		}
	}
}
