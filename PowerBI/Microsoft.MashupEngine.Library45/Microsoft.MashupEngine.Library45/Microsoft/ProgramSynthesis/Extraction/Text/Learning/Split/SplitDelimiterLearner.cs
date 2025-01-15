using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F76 RID: 3958
	internal class SplitDelimiterLearner : SplitLearner
	{
		// Token: 0x06006DCB RID: 28107 RVA: 0x00166297 File Offset: 0x00164497
		protected override IEnumerable<SplitNode> Learn(TableExample tableExample, CancellationToken cancel)
		{
			List<Record<string, int>> list = null;
			using (IEnumerator<Record<StringRegion, IReadOnlyList<StringRegionCell>>> enumerator = tableExample.RowExamples.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StringRegion stringRegion;
					IReadOnlyList<StringRegionCell> readOnlyList;
					enumerator.Current.Deconstruct(out stringRegion, out readOnlyList);
					StringRegion input = stringRegion;
					IReadOnlyList<StringRegionCell> row = readOnlyList;
					if (!(row[0].Value == null) && !(row[1].Value == null))
					{
						uint gapStart = row[0].End;
						uint gapEnd = row[1].Start;
						if (list == null)
						{
							list = new List<Record<string, int>>();
							StringRegion stringRegion2 = input.Slice(gapStart, gapEnd);
							StringRegion stringRegion3 = input.Slice(input.Start, gapEnd);
							using (HashSet<string>.Enumerator enumerator2 = SplitDelimiterLearner.GetPossibleDelimiters(stringRegion2).GetEnumerator())
							{
								Func<int, uint, bool> <>9__1;
								while (enumerator2.MoveNext())
								{
									string candidate = enumerator2.Current;
									IEnumerable<uint> enumerable = Semantics.FindAllIndexes(stringRegion3, candidate);
									List<Record<string, int>> list2 = list;
									IEnumerable<Record<int, uint>> enumerable2 = enumerable.Enumerate<uint>();
									Func<int, uint, bool> func;
									if ((func = <>9__1) == null)
									{
										func = (<>9__1 = (int index, uint pos) => pos >= gapStart);
									}
									list2.AddRange(enumerable2.Where2(func).Select2((int index, uint pos) => Record.Create<string, int>(candidate, index + 1)));
								}
								continue;
							}
						}
						list.RemoveAll(delegate(Record<string, int> delimiter)
						{
							StringRegion stringRegion4;
							StringRegion stringRegion5;
							Semantics.SplitDelimiter(input, delimiter.Item1, delimiter.Item2).Deconstruct(out stringRegion4, out stringRegion5);
							StringRegion stringRegion6 = stringRegion4;
							StringRegion stringRegion7 = stringRegion5;
							return (row[0].IsUserSpecified && (stringRegion6 == null || stringRegion6.End < gapStart || stringRegion6.End > gapEnd)) || (row[1].IsUserSpecified && (stringRegion7 == null || stringRegion7.Start > gapEnd));
						});
					}
				}
			}
			if (list == null)
			{
				yield break;
			}
			foreach (Record<string, int> record in (from d in list
				orderby d.Item2, d.Item1.Length descending
				select d).Take(5))
			{
				if (cancel.IsCancellationRequested)
				{
					yield break;
				}
				yield return new SplitDelimiter(record.Item1, record.Item2);
			}
			IEnumerator<Record<string, int>> enumerator3 = null;
			yield break;
			yield break;
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x001662B0 File Offset: 0x001644B0
		internal static HashSet<string> GetPossibleDelimiters(StringRegion gap)
		{
			HashSet<string> hashSet = new HashSet<string>();
			for (uint num = gap.Start; num < gap.End; num += 1U)
			{
				char c = gap.Source[(int)num];
				if (char.IsDigit(c))
				{
					while (num + 1U < gap.End && char.IsDigit(gap.Source[(int)(num + 1U)]))
					{
						num += 1U;
					}
					hashSet.Add(gap.Slice(gap.Start, num + 1U).Value);
				}
				else if (char.IsLetter(c))
				{
					while (num + 1U < gap.End && char.IsLetter(gap.Source[(int)(num + 1U)]))
					{
						num += 1U;
					}
					hashSet.Add(gap.Slice(gap.Start, num + 1U).Value);
				}
				else
				{
					hashSet.Add(c.ToString());
					hashSet.Add(gap.Slice(gap.Start, num + 1U).Value);
				}
			}
			return hashSet;
		}
	}
}
