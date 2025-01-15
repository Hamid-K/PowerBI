using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006E3 RID: 1763
	public class Tn3270TracePoint : SiTracePoint
	{
		// Token: 0x0600387A RID: 14458 RVA: 0x000BD8D3 File Offset: 0x000BBAD3
		public Tn3270TracePoint(LU2TracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.DataStreamAutomaton)
		{
		}

		// Token: 0x17000C87 RID: 3207
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
