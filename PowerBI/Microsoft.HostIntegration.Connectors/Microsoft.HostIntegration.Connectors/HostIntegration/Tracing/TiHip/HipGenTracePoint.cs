using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C7 RID: 1735
	public class HipGenTracePoint : TiHipTracePoint
	{
		// Token: 0x06003843 RID: 14403 RVA: 0x000BD447 File Offset: 0x000BB647
		protected HipGenTracePoint()
		{
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000BD475 File Offset: 0x000BB675
		public HipGenTracePoint(TiHipTraceContainer traceContainer)
			: base(traceContainer, 3)
		{
		}

		// Token: 0x17000C77 RID: 3191
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
