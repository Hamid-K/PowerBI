using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B1 RID: 1713
	public class SessionTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003816 RID: 14358 RVA: 0x000BCEE1 File Offset: 0x000BB0E1
		public SessionTracePoint(DrdaAsTraceContainer traceContainer)
			: base(traceContainer, 5)
		{
		}

		// Token: 0x17000C66 RID: 3174
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
