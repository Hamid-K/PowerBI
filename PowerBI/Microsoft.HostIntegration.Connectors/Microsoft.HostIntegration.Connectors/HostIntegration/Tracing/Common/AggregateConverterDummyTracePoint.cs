using System;

namespace Microsoft.HostIntegration.Tracing.Common
{
	// Token: 0x020006CF RID: 1743
	public class AggregateConverterDummyTracePoint : AggregateConverterTracePoint
	{
		// Token: 0x17000C7D RID: 3197
		public override object this[AggregateConverterPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
