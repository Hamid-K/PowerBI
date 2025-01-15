using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006E2 RID: 1762
	public class TerminalTracePoint : SiTracePoint
	{
		// Token: 0x06003878 RID: 14456 RVA: 0x000BD8C9 File Offset: 0x000BBAC9
		public TerminalTracePoint(LU2TracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Terminal)
		{
		}

		// Token: 0x17000C86 RID: 3206
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
