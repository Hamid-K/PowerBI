using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A3 RID: 1699
	public class SecurityManagerTracePoint : DrdaArTracePoint
	{
		// Token: 0x060037FA RID: 14330 RVA: 0x000BCAE7 File Offset: 0x000BACE7
		public SecurityManagerTracePoint(ApplicationRequesterTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.SecurityManager)
		{
		}
	}
}
