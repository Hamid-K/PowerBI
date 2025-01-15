using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F61 RID: 3937
	internal abstract class GroupLearner
	{
		// Token: 0x06006D73 RID: 28019 RVA: 0x00165167 File Offset: 0x00163367
		public static IEnumerable<Record<records, TableExample>> Learn(StringRegion input, int skipLines, IReadOnlyList<List<StringRegionCell>> table, GrammarBuilders builder)
		{
			IReadOnlyList<StringRegion> lines = Semantics.Skip(skipLines, Semantics.SplitLines(input));
			skip skipNode = ((skipLines == 0) ? builder.Node.UnnamedConversion.skip_lines(builder.Node.Rule.SplitLines(builder.Node.Variable.v)) : builder.Node.Rule.Skip(builder.Node.Rule.k(skipLines), builder.Node.Rule.SplitLines(builder.Node.Variable.v)));
			List<StringRegion> records = table.Select((List<StringRegionCell> row) => input.Slice(GroupLearner.GetStartOfLine(input, GroupLearner.GetRowStart(row)), GroupLearner.GetRowEnd(row))).ToList<StringRegion>();
			GroupLearner[] array = new GroupLearner[]
			{
				new EveryLineLearner(),
				new SelectRegexLearner(),
				new GroupRegexLearner(),
				new MergeEveryLearner()
			};
			foreach (GroupLearner groupLearner in array)
			{
				foreach (Record<records, TableExample> record in groupLearner.Learn(input, table, records, lines, skipNode, builder))
				{
					yield return record;
				}
				IEnumerator<Record<records, TableExample>> enumerator = null;
			}
			GroupLearner[] array2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06006D74 RID: 28020
		protected abstract IEnumerable<Record<records, TableExample>> Learn(StringRegion input, IReadOnlyList<List<StringRegionCell>> table, IReadOnlyList<StringRegion> records, IReadOnlyList<StringRegion> lines, skip skipNode, GrammarBuilders builder);

		// Token: 0x06006D75 RID: 28021 RVA: 0x0016518C File Offset: 0x0016338C
		internal static TableExample AssignRecordsToExamples(IReadOnlyList<StringRegion> allRecords, IReadOnlyList<List<StringRegionCell>> table)
		{
			if (allRecords.Count < table.Count)
			{
				return null;
			}
			List<Record<StringRegion, IReadOnlyList<StringRegionCell>>> list = new List<Record<StringRegion, IReadOnlyList<StringRegionCell>>>();
			int num = 0;
			while (num < table.Count && allRecords[num].Start <= GroupLearner.GetRowStart(table[num]) && GroupLearner.GetRowEnd(table[num]) <= allRecords[num].End)
			{
				list.Add(new Record<StringRegion, IReadOnlyList<StringRegionCell>>(allRecords[num], table[num]));
				num++;
			}
			if (list.Count != table.Count)
			{
				return null;
			}
			List<StringRegion> list2 = allRecords.Skip(list.Count).SkipLast(1).ToList<StringRegion>();
			return new TableExample(list, list2);
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x0016523C File Offset: 0x0016343C
		internal static IEnumerable<Record<Regex, TableExample>> FindRegexAndGroupExample(IReadOnlyList<List<StringRegionCell>> table, IReadOnlyList<StringRegion> records, IReadOnlyList<StringRegion> lines, Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>> groupFunc)
		{
			foreach (Regex regex in GroupLearner.GetGroupRegexes(records))
			{
				TableExample tableExample = GroupLearner.AssignRecordsToExamples(groupFunc(regex, lines), table);
				if (tableExample != null)
				{
					yield return Record.Create<Regex, TableExample>(regex, tableExample);
				}
			}
			IEnumerator<Regex> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x00165264 File Offset: 0x00163464
		private static IReadOnlyList<Regex> GetGroupRegexes(IReadOnlyList<StringRegion> startingLines)
		{
			List<Regex> list = Witnesses.GroupRegexes.Keys.Where((Regex re) => startingLines.All((StringRegion line) => re.IsMatch(line.Value))).ToList<Regex>();
			uint num = startingLines.Min((StringRegion line) => line.Length);
			StringRegion stringRegion = startingLines[0];
			List<char> list2 = new List<char>();
			bool flag = false;
			uint i;
			uint j;
			for (i = 0U; i < num; i = j + 1U)
			{
				char c2 = stringRegion[i];
				if ((flag && !char.IsLetterOrDigit(c2)) || startingLines.Skip(1).Any((StringRegion line) => line[i] != c2))
				{
					break;
				}
				flag = char.IsLetterOrDigit(c2);
				list2.Add(c2);
				j = i;
			}
			string text = new string(list2.TakeWhile((char c) => !char.IsLetterOrDigit(c)).ToArray<char>());
			string text2 = new string(list2.ToArray());
			if (!string.IsNullOrWhiteSpace(text) && text != text2)
			{
				list.Add(new Regex("^" + Regex.Escape(text)));
			}
			list.Add(new Regex("^" + Regex.Escape(text2)));
			return list;
		}

		// Token: 0x06006D78 RID: 28024 RVA: 0x0016541A File Offset: 0x0016361A
		protected static uint GetRowStart(IReadOnlyList<StringRegionCell> row)
		{
			return row.First((StringRegionCell cell) => cell.Value != null).Start;
		}

		// Token: 0x06006D79 RID: 28025 RVA: 0x00165446 File Offset: 0x00163646
		protected static uint GetRowEnd(IReadOnlyList<StringRegionCell> row)
		{
			return row.Last((StringRegionCell cell) => cell.Value != null).End;
		}

		// Token: 0x06006D7A RID: 28026 RVA: 0x00165474 File Offset: 0x00163674
		private static uint GetStartOfLine(StringRegion input, uint position)
		{
			int num = input.Source.LastIndexOf('\n', (int)position, (int)(position - input.Start));
			if (num == -1)
			{
				return input.Start;
			}
			return (uint)(num + 1);
		}
	}
}
