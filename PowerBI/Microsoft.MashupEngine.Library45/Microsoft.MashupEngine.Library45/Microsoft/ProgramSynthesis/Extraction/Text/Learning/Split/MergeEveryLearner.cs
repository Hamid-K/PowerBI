using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F6D RID: 3949
	internal class MergeEveryLearner : GroupLearner
	{
		// Token: 0x06006DA8 RID: 28072 RVA: 0x00165CAF File Offset: 0x00163EAF
		protected override IEnumerable<Record<records, TableExample>> Learn(StringRegion input, IReadOnlyList<List<StringRegionCell>> table, IReadOnlyList<StringRegion> records, IReadOnlyList<StringRegion> lines, skip skipNode, GrammarBuilders builder)
		{
			int num = table.Windowed<List<StringRegionCell>>().Select(delegate(Record<List<StringRegionCell>, List<StringRegionCell>> tup)
			{
				uint rowStart = GroupLearner.GetRowStart(tup.Item1);
				uint rowStart2 = GroupLearner.GetRowStart(tup.Item2);
				return input.Slice(rowStart, rowStart2).Value.Count((char c) => c == '\n');
			}).Distinct<int>()
				.OnlyOrDefault<int>();
			if (num < 1)
			{
				yield break;
			}
			TableExample tableExample = GroupLearner.AssignRecordsToExamples(Semantics.MergeEvery(num, lines), table);
			if (tableExample != null)
			{
				yield return new Record<records, TableExample>(builder.Node.Rule.MergeEvery(builder.Node.Rule.k(num), skipNode), tableExample);
			}
			yield break;
		}
	}
}
