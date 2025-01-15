using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DFD RID: 7677
	public interface IEvaluationSession : IDisposable
	{
		// Token: 0x17002EAA RID: 11946
		// (get) Token: 0x0600BDB3 RID: 48563
		string Identity { get; }

		// Token: 0x17002EAB RID: 11947
		// (get) Token: 0x0600BDB4 RID: 48564
		IEngineHost EngineHost { get; }

		// Token: 0x17002EAC RID: 11948
		// (get) Token: 0x0600BDB5 RID: 48565
		IProgressReader ProgressReader { get; }
	}
}
