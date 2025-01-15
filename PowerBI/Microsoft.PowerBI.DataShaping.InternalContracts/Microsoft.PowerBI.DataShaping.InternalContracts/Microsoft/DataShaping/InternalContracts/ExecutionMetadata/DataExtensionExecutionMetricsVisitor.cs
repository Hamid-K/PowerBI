using System;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.ExecutionMetadata
{
	// Token: 0x02000028 RID: 40
	public sealed class DataExtensionExecutionMetricsVisitor : IExecutionMetricsVisitor
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public DataExtensionExecutionMetricsVisitor(IDataShapingExecutionMetricsService executionMetricsService, string eventsTruncatedEventName, string eventsTruncatedComponent, string defaultParentEventId)
		{
			this._executionMetricsService = executionMetricsService;
			this._eventsTruncatedEventName = eventsTruncatedEventName;
			this._eventsTruncatedComponent = eventsTruncatedComponent;
			this._defaultParentEventId = defaultParentEventId;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public void VisitEvent(in ExecutionEventData eventData)
		{
			ExecutionEvent executionEvent = new ExecutionEvent
			{
				Id = eventData.Id,
				ParentId = (eventData.ParentId ?? this._defaultParentEventId),
				Name = eventData.Name,
				Component = eventData.Component,
				Start = eventData.Start,
				End = eventData.End,
				MetricsRawJson = eventData.Metrics
			};
			this._executionMetricsService.AttachExternalEvent(executionEvent);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003B48 File Offset: 0x00001D48
		public void EventsTruncated()
		{
			ExecutionEvent executionEvent = new ExecutionEvent
			{
				Id = ExecutionMetricsServiceUtils.NewId(),
				ParentId = this._defaultParentEventId,
				Name = this._eventsTruncatedEventName,
				Component = this._eventsTruncatedComponent,
				Start = ExecutionMetricsServiceUtils.Timestamp()
			};
			this._executionMetricsService.AttachExternalEvent(executionEvent);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003BA1 File Offset: 0x00001DA1
		void IExecutionMetricsVisitor.VisitEvent(in ExecutionEventData eventData)
		{
			this.VisitEvent(in eventData);
		}

		// Token: 0x04000074 RID: 116
		private readonly IDataShapingExecutionMetricsService _executionMetricsService;

		// Token: 0x04000075 RID: 117
		private readonly string _eventsTruncatedEventName;

		// Token: 0x04000076 RID: 118
		private readonly string _eventsTruncatedComponent;

		// Token: 0x04000077 RID: 119
		private readonly string _defaultParentEventId;
	}
}
