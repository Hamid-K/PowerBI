using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B3 RID: 1715
	public class DatabaseTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600381A RID: 14362 RVA: 0x000BCEF5 File Offset: 0x000BB0F5
		public DatabaseTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Database)
		{
		}

		// Token: 0x17000C68 RID: 3176
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
