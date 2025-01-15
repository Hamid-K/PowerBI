using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F71 RID: 3953
	internal class SelectRegexLearner : GroupLearner
	{
		// Token: 0x06006DB7 RID: 28087 RVA: 0x00165ECB File Offset: 0x001640CB
		protected override IEnumerable<Record<records, TableExample>> Learn(StringRegion input, IReadOnlyList<List<StringRegionCell>> table, IReadOnlyList<StringRegion> records, IReadOnlyList<StringRegion> lines, skip skipNode, GrammarBuilders builder)
		{
			if (records.Any((StringRegion record) => record.PositionOf('\n').HasValue))
			{
				yield break;
			}
			if (!records.Any((StringRegion record) => !SelectRegexLearner.SpanWholeLine(input, record)))
			{
				IEnumerable<StringRegion> enumerable = records.Windowed<StringRegion>().Select2((StringRegion r1, StringRegion r2) => input.Slice(r1.End, r2.Start));
				Func<StringRegion, bool> func;
				if ((func = SelectRegexLearner.<>O.<0>__HasMultipleNewLine) == null)
				{
					func = (SelectRegexLearner.<>O.<0>__HasMultipleNewLine = new Func<StringRegion, bool>(SelectRegexLearner.HasMultipleNewLine));
				}
				if (!enumerable.Any(func))
				{
					goto IL_018B;
				}
			}
			Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>> func2;
			if ((func2 = SelectRegexLearner.<>O.<1>__Select) == null)
			{
				func2 = (SelectRegexLearner.<>O.<1>__Select = new Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>>(Semantics.Select));
			}
			foreach (Record<Regex, TableExample> record2 in GroupLearner.FindRegexAndGroupExample(table, records, lines, func2))
			{
				Regex regex;
				TableExample tableExample;
				record2.Deconstruct(out regex, out tableExample);
				Regex regex2 = regex;
				TableExample tableExample2 = tableExample;
				yield return new Record<records, TableExample>(builder.Node.Rule.Select(builder.Node.Rule.re(regex2), skipNode), tableExample2);
			}
			IEnumerator<Record<Regex, TableExample>> enumerator = null;
			IL_018B:
			yield break;
			yield break;
		}

		// Token: 0x06006DB8 RID: 28088 RVA: 0x00165F01 File Offset: 0x00164101
		private static bool HasMultipleNewLine(StringRegion gap)
		{
			return gap.Value.Where((char c) => c == '\n').HasAtLeast(2);
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x00165F34 File Offset: 0x00164134
		private static bool SpanWholeLine(StringRegion input, StringRegion record)
		{
			for (uint num = record.End; num < input.End; num += 1U)
			{
				char c = input[num];
				if (c == '\n')
				{
					return true;
				}
				if (!char.IsWhiteSpace(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x02000F72 RID: 3954
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002FA7 RID: 12199
			public static Func<StringRegion, bool> <0>__HasMultipleNewLine;

			// Token: 0x04002FA8 RID: 12200
			public static Func<Regex, IReadOnlyList<StringRegion>, IReadOnlyList<StringRegion>> <1>__Select;
		}
	}
}
