using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000015 RID: 21
	public interface IInterlocked<T>
	{
		// Token: 0x06000063 RID: 99
		T CompareExchange(ref T location1, T value, T comparand);
	}
}
