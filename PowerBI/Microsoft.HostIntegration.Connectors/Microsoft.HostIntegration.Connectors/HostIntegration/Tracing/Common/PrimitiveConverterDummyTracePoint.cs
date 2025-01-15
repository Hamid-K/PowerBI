using System;

namespace Microsoft.HostIntegration.Tracing.Common
{
	// Token: 0x020006D3 RID: 1747
	public class PrimitiveConverterDummyTracePoint : PrimitiveConverterTracePoint
	{
		// Token: 0x17000C7F RID: 3199
		public override object this[PrimitiveConverterPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
