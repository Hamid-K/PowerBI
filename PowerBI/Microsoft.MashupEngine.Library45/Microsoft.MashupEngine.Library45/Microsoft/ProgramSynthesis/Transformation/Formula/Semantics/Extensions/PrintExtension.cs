using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001791 RID: 6033
	public static class PrintExtension
	{
		// Token: 0x0600C7D2 RID: 51154 RVA: 0x002AE55A File Offset: 0x002AC75A
		public static object Print(this string source, string label = null, int? labelWidth = null, int maxTotalWidth = 100)
		{
			return source.Print(label, labelWidth, maxTotalWidth);
		}

		// Token: 0x0600C7D3 RID: 51155 RVA: 0x002AE568 File Offset: 0x002AC768
		public static object Print(this object source, string label = null, int? labelWidth = null, int maxTotalWidth = 100)
		{
			string text = source.Format(label, labelWidth, false, maxTotalWidth);
			if (text.EndsWith(Environment.NewLine))
			{
				Console.Write(text);
			}
			else
			{
				Console.WriteLine(text);
			}
			return source;
		}

		// Token: 0x0600C7D4 RID: 51156 RVA: 0x002AE59C File Offset: 0x002AC79C
		public static IReadOnlyList<T> Print<T, TSelect>(this IEnumerable<T> items, Func<T, TSelect> select, Func<TSelect, bool> where = null, int limit = 100)
		{
			IReadOnlyList<T> readOnlyList = items.ToReadOnlyList<T>();
			readOnlyList.PrintHeadTail(delegate(TSelect i)
			{
				i.Print(null, null, 100);
			}, select, where, limit);
			return readOnlyList;
		}

		// Token: 0x0600C7D5 RID: 51157 RVA: 0x002AE5CD File Offset: 0x002AC7CD
		public static IReadOnlyList<T> Print<T>(this IEnumerable<T> items, Func<T, bool> where = null, int limit = 100)
		{
			return items.PrintHeadTail(delegate(T i)
			{
				i.Print(null, null, 100);
			}, where, limit);
		}

		// Token: 0x0600C7D6 RID: 51158 RVA: 0x002AE5F6 File Offset: 0x002AC7F6
		public static IReadOnlyList<T> PrintLiteral<T, TSelect>(this IEnumerable<T> items, Func<T, TSelect> select, Func<TSelect, bool> where = null, int limit = 100)
		{
			return items.PrintHeadTail(delegate(TSelect i)
			{
				i.ToCSharpPseudoLiteral().Print(null, null, 100);
			}, select, where, limit);
		}

		// Token: 0x0600C7D7 RID: 51159 RVA: 0x002AE620 File Offset: 0x002AC820
		public static IReadOnlyList<T> PrintLiteral<T>(this IEnumerable<T> items, Func<T, bool> where = null, int limit = 100)
		{
			return items.PrintHeadTail(delegate(T i)
			{
				i.ToCSharpPseudoLiteral().Print(null, null, 100);
			}, where, limit);
		}

		// Token: 0x0600C7D8 RID: 51160 RVA: 0x002AE64C File Offset: 0x002AC84C
		public static IEnumerable<T> PrintMessage<T>(this IEnumerable<T> items, string message)
		{
			message.Print(null, null, 100);
			"---".Print(null, null, 100);
			return items;
		}

		// Token: 0x0600C7D9 RID: 51161 RVA: 0x002AE684 File Offset: 0x002AC884
		public static IReadOnlyList<TSelect> PrintJson<T, TSelect>(this IEnumerable<T> items, Func<T, TSelect> select, Func<TSelect, bool> where = null, int limit = 100)
		{
			return items.Select(select).PrintJson(where, Formatting.Indented, limit);
		}

		// Token: 0x0600C7DA RID: 51162 RVA: 0x002AE6A4 File Offset: 0x002AC8A4
		public static IReadOnlyList<T> PrintJson<T>(this IEnumerable<T> items, Func<T, bool> where = null, Formatting format = Formatting.Indented, int limit = 100)
		{
			IReadOnlyList<T> readOnlyList = ((where == null) ? items.ToReadOnlyList<T>() : items.Where(where).ToReadOnlyList<T>());
			readOnlyList.Take(limit).FormatJson(null, null, format, 100).Print(null, null, 100);
			if (readOnlyList.Count > limit)
			{
				string.Format(" ... {0:N0} more ...", readOnlyList.Count - limit).Print(null, null, 100);
			}
			"---".Print(null, null, 100);
			return readOnlyList;
		}

		// Token: 0x0600C7DB RID: 51163 RVA: 0x002AE740 File Offset: 0x002AC940
		public static IReadOnlyList<TSelect> PrintJsonLine<T, TSelect>(this IEnumerable<T> items, Func<T, TSelect> select, Func<TSelect, bool> where = null, int limit = 100)
		{
			return items.Select(select).PrintJsonLine(where, limit);
		}

		// Token: 0x0600C7DC RID: 51164 RVA: 0x002AE75D File Offset: 0x002AC95D
		public static IReadOnlyList<T> PrintJsonLine<T>(this IEnumerable<T> items, Func<T, bool> where = null, int limit = 100)
		{
			return items.PrintJson(where, Formatting.None, limit);
		}

		// Token: 0x0600C7DD RID: 51165 RVA: 0x002AE768 File Offset: 0x002AC968
		public static IReadOnlyList<T> PrintCount<T>(this IEnumerable<T> items, string label = null)
		{
			IReadOnlyList<T> readOnlyList = items.ToReadOnlyList<T>();
			readOnlyList.Count.ToString("N0").Print(label, null, 100);
			"---".Print(null, null, 100);
			return readOnlyList;
		}

		// Token: 0x0600C7DE RID: 51166 RVA: 0x002AE7B8 File Offset: 0x002AC9B8
		public static IReadOnlyList<T> PrintDistinctCount<T>(this IEnumerable<T> items, string label = null)
		{
			IReadOnlyList<T> readOnlyList = items.ToReadOnlyList<T>();
			readOnlyList.Distinct<T>().Count<T>().ToString("N0")
				.Print(label, null, 100);
			"---".Print(null, null, 100);
			return readOnlyList;
		}

		// Token: 0x0600C7DF RID: 51167 RVA: 0x002AE80C File Offset: 0x002ACA0C
		private static IReadOnlyList<T> PrintHeadTail<T>(this IEnumerable<T> items, Action<T> output, Func<T, bool> where = null, int limit = 100)
		{
			IReadOnlyList<T> readOnlyList = items.ToReadOnlyList<T>();
			int num = readOnlyList.Count;
			int num2 = 0;
			if (readOnlyList.Count > limit)
			{
				double num3 = (double)limit / 2.0;
				num = Convert.ToInt32(Math.Ceiling(num3));
				num2 = Convert.ToInt32(Math.Floor(num3));
			}
			List<T> list = ((where == null) ? readOnlyList.ToList<T>() : readOnlyList.Where(where).ToList<T>());
			list.Head(num).ForEach(output);
			if (num2 > 0)
			{
				if (list.Count > limit)
				{
					string.Format(" ... {0:N0} skipped ...", list.Count - limit).Print(null, null, 100);
				}
				list.Tail(num2).ForEach(output);
			}
			"---".Print(null, null, 100);
			return readOnlyList;
		}

		// Token: 0x0600C7E0 RID: 51168 RVA: 0x002AE8D7 File Offset: 0x002ACAD7
		private static IReadOnlyList<T> PrintHeadTail<T, TSelect>(this IEnumerable<T> items, Action<TSelect> output, Func<T, TSelect> select, Func<TSelect, bool> where = null, int limit = 100)
		{
			IReadOnlyList<T> readOnlyList = items.ToReadOnlyList<T>();
			readOnlyList.Select(select).PrintHeadTail(output, where, limit);
			return readOnlyList;
		}

		// Token: 0x0600C7E1 RID: 51169 RVA: 0x002AE8F0 File Offset: 0x002ACAF0
		private static string Format(this object source, string label = null, int? labelWidth = null, bool literal = false, int maxTotalWidth = 100)
		{
			string newLine = Environment.NewLine;
			string text = (literal ? source.ToCSharpPseudoLiteral() : ((source != null) ? source.ToString() : null));
			if (text == null)
			{
				text = "<null>";
			}
			if (label == null)
			{
				return text;
			}
			if (labelWidth != null)
			{
				label = label.PadRight(labelWidth.Value);
			}
			if (!text.Contains(newLine) && Math.Max(label.Length + 1, labelWidth.GetValueOrDefault()) + text.Length < maxTotalWidth)
			{
				return label + ": " + text;
			}
			string text2 = new string(' ', 4);
			text = text.Replace(newLine, newLine + text2);
			return string.Concat(new string[] { label, ":", newLine, text2, text });
		}

		// Token: 0x0600C7E2 RID: 51170 RVA: 0x002AE9B4 File Offset: 0x002ACBB4
		private static string FormatJson(this object source, string label = null, int? labelWidth = null, Formatting format = Formatting.Indented, int maxTotalWidth = 100)
		{
			IToJson toJson = source as IToJson;
			return ((toJson != null) ? toJson.ToJson().ToString(format, Array.Empty<JsonConverter>()) : JsonConvert.SerializeObject(source, format)).Format(label, labelWidth, false, maxTotalWidth);
		}
	}
}
