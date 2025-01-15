using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x020000A6 RID: 166
	internal interface IBlockAllocator<T>
	{
		// Token: 0x0600074F RID: 1871
		T[] New(int length);
	}
}
