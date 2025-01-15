using System;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CD3 RID: 7379
	internal interface IPooledContainerFactory : IContainerFactory, IDisposable
	{
		// Token: 0x17002DB0 RID: 11696
		// (get) Token: 0x0600B7C6 RID: 47046
		int ActiveContainerCount { get; }

		// Token: 0x0600B7C7 RID: 47047
		IContainer CreateContainer(ConcurrentSet<int> preferredContainerIDs);
	}
}
