using System;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000008 RID: 8
	public sealed class DbCommandOptions
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002959 File Offset: 0x00000B59
		public DbCommandOptions(int commandMemoryLimitKB, int commandTimeout, RequestPriorityKind commandRequestPriority, ApplicationContext applicationContextObject = null)
		{
			this.MemoryLimit = commandMemoryLimitKB;
			this.Timeout = commandTimeout;
			this.RequestPriority = commandRequestPriority;
			this.ApplicationContextObject = applicationContextObject;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000297E File Offset: 0x00000B7E
		public int MemoryLimit { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002986 File Offset: 0x00000B86
		public int Timeout { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000298E File Offset: 0x00000B8E
		public RequestPriorityKind RequestPriority { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002996 File Offset: 0x00000B96
		public ApplicationContext ApplicationContextObject { get; }

		// Token: 0x06000028 RID: 40 RVA: 0x0000299E File Offset: 0x00000B9E
		internal QueryCommandOptions ToQueryCommandOptions(RequestExecutionMetricsKind commandExecutionMetrics, int? maxExecutionEventsPerQuery)
		{
			return new QueryCommandOptions(this.MemoryLimit, this.Timeout, this.RequestPriority, commandExecutionMetrics, maxExecutionEventsPerQuery, this.ApplicationContextObject);
		}
	}
}
