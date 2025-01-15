using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x0200068D RID: 1677
	public class ConnectionTracePoint : MqTracePoint
	{
		// Token: 0x060037CC RID: 14284 RVA: 0x000BC3BF File Offset: 0x000BA5BF
		public ConnectionTracePoint(ApplicationTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Connection)
		{
		}

		// Token: 0x17000C55 RID: 3157
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
