using System;

namespace Microsoft.HostIntegration.Tracing.HostFiles
{
	// Token: 0x0200069B RID: 1691
	public class HostFileClientDummyTracePoint : HostFileClientTracePoint
	{
		// Token: 0x17000C5C RID: 3164
		public override object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
