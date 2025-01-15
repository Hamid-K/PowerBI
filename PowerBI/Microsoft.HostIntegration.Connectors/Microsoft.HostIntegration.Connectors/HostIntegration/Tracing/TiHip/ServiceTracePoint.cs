using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C3 RID: 1731
	public class ServiceTracePoint : TiHipTracePoint
	{
		// Token: 0x06003837 RID: 14391 RVA: 0x000BD447 File Offset: 0x000BB647
		protected ServiceTracePoint()
		{
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000BD44F File Offset: 0x000BB64F
		public ServiceTracePoint(TiHipTraceContainer traceContainer)
			: base(traceContainer, 0)
		{
		}

		// Token: 0x17000C73 RID: 3187
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
