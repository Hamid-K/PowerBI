using System;
using System.Runtime.CompilerServices;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x0200001B RID: 27
	public sealed class QueryExecutionOptions
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000029D7 File Offset: 0x00000BD7
		private static bool DefaultIsRetriableFailure(uint asErrorCode)
		{
			return false;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000029DA File Offset: 0x00000BDA
		public QueryExecutionOptions(int pooledConnectionAttempts, int pooledConnectionsToDrainOnFailure)
		{
			int num = 1;
			IsRetriableFailure isRetriableFailure;
			if ((isRetriableFailure = QueryExecutionOptions.<>O.<0>__DefaultIsRetriableFailure) == null)
			{
				isRetriableFailure = (QueryExecutionOptions.<>O.<0>__DefaultIsRetriableFailure = new IsRetriableFailure(QueryExecutionOptions.DefaultIsRetriableFailure));
			}
			this..ctor(pooledConnectionAttempts, pooledConnectionsToDrainOnFailure, num, isRetriableFailure);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002A00 File Offset: 0x00000C00
		public QueryExecutionOptions(int pooledConnectionAttempts, int pooledConnectionsToDrainOnFailure, int newConnectionAttempts, IsRetriableFailure retriableFailureCallback)
		{
			this.PooledConnectionAttempts = pooledConnectionAttempts;
			this.PooledConnectionsToDrainOnFailure = pooledConnectionsToDrainOnFailure;
			this.NewConnectionAttempts = newConnectionAttempts;
			this.IsRetriableFailure = retriableFailureCallback;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002A25 File Offset: 0x00000C25
		public int PooledConnectionAttempts { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002A2D File Offset: 0x00000C2D
		public int PooledConnectionsToDrainOnFailure { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002A35 File Offset: 0x00000C35
		public int NewConnectionAttempts { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002A3D File Offset: 0x00000C3D
		public IsRetriableFailure IsRetriableFailure { get; }

		// Token: 0x04000094 RID: 148
		public static readonly QueryExecutionOptions Default = new QueryExecutionOptions(3, 26, 1, new IsRetriableFailure(QueryExecutionOptions.DefaultIsRetriableFailure));

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040000D4 RID: 212
			public static IsRetriableFailure <0>__DefaultIsRetriableFailure;
		}
	}
}
