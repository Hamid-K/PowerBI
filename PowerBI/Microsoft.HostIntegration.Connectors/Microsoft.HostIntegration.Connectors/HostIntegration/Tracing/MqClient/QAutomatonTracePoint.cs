using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x0200068C RID: 1676
	public class QAutomatonTracePoint : MqTracePoint
	{
		// Token: 0x060037CA RID: 14282 RVA: 0x000BC3B5 File Offset: 0x000BA5B5
		public QAutomatonTracePoint(QueueTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.QueueAutomaton)
		{
		}

		// Token: 0x17000C54 RID: 3156
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
