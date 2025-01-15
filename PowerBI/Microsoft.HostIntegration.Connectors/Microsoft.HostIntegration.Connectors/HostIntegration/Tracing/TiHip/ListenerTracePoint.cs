using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C5 RID: 1733
	public class ListenerTracePoint : TiHipTracePoint
	{
		// Token: 0x0600383D RID: 14397 RVA: 0x000BD447 File Offset: 0x000BB647
		protected ListenerTracePoint()
		{
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000BD461 File Offset: 0x000BB661
		public ListenerTracePoint(ServiceTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.CallProcessing)
		{
		}

		// Token: 0x17000C75 RID: 3189
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
