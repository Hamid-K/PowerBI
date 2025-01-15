using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.QueryExecution
{
	// Token: 0x02000022 RID: 34
	internal sealed class QueryCommandOptions
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000367C File Offset: 0x0000187C
		public QueryCommandOptions(int memoryLimit, int timeout, RequestPriorityKind requestPriority, RequestExecutionMetricsKind requestExecutionMetrics, int? maxExecutionEventsPerQuery, ApplicationContext applicationContextObject = null)
		{
			this.MemoryLimit = memoryLimit;
			this.Timeout = timeout;
			this.RequestPriority = requestPriority;
			this.RequestExecutionMetrics = requestExecutionMetrics;
			this.MaxExecutionEventsPerQuery = maxExecutionEventsPerQuery;
			this.ApplicationContextObject = applicationContextObject;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000036B1 File Offset: 0x000018B1
		public int MemoryLimit { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000036B9 File Offset: 0x000018B9
		public int Timeout { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000036C1 File Offset: 0x000018C1
		public RequestPriorityKind RequestPriority { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000036C9 File Offset: 0x000018C9
		public RequestExecutionMetricsKind RequestExecutionMetrics { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000036D1 File Offset: 0x000018D1
		public int? MaxExecutionEventsPerQuery { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000036D9 File Offset: 0x000018D9
		public ApplicationContext ApplicationContextObject { get; }
	}
}
