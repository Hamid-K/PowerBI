using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000689 RID: 1673
	public class QueueManagerTracePoint : MqTracePoint
	{
		// Token: 0x060037C4 RID: 14276 RVA: 0x000BC397 File Offset: 0x000BA597
		public QueueManagerTracePoint(ApplicationTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.QueueManager)
		{
		}

		// Token: 0x17000C51 RID: 3153
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
