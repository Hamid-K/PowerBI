using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B2 RID: 1714
	public class SqlApplicationManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003818 RID: 14360 RVA: 0x000BCEEB File Offset: 0x000BB0EB
		public SqlApplicationManagerTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.SqlApplicationManager)
		{
		}

		// Token: 0x17000C67 RID: 3175
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
