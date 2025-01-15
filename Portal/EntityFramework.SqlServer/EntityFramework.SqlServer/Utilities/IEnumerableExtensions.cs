using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002A RID: 42
	[DebuggerStepThrough]
	internal static class IEnumerableExtensions
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public static string Uniquify(this IEnumerable<string> inputStrings, string targetString)
		{
			IEnumerableExtensions.<>c__DisplayClass0_0 CS$<>8__locals1 = new IEnumerableExtensions.<>c__DisplayClass0_0();
			CS$<>8__locals1.uniqueString = targetString;
			int num = 0;
			while (inputStrings.Any((string n) => string.Equals(n, CS$<>8__locals1.uniqueString, StringComparison.Ordinal)))
			{
				IEnumerableExtensions.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
				int num2;
				num = (num2 = num + 1);
				CS$<>8__locals2.uniqueString = targetString + num2.ToString();
			}
			return CS$<>8__locals1.uniqueString;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00010408 File Offset: 0x0000E608
		public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action)
		{
			int num = 0;
			foreach (T t in ts)
			{
				action(t, num++);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00010458 File Offset: 0x0000E658
		public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
		{
			foreach (T t in ts)
			{
				action(t);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action)
		{
			foreach (T t in ts)
			{
				action(t);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000104EC File Offset: 0x0000E6EC
		public static string Join<T>(this IEnumerable<T> ts, Func<T, string> selector = null, string separator = ", ")
		{
			Func<T, string> func;
			if ((func = selector) == null && (func = IEnumerableExtensions.<>c__4<T>.<>9__4_0) == null)
			{
				func = (IEnumerableExtensions.<>c__4<T>.<>9__4_0 = (T t) => t.ToString());
			}
			selector = func;
			return string.Join(separator, ts.Where((T t) => t != null).Select(selector));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010550 File Offset: 0x0000E750
		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			yield return value;
			foreach (TSource tsource in source)
			{
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00010567 File Offset: 0x0000E767
		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			foreach (TSource tsource in source)
			{
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield return value;
			yield break;
			yield break;
		}
	}
}
