using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x02000699 RID: 1689
	public interface IAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600244D RID: 9293
		bool IsCompleted { get; }

		// Token: 0x0600244E RID: 9294
		void GetResult();
	}
}
