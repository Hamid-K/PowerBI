using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006CA RID: 1738
	public class DotNetInvokerTracePoint : TiHipTracePoint
	{
		// Token: 0x0600384C RID: 14412 RVA: 0x000BD447 File Offset: 0x000BB647
		protected DotNetInvokerTracePoint()
		{
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000BD491 File Offset: 0x000BB691
		public DotNetInvokerTracePoint(FlowControlTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.DotNetInvoker)
		{
		}

		// Token: 0x17000C7A RID: 3194
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
