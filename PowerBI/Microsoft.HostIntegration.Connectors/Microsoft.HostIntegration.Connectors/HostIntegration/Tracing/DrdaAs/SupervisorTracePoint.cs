using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B9 RID: 1721
	public class SupervisorTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003826 RID: 14374 RVA: 0x000BCF2B File Offset: 0x000BB12B
		public SupervisorTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Supervisor)
		{
		}

		// Token: 0x17000C6E RID: 3182
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
