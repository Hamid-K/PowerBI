using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C8 RID: 1736
	public class FlowControlTracePoint : TiHipTracePoint
	{
		// Token: 0x06003846 RID: 14406 RVA: 0x000BD447 File Offset: 0x000BB647
		protected FlowControlTracePoint()
		{
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000BD47F File Offset: 0x000BB67F
		public FlowControlTracePoint(HipGenTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.FlowControl)
		{
		}

		// Token: 0x17000C78 RID: 3192
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
