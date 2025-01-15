using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000014 RID: 20
	public sealed class ExecutionMetricsOptions
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002F9E File Offset: 0x0000119E
		public ExecutionMetricsOptions(ExecutionMetricsKind allowedExecutionMetrics = ExecutionMetricsKind.None, int? maxExecutionEvents = 300, int? maxExecutionEventsPerQuery = 200)
		{
			this.AllowedExecutionMetrics = allowedExecutionMetrics;
			this.MaxExecutionEvents = maxExecutionEvents;
			this.MaxExecutionEventsPerQuery = maxExecutionEventsPerQuery;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002FBB File Offset: 0x000011BB
		public ExecutionMetricsKind AllowedExecutionMetrics { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002FC3 File Offset: 0x000011C3
		public int? MaxExecutionEvents { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002FCB File Offset: 0x000011CB
		public int? MaxExecutionEventsPerQuery { get; }

		// Token: 0x04000059 RID: 89
		public static readonly ExecutionMetricsOptions Default = new ExecutionMetricsOptions(ExecutionMetricsKind.None, new int?(300), new int?(200));
	}
}
