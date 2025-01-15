using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract
{
	// Token: 0x02000F89 RID: 3977
	internal class ExtractExample
	{
		// Token: 0x06006E1A RID: 28186 RVA: 0x001678A0 File Offset: 0x00165AA0
		public ExtractExample(IReadOnlyList<Record<StringRegion, StringRegionCell>> examples, IReadOnlyList<StringRegion> additionalInputs)
		{
			List<Record<StringRegion, StringRegionCell>> list = new List<Record<StringRegion, StringRegionCell>>();
			List<StringRegion> list2 = new List<StringRegion>(additionalInputs);
			foreach (Record<StringRegion, StringRegionCell> record in examples)
			{
				if (record.Item2.Value != null && record.Item2.IsUserSpecified)
				{
					list.Add(record);
				}
				else
				{
					list2.Add(record.Item1);
				}
			}
			this.Examples = list;
			this.AdditionalInputs = list2;
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06006E1B RID: 28187 RVA: 0x00167938 File Offset: 0x00165B38
		public IReadOnlyList<Record<StringRegion, StringRegionCell>> Examples { get; }

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06006E1C RID: 28188 RVA: 0x00167940 File Offset: 0x00165B40
		public IReadOnlyList<StringRegion> AdditionalInputs { get; }
	}
}
