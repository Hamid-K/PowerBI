using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C4 RID: 1732
	public class ServiceDummyTracePoint : ServiceTracePoint
	{
		// Token: 0x17000C74 RID: 3188
		public override object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
