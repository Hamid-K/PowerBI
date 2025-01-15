using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DE7 RID: 7655
	public interface IContainerEvaluationMonitorService
	{
		// Token: 0x0600BD97 RID: 48535
		IDisposable BeginEvaluation(SafeHandle processHandle);
	}
}
