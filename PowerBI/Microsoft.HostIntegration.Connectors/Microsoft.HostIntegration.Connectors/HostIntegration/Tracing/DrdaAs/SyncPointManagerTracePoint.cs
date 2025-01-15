using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B5 RID: 1717
	public class SyncPointManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600381E RID: 14366 RVA: 0x000BCF0A File Offset: 0x000BB10A
		public SyncPointManagerTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.SyncPointManager)
		{
		}

		// Token: 0x17000C6A RID: 3178
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
