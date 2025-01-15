using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x0200000A RID: 10
	internal static class EnumerableExtensions
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020A0 File Offset: 0x000002A0
		[NullableContext(1)]
		public static HashSet<TSource> ToHashSet<[Nullable(2)] TSource>(this IEnumerable<TSource> source)
		{
			return new HashSet<TSource>(source);
		}
	}
}
