using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B0 RID: 1712
	public class SessionManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003814 RID: 14356 RVA: 0x000BCED7 File Offset: 0x000BB0D7
		public SessionManagerTracePoint(DrdaAsTraceContainer traceContainer)
			: base(traceContainer, 4)
		{
		}

		// Token: 0x17000C65 RID: 3173
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
