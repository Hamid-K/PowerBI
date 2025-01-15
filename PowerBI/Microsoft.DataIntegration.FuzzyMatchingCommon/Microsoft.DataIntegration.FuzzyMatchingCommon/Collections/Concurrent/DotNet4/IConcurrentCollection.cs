using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000C1 RID: 193
	public interface IConcurrentCollection<T> : IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x0600085E RID: 2142
		bool Add(T item);

		// Token: 0x0600085F RID: 2143
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x06000860 RID: 2144
		bool Remove(out T item);

		// Token: 0x06000861 RID: 2145
		T[] ToArray();
	}
}
