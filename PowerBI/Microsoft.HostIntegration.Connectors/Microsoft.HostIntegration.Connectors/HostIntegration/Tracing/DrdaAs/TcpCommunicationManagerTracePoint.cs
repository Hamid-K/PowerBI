using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006AE RID: 1710
	public class TcpCommunicationManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003810 RID: 14352 RVA: 0x000BCEC3 File Offset: 0x000BB0C3
		public TcpCommunicationManagerTracePoint(DrdaAsTraceContainer traceContainer)
			: base(traceContainer, 2)
		{
		}

		// Token: 0x17000C63 RID: 3171
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
