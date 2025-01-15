using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000688 RID: 1672
	public class ApiTracePoint : MqTracePoint
	{
		// Token: 0x060037C2 RID: 14274 RVA: 0x000BC38D File Offset: 0x000BA58D
		public ApiTracePoint(ApplicationTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.Api)
		{
		}

		// Token: 0x17000C50 RID: 3152
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
