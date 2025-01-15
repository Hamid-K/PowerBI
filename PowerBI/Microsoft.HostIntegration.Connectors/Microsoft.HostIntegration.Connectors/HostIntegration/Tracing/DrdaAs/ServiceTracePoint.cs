using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006AC RID: 1708
	public class ServiceTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600380C RID: 14348 RVA: 0x000BCEAF File Offset: 0x000BB0AF
		public ServiceTracePoint(DrdaAsTraceContainer traceContainer)
			: base(traceContainer, 0)
		{
		}

		// Token: 0x17000C61 RID: 3169
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
