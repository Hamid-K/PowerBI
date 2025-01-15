using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract
{
	// Token: 0x02000F8A RID: 3978
	internal abstract class ExtractLearner
	{
		// Token: 0x06006E1D RID: 28189 RVA: 0x00167948 File Offset: 0x00165B48
		public static LearnResult<trimExtract> Learn(ExtractExample example, GrammarBuilders builder, CancellationToken cancel)
		{
			ExtractLearner.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.builder = builder;
			ExtractLearner[] array = new ExtractLearner[]
			{
				new RowExtractLearner(),
				new BetweenDelimitersExtractLearner(),
				new PositionExtractLearner(),
				new RangeExtractLearner()
			};
			bool flag = false;
			List<Record<StringRegion, StringRegionCell>> list = new List<Record<StringRegion, StringRegionCell>>(example.Examples.Count);
			foreach (Record<StringRegion, StringRegionCell> record in example.Examples)
			{
				StringRegion stringRegion;
				StringRegionCell stringRegionCell;
				record.Deconstruct(out stringRegion, out stringRegionCell);
				StringRegion stringRegion2 = stringRegion;
				StringRegionCell stringRegionCell2 = stringRegionCell;
				bool flag2 = false;
				int num = (int)(stringRegionCell2.Start - 1U);
				while ((long)num >= (long)((ulong)stringRegion2.Start) && char.IsWhiteSpace(stringRegion2.Source[num]))
				{
					flag2 = true;
					num--;
				}
				uint num2 = stringRegionCell2.End;
				while (num2 < stringRegion2.End && char.IsWhiteSpace(stringRegion2.Source[(int)num2]))
				{
					flag2 = true;
					num2 += 1U;
				}
				flag = flag || flag2;
				list.Add(flag2 ? Record.Create<StringRegion, StringRegionCell>(stringRegion2, new StringRegionCell(stringRegion2.Slice((uint)(num + 1), num2), stringRegionCell2.IsUserSpecified)) : record);
			}
			ExtractExample extractExample = new ExtractExample(list, example.AdditionalInputs);
			LearnResult<extract> learnResult = null;
			foreach (ExtractLearner extractLearner in array)
			{
				if (cancel.IsCancellationRequested)
				{
					break;
				}
				LearnResult<extract> learnResult2 = extractLearner.LearnImpl(example, CS$<>8__locals1.builder, learnResult, cancel);
				if (learnResult2 != null)
				{
					if (!learnResult2.HasNullOrWhiteSpace)
					{
						return ExtractLearner.<Learn>g__CreateTrimResult|0_0(learnResult2, false, ref CS$<>8__locals1);
					}
					learnResult = learnResult2;
				}
				else if (flag)
				{
					learnResult2 = extractLearner.LearnImpl(extractExample, CS$<>8__locals1.builder, learnResult, cancel);
					if (learnResult2 != null)
					{
						if (!learnResult2.HasNullOrWhiteSpace)
						{
							return ExtractLearner.<Learn>g__CreateTrimResult|0_0(learnResult2, true, ref CS$<>8__locals1);
						}
						learnResult2 = new LearnResult<extract>(learnResult2.ProgramSet, learnResult2.NullRatio, learnResult2.NullOrWhitespaceRatio, true);
						learnResult = learnResult2;
					}
				}
			}
			if (learnResult == null)
			{
				return null;
			}
			return ExtractLearner.<Learn>g__CreateTrimResult|0_0(learnResult, learnResult.NeedTrim, ref CS$<>8__locals1);
		}

		// Token: 0x06006E1E RID: 28190
		protected abstract LearnResult<extract> LearnImpl(ExtractExample example, GrammarBuilders builder, LearnResult<extract> topResult, CancellationToken cancel);

		// Token: 0x06006E20 RID: 28192 RVA: 0x00167B68 File Offset: 0x00165D68
		[CompilerGenerated]
		internal static LearnResult<trimExtract> <Learn>g__CreateTrimResult|0_0(LearnResult<extract> extractLearnResult, bool needTrim, ref ExtractLearner.<>c__DisplayClass0_0 A_2)
		{
			return new LearnResult<trimExtract>(needTrim ? A_2.builder.Set.Join.Trim(extractLearnResult.ProgramSet) : A_2.builder.Set.UnnamedConversion.trimExtract_extract(extractLearnResult.ProgramSet), extractLearnResult.NullRatio, extractLearnResult.NullOrWhitespaceRatio, false);
		}
	}
}
