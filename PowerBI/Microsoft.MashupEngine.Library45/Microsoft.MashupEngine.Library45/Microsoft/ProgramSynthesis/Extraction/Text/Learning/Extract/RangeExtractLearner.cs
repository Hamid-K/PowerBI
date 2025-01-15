using System;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract
{
	// Token: 0x02000F8F RID: 3983
	internal class RangeExtractLearner : ExtractLearner
	{
		// Token: 0x06006E31 RID: 28209 RVA: 0x00167FA0 File Offset: 0x001661A0
		protected override LearnResult<extract> LearnImpl(ExtractExample example, GrammarBuilders builder, LearnResult<extract> topResult, CancellationToken cancel)
		{
			int? start = null;
			int? length = null;
			foreach (Record<StringRegion, StringRegionCell> record in example.Examples)
			{
				StringRegion stringRegion;
				StringRegionCell stringRegionCell;
				record.Deconstruct(out stringRegion, out stringRegionCell);
				StringRegion stringRegion2 = stringRegion;
				StringRegionCell stringRegionCell2 = stringRegionCell;
				if (stringRegionCell2 == null)
				{
					return null;
				}
				int num = stringRegion2.Value.IndexOf(stringRegionCell2.StringValue, StringComparison.Ordinal);
				if (num == -1)
				{
					return null;
				}
				int num2 = num + 1;
				if (start != null)
				{
					int? num3 = start;
					int num4 = num2;
					if ((num3.GetValueOrDefault() == num4) & (num3 != null))
					{
						num3 = length;
						num4 = stringRegionCell2.Length;
						if ((num3.GetValueOrDefault() == num4) & (num3 != null))
						{
							continue;
						}
					}
					return null;
				}
				start = new int?(num2);
				length = new int?(stringRegionCell2.Length);
			}
			if (start == null)
			{
				return null;
			}
			LearnResult<extract> learnResult = LearnResult<extract>.BuildLearnResult(builder.Set.Join.Substring(ProgramSetBuilder.List<row>(builder.Symbol.row, new row[] { builder.Node.Variable.row }), ProgramSetBuilder.List<k>(builder.Symbol.k, new k[] { builder.Node.Rule.k(start.Value) }), ProgramSetBuilder.List<k>(builder.Symbol.k, new k[] { builder.Node.Rule.k(length.Value) })), example, (StringRegion input) => Semantics.Substring(input, start.Value, length.Value));
			if (!(learnResult > topResult))
			{
				return null;
			}
			return learnResult;
		}
	}
}
