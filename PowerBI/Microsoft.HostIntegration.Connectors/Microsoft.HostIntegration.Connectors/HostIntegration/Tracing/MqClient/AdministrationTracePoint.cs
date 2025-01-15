using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000685 RID: 1669
	public class AdministrationTracePoint : MqTracePoint
	{
		// Token: 0x060037BC RID: 14268 RVA: 0x000BC34B File Offset: 0x000BA54B
		public AdministrationTracePoint(MqTraceContainer traceContainer)
			: base(traceContainer, TracePointIdentifiers.Administration)
		{
		}

		// Token: 0x17000C4D RID: 3149
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				throw new Exception("BUGBUG: Administration Tracepoint has no properties to set");
			}
		}
	}
}
