using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012FD RID: 4861
	public class SplitProgram : Program<StringRegion, SplitCell[]>
	{
		// Token: 0x06009276 RID: 37494 RVA: 0x001EC9D8 File Offset: 0x001EABD8
		public SplitProgram(regionSplit programNode)
			: base(programNode.Node, 0.0, null)
		{
			string text = null;
			QuotingConfiguration quotingConfiguration = default(QuotingConfiguration);
			Record<int, int?>[] array = null;
			SplitRegion splitRegion;
			int num;
			int num2;
			if (programNode.Is_SplitRegion(Language.Build, out splitRegion))
			{
				splitMatches splitMatches = splitRegion.splitMatches;
				splitMatches_constantDelimiterMatches splitMatches_constantDelimiterMatches;
				splitMatches_fixedWidthMatches splitMatches_fixedWidthMatches;
				if (splitMatches.Is_splitMatches_constantDelimiterMatches(Language.Build, out splitMatches_constantDelimiterMatches))
				{
					constantDelimiterMatches constantDelimiterMatches = splitMatches_constantDelimiterMatches.constantDelimiterMatches;
					ConstantDelimiter constantDelimiter;
					if (constantDelimiterMatches.Is_ConstantDelimiter(Language.Build, out constantDelimiter))
					{
						text = constantDelimiter.s.Value;
						quotingConfiguration = new QuotingConfiguration(null, false, null, QuotingStyle.Adaptive);
					}
					else
					{
						ConstantDelimiterWithQuoting constantDelimiterWithQuoting = constantDelimiterMatches.Cast_ConstantDelimiterWithQuoting(Language.Build);
						text = constantDelimiterWithQuoting.s.Value;
						quotingConfiguration = constantDelimiterWithQuoting.quotingConf.Value;
					}
				}
				else if (splitMatches.Is_splitMatches_fixedWidthMatches(Language.Build, out splitMatches_fixedWidthMatches))
				{
					FixedWidth fixedWidth;
					FixedWidthDelimiters fixedWidthDelimiters;
					if (splitMatches_fixedWidthMatches.fixedWidthMatches.Is_FixedWidth(Language.Build, out fixedWidth))
					{
						IEnumerable<int> enumerable = fixedWidth.fieldStartPositions.Value.PrependItem(0);
						IEnumerable<int?> enumerable2 = fixedWidth.fieldStartPositions.Value.Cast<int?>().AppendItem(null);
						array = (from tup in enumerable.ZipWith(enumerable2)
							select new Record<int, int?>(tup.Item1, tup.Item2)).ToArray<Record<int, int?>>();
					}
					else if (splitMatches_fixedWidthMatches.fixedWidthMatches.Is_FixedWidthDelimiters(Language.Build, out fixedWidthDelimiters))
					{
						Record<int, int>[] value = fixedWidthDelimiters.delimiterPositions.Value;
						if (value.Length == 0)
						{
							array = new Record<int, int?>[]
							{
								new Record<int, int?>(0, null)
							};
						}
						else
						{
							HashSet<int> hashSet = splitRegion.ignoreIndexes.Value.ConvertToHashSet<int>();
							List<Record<int, int?>> list = new List<Record<int, int?>>();
							if (!hashSet.Contains(0))
							{
								list.Add(new Record<int, int?>(0, new int?(value[0].Item1)));
							}
							for (int i = 0; i < value.Length - 1; i++)
							{
								list.Add(new Record<int, int?>(value[i].Item2, new int?(value[i + 1].Item1)));
							}
							if (!hashSet.Contains(value.Length - 1))
							{
								list.Add(new Record<int, int?>(value.Last<Record<int, int>>().Item2, null));
							}
							array = list.ToArray();
						}
					}
				}
				num = splitRegion.numSplits.Value;
				bool value2 = splitRegion.delimiterStart.Value;
				bool value3 = splitRegion.delimiterEnd.Value;
				num2 = (splitRegion.includeDelimiters.Value ? ((num - ((value2 > false) ? 1 : 0) - ((value3 > false) ? 1 : 0)) / 2 + 1) : num);
			}
			else
			{
				num = this.CountExtPointsList(programNode.Cast_ExtractionSplit(Language.Build).extractionPoints);
				num2 = num;
			}
			this.Properties = new ProgramProperties(text, num2, num, array, quotingConfiguration);
		}

		// Token: 0x06009277 RID: 37495 RVA: 0x001ECD08 File Offset: 0x001EAF08
		private int CountExtPointsList(extractionPoints extractionPoints)
		{
			ExtPointsList extPointsList;
			if (!extractionPoints.Is_ExtPointsList(Language.Build, out extPointsList))
			{
				return 0;
			}
			return 1 + this.CountExtPointsList(extPointsList.extractionPoints);
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06009278 RID: 37496 RVA: 0x001ECD36 File Offset: 0x001EAF36
		public static Symbol ProgramSymbol { get; } = Language.Build.Symbol.regionSplit;

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06009279 RID: 37497 RVA: 0x001ECD3D File Offset: 0x001EAF3D
		public ProgramProperties Properties { get; }

		// Token: 0x0600927A RID: 37498 RVA: 0x001ECD48 File Offset: 0x001EAF48
		public override SplitCell[] Run(StringRegion input)
		{
			State state = State.CreateForExecution(Language.Grammar.InputSymbol, input);
			return base.ProgramNode.Invoke(state) as SplitCell[];
		}
	}
}
