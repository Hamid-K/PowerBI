using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000090 RID: 144
	internal interface IObjectAllocator<T> : IAllocator<T>
	{
		// Token: 0x06000652 RID: 1618
		void Return(T item);
	}
}
