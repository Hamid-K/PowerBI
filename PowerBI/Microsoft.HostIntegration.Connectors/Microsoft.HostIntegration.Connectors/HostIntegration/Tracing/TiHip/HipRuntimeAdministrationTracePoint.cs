using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C6 RID: 1734
	public class HipRuntimeAdministrationTracePoint : TiHipTracePoint
	{
		// Token: 0x06003840 RID: 14400 RVA: 0x000BD447 File Offset: 0x000BB647
		protected HipRuntimeAdministrationTracePoint()
		{
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000BD46B File Offset: 0x000BB66B
		public HipRuntimeAdministrationTracePoint(ServiceTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Administration)
		{
		}

		// Token: 0x17000C76 RID: 3190
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
