using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F3 RID: 243
	public sealed class ExecutionEventBuilder : BaseBuilder<ExecutionEvent, ExecutionMetricsBuilder>
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x0000D5C9 File Offset: 0x0000B7C9
		public ExecutionEventBuilder(ExecutionMetricsBuilder parent, string name, string component)
			: base(parent)
		{
			this._event = new ExecutionEvent();
			this.WithName(name);
			this.WithComponent(component);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		public ExecutionEventBuilder(ExecutionMetricsBuilder parent, ExecutionEvent eventData)
			: base(parent)
		{
			this._event = eventData;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000D5FD File Offset: 0x0000B7FD
		public ExecutionEventBuilder WithName(string name)
		{
			this._event.Name = name;
			return this;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000D60C File Offset: 0x0000B80C
		public ExecutionEventBuilder WithComponent(string component)
		{
			this._event.Component = component;
			return this;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000D61B File Offset: 0x0000B81B
		public ExecutionEventBuilder WithId(string id)
		{
			this._event.Id = id;
			return this;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000D62A File Offset: 0x0000B82A
		public ExecutionEventBuilder WithParentId(string parentId)
		{
			this._event.ParentId = parentId;
			return this;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000D639 File Offset: 0x0000B839
		public ExecutionEventBuilder WithStart(DateTime start)
		{
			this._event.Start = start;
			return this;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000D648 File Offset: 0x0000B848
		public ExecutionEventBuilder WithEnd(DateTime? end)
		{
			this._event.End = end;
			return this;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000D657 File Offset: 0x0000B857
		public ExecutionEventBuilder WithMetric(string name, object value)
		{
			if (this._event.Metrics == null)
			{
				this._event.Metrics = new Dictionary<string, object>(StringComparer.Ordinal);
			}
			this._event.Metrics.Add(name, value);
			return this;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000D68E File Offset: 0x0000B88E
		public ExecutionEventBuilder WithMetricsRawJson(string metrics)
		{
			this._event.MetricsRawJson = metrics;
			return this;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000D69D File Offset: 0x0000B89D
		public override ExecutionEvent Build()
		{
			return this._event;
		}

		// Token: 0x040002BD RID: 701
		private readonly ExecutionEvent _event;
	}
}
