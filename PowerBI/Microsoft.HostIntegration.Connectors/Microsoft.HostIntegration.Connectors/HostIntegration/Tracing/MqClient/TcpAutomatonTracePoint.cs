using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x0200068E RID: 1678
	public class TcpAutomatonTracePoint : MqTracePoint
	{
		// Token: 0x060037CE RID: 14286 RVA: 0x000BC3C9 File Offset: 0x000BA5C9
		public TcpAutomatonTracePoint(ConnectionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.TcpAutomaton)
		{
		}

		// Token: 0x17000C56 RID: 3158
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
