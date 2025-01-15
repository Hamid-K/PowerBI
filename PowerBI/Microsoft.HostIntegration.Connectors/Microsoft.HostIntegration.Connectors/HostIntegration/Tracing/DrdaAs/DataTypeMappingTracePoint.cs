using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B7 RID: 1719
	public class DataTypeMappingTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003822 RID: 14370 RVA: 0x000BCF15 File Offset: 0x000BB115
		public DataTypeMappingTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.DataTypeMapping)
		{
		}

		// Token: 0x17000C6C RID: 3180
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
