using System;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD0 RID: 7376
	internal interface IProcessContainer : IContainer, IDisposable
	{
		// Token: 0x0600B7C3 RID: 47043
		void SetProcessWorkingSetSize(int maxWorkingSetInMB);
	}
}
