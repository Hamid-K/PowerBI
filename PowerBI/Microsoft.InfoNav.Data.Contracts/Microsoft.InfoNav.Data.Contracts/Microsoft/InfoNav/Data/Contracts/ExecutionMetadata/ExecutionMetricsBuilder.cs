using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F2 RID: 242
	public sealed class ExecutionMetricsBuilder : BaseBuilder<ExecutionMetrics, ExecutionMetrics>
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0000D540 File Offset: 0x0000B740
		public ExecutionMetricsBuilder(string version)
			: base(null)
		{
			this._metrics = new ExecutionMetrics();
			this.WithVersion(version);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000D55B File Offset: 0x0000B75B
		public void WithVersion(string version)
		{
			this._metrics.Version = version;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000D56C File Offset: 0x0000B76C
		public ExecutionEventBuilder WithEvent(string name, string component)
		{
			ExecutionEventBuilder executionEventBuilder = new ExecutionEventBuilder(this, name, component);
			this.WithEvent(executionEventBuilder.Build());
			return executionEventBuilder;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000D590 File Offset: 0x0000B790
		public ExecutionMetricsBuilder WithEvent(ExecutionEvent executionEvent)
		{
			if (this._metrics.Events == null)
			{
				this._metrics.Events = new List<ExecutionEvent>();
			}
			this._metrics.Events.Add(executionEvent);
			return this;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000D5C1 File Offset: 0x0000B7C1
		public override ExecutionMetrics Build()
		{
			return this._metrics;
		}

		// Token: 0x040002BC RID: 700
		private ExecutionMetrics _metrics;
	}
}
