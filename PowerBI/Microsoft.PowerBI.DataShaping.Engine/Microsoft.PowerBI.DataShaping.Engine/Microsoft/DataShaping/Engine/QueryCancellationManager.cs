using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200001D RID: 29
	public class QueryCancellationManager : IQueryCancellationManager
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003381 File Offset: 0x00001581
		public int RunningQueryCount
		{
			get
			{
				return this.m_runningQueries.Count;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003390 File Offset: 0x00001590
		public bool CancelRunningQuery(string queryId)
		{
			QueryCancelTokenWrapper queryCancelTokenWrapper;
			if (this.TryRemoveQueryToken(queryId, out queryCancelTokenWrapper))
			{
				queryCancelTokenWrapper.Cancel();
				return true;
			}
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000033B4 File Offset: 0x000015B4
		public void CompleteRunningQuery(string queryId)
		{
			QueryCancelTokenWrapper queryCancelTokenWrapper;
			this.TryRemoveQueryToken(queryId, out queryCancelTokenWrapper);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000033CC File Offset: 0x000015CC
		public QueryCancelTokenWrapper CreateTokenForQuery(string queryId, ITracer tracer)
		{
			if (string.IsNullOrWhiteSpace(queryId))
			{
				return QueryCancelTokenWrapper.None;
			}
			QueryCancelTokenWrapper queryCancelTokenWrapper = new QueryCancelTokenWrapper(queryId, new CancellationTokenSource(), this);
			if (this.m_runningQueries.TryAdd(queryId, queryCancelTokenWrapper))
			{
				return queryCancelTokenWrapper;
			}
			queryCancelTokenWrapper.Dispose();
			string text = StringUtil.FormatInvariant("Query {0} already has a cancellation token. Queries should not be sent with the same query id while the query is still running.", new object[] { "<<removed>>" });
			tracer.SanitizedTrace(TraceLevel.Warning, text);
			return QueryCancelTokenWrapper.None;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003434 File Offset: 0x00001634
		public void Clear()
		{
			IEnumerable<QueryCancelTokenWrapper> values = this.m_runningQueries.Values;
			this.m_runningQueries.Clear();
			foreach (QueryCancelTokenWrapper queryCancelTokenWrapper in values)
			{
				queryCancelTokenWrapper.Cancel();
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003490 File Offset: 0x00001690
		private bool TryRemoveQueryToken(string queryId, out QueryCancelTokenWrapper tokenWrapper)
		{
			tokenWrapper = null;
			return !string.IsNullOrWhiteSpace(queryId) && this.m_runningQueries.TryRemove(queryId, out tokenWrapper);
		}

		// Token: 0x04000074 RID: 116
		protected internal readonly ConcurrentDictionary<string, QueryCancelTokenWrapper> m_runningQueries = new ConcurrentDictionary<string, QueryCancelTokenWrapper>(StringComparer.Ordinal);
	}
}
