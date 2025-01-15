using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200015B RID: 347
	internal static class EntityUtil
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x0003A091 File Offset: 0x00038291
		internal static IEnumerable<KeyValuePair<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
		{
			if (first == null || second == null)
			{
				yield break;
			}
			using (IEnumerator<T1> firstEnumerator = first.GetEnumerator())
			{
				using (IEnumerator<T2> secondEnumerator = second.GetEnumerator())
				{
					while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
					{
						yield return new KeyValuePair<T1, T2>(firstEnumerator.Current, secondEnumerator.Current);
					}
				}
				IEnumerator<T2> secondEnumerator = null;
			}
			IEnumerator<T1> firstEnumerator = null;
			yield break;
			yield break;
		}
	}
}
