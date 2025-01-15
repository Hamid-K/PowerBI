using System;

namespace Microsoft.HostIntegration.Tracing.TiWip
{
	// Token: 0x020006EC RID: 1772
	public class TBGenTracePoint : TiWipTracePoint
	{
		// Token: 0x06003888 RID: 14472 RVA: 0x000BDD63 File Offset: 0x000BBF63
		public TBGenTracePoint(TiWipTraceContainer traceContainer)
			: base(traceContainer, 0)
		{
		}

		// Token: 0x17000C8B RID: 3211
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
