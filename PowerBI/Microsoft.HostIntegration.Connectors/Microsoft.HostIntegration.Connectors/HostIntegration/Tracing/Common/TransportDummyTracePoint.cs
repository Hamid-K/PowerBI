using System;

namespace Microsoft.HostIntegration.Tracing.Common
{
	// Token: 0x020006D7 RID: 1751
	public class TransportDummyTracePoint : TransportTracePoint
	{
		// Token: 0x17000C81 RID: 3201
		public override object this[TransportPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x06003869 RID: 14441 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
