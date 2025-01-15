using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006AD RID: 1709
	public class ConfigurationManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600380E RID: 14350 RVA: 0x000BCEB9 File Offset: 0x000BB0B9
		public ConfigurationManagerTracePoint(DrdaAsTraceContainer traceContainer)
			: base(traceContainer, 1)
		{
		}

		// Token: 0x17000C62 RID: 3170
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
