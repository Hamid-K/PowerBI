using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001789 RID: 6025
	public static class LinqExtension
	{
		// Token: 0x0600C797 RID: 51095 RVA: 0x002ADC37 File Offset: 0x002ABE37
		public static void AddDistinct<T>(this IList<T> list, T value)
		{
			if (!list.Contains(value))
			{
				list.Add(value);
			}
		}

		// Token: 0x0600C798 RID: 51096 RVA: 0x002ADC4C File Offset: 0x002ABE4C
		public static bool Any<T>(this IEnumerable<T> items, T item)
		{
			return items.Any((T i) => i.Equals(item));
		}

		// Token: 0x0600C799 RID: 51097 RVA: 0x002ADC78 File Offset: 0x002ABE78
		public static void ForEach<TSource>(this IEnumerable<TSource> subject, Action<TSource> onNext)
		{
			if (subject == null)
			{
				throw new ArgumentNullException("subject");
			}
			if (onNext == null)
			{
				throw new ArgumentNullException("onNext");
			}
			foreach (TSource tsource in subject)
			{
				onNext(tsource);
			}
		}

		// Token: 0x0600C79A RID: 51098 RVA: 0x002ADCDC File Offset: 0x002ABEDC
		public static IEnumerable<T> Head<T>(this IEnumerable<T> items, int size = 10)
		{
			return items.Take(size);
		}

		// Token: 0x0600C79B RID: 51099 RVA: 0x002ADCE5 File Offset: 0x002ABEE5
		public static bool None<T>(this IEnumerable<T> items, T item)
		{
			return !items.Any(item);
		}

		// Token: 0x0600C79C RID: 51100 RVA: 0x002ADCF1 File Offset: 0x002ABEF1
		public static bool None<T>(this IEnumerable<T> items)
		{
			return !items.Any<T>();
		}

		// Token: 0x0600C79D RID: 51101 RVA: 0x002ADCFC File Offset: 0x002ABEFC
		public static bool None<T>(this IEnumerable<T> items, Func<T, bool> predicate)
		{
			return !items.Any(predicate);
		}

		// Token: 0x0600C79E RID: 51102 RVA: 0x002ADD08 File Offset: 0x002ABF08
		public static IEnumerable<string> NotNullOrEmpty(this IEnumerable<string> subject)
		{
			return subject.Where((string s) => !string.IsNullOrEmpty(s));
		}

		// Token: 0x0600C79F RID: 51103 RVA: 0x002ADD2F File Offset: 0x002ABF2F
		public static IEnumerable<T> Page<T>(this IEnumerable<T> items, int page, int pageSize)
		{
			return items.Skip(pageSize * page).Take(pageSize);
		}

		// Token: 0x0600C7A0 RID: 51104 RVA: 0x002ADD40 File Offset: 0x002ABF40
		public static int PageCount<T>(this IEnumerable<T> items, int pageSize)
		{
			return Convert.ToInt32(Math.Ceiling((double)items.Count<T>() / Convert.ToDouble(pageSize)));
		}

		// Token: 0x0600C7A1 RID: 51105 RVA: 0x002ADD5A File Offset: 0x002ABF5A
		public static IEnumerable<TTarget> ParseJson<TTarget>(this IEnumerable<string> items)
		{
			Func<string, TTarget> func;
			if ((func = LinqExtension.<ParseJson>O__10_0<TTarget>.<0>__DeserializeObject) == null)
			{
				func = (LinqExtension.<ParseJson>O__10_0<TTarget>.<0>__DeserializeObject = new Func<string, TTarget>(JsonConvert.DeserializeObject<TTarget>));
			}
			return items.Select(func);
		}

		// Token: 0x0600C7A2 RID: 51106 RVA: 0x002ADD7D File Offset: 0x002ABF7D
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> items, int size)
		{
			items = (items as IReadOnlyCollection<T>) ?? items.ToList<T>();
			int i = 0;
			for (;;)
			{
				IEnumerable<T> enumerable = items.Skip(size * i).Take(size).ToList<T>();
				if (enumerable.FirstOrDefault<T>() == null)
				{
					break;
				}
				yield return enumerable;
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x0600C7A3 RID: 51107 RVA: 0x002ADD94 File Offset: 0x002ABF94
		public static IEnumerable<string> SerializeJson<T>(this IEnumerable<T> items, Formatting format = Formatting.None)
		{
			return items.Select((T item) => JsonConvert.SerializeObject(item, format));
		}

		// Token: 0x0600C7A4 RID: 51108 RVA: 0x002ADDC0 File Offset: 0x002ABFC0
		public static IEnumerable<T> SkipTake<T>(this IEnumerable<T> items, int skip, int take)
		{
			return items.Skip(skip).Take(take);
		}

		// Token: 0x0600C7A5 RID: 51109 RVA: 0x002ADDD0 File Offset: 0x002ABFD0
		public static IEnumerable<T> Tail<T>(this IEnumerable<T> items, int size)
		{
			IReadOnlyList<T> readOnlyList = items.ToReadOnlyList<T>();
			int count = readOnlyList.Count;
			int num = Math.Max(0, count - size);
			return readOnlyList.Skip(num);
		}

		// Token: 0x0600C7A6 RID: 51110 RVA: 0x002ADDFA File Offset: 0x002ABFFA
		public static T[] ToArrayOrEmpty<T>(this IEnumerable<T> subject)
		{
			T[] array;
			if ((array = subject as T[]) == null)
			{
				array = ((subject != null) ? subject.ToArray<T>() : null) ?? new T[0];
			}
			return array;
		}

		// Token: 0x0600C7A7 RID: 51111 RVA: 0x002ADE1C File Offset: 0x002AC01C
		public static IReadOnlyList<T> ToDistinctReadOnlyList<T>(this IEnumerable<T> subject)
		{
			return ((subject != null) ? subject.Distinct<T>() : null).ToReadOnlyList<T>();
		}

		// Token: 0x0600C7A8 RID: 51112 RVA: 0x002ADE2F File Offset: 0x002AC02F
		public static string ToJoinNewlineString(this IEnumerable<string> subject)
		{
			return string.Join(Environment.NewLine, subject);
		}

		// Token: 0x0600C7A9 RID: 51113 RVA: 0x002ADE3C File Offset: 0x002AC03C
		public static string ToJoinString(this IEnumerable<char> subject)
		{
			return string.Concat<char>(subject);
		}

		// Token: 0x0600C7AA RID: 51114 RVA: 0x002ADE44 File Offset: 0x002AC044
		public static string ToJoinString(this IEnumerable<string> subject)
		{
			return string.Concat(subject);
		}

		// Token: 0x0600C7AB RID: 51115 RVA: 0x002ADE4C File Offset: 0x002AC04C
		public static string ToJoinString(this IEnumerable<string> subject, string delimiter)
		{
			if (subject != null)
			{
				return string.Join(delimiter, subject);
			}
			return string.Empty;
		}

		// Token: 0x0600C7AC RID: 51116 RVA: 0x002ADE60 File Offset: 0x002AC060
		public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> subject)
		{
			IReadOnlyList<T> readOnlyList;
			if ((readOnlyList = subject as IReadOnlyList<T>) == null)
			{
				IReadOnlyList<T> readOnlyList2 = ((subject != null) ? subject.ToList<T>() : null);
				readOnlyList = readOnlyList2 ?? new T[0];
			}
			return readOnlyList;
		}

		// Token: 0x0600C7AD RID: 51117 RVA: 0x002ADE90 File Offset: 0x002AC090
		public static bool TryFirst<T>(this IEnumerable<T> subject, out T item)
		{
			using (IEnumerator<T> enumerator = subject.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					item = t;
					return true;
				}
			}
			item = default(T);
			return false;
		}

		// Token: 0x0600C7AE RID: 51118 RVA: 0x002ADEE8 File Offset: 0x002AC0E8
		public static bool TryFirst<T>(this IEnumerable<T> subject, Func<T, bool> predicate, out T item)
		{
			foreach (T t in subject)
			{
				if (predicate(t))
				{
					item = t;
					return true;
				}
			}
			item = default(T);
			return false;
		}

		// Token: 0x0200178D RID: 6029
		[CompilerGenerated]
		private static class <ParseJson>O__10_0<TTarget>
		{
			// Token: 0x04004E8C RID: 20108
			public static Func<string, TTarget> <0>__DeserializeObject;
		}
	}
}
