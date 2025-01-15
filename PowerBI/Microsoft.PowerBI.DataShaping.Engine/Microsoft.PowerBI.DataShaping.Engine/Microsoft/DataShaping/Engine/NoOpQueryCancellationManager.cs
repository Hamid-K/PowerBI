using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200001C RID: 28
	public sealed class NoOpQueryCancellationManager : IQueryCancellationManager
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003344 File Offset: 0x00001544
		public int RunningQueryCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003347 File Offset: 0x00001547
		private NoOpQueryCancellationManager()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000334F File Offset: 0x0000154F
		public bool CancelRunningQuery(string queryId)
		{
			return false;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003352 File Offset: 0x00001552
		public void CompleteRunningQuery(string queryId)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003354 File Offset: 0x00001554
		public QueryCancelTokenWrapper CreateTokenForQuery(string queryId, ITracer tracer)
		{
			return QueryCancelTokenWrapper.None;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000335B File Offset: 0x0000155B
		public void Clear()
		{
		}

		// Token: 0x04000073 RID: 115
		public static readonly NoOpQueryCancellationManager Instance = new NoOpQueryCancellationManager();
	}
}
