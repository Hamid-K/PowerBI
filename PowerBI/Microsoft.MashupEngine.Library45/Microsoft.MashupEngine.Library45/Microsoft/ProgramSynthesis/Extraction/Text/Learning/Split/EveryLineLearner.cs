using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F5E RID: 3934
	internal class EveryLineLearner : GroupLearner
	{
		// Token: 0x06006D66 RID: 28006 RVA: 0x00164FBC File Offset: 0x001631BC
		protected override IEnumerable<Record<records, TableExample>> Learn(StringRegion input, IReadOnlyList<List<StringRegionCell>> table, IReadOnlyList<StringRegion> records, IReadOnlyList<StringRegion> lines, skip skipNode, GrammarBuilders builder)
		{
			if (records.Any((StringRegion record) => record.PositionOf('\n').HasValue))
			{
				yield break;
			}
			TableExample tableExample = GroupLearner.AssignRecordsToExamples(lines, table);
			if (tableExample != null)
			{
				yield return new Record<records, TableExample>(builder.Node.UnnamedConversion.records_skip(skipNode), tableExample);
			}
			yield break;
		}
	}
}
