using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006BC RID: 1724
	public class DataConverterTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600382C RID: 14380 RVA: 0x000BCF4C File Offset: 0x000BB14C
		public DataConverterTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.DataConverter)
		{
		}

		// Token: 0x17000C71 RID: 3185
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
