using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections.Concurrent.DotNet4
{
	// Token: 0x020000C2 RID: 194
	public interface IProducerConsumerCollection<T> : IEnumerable<T>, IEnumerable, ICollection
	{
		// Token: 0x06000862 RID: 2146
		void CopyTo(T[] array, int index);

		// Token: 0x06000863 RID: 2147
		bool TryAdd(T item);

		// Token: 0x06000864 RID: 2148
		bool TryTake(out T item);

		// Token: 0x06000865 RID: 2149
		T[] ToArray();
	}
}
