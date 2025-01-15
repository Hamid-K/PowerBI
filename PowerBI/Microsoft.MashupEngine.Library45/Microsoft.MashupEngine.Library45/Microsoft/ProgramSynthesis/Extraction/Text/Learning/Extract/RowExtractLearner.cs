using System;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract
{
	// Token: 0x02000F91 RID: 3985
	internal class RowExtractLearner : ExtractLearner
	{
		// Token: 0x06006E35 RID: 28213 RVA: 0x001681C8 File Offset: 0x001663C8
		protected override LearnResult<extract> LearnImpl(ExtractExample example, GrammarBuilders builder, LearnResult<extract> topResult, CancellationToken cancel)
		{
			if (example.Examples.Any((Record<StringRegion, StringRegionCell> tup) => tup.Item1 != tup.Item2.Value))
			{
				return null;
			}
			LearnResult<extract> learnResult = LearnResult<extract>.BuildLearnResult(builder.Set.UnnamedConversion.extract_row(ProgramSetBuilder.List<row>(builder.Symbol.row, new row[] { builder.Node.Variable.row })), example, (StringRegion input) => input);
			if (!(learnResult > topResult))
			{
				return null;
			}
			return learnResult;
		}
	}
}
