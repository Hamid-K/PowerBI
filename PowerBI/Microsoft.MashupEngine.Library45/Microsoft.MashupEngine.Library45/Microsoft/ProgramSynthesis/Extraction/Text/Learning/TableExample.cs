using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F58 RID: 3928
	internal class TableExample
	{
		// Token: 0x06006D54 RID: 27988 RVA: 0x001647B4 File Offset: 0x001629B4
		public TableExample(IReadOnlyList<Record<StringRegion, IReadOnlyList<StringRegionCell>>> rowExamples, IReadOnlyList<StringRegion> additionalInputs)
		{
			if (rowExamples.Count == 0)
			{
				throw new ArgumentException("Table cannot be empty", "rowExamples");
			}
			this.ColumnCount = rowExamples.First<Record<StringRegion, IReadOnlyList<StringRegionCell>>>().Item2.Count;
			List<Record<StringRegion, IReadOnlyList<StringRegionCell>>> list = new List<Record<StringRegion, IReadOnlyList<StringRegionCell>>>();
			List<StringRegion> list2 = new List<StringRegion>(additionalInputs);
			foreach (Record<StringRegion, IReadOnlyList<StringRegionCell>> record in rowExamples)
			{
				if (record.Item2.Any((StringRegionCell cell) => cell.IsUserSpecified))
				{
					list.Add(record);
				}
				else if (record.Item1 != null)
				{
					list2.Add(record.Item1);
				}
			}
			this.RowExamples = list;
			this.AdditionalInputs = list2;
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06006D55 RID: 27989 RVA: 0x00164894 File Offset: 0x00162A94
		public int ColumnCount { get; }

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06006D56 RID: 27990 RVA: 0x0016489C File Offset: 0x00162A9C
		public IReadOnlyList<Record<StringRegion, IReadOnlyList<StringRegionCell>>> RowExamples { get; }

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06006D57 RID: 27991 RVA: 0x001648A4 File Offset: 0x00162AA4
		public IReadOnlyList<StringRegion> AdditionalInputs { get; }
	}
}
