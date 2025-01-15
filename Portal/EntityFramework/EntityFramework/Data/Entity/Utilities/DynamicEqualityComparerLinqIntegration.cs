using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200007C RID: 124
	internal static class DynamicEqualityComparerLinqIntegration
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0000FBF5 File Offset: 0x0000DDF5
		public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> func) where T : class
		{
			return source.Distinct(new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000FC03 File Offset: 0x0000DE03
		public static IEnumerable<IGrouping<TSource, TSource>> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> func) where TSource : class
		{
			return source.GroupBy((TSource t) => t, new DynamicEqualityComparer<TSource>(func));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000FC30 File Offset: 0x0000DE30
		public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> func) where T : class
		{
			return first.Intersect(second, new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000FC3F File Offset: 0x0000DE3F
		public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> func) where T : class
		{
			return first.Except(second, new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000FC4E File Offset: 0x0000DE4E
		public static bool Contains<T>(this IEnumerable<T> source, T value, Func<T, T, bool> func) where T : class
		{
			return source.Contains(value, new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000FC5D File Offset: 0x0000DE5D
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TSource, bool> func) where TSource : class
		{
			return source.SequenceEqual(other, new DynamicEqualityComparer<TSource>(func));
		}
	}
}
