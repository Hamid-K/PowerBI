using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C9 RID: 1737
	public class FlowControlDummyTracePoint : FlowControlTracePoint
	{
		// Token: 0x17000C79 RID: 3193
		public override object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
