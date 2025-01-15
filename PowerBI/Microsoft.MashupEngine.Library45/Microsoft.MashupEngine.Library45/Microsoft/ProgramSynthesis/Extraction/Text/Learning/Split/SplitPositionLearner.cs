using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split
{
	// Token: 0x02000F83 RID: 3971
	internal class SplitPositionLearner : SplitLearner
	{
		// Token: 0x06006E03 RID: 28163 RVA: 0x00166FE0 File Offset: 0x001651E0
		protected override IEnumerable<SplitNode> Learn(TableExample tableExample, CancellationToken cancel)
		{
			HashSet<int> hashSet = null;
			foreach (Record<StringRegion, IReadOnlyList<StringRegionCell>> record in tableExample.RowExamples.Where2((StringRegion input, IReadOnlyList<StringRegionCell> row) => row[0].Value != null && row[1].Value != null))
			{
				StringRegion stringRegion;
				IReadOnlyList<StringRegionCell> readOnlyList;
				record.Deconstruct(out stringRegion, out readOnlyList);
				StringRegion stringRegion2 = stringRegion;
				IReadOnlyList<StringRegionCell> readOnlyList2 = readOnlyList;
				int num = (int)(readOnlyList2[0].End - stringRegion2.Start);
				StringRegionCell stringRegionCell = readOnlyList2.Skip(1).FirstOrDefault((StringRegionCell cell) => cell.Value != null);
				int num2 = (int)((stringRegionCell == null) ? stringRegion2.Length : (stringRegionCell.Start - stringRegion2.Start));
				IEnumerable<int> enumerable = Enumerable.Range(num + 1, num2 - num + 1);
				if (hashSet == null)
				{
					hashSet = enumerable.ConvertToHashSet<int>();
				}
				else
				{
					hashSet.IntersectWith(enumerable);
				}
			}
			if (hashSet == null)
			{
				yield break;
			}
			foreach (int num3 in hashSet.OrderBy((int p) => p).Take(5))
			{
				if (cancel.IsCancellationRequested)
				{
					yield break;
				}
				yield return new SplitPosition(num3);
			}
			IEnumerator<int> enumerator2 = null;
			yield break;
			yield break;
		}
	}
}
