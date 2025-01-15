using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A1 RID: 1697
	public class XaManagerTracePoint : DrdaArTracePoint
	{
		// Token: 0x060037F8 RID: 14328 RVA: 0x000BCAD3 File Offset: 0x000BACD3
		public XaManagerTracePoint(ApplicationRequesterTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.XaManager)
		{
		}
	}
}
