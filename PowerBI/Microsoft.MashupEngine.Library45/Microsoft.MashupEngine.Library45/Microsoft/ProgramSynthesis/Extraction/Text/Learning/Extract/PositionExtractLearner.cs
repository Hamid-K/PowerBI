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
	// Token: 0x02000F8D RID: 3981
	internal class PositionExtractLearner : ExtractLearner
	{
		// Token: 0x06006E2D RID: 28205 RVA: 0x00167D6C File Offset: 0x00165F6C
		protected override LearnResult<extract> LearnImpl(ExtractExample example, GrammarBuilders builder, LearnResult<extract> topResult, CancellationToken cancel)
		{
			int? start = null;
			int? end = null;
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
				int num3 = stringRegionCell2.Length + num - (int)stringRegion2.Length - 1;
				if (start != null)
				{
					int? num4 = start;
					int num5 = num2;
					if ((num4.GetValueOrDefault() == num5) & (num4 != null))
					{
						num4 = end;
						num5 = num3;
						if ((num4.GetValueOrDefault() == num5) & (num4 != null))
						{
							continue;
						}
					}
					return null;
				}
				start = new int?(num2);
				end = new int?(num3);
			}
			if (start == null)
			{
				return null;
			}
			LearnResult<extract> learnResult = LearnResult<extract>.BuildLearnResult(builder.Set.Join.Slice(ProgramSetBuilder.List<row>(builder.Symbol.row, new row[] { builder.Node.Variable.row }), ProgramSetBuilder.List<k>(builder.Symbol.k, new k[] { builder.Node.Rule.k(start.Value) }), ProgramSetBuilder.List<k>(builder.Symbol.k, new k[] { builder.Node.Rule.k(end.Value) })), example, (StringRegion input) => Semantics.Slice(input, start.Value, end.Value));
			if (!(learnResult > topResult))
			{
				return null;
			}
			return learnResult;
		}
	}
}
