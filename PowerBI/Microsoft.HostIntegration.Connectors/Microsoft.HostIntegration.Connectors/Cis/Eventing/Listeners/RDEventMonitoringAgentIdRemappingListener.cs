using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing.Listeners
{
	// Token: 0x02000494 RID: 1172
	public class RDEventMonitoringAgentIdRemappingListener : RDEventMonitoringAgentListener
	{
		// Token: 0x06002899 RID: 10393 RVA: 0x00077954 File Offset: 0x00075B54
		public RDEventMonitoringAgentIdRemappingListener(Guid providerGuid)
			: base(providerGuid)
		{
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0007794B File Offset: 0x00075B4B
		public RDEventMonitoringAgentIdRemappingListener(string providerGuid)
			: base(providerGuid)
		{
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0007B6C0 File Offset: 0x000798C0
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			base.TraceEvent(eventCache, source, EventPriorityMappings.GetEventPriority(id, eventType), id);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0007B6D4 File Offset: 0x000798D4
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			base.TraceEvent(eventCache, source, EventPriorityMappings.GetEventPriority(id, eventType), id, message);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0007B6EA File Offset: 0x000798EA
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			base.TraceEvent(eventCache, source, EventPriorityMappings.GetEventPriority(id, eventType), id, format, args);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0007B702 File Offset: 0x00079902
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			base.TraceData(eventCache, source, EventPriorityMappings.GetEventPriority(id, eventType), id, data);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0007B718 File Offset: 0x00079918
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			base.TraceData(eventCache, source, EventPriorityMappings.GetEventPriority(id, eventType), id, data);
		}
	}
}
