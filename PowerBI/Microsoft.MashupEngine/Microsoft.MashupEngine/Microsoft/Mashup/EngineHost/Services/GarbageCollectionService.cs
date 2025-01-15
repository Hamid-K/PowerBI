using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019E8 RID: 6632
	internal class GarbageCollectionService : IDisposable
	{
		// Token: 0x0600A7DE RID: 42974 RVA: 0x0022B690 File Offset: 0x00229890
		public GarbageCollectionService(IEvaluationConstants evaluationConstants)
		{
			this.evaluationConstants = evaluationConstants;
		}

		// Token: 0x0600A7DF RID: 42975 RVA: 0x0022B6A0 File Offset: 0x002298A0
		public void Dispose()
		{
			if (EvaluationHost.IsContainer)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("GarbageCollectionService", this.evaluationConstants, TraceEventType.Information, null))
				{
					hostTrace.Add("CommitBefore", ProcessInfo.GetCommit(null), false);
					GC.Collect();
					hostTrace.Add("CommitAfter", ProcessInfo.GetCommit(null), false);
				}
			}
		}

		// Token: 0x04005769 RID: 22377
		private readonly IEvaluationConstants evaluationConstants;
	}
}
