using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000073 RID: 115
	public static class EnumerableExtensionMethods
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0001CBAB File Offset: 0x0001ADAB
		public static IEnumerable<T> Interleave<T>(params IEnumerable<T>[] interleavedItems)
		{
			IEnumerator<T>[] all = Enumerable.ToArray<IEnumerator<T>>(Enumerable.Select<IEnumerable<T>, IEnumerator<T>>(interleavedItems, (IEnumerable<T> e) => e.GetEnumerator()));
			int i = 0;
			while (i < all.Length)
			{
				foreach (IEnumerator<T> enumerator in all)
				{
					if (enumerator.MoveNext())
					{
						yield return enumerator.Current;
						i = 0;
					}
					else
					{
						i++;
					}
				}
				IEnumerator<T>[] array = null;
			}
			yield break;
		}
	}
}
