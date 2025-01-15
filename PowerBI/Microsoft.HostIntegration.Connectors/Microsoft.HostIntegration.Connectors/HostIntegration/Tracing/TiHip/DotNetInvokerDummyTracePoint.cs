using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006CB RID: 1739
	public class DotNetInvokerDummyTracePoint : DotNetInvokerTracePoint
	{
		// Token: 0x17000C7B RID: 3195
		public override object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
