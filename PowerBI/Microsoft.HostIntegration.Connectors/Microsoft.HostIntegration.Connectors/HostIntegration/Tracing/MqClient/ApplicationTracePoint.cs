using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000687 RID: 1671
	public class ApplicationTracePoint : MqTracePoint
	{
		// Token: 0x060037C0 RID: 14272 RVA: 0x000BC377 File Offset: 0x000BA577
		public ApplicationTracePoint(MqTraceContainer traceContainer)
			: base(traceContainer, TracePointIdentifiers.Application)
		{
		}

		// Token: 0x17000C4F RID: 3151
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				throw new Exception("BUGBUG: Application Tracepoint has no properties to set");
			}
		}
	}
}
