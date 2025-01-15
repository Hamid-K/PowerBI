using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000098 RID: 152
	internal class DiagnosticsEventListener : EventListener
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x000147B0 File Offset: 0x000129B0
		public DiagnosticsEventListener(DiagnosticsListener listener, EventLevel logLevel)
		{
			this.listener = listener;
			this.logLevel = logLevel;
			List<EventSource> list = this.eventSourcesDuringConstruction;
			List<EventSource> list2;
			lock (list)
			{
				list2 = this.eventSourcesDuringConstruction;
				this.eventSourcesDuringConstruction = null;
			}
			foreach (EventSource eventSource in list2)
			{
				base.EnableEvents(eventSource, this.logLevel, EventKeywords.All);
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00014860 File Offset: 0x00012A60
		protected override void OnEventWritten(EventWrittenEventArgs eventSourceEvent)
		{
			if (eventSourceEvent == null || this.listener == null)
			{
				return;
			}
			EventMetaData eventMetaData = new EventMetaData();
			EventSource eventSource = eventSourceEvent.EventSource;
			eventMetaData.EventSourceName = ((eventSource != null) ? eventSource.Name : null);
			eventMetaData.Keywords = (long)eventSourceEvent.Keywords;
			eventMetaData.MessageFormat = eventSourceEvent.Message;
			eventMetaData.EventId = eventSourceEvent.EventId;
			eventMetaData.Level = eventSourceEvent.Level;
			EventMetaData eventMetaData2 = eventMetaData;
			TraceEvent traceEvent = new TraceEvent();
			traceEvent.MetaData = eventMetaData2;
			ReadOnlyCollection<object> payload = eventSourceEvent.Payload;
			traceEvent.Payload = ((payload != null) ? payload.ToArray<object>() : null);
			TraceEvent traceEvent2 = traceEvent;
			this.listener.WriteEvent(traceEvent2);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000148F8 File Offset: 0x00012AF8
		protected override void OnEventSourceCreated(EventSource eventSource)
		{
			if (eventSource.Name.StartsWith("Microsoft-ApplicationInsights-", StringComparison.Ordinal) || eventSource.Name.Equals("Microsoft-AspNet-Telemetry-Correlation", StringComparison.Ordinal))
			{
				List<EventSource> list = this.eventSourcesDuringConstruction;
				if (list != null)
				{
					List<EventSource> list2 = list;
					lock (list2)
					{
						if (this.eventSourcesDuringConstruction != null)
						{
							this.eventSourcesDuringConstruction.Add(eventSource);
							return;
						}
					}
				}
				base.EnableEvents(eventSource, this.logLevel, EventKeywords.All);
			}
			base.OnEventSourceCreated(eventSource);
		}

		// Token: 0x040001E0 RID: 480
		private const long AllKeyword = -1L;

		// Token: 0x040001E1 RID: 481
		private readonly EventLevel logLevel;

		// Token: 0x040001E2 RID: 482
		private readonly DiagnosticsListener listener;

		// Token: 0x040001E3 RID: 483
		private readonly List<EventSource> eventSourcesDuringConstruction = new List<EventSource>();
	}
}
