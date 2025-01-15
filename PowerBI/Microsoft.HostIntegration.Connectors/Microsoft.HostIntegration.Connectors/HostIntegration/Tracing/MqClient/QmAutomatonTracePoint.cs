using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x0200068A RID: 1674
	public class QmAutomatonTracePoint : MqTracePoint
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x000BC3A1 File Offset: 0x000BA5A1
		public QmAutomatonTracePoint(QueueManagerTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.QueueManagerAutomaton)
		{
		}

		// Token: 0x17000C52 RID: 3154
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
