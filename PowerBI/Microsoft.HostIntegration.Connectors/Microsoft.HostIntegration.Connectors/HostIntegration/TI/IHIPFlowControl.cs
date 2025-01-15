using System;
using Microsoft.HostIntegration.Tracing.TiHip;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200070E RID: 1806
	public interface IHIPFlowControl
	{
		// Token: 0x06003942 RID: 14658
		void Invoke(ref RuntimeCallContext runtimeCallContext);

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003943 RID: 14659
		// (set) Token: 0x06003944 RID: 14660
		FlowControlTracePoint TracePoint { get; set; }
	}
}
