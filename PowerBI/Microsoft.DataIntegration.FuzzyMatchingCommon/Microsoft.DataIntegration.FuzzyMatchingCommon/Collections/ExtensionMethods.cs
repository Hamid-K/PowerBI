using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.DotNet4;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Linq.DotNet4;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Text;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000078 RID: 120
	public static class ExtensionMethods
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			TValue tvalue;
			if (!d.TryGetValue(key, ref tvalue))
			{
				d[key] = addValueFactory.Invoke(key);
				return;
			}
			d[key] = updateValueFactory.Invoke(key, tvalue);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		public static IEnumerable<T> Apply<T>(this IEnumerable<T> items, Action<T> action)
		{
			return Enumerable.Select<T, T>(items, delegate(T i)
			{
				action.Invoke(i);
				return i;
			});
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001CD10 File Offset: 0x0001AF10
		public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> items)
		{
			if (items == null)
			{
				return Enumerable.Empty<T>();
			}
			return items;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001CD1C File Offset: 0x0001AF1C
		public static IEnumerable<T> NullToEmpty<T>(this IList<T> items)
		{
			if (items == null)
			{
				return Enumerable.Empty<T>();
			}
			return items;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001CD28 File Offset: 0x0001AF28
		public static IEnumerable<T> Interleave<T>(this IEnumerable<T> items, params IEnumerable<T>[] interleavedItems)
		{
			return EnumerableExtensionMethods.Interleave<T>(Enumerable.ToArray<IEnumerable<T>>(Enumerable.Concat<IEnumerable<T>>(items.SingletonEnumerable<IEnumerable<T>>(), interleavedItems)));
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001CD40 File Offset: 0x0001AF40
		public static IEnumerable<T> OrderPreservingDistinct<T>(this IEnumerable<T> items)
		{
			HashSet<T> h = new HashSet<T>();
			foreach (T t in items)
			{
				if (h.Add(t))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001CD50 File Offset: 0x0001AF50
		public static IEnumerable<T> SingletonEnumerable<T>(this T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001CD60 File Offset: 0x0001AF60
		public static IEnumerable<T> AsEnumerable<T>(this ICollection items)
		{
			foreach (object obj in items)
			{
				yield return (T)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001CD70 File Offset: 0x0001AF70
		public static IEnumerable<T> AsEnumerable<T>(this IEnumerable items)
		{
			foreach (object obj in items)
			{
				yield return (T)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001CD80 File Offset: 0x0001AF80
		public static IEnumerable<T> AsEnumerable<T>(this ArraySegment<T> segment)
		{
			int num;
			for (int i = 0; i < segment.Count; i = num + 1)
			{
				yield return segment.Array[segment.Offset + i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001CD90 File Offset: 0x0001AF90
		public static IEnumerable<T> YieldEmpty<T>(this T item)
		{
			yield break;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001CD99 File Offset: 0x0001AF99
		public static IEnumerable<TResult> SelectIf<T, TResult>(this IEnumerable<T> items, ExtensionMethods.SelectIfFunc<T, TResult> f)
		{
			foreach (T t in items)
			{
				bool flag;
				TResult tresult = f(t, out flag);
				if (flag)
				{
					yield return tresult;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001CDB0 File Offset: 0x0001AFB0
		public static IEnumerable<TResult> ZipIndex<TFirst, TResult>(this IEnumerable<TFirst> first, Func<TFirst, int, TResult> resultSelector)
		{
			IEnumerable<int> enumerable = Enumerable.Select<int, int>(Enumerable.Range(0, int.MaxValue), delegate(int i)
			{
				if (i == 2147483647)
				{
					throw new OverflowException();
				}
				return i;
			});
			return first.Zip(enumerable, resultSelector);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		public static IEnumerable<ExtensionMethods.IndexedItem<TFirst>> ZipIndex<TFirst>(this IEnumerable<TFirst> first)
		{
			IEnumerable<int> enumerable = Enumerable.Select<int, int>(Enumerable.Range(0, int.MaxValue), delegate(int i)
			{
				if (i == 2147483647)
				{
					throw new OverflowException();
				}
				return i;
			});
			return first.Zip(enumerable, (TFirst item, int i) => new ExtensionMethods.IndexedItem<TFirst>
			{
				Item = item,
				Index = i
			});
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001CE5C File Offset: 0x0001B05C
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue = default(TSource))
		{
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return defaultValue;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defaultValue = default(TSource))
		{
			foreach (TSource tsource in source)
			{
				if (predicate.Invoke(tsource))
				{
					return tsource;
				}
			}
			return defaultValue;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T item)
		{
			return Enumerable.Concat<T>(items, Enumerable.Repeat<T>(item, 1));
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001CF08 File Offset: 0x0001B108
		public static int MaxOrDefault(this IEnumerable<int> items, int defaultValue = 0)
		{
			bool flag = false;
			int num = defaultValue;
			foreach (int num2 in items)
			{
				if (!flag)
				{
					num = num2;
					flag = true;
				}
				else if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001CF60 File Offset: 0x0001B160
		public static long MaxOrDefault(this IEnumerable<long> items, long defaultValue = 0L)
		{
			bool flag = false;
			long num = defaultValue;
			foreach (long num2 in items)
			{
				if (!flag)
				{
					num = num2;
					flag = true;
				}
				else if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		public static void ForAll<T>(this IEnumerable<T> items)
		{
			foreach (T t in items)
			{
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (T t in items)
			{
				action.Invoke(t);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001D044 File Offset: 0x0001B244
		public static void ForEach<T>(this IEnumerable<T> items)
		{
			foreach (T t in items)
			{
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001D088 File Offset: 0x0001B288
		public static void ForEach<T>(this ArraySegment<T> segment, Action<T> action)
		{
			for (int i = 0; i < segment.Count; i++)
			{
				action.Invoke(segment.Array[segment.Offset + i]);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001D0C4 File Offset: 0x0001B2C4
		public static void ForEachWithIndex<T>(this IEnumerable<T> items, Action<T, int> action)
		{
			int num = 0;
			foreach (T t in items)
			{
				action.Invoke(t, num++);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001D114 File Offset: 0x0001B314
		public static IEnumerable<TResult> ZipIndex<T, TResult>(this IList<T> items, Func<T, int, TResult> action)
		{
			return items.Zip(Enumerable.Range(0, items.Count), (T item, int idx) => action.Invoke(item, idx));
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001D14C File Offset: 0x0001B34C
		public static IEnumerable<TResult> ZipIndex<T, TResult>(this IEnumerable<T> items, int itemCount, Func<T, int, TResult> action)
		{
			return items.Zip(Enumerable.Range(0, itemCount), (T item, int idx) => action.Invoke(item, idx));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001D17F File Offset: 0x0001B37F
		public static bool IsNullOrEmpty<T>(this T[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001D18B File Offset: 0x0001B38B
		public static bool IsNullOrEmpty<T>(this IList<T> list)
		{
			return list == null || list.Count == 0;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001D19B File Offset: 0x0001B39B
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
		{
			return list == null || !list.GetEnumerator().MoveNext();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		public static T[] SubArray<T>(this T[] data, int index, int length)
		{
			T[] array = new T[length];
			Array.Copy(data, index, array, 0, length);
			return array;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001D1CF File Offset: 0x0001B3CF
		public static IEnumerable<T> Distinct<T, U>(this IEnumerable<T> items, Func<T, U> keySelector)
		{
			return Enumerable.Distinct<T>(items, new EqualityComparerShim<T, U>(keySelector));
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001D1DD File Offset: 0x0001B3DD
		public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> items, Func<T, T, int> comparison)
		{
			return Enumerable.OrderBy<T, T>(items, (T x) => x, new ComparerShim<T>(comparison));
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001D20A File Offset: 0x0001B40A
		public static IEnumerable<T> OrderByDescending<T>(this IEnumerable<T> items, Func<T, T, int> comparison)
		{
			return Enumerable.OrderByDescending<T, T>(items, (T x) => x, new ComparerShim<T>(comparison));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001D238 File Offset: 0x0001B438
		public static IEnumerable<T> OrderByTop<T, TKey>(this IEnumerable<T> items, Func<T, TKey> selector, int top)
		{
			return items.OrderByTop((T x, T y) => Comparer<TKey>.Default.Compare(selector.Invoke(x), selector.Invoke(y)), top);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001D265 File Offset: 0x0001B465
		public static IEnumerable<T> OrderByTop<T>(this IEnumerable<T> items, int top)
		{
			return items.OrderByTop((T x) => x, top);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001D290 File Offset: 0x0001B490
		public static IEnumerable<T> OrderByDescendingTop<T, TKey>(this IEnumerable<T> items, Func<T, TKey> selector, int top)
		{
			return items.OrderByTop((T x, T y) => -Comparer<TKey>.Default.Compare(selector.Invoke(x), selector.Invoke(y)), top);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001D2BD File Offset: 0x0001B4BD
		public static IEnumerable<T> OrderByDescendingTop<T>(this IEnumerable<T> items, int top)
		{
			return items.OrderByDescendingTop((T x) => x, top);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001D2E5 File Offset: 0x0001B4E5
		public static IEnumerable<T> OrderByTop<T>(this IEnumerable<T> items, Comparison<T> comparison, int top)
		{
			if (top > 0)
			{
				Heap<T> h = new Heap<T>(comparison);
				foreach (T t in items)
				{
					if (h.Count < top)
					{
						h.Add(t);
					}
					else if (comparison.Invoke(t, h.Top) < 0)
					{
						h.Pop();
						h.Add(t);
					}
				}
				h.Comparison = (T x, T y) => -comparison.Invoke(x, y);
				while (h.Count > 0)
				{
					yield return h.Pop();
				}
				h = null;
			}
			yield break;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001D304 File Offset: 0x0001B504
		public static string ToCsv<T>(this IEnumerable<T> items, string delimiter = ", ")
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in items)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(t);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001D370 File Offset: 0x0001B570
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
		{
			HashSet<T> hashSet = new HashSet<T>();
			items.ForEach(delegate(T i)
			{
				hashSet.Add(i);
			});
			return hashSet;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer)
		{
			HashSet<T> hashSet = new HashSet<T>(comparer);
			items.ForEach(delegate(T i)
			{
				hashSet.Add(i);
			});
			return hashSet;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		static ExtensionMethods()
		{
			ExtensionMethods.RunTests();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001D430 File Offset: 0x0001B630
		private static void RunTests()
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001D432 File Offset: 0x0001B632
		public static bool IsDefault<T>(this T obj)
		{
			return object.Equals(obj, Default<T>.Value);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001D44C File Offset: 0x0001B64C
		public static object PropertyValue<T>(this T obj, string propertyName)
		{
			return Enumerable.First<PropertyInfo>(Enumerable.Where<PropertyInfo>(typeof(T).GetProperties(20), (PropertyInfo p) => p.GetIndexParameters().Length == 0 && string.Equals(p.Name, propertyName))).GetValue(obj, null);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001D499 File Offset: 0x0001B699
		public static object GetPropertyValue<T>(this T obj, string propertyName)
		{
			return typeof(T).GetProperty(propertyName).GetValue(obj, null);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		public static string FormatPropertyValue<T>(this T obj, int maxLength = 1024)
		{
			if (obj == null)
			{
				return string.Empty;
			}
			if (obj.GetType().IsArray)
			{
				string text = string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<object, string>(Enumerable.Cast<object>(obj as IEnumerable), delegate(object e)
				{
					if (e != null)
					{
						return e.ToString();
					}
					return "null";
				})));
				if (text.Length > maxLength)
				{
					text = text.Substring(0, maxLength);
					text += " ...";
				}
				return text;
			}
			if (obj is double || obj is float)
			{
				return string.Format("{0:R}", obj);
			}
			return obj.ToString();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001D584 File Offset: 0x0001B784
		public static string ToPropertyNameValuesString<T>(this T obj, bool orderByDispId = false, bool filterDefaults = false, string keyValDelim = "=", string keyValPairDelim = "\n")
		{
			StringBuilder sb = new StringBuilder();
			obj.PropertyNameValues(orderByDispId, filterDefaults).ForEach(delegate(Tuple<string, object> t)
			{
				sb.AppendFormat("{0}{2}{1}{3}", new object[]
				{
					t.Item1,
					t.Item2.FormatPropertyValue(1024),
					keyValDelim,
					keyValPairDelim
				});
			});
			return sb.ToString();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		public static string ToFieldNameValuesString<T>(this T obj, string keyValDelim = "=", string keyValPairDelim = "\n")
		{
			StringBuilder sb = new StringBuilder();
			obj.FieldNameValues<T>().ForEach(delegate(Tuple<string, object> t)
			{
				sb.AppendFormat("{0}{2}{1}{3}", new object[]
				{
					t.Item1,
					t.Item2.FormatPropertyValue(1024),
					keyValDelim,
					keyValPairDelim
				});
			});
			return sb.ToString();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001D628 File Offset: 0x0001B828
		public static bool EqualsAny(this object obj, params object[] comparands)
		{
			foreach (object obj2 in comparands)
			{
				if (obj == obj2 || object.Equals(obj, obj2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001D65C File Offset: 0x0001B85C
		public static IEnumerable<PropertyInfo> PropertyInfo<T>(this T obj, bool orderByDispId = false)
		{
			IEnumerable<PropertyInfo> enumerable = Enumerable.Where<PropertyInfo>(Enumerable.Where<PropertyInfo>(typeof(T).GetProperties(20), (PropertyInfo p) => p.GetGetMethod() != null), (PropertyInfo p) => p.GetIndexParameters().Length == 0);
			if (orderByDispId)
			{
				enumerable = Enumerable.OrderBy<PropertyInfo, int>(enumerable, delegate(PropertyInfo p)
				{
					object[] customAttributes = p.GetCustomAttributes(typeof(DispIdAttribute), false);
					if (customAttributes.Length == 0)
					{
						return -1;
					}
					return (customAttributes[0] as DispIdAttribute).Value;
				});
			}
			return enumerable;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public static IEnumerable<object> PropertyValues<T>(this T obj, bool orderByDispId = false, bool filterDefaults = false)
		{
			IEnumerable<object> enumerable = Enumerable.Select<PropertyInfo, object>(Enumerable.Where<PropertyInfo>(Enumerable.Where<PropertyInfo>(obj.PropertyInfo(orderByDispId), (PropertyInfo p) => p.GetGetMethod() != null), (PropertyInfo p) => p.GetIndexParameters().Length == 0), (PropertyInfo p) => p.GetValue(obj, null));
			if (filterDefaults)
			{
				enumerable = Enumerable.Where<object>(enumerable, (object v) => !v.EqualsAny(ExtensionMethods.DefaultFilteredValues));
			}
			return enumerable;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001D79C File Offset: 0x0001B99C
		public static IEnumerable<Tuple<string, object>> PropertyNameValues<T>(this T obj, bool orderByDispId = false, bool filterDefaults = false)
		{
			if (obj == null)
			{
				return Enumerable.Empty<Tuple<string, object>>();
			}
			IEnumerable<PropertyInfo> enumerable = Enumerable.Where<PropertyInfo>(Enumerable.Where<PropertyInfo>(typeof(T).GetProperties(20), (PropertyInfo p) => p.GetGetMethod() != null), (PropertyInfo p) => p.GetIndexParameters().Length == 0);
			if (orderByDispId)
			{
				enumerable = Enumerable.OrderBy<PropertyInfo, int>(enumerable, delegate(PropertyInfo p)
				{
					object[] customAttributes = p.GetCustomAttributes(typeof(DispIdAttribute), false);
					if (customAttributes.Length == 0)
					{
						return -1;
					}
					return (customAttributes[0] as DispIdAttribute).Value;
				});
			}
			IEnumerable<Tuple<string, object>> enumerable2 = Enumerable.Select<PropertyInfo, Tuple<string, object>>(enumerable, (PropertyInfo p) => new Tuple<string, object>(p.Name, p.GetValue(obj, null)));
			if (filterDefaults)
			{
				enumerable2 = Enumerable.Where<Tuple<string, object>>(enumerable2, (Tuple<string, object> pn) => !pn.Item2.EqualsAny(new object[]
				{
					default(object),
					0,
					-1,
					0.0,
					"",
					DBNull.Value
				}));
			}
			return enumerable2;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001D88C File Offset: 0x0001BA8C
		public static IEnumerable<Tuple<string, object>> FieldNameValues<T>(this T obj)
		{
			return Enumerable.Select<FieldInfo, Tuple<string, object>>(typeof(T).GetFields(20), (FieldInfo p) => new Tuple<string, object>(p.Name, p.GetValue(obj)));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001D8C8 File Offset: 0x0001BAC8
		public static string ToFieldNameValuesString<T>(this T obj)
		{
			return Enumerable.Select<Tuple<string, object>, string>(obj.FieldNameValues<T>(), (Tuple<string, object> t) => string.Format("{0}: {1}\n", t.Item1, t.Item2)).Aggregate();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		public static IEnumerable<Tuple<string, object>> FieldNameValues(this object obj)
		{
			return Enumerable.Select<FieldInfo, Tuple<string, object>>(obj.GetType().GetFields(20), (FieldInfo p) => new Tuple<string, object>(p.Name, p.GetValue(obj)));
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001D939 File Offset: 0x0001BB39
		public static string ToFieldNameValuesString(this object obj)
		{
			return Enumerable.Select<Tuple<string, object>, string>(obj.FieldNameValues(), (Tuple<string, object> t) => string.Format("{0}: {1}\n", t.Item1, t.Item2)).Aggregate();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001D96A File Offset: 0x0001BB6A
		public static IEnumerator<T> GetEnumerator<T>(this IArray<T> array)
		{
			return new ExtensionMethods.ArrayEnumerator<T>
			{
				m_array = array
			};
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001D978 File Offset: 0x0001BB78
		public static IEnumerable<T> AsSafeEnumerable<T>(this ArraySegment<T> array)
		{
			return new ArraySegment32<T>(array);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001D988 File Offset: 0x0001BB88
		public static IEnumerator<T> GetEnumerator<T>(this ArraySegment<T> array)
		{
			return new ArraySegment32<T>(array).GetEnumerator();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		public static void Write(this ArraySegment<int> segment, BinaryWriter w)
		{
			w.Write(segment.Count);
			for (int i = 0; i < segment.Count; i++)
			{
				w.Write(segment.Array[segment.Offset + i]);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		public static ArraySegment<int> Read(this ArraySegment<int> segment, BinaryReader r, ISegmentAllocator<int> allocator)
		{
			segment = allocator.New(r.ReadInt32());
			for (int i = 0; i < segment.Count; i++)
			{
				segment.Array[segment.Offset + i] = r.ReadInt32();
			}
			return segment;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001DA2D File Offset: 0x0001BC2D
		public static void Write(this ArraySegment<byte> segment, BinaryWriter w)
		{
			w.Write(segment.Count);
			w.Write(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001DA57 File Offset: 0x0001BC57
		public static ArraySegment<byte> Read(this ArraySegment<byte> segment, BinaryReader r, ISegmentAllocator<byte> allocator)
		{
			segment = allocator.New(r.ReadInt32());
			r.Read(segment.Array, segment.Offset, segment.Count);
			return segment;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001DA84 File Offset: 0x0001BC84
		public static ArraySegment<T> Clone<T>(this ArraySegment<T> segment, ISegmentAllocator<T> allocator)
		{
			ArraySegment<T> arraySegment = allocator.New(segment.Count);
			if (segment.Count > 0)
			{
				Array.ConstrainedCopy(segment.Array, segment.Offset, arraySegment.Array, arraySegment.Offset, segment.Count);
			}
			return arraySegment;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		public static T[] ToArray<T>(this ArraySegment<T> segment)
		{
			T[] array;
			if (segment.Count > 0)
			{
				array = new T[segment.Count];
				Array.ConstrainedCopy(segment.Array, segment.Offset, array, 0, segment.Count);
			}
			else
			{
				array = new T[0];
			}
			return array;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001DB1E File Offset: 0x0001BD1E
		public static string ToStringEx(this ArraySegment<char> segment)
		{
			if (segment.Array != null && segment.Count != 0)
			{
				return new string(segment.Array, segment.Offset, segment.Count);
			}
			return string.Empty;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001DB52 File Offset: 0x0001BD52
		public static bool IsEmpty<T>(this ArraySegment<T> segment)
		{
			return segment.Array == null || segment.Count == 0;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public static bool DeepEquals<T>(this ArraySegment<T> segment1, ArraySegment<T> segment2)
		{
			if (segment1.Count == segment2.Count)
			{
				for (int i = 0; i < segment1.Count; i++)
				{
					if (!segment1.Array[segment1.Offset + i].Equals(segment2.Array[segment2.Offset + i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		public static string GetNamedItemOrDefault(this XmlAttributeCollection c, string attributeName, string defaultValue = null)
		{
			XmlNode namedItem = c.GetNamedItem(attributeName);
			if (namedItem == null)
			{
				return defaultValue;
			}
			return namedItem.Value;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001DC00 File Offset: 0x0001BE00
		public static T ReadXmlObject<T>(this XmlNode n) where T : IXmlSerializable, new()
		{
			if (n != null)
			{
				XmlNodeReader xmlNodeReader = new XmlNodeReader(n);
				T t = new T();
				t.ReadXml(xmlNodeReader);
				return t;
			}
			return default(T);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001DC36 File Offset: 0x0001BE36
		public static T ReadXmlObject<T>(this XmlNode n, string xpath, XmlNamespaceManager nsmgr) where T : IXmlSerializable, new()
		{
			return n.SelectSingleNode(xpath, nsmgr).ReadXmlObject<T>();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001DC45 File Offset: 0x0001BE45
		public static List<T> ReadXmlObjects<T>(this XmlNode n, string collectionTagName) where T : IXmlSerializable, new()
		{
			return CollectionSerialization.ReadXmlObjects<T>(new XmlNodeReader(n), collectionTagName, typeof(T).Name);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001DC62 File Offset: 0x0001BE62
		public static List<T> ReadXmlObjects<T>(this XmlNode n, string collectionTagName, string elementTagName) where T : IXmlSerializable, new()
		{
			return CollectionSerialization.ReadXmlObjects<T>(new XmlNodeReader(n), collectionTagName, elementTagName);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001DC74 File Offset: 0x0001BE74
		public static List<T> ReadXmlObjects<T>(this XmlNodeList nodes, string collectionTagName, string elementTagName) where T : IXmlSerializable, new()
		{
			List<T> list = new List<T>();
			if (nodes != null)
			{
				foreach (object obj in nodes)
				{
					CollectionSerialization.ReadXml<T>(new XmlNodeReader((XmlNode)obj), collectionTagName, elementTagName, list);
				}
			}
			return list;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001DCD8 File Offset: 0x0001BED8
		public static List<T> ReadXmlObjects<T>(this XmlNodeList nodes) where T : IXmlSerializable, new()
		{
			List<T> list = new List<T>();
			if (nodes != null)
			{
				foreach (object obj in nodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					T t = new T();
					t.ReadXml(new XmlNodeReader(xmlNode));
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x040000DD RID: 221
		private static object[] DefaultFilteredValues = new object[]
		{
			default(object),
			0,
			-1,
			0.0,
			"",
			DBNull.Value
		};

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x06000938 RID: 2360
		public delegate TResult SelectIfFunc<in T1, out TResult>(T1 arg1, out bool selectIf);

		// Token: 0x020000FC RID: 252
		public struct IndexedItem<T>
		{
			// Token: 0x04000279 RID: 633
			public T Item;

			// Token: 0x0400027A RID: 634
			public int Index;
		}

		// Token: 0x020000FD RID: 253
		internal class ArrayEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x1700017F RID: 383
			// (get) Token: 0x0600093B RID: 2363 RVA: 0x0002D1D8 File Offset: 0x0002B3D8
			T IEnumerator<T>.Current
			{
				get
				{
					return this.m_array[this.m_idx];
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002D1EB File Offset: 0x0002B3EB
			object IEnumerator.Current
			{
				get
				{
					return this.m_array[this.m_idx];
				}
			}

			// Token: 0x0600093D RID: 2365 RVA: 0x0002D203 File Offset: 0x0002B403
			bool IEnumerator.MoveNext()
			{
				if (this.m_idx < this.m_array.Length)
				{
					this.m_idx++;
					return true;
				}
				return false;
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x0002D229 File Offset: 0x0002B429
			void IEnumerator.Reset()
			{
				this.m_idx = -1;
			}

			// Token: 0x0600093F RID: 2367 RVA: 0x0002D232 File Offset: 0x0002B432
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400027B RID: 635
			public IArray<T> m_array;

			// Token: 0x0400027C RID: 636
			private int m_idx = -1;
		}
	}
}
