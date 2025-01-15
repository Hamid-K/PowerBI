using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000074 RID: 116
	public static class EnumerableLinqEx
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000A690 File Offset: 0x00008890
		public static bool SequenceEqual<TSource>(this TSource[] source, TSource[] target)
		{
			if (source == null && target == null)
			{
				return true;
			}
			if (source == null || target == null)
			{
				return false;
			}
			if (source.Length != target.Length)
			{
				return false;
			}
			int num = source.Length;
			for (int i = 0; i < num; i++)
			{
				if (source[i] != null || target[i] != null)
				{
					if (source[i] == null || target[i] == null)
					{
						return false;
					}
					int num2 = i;
					ref TSource ptr = ref source[num2];
					if (default(TSource) == null)
					{
						TSource tsource = source[num2];
						ptr = ref tsource;
					}
					if (!ptr.Equals(target[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A740 File Offset: 0x00008940
		public static bool SequenceEqual<TSource>(this IReadOnlyList<TSource> source, IReadOnlyList<TSource> target)
		{
			if (source == null && target == null)
			{
				return true;
			}
			if (source == null || target == null)
			{
				return false;
			}
			if (source.Count != target.Count)
			{
				return false;
			}
			int count = source.Count;
			for (int i = 0; i < count; i++)
			{
				if (source[i] != null || target[i] != null)
				{
					if (source[i] == null || target[i] == null)
					{
						return false;
					}
					TSource tsource = source[i];
					if (!tsource.Equals(target[i]))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
