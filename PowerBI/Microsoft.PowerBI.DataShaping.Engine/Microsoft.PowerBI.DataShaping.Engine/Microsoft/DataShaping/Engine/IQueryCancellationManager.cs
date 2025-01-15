using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200001A RID: 26
	public interface IQueryCancellationManager
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A5 RID: 165
		int RunningQueryCount { get; }

		// Token: 0x060000A6 RID: 166
		QueryCancelTokenWrapper CreateTokenForQuery(string queryId, ITracer tracer);

		// Token: 0x060000A7 RID: 167
		bool CancelRunningQuery(string queryId);

		// Token: 0x060000A8 RID: 168
		void CompleteRunningQuery(string queryId);

		// Token: 0x060000A9 RID: 169
		void Clear();
	}
}
