using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001961 RID: 6497
	public interface IMutableEvaluationSession : IEvaluationSession, IDisposable
	{
		// Token: 0x170029FA RID: 10746
		// (get) Token: 0x0600A4B2 RID: 42162
		MutableEngineHost EngineHost { get; }

		// Token: 0x0600A4B3 RID: 42163
		void AddDisposable(IDisposable disposable);
	}
}
