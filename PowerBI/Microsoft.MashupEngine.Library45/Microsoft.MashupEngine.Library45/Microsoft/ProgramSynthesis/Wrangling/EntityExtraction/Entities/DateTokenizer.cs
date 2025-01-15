using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001CA RID: 458
	public class DateTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0001DB48 File Offset: 0x0001BD48
		[JsonConstructor]
		public DateTokenizer()
		{
			OverlapStrategy overlapStrategy = OverlapStrategy.Subsumption;
			RegexBasedTokenizer.TokenFactoryDelegate tokenFactoryDelegate;
			if ((tokenFactoryDelegate = DateTokenizer.<>O.<0>__ProcessMatch) == null)
			{
				tokenFactoryDelegate = (DateTokenizer.<>O.<0>__ProcessMatch = new RegexBasedTokenizer.TokenFactoryDelegate(DateTokenizer.ProcessMatch));
			}
			base..ctor(overlapStrategy, tokenFactoryDelegate, new TokenPattern[]
			{
				new TokenPattern(DateTokenizer.SeparatedNumericDatePattern, "(?:^|[^\\d\\.\\-])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(DateTokenizer.NonSeparatedNumericDatePattern1, "(?:^|[^\\d\\.\\-])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(DateTokenizer.NonSeparatedNumericDatePattern2, "(?:^|[^\\d\\.\\-])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(DateTokenizer.MonthFirstDatePattern, "(?:^|[^\\d\\.\\-\\p{L}])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(DateTokenizer.MonthSecondDatePattern, "(?:^|[^\\d\\.\\-])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(DateTokenizer.YearFirstDatePattern, "(?:^|[^\\d\\.\\-])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(DateTokenizer.YearFirstNamedMonthPattern, "(?:^|[^\\d\\.\\-])", "(?:$|[^\\d\\.\\-])", false, false, Array.Empty<RegexOptions>())
			});
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001DC4F File Offset: 0x0001BE4F
		private static IEnumerable<DateToken> ResolveFromComponents(int[] components, TokenPatternMatch m)
		{
			List<int> list = Enumerable.Range(0, components.Length).ToList<int>();
			IReadOnlyList<int> readOnlyList = list.Where((int idx) => components[idx] > 0 && components[idx] <= 31).ToList<int>();
			IReadOnlyList<int> readOnlyList2 = list.Where((int idx) => components[idx] > 0 && components[idx] <= 12).ToList<int>();
			IReadOnlyList<int> readOnlyList3 = list.Where((int idx) => components[idx] > 0).ToList<int>();
			IEnumerable<IReadOnlyList<int>> enumerable = from t in new IReadOnlyList<int>[] { readOnlyList3, readOnlyList2, readOnlyList }.CartesianProduct<int>()
				select t.ToList<int>() into t
				where !t.HasRepeats<int>() && CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(components[t[0]], components[t[1]]) >= components[t[2]]
				select t;
			foreach (IReadOnlyList<int> readOnlyList4 in enumerable)
			{
				int day = components[readOnlyList4[2]];
				int month = components[readOnlyList4[1]];
				int num = components[readOnlyList4[0]];
				IEnumerable<int> enumerable2 = new int[] { num };
				if (num < 100)
				{
					enumerable2 = enumerable2.Concat(new int[]
					{
						num + 1900,
						num + 2000
					});
				}
				using (IEnumerator<int> enumerator2 = enumerable2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DateTime dateTime;
						if (DateTokenizer.TryCreateDateTime(enumerator2.Current, month, day, out dateTime))
						{
							yield return new DateToken(m.Source, m.Start, m.End, dateTime);
						}
					}
				}
				IEnumerator<int> enumerator2 = null;
			}
			IEnumerator<IReadOnlyList<int>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001DC68 File Offset: 0x0001BE68
		private static bool TryCreateDateTime(int year, int month, int day, out DateTime result)
		{
			bool flag;
			try
			{
				result = new DateTime(year, month, day);
				flag = true;
			}
			catch (ArgumentOutOfRangeException)
			{
				result = DateTime.MinValue;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001DCA8 File Offset: 0x0001BEA8
		private static IEnumerable<DateToken> ResolveFromComponents(string[] components, TokenPatternMatch m)
		{
			List<int> list = Enumerable.Range(0, components.Length).ToList<int>();
			int v;
			List<int> list2 = list.Where((int idx) => !int.TryParse(components[idx], out v)).ToList<int>();
			List<int> list3 = list.Where((int idx) => int.TryParse(components[idx], out v) && v > 0 && v <= 31).ToList<int>();
			List<int> list4 = list.Where((int idx) => int.TryParse(components[idx], out v) && v > 0).ToList<int>();
			IEnumerable<List<int>> enumerable = from t in new List<int>[] { list4, list2, list3 }.CartesianProduct<int>()
				select t.ToList<int>() into t
				where t.Distinct<int>().Count<int>() == t.Count
				select t;
			Func<int, string> <>9__5;
			foreach (List<int> list5 in enumerable)
			{
				string text = "-";
				IEnumerable<int> enumerable2 = list5;
				Func<int, string> func;
				if ((func = <>9__5) == null)
				{
					func = (<>9__5 = (int idx) => components[idx]);
				}
				DateTime dateTime;
				if (DateTime.TryParse(string.Join(text, enumerable2.Select(func)), out dateTime))
				{
					yield return new DateToken(m.Source, m.Start, m.End, dateTime);
				}
			}
			IEnumerator<List<int>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001DCBF File Offset: 0x0001BEBF
		private static IEnumerable<DateToken> ProcessMatch(TokenPatternMatch m)
		{
			if (m.FullMatch.Groups["Month"].Success)
			{
				string[] array = new string[]
				{
					m.FullMatch.Groups["First"].Value,
					m.FullMatch.Groups["Month"].Value,
					m.FullMatch.Groups["Second"].Value
				};
				foreach (DateToken dateToken in DateTokenizer.ResolveFromComponents(array, m))
				{
					yield return dateToken;
				}
				IEnumerator<DateToken> enumerator = null;
			}
			else
			{
				int[] array2 = (from name in Seq.Of<string>(new string[] { "First", "Second", "Third" })
					where m.FullMatch.Groups[name].Success
					select int.Parse(m.FullMatch.Groups[name].Value)).ToArray<int>();
				foreach (DateToken dateToken2 in DateTokenizer.ResolveFromComponents(array2, m))
				{
					yield return dateToken2;
				}
				IEnumerator<DateToken> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x040004DE RID: 1246
		private const string LeftContext = "(?:^|[^\\d\\.\\-])";

		// Token: 0x040004DF RID: 1247
		private const string MonthFirstLeftContext = "(?:^|[^\\d\\.\\-\\p{L}])";

		// Token: 0x040004E0 RID: 1248
		private const string OneOrTwoDigits = "\\d{1,2}";

		// Token: 0x040004E1 RID: 1249
		private const string TwoDigits = "\\d{2}";

		// Token: 0x040004E2 RID: 1250
		private const string Separator = "\\s*[\\.\\-\\\\/\\s]\\s*";

		// Token: 0x040004E3 RID: 1251
		private const string TwoOrFourDigits = "\\d{2,4}";

		// Token: 0x040004E4 RID: 1252
		private const string FourDigits = "\\d{4}";

		// Token: 0x040004E5 RID: 1253
		private const string RightContext = "(?:$|[^\\d\\.\\-])";

		// Token: 0x040004E6 RID: 1254
		private const string FirstNumericGroupName = "First";

		// Token: 0x040004E7 RID: 1255
		private const string SecondNumericGroupName = "Second";

		// Token: 0x040004E8 RID: 1256
		private const string ThirdNumericGroupName = "Third";

		// Token: 0x040004E9 RID: 1257
		private const string MonthGroupName = "Month";

		// Token: 0x040004EA RID: 1258
		private static readonly string SeparatedNumericDatePattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}(?<{3}>{4}){5}'?(?<{6}>{7})", new object[] { "First", "\\d{1,2}", "\\s*[\\.\\-\\\\/\\s]\\s*", "Second", "\\d{1,2}", "\\s*[\\.\\-\\\\/\\s]\\s*", "Third", "\\d{2,4}" }));

		// Token: 0x040004EB RID: 1259
		private static readonly string NonSeparatedNumericDatePattern1 = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1})(?<{2}>{3})(?<{4}>{5})", new object[] { "First", "\\d{2}", "Second", "\\d{2}", "Third", "\\d{4}" }));

		// Token: 0x040004EC RID: 1260
		private static readonly string NonSeparatedNumericDatePattern2 = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1})(?<{2}>{3})(?<{4}>{5})", new object[] { "First", "\\d{4}", "Second", "\\d{2}", "Third", "\\d{2}" }));

		// Token: 0x040004ED RID: 1261
		private static readonly string MonthFirstDatePattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>\\p{{L}}{{3,20}})\\s*,?[\\.\\-\\\\/\\s]\\s*(?<{1}>{2})\\s*[\\.\\-\\\\/\\s]?,?\\s*'?(?<{3}>{4})", new object[] { "Month", "First", "\\d{1,2}", "Second", "\\d{2,4}" }));

		// Token: 0x040004EE RID: 1262
		private static readonly string MonthSecondDatePattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}(?<{3}>\\p{{L}}{{3,20}}){4}'?(?<{5}>{6})", new object[] { "First", "\\d{1,2}", "\\s*[\\.\\-\\\\/\\s]\\s*", "Month", "\\s*[\\.\\-\\\\/\\s]\\s*", "Second", "\\d{2,4}" }));

		// Token: 0x040004EF RID: 1263
		private static readonly string YearFirstDatePattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}(?<{3}>{4}){5}(?<{6}>{7})", new object[] { "First", "\\d{2,4}", "\\s*[\\.\\-\\\\/\\s]\\s*", "Second", "\\d{1,2}", "\\s*[\\.\\-\\\\/\\s]\\s*", "Third", "\\d{1,2}" }));

		// Token: 0x040004F0 RID: 1264
		private static readonly string YearFirstNamedMonthPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}(?<{3}>\\p{{L}}{{3,20}}){4}(?<{5}>{6})", new object[] { "First", "\\d{2,4}", "\\s*[\\.\\-\\\\/\\s]\\s*", "Month", "\\s*[\\.\\-\\\\/\\s]\\s*", "Second", "\\d{1,2}" }));

		// Token: 0x020001CB RID: 459
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004F1 RID: 1265
			public static RegexBasedTokenizer.TokenFactoryDelegate <0>__ProcessMatch;
		}
	}
}
