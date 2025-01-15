using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x0200068B RID: 1675
	public class QueueTracePoint : MqTracePoint
	{
		// Token: 0x060037C8 RID: 14280 RVA: 0x000BC3AB File Offset: 0x000BA5AB
		public QueueTracePoint(ApplicationTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Queue)
		{
		}

		// Token: 0x17000C53 RID: 3155
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
