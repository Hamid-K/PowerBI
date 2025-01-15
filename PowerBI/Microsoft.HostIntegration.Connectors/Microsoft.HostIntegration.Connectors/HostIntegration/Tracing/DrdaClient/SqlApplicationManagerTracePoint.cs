using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A5 RID: 1701
	public class SqlApplicationManagerTracePoint : DrdaArTracePoint
	{
		// Token: 0x060037FC RID: 14332 RVA: 0x000BCAFB File Offset: 0x000BACFB
		public SqlApplicationManagerTracePoint(ApplicationRequesterTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.SqlApplicationManager)
		{
		}
	}
}
