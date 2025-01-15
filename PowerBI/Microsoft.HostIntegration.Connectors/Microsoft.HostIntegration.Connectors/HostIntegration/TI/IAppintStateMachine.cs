using System;
using Microsoft.HostIntegration.Tracing.TiWip;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000738 RID: 1848
	public interface IAppintStateMachine
	{
		// Token: 0x060039F1 RID: 14833
		object Invoke(RuntimeCallContext runtimeCallContext, int dispID, ref object[] inArray, out object[] outArray);

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060039F2 RID: 14834
		// (set) Token: 0x060039F3 RID: 14835
		StateMachineTracePoint TracePoint { get; set; }
	}
}
