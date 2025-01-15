using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F1 RID: 2033
	internal sealed class ExecutedQueryCache
	{
		// Token: 0x0600719F RID: 29087 RVA: 0x001D83E2 File Offset: 0x001D65E2
		internal ExecutedQueryCache()
		{
			this.m_queries = new List<ExecutedQuery>();
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x001D83F8 File Offset: 0x001D65F8
		internal void Add(ExecutedQuery query)
		{
			int indexInCollection = query.DataSet.IndexInCollection;
			for (int i = this.m_queries.Count - 1; i <= indexInCollection; i++)
			{
				this.m_queries.Add(null);
			}
			this.m_queries[indexInCollection] = query;
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x001D8444 File Offset: 0x001D6644
		internal void Extract(DataSet dataSet, out ExecutedQuery query)
		{
			int indexInCollection = dataSet.IndexInCollection;
			if (indexInCollection >= this.m_queries.Count)
			{
				query = null;
				return;
			}
			query = this.m_queries[indexInCollection];
			this.m_queries[indexInCollection] = null;
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x001D8488 File Offset: 0x001D6688
		internal void Close()
		{
			for (int i = 0; i < this.m_queries.Count; i++)
			{
				ExecutedQuery executedQuery = this.m_queries[i];
				if (executedQuery != null)
				{
					executedQuery.Close();
				}
				this.m_queries[i] = null;
			}
		}

		// Token: 0x04003A7F RID: 14975
		private readonly List<ExecutedQuery> m_queries;
	}
}
