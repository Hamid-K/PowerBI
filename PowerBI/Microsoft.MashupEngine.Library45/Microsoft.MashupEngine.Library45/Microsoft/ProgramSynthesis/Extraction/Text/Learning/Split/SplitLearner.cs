using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F7B RID: 3963
	internal abstract class SplitLearner
	{
		// Token: 0x06006DE0 RID: 28128
		protected abstract IEnumerable<SplitNode> Learn(TableExample tableExample, CancellationToken cancel);

		// Token: 0x06006DE1 RID: 28129 RVA: 0x0016686F File Offset: 0x00164A6F
		public static IEnumerable<Record<ProgramSetBuilder<split>, ExtractExample, TableExample>> Learn(TableExample tableExample, GrammarBuilders builder, CancellationToken cancel)
		{
			SplitLearner[] array = new SplitLearner[]
			{
				new SplitDelimiterLearner(),
				new SplitPositionLearner()
			};
			array.SelectMany((SplitLearner learner) => learner.Learn(tableExample, cancel));
			foreach (SplitLearner splitLearner in array)
			{
				bool flag = false;
				foreach (SplitNode splitNode in splitLearner.Learn(tableExample, cancel))
				{
					if (cancel.IsCancellationRequested)
					{
						yield break;
					}
					List<Record<StringRegion, StringRegionCell>> list = new List<Record<StringRegion, StringRegionCell>>();
					List<StringRegion> list2 = new List<StringRegion>();
					List<Record<StringRegion, IReadOnlyList<StringRegionCell>>> list3 = new List<Record<StringRegion, IReadOnlyList<StringRegionCell>>>();
					List<StringRegion> list4 = new List<StringRegion>();
					bool flag2 = false;
					foreach (Record<StringRegion, IReadOnlyList<StringRegionCell>> record in tableExample.RowExamples)
					{
						StringRegion stringRegion;
						IReadOnlyList<StringRegionCell> readOnlyList;
						record.Deconstruct(out stringRegion, out readOnlyList);
						StringRegion stringRegion2 = stringRegion;
						IReadOnlyList<StringRegionCell> readOnlyList2 = readOnlyList;
						StringRegionCell stringRegionCell = readOnlyList2.First<StringRegionCell>();
						StringRegionCell[] array3 = readOnlyList2.Skip(1).ToArray<StringRegionCell>();
						StringRegion stringRegion3;
						splitNode.Run(stringRegion2).Deconstruct(out stringRegion, out stringRegion3);
						StringRegion stringRegion4 = stringRegion;
						StringRegion stringRegion5 = stringRegion3;
						if (!SplitLearner.<Learn>g__IsValidSplit|1_0(stringRegion4, new StringRegionCell[] { readOnlyList2.First<StringRegionCell>() }) || !SplitLearner.<Learn>g__IsValidSplit|1_0(stringRegion5, array3))
						{
							flag2 = true;
							break;
						}
						list.Add(new Record<StringRegion, StringRegionCell>(stringRegion4, stringRegionCell));
						list3.Add(new Record<StringRegion, IReadOnlyList<StringRegionCell>>(stringRegion5, array3));
					}
					if (!flag2)
					{
						foreach (StringRegion stringRegion6 in tableExample.AdditionalInputs)
						{
							StringRegion stringRegion;
							StringRegion stringRegion3;
							splitNode.Run(stringRegion6).Deconstruct(out stringRegion3, out stringRegion);
							StringRegion stringRegion7 = stringRegion3;
							StringRegion stringRegion8 = stringRegion;
							if (stringRegion7 == null)
							{
								if (list.Any((Record<StringRegion, StringRegionCell> e) => e.Item2.IsUserSpecified && e.Item2.Value != null))
								{
									goto IL_028B;
								}
							}
							if (stringRegion8 == null)
							{
								if (list3.Any((Record<StringRegion, IReadOnlyList<StringRegionCell>> cdr) => cdr.Item2.Any((StringRegionCell e) => e.IsUserSpecified && e.Value != null)))
								{
									goto IL_028B;
								}
							}
							list2.Add(stringRegion7);
							list4.Add(stringRegion8);
							continue;
							IL_028B:
							flag2 = true;
							break;
						}
						if (!flag2)
						{
							yield return new Record<ProgramSetBuilder<split>, ExtractExample, TableExample>(splitNode.Node(builder), new ExtractExample(list, list2), new TableExample(list3, list4));
							flag = true;
						}
					}
				}
				IEnumerator<SplitNode> enumerator = null;
				if (flag)
				{
					yield break;
				}
			}
			SplitLearner[] array2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x00166890 File Offset: 0x00164A90
		[CompilerGenerated]
		internal static bool <Learn>g__IsValidSplit|1_0(StringRegion input, StringRegionCell[] examples)
		{
			if (!(input == null))
			{
				return examples.All((StringRegionCell e) => !e.IsUserSpecified || input.Contains(e.Value));
			}
			return examples.All((StringRegionCell e) => !e.IsUserSpecified || e.Value == null);
		}
	}
}
