using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006AF RID: 1711
	public class SnaCommunicationManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003812 RID: 14354 RVA: 0x000BCECD File Offset: 0x000BB0CD
		public SnaCommunicationManagerTracePoint(DrdaAsTraceContainer traceContainer)
			: base(traceContainer, 3)
		{
		}

		// Token: 0x17000C64 RID: 3172
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
