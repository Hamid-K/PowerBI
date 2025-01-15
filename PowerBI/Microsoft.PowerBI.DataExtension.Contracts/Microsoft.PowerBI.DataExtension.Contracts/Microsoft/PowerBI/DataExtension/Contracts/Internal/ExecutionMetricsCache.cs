using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200000E RID: 14
	public sealed class ExecutionMetricsCache
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000029C9 File Offset: 0x00000BC9
		public bool HasCachedResultSet
		{
			get
			{
				Queue<ExecutionMetricsCachedResultSet> executionMetrics = this._executionMetrics;
				return executionMetrics != null && executionMetrics.Count > 0;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029DF File Offset: 0x00000BDF
		public void Enqueue(ExecutionMetricsCachedResultSet resultSet)
		{
			if (this._executionMetrics == null)
			{
				this._executionMetrics = new Queue<ExecutionMetricsCachedResultSet>();
			}
			this._executionMetrics.Enqueue(resultSet);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A00 File Offset: 0x00000C00
		public void ReadExecutionMetrics(IExecutionMetricsVisitor visitor)
		{
			if (this._executionMetrics == null || this._executionMetrics.Count == 0)
			{
				return;
			}
			ExecutionMetricsCachedResultSet executionMetricsCachedResultSet = this._executionMetrics.Dequeue();
			foreach (ExecutionEventData executionEventData in executionMetricsCachedResultSet.Events)
			{
				visitor.VisitEvent(in executionEventData);
			}
			if (executionMetricsCachedResultSet.WasTruncated)
			{
				visitor.EventsTruncated();
			}
		}

		// Token: 0x0400006D RID: 109
		private Queue<ExecutionMetricsCachedResultSet> _executionMetrics;
	}
}
