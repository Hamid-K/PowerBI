using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006E5 RID: 1765
	public class TcpTracePoint : SiTracePoint
	{
		// Token: 0x0600387E RID: 14462 RVA: 0x000BD8E7 File Offset: 0x000BBAE7
		public TcpTracePoint(TnTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.TcpAutomaton)
		{
		}

		// Token: 0x17000C89 RID: 3209
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
