using System;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD2 RID: 7378
	internal interface IProcessContainerFactory : IContainerFactory, IDisposable
	{
		// Token: 0x0600B7C5 RID: 47045
		IProcessContainer CreateProcessContainer();
	}
}
