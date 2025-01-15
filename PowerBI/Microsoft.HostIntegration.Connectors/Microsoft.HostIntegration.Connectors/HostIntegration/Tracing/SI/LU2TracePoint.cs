using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006E0 RID: 1760
	public class LU2TracePoint : SiTracePoint
	{
		// Token: 0x06003874 RID: 14452 RVA: 0x000BD8B5 File Offset: 0x000BBAB5
		public LU2TracePoint(SiTraceContainer traceContainer)
			: base(traceContainer, TracePointIdentifiers.Lu2)
		{
		}

		// Token: 0x17000C84 RID: 3204
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
