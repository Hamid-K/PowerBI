using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000093 RID: 147
	public interface IArray<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000662 RID: 1634
		int Length { get; }

		// Token: 0x170000F8 RID: 248
		T this[int i] { get; }
	}
}
