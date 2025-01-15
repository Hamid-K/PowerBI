using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200008C RID: 140
	[DebuggerStepThrough]
	internal static class IEnumerableExtensions
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00010840 File Offset: 0x0000EA40
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

		// Token: 0x06000479 RID: 1145 RVA: 0x00010894 File Offset: 0x0000EA94
		public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action)
		{
			int num = 0;
			foreach (T t in ts)
			{
				action(t, num++);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000108E4 File Offset: 0x0000EAE4
		public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
		{
			foreach (T t in ts)
			{
				action(t);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action)
		{
			foreach (T t in ts)
			{
				action(t);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00010978 File Offset: 0x0000EB78
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

		// Token: 0x0600047D RID: 1149 RVA: 0x000109DC File Offset: 0x0000EBDC
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

		// Token: 0x0600047E RID: 1150 RVA: 0x000109F3 File Offset: 0x0000EBF3
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
