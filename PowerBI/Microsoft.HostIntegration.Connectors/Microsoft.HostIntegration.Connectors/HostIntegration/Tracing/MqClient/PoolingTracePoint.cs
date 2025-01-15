using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000686 RID: 1670
	public class PoolingTracePoint : MqTracePoint
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x000BC361 File Offset: 0x000BA561
		public PoolingTracePoint(MqTraceContainer traceContainer)
			: base(traceContainer, TracePointIdentifiers.Pooling)
		{
		}

		// Token: 0x17000C4E RID: 3150
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				throw new Exception("BUGBUG: Pooling Tracepoint has no properties to set");
			}
		}
	}
}
