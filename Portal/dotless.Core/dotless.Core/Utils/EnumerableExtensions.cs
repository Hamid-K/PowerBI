using System;
using System.Collections.Generic;

namespace dotless.Core.Utils
{
	// Token: 0x0200000A RID: 10
	internal static class EnumerableExtensions
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002A23 File Offset: 0x00000C23
		internal static bool IsSubsequenceOf<TElement>(this IList<TElement> subsequence, IList<TElement> sequence)
		{
			return subsequence.IsSubsequenceOf(sequence, (TElement element1, TElement element2) => object.Equals(element1, element2));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A4C File Offset: 0x00000C4C
		internal static bool IsSubsequenceOf<TElement>(this IList<TElement> subsequence, IList<TElement> sequence, Func<TElement, TElement, bool> areEqual)
		{
			return subsequence.IsSubsequenceOf(sequence, (int _, TElement element1, int __, TElement element2) => areEqual(element1, element2));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A7C File Offset: 0x00000C7C
		internal static bool IsSubsequenceOf<TElement>(this IList<TElement> subsequence, IList<TElement> sequence, Func<int, TElement, int, TElement, bool> areEqual)
		{
			if (subsequence.Count == 0)
			{
				return true;
			}
			if (sequence.Count == 0)
			{
				return false;
			}
			int num = 0;
			while (!areEqual(0, subsequence[0], num, sequence[num]))
			{
				num++;
				if (num >= sequence.Count)
				{
					return false;
				}
			}
			int num2 = 0;
			for (;;)
			{
				num++;
				num2++;
				if (num2 >= subsequence.Count)
				{
					break;
				}
				if (num >= sequence.Count)
				{
					return false;
				}
				if (!areEqual(num2, subsequence[num2], num, sequence[num]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
