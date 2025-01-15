using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006E4 RID: 1764
	public class TnTracePoint : SiTracePoint
	{
		// Token: 0x0600387C RID: 14460 RVA: 0x000BD8DD File Offset: 0x000BBADD
		public TnTracePoint(Tn3270TracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.TnAutomaton)
		{
		}

		// Token: 0x17000C88 RID: 3208
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
