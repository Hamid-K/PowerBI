using System;
using System.Threading;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E32 RID: 7730
	internal interface ISignalEvaluationCanceled
	{
		// Token: 0x17002ED1 RID: 11985
		// (get) Token: 0x0600BE47 RID: 48711
		WaitHandle EvaluationCanceled { get; }
	}
}
