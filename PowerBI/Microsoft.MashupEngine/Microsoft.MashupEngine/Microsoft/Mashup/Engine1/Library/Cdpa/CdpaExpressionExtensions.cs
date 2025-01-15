using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E0F RID: 3599
	internal static class CdpaExpressionExtensions
	{
		// Token: 0x060060E6 RID: 24806 RVA: 0x0014A48E File Offset: 0x0014868E
		public static bool IsNullOrEmpty<T>(this IList<T> list)
		{
			return list == null || list.Count == 0;
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x0014A49E File Offset: 0x0014869E
		public static string NullableMerge(this string s1, string s2)
		{
			if (s1 == null)
			{
				return s2;
			}
			if (s2 == null)
			{
				return s1;
			}
			if (s1 == s2)
			{
				return s1;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x0014A4BA File Offset: 0x001486BA
		public static bool NullableEquals<T>(this T item1, T item2)
		{
			return (item1 == null && item2 == null) || (item1 != null && item2 != null && item1.Equals(item2));
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x0014A4F3 File Offset: 0x001486F3
		public static bool NullableSetEquals<T>(this IEnumerable<T> set1, IEnumerable<T> set2)
		{
			return (set1 == null && set2 == null) || ((set1 == null || set2 == null) && set1.SetEquals(set2));
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x0014A50C File Offset: 0x0014870C
		public static int NullableGetHashCode<T>(this T item)
		{
			if (item == null)
			{
				return 0;
			}
			return item.GetHashCode();
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0014A528 File Offset: 0x00148728
		public static int NullableSetGetHashCode<T>(this IEnumerable<T> set)
		{
			if (set == null)
			{
				return 0;
			}
			HashSet<T> hashSet = new HashSet<T>(set);
			int num = 0;
			foreach (T t in hashSet)
			{
				num += t.GetHashCode();
			}
			return num;
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x0014A58C File Offset: 0x0014878C
		public static T NullableIntersect<T>(this T item1, T item2) where T : IIntersectable<T>
		{
			if (item1 == null)
			{
				return item2;
			}
			if (item2 == null)
			{
				return item1;
			}
			return item1.Intersect(item2);
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x0014A5B0 File Offset: 0x001487B0
		public static T NullableUnion<T>(this T item1, T item2) where T : IUnionable<T>
		{
			if (item1 == null)
			{
				return item2;
			}
			if (item2 == null)
			{
				return item1;
			}
			return item1.Union(item2);
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x0014A5D4 File Offset: 0x001487D4
		public static IEnumerable<T> NullableSetUnion<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
		{
			if (list1 == null)
			{
				return list2;
			}
			if (list2 == null)
			{
				return list1;
			}
			return list1.Union(list2);
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x0014A5E7 File Offset: 0x001487E7
		public static IEnumerable<T> NullableSetIntersect<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
		{
			if (list1 == null)
			{
				return list2;
			}
			if (list2 == null)
			{
				return list1;
			}
			return list1.Intersect(list2);
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x0014A5FC File Offset: 0x001487FC
		public static IEnumerable<CdpaMetricSplit> CrossJoin(this IEnumerable<CdpaMetricSplit> splits1, IEnumerable<CdpaMetricSplit> splits2)
		{
			Dictionary<string, CdpaMetricSplit> dictionary = new Dictionary<string, CdpaMetricSplit>();
			foreach (CdpaMetricSplit cdpaMetricSplit in splits1)
			{
				dictionary.Add(cdpaMetricSplit.PropertyName, cdpaMetricSplit);
			}
			foreach (CdpaMetricSplit cdpaMetricSplit2 in splits2)
			{
				CdpaMetricSplit cdpaMetricSplit3;
				if (dictionary.TryGetValue(cdpaMetricSplit2.PropertyName, out cdpaMetricSplit3))
				{
					dictionary[cdpaMetricSplit2.PropertyName] = cdpaMetricSplit3.Intersect(cdpaMetricSplit2);
				}
				else
				{
					dictionary[cdpaMetricSplit2.PropertyName] = cdpaMetricSplit2;
				}
			}
			return dictionary.Values;
		}
	}
}
