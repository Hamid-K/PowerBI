using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A4 RID: 1700
	public class KerberosManagerTracePoint : DrdaArTracePoint
	{
		// Token: 0x060037FB RID: 14331 RVA: 0x000BCAF1 File Offset: 0x000BACF1
		public KerberosManagerTracePoint(SecurityManagerTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.KerberosManager)
		{
		}
	}
}
