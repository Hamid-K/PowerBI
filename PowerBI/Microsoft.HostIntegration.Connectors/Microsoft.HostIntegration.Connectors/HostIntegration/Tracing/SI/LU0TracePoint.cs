using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006DF RID: 1759
	public class LU0TracePoint : SiTracePoint
	{
		// Token: 0x06003872 RID: 14450 RVA: 0x000BD8AB File Offset: 0x000BBAAB
		public LU0TracePoint(SiTraceContainer traceContainer)
			: base(traceContainer, TracePointIdentifiers.Lu0)
		{
		}

		// Token: 0x17000C83 RID: 3203
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
