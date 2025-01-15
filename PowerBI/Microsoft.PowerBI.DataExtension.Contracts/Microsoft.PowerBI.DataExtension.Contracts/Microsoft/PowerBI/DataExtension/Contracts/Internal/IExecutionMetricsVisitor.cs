using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200001E RID: 30
	public interface IExecutionMetricsVisitor
	{
		// Token: 0x06000084 RID: 132
		void VisitEvent(in ExecutionEventData eventData);

		// Token: 0x06000085 RID: 133
		void EventsTruncated();
	}
}
