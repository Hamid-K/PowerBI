using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A0 RID: 1696
	public class TcpConnectionManagerTracePoint : DrdaArTracePoint
	{
		// Token: 0x060037F7 RID: 14327 RVA: 0x000BCAC9 File Offset: 0x000BACC9
		public TcpConnectionManagerTracePoint(ApplicationRequesterTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.TcpConnectionManager)
		{
		}
	}
}
