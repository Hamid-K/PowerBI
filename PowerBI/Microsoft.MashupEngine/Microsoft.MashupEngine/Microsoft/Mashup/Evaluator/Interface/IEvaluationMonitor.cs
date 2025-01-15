using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DFE RID: 7678
	internal interface IEvaluationMonitor
	{
		// Token: 0x0600BDB6 RID: 48566
		IDisposable BeginEvaluation(IEngineHost engineHost);
	}
}
