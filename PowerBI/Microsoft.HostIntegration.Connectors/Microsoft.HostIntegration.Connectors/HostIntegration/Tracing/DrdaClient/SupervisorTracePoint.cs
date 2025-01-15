using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A2 RID: 1698
	public class SupervisorTracePoint : DrdaArTracePoint
	{
		// Token: 0x060037F9 RID: 14329 RVA: 0x000BCADD File Offset: 0x000BACDD
		public SupervisorTracePoint(ApplicationRequesterTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Supervisor)
		{
		}
	}
}
