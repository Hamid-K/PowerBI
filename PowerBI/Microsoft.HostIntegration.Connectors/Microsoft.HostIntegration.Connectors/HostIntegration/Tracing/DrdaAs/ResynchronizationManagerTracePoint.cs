using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B6 RID: 1718
	public class ResynchronizationManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003820 RID: 14368 RVA: 0x000BCF0A File Offset: 0x000BB10A
		public ResynchronizationManagerTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.SyncPointManager)
		{
		}

		// Token: 0x17000C6B RID: 3179
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
