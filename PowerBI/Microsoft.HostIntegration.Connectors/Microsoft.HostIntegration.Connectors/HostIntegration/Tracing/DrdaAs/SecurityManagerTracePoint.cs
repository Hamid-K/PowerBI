using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B4 RID: 1716
	public class SecurityManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600381C RID: 14364 RVA: 0x000BCEFF File Offset: 0x000BB0FF
		public SecurityManagerTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.SecurityManager)
		{
		}

		// Token: 0x17000C69 RID: 3177
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
