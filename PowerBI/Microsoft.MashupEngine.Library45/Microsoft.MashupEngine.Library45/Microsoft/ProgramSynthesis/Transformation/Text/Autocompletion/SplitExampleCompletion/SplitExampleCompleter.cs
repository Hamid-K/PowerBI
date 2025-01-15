using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.SplitExampleCompletion
{
	// Token: 0x02001E0C RID: 7692
	public static class SplitExampleCompleter
	{
		// Token: 0x06010158 RID: 65880 RVA: 0x00373CF5 File Offset: 0x00371EF5
		private static IEnumerable<string> GetUncoveredRegions(string fullString, IEnumerable<string> covered)
		{
			List<KeyValuePair<string, int>> list = (from s in covered
				where !string.IsNullOrWhiteSpace(s)
				select new KeyValuePair<string, int>(s, fullString.IndexOf(s, StringComparison.Ordinal)) into kvp
				where kvp.Value != -1
				orderby kvp.Value
				select kvp).ToList<KeyValuePair<string, int>>();
			int num = 0;
			foreach (KeyValuePair<string, int> kvp2 in list)
			{
				string text = fullString.Substring(num, kvp2.Value - num);
				if (!string.IsNullOrWhiteSpace(text))
				{
					yield return text;
				}
				num = kvp2.Value + kvp2.Key.Length;
				kvp2 = default(KeyValuePair<string, int>);
			}
			List<KeyValuePair<string, int>>.Enumerator enumerator = default(List<KeyValuePair<string, int>>.Enumerator);
			string text2 = fullString.Substring(num, fullString.Length - num);
			if (!string.IsNullOrEmpty(text2))
			{
				yield return text2;
			}
			yield break;
			yield break;
		}

		// Token: 0x06010159 RID: 65881 RVA: 0x00373D0C File Offset: 0x00371F0C
		private static double NormalizedCharCategoryBasedEntropy(string s)
		{
			return (from c in s
				group c by c.GetUnicodeCategory()).Aggregate(0.0, delegate(double entropy, IGrouping<UnicodeCategory, char> g)
			{
				double num = (double)g.Count<char>() / (double)s.Length;
				return -num * Math.Log(num) + entropy;
			}) / (double)s.Length;
		}

		// Token: 0x0601015A RID: 65882 RVA: 0x00373D78 File Offset: 0x00371F78
		private static List<string> CleanupResults(IReadOnlyList<string> results, int minLength)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in results)
			{
				hashSet.Add(text.Trim());
				foreach (Match match in SplitExampleCompleter.SplitPointRegex.NonCachingMatches(text))
				{
					string text2 = text.Substring(0, match.Index);
					if (!string.IsNullOrWhiteSpace(text2) && text2.Length >= minLength)
					{
						hashSet.Add(text2.Trim());
					}
				}
			}
			IOrderedEnumerable<string> orderedEnumerable = hashSet.OrderBy((string s) => s.Length);
			Func<string, double> func;
			if ((func = SplitExampleCompleter.<>O.<0>__NormalizedCharCategoryBasedEntropy) == null)
			{
				func = (SplitExampleCompleter.<>O.<0>__NormalizedCharCategoryBasedEntropy = new Func<string, double>(SplitExampleCompleter.NormalizedCharCategoryBasedEntropy));
			}
			return orderedEnumerable.ThenBy(func).ThenBy((string s) => s).ToList<string>();
		}

		// Token: 0x0601015B RID: 65883 RVA: 0x00373EA8 File Offset: 0x003720A8
		public static IReadOnlyList<string> Suggest(string fullColumnValue, IReadOnlyList<string> allExamples, int selectedColumnId, int? positionInSelectedColumn = null)
		{
			fullColumnValue = fullColumnValue.Normalize();
			allExamples = allExamples.Select((string s) => s.Normalize()).ToList<string>();
			if (string.IsNullOrWhiteSpace(fullColumnValue))
			{
				return new string[0];
			}
			string text = ((positionInSelectedColumn != null) ? allExamples[selectedColumnId].Substring(0, positionInSelectedColumn.Value) : allExamples[selectedColumnId]);
			IEnumerable<string> enumerable = allExamples.Where((string _, int idx) => idx != selectedColumnId);
			return SplitExampleCompleter.CleanupResults((from v in new StringSuffixTree(SplitExampleCompleter.GetUncoveredRegions(fullColumnValue, enumerable)).LookupPrefixIgnoreCase(text, 0.0, 1.0)
				select v.Value).ToList<string>(), text.Length);
		}

		// Token: 0x040060E7 RID: 24807
		private static readonly Regex SplitPointRegex = new Regex("[^\\p{L}\\s\\d]+", RegexOptions.Compiled);

		// Token: 0x02001E0D RID: 7693
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040060E8 RID: 24808
			public static Func<string, double> <0>__NormalizedCharCategoryBasedEntropy;
		}
	}
}
