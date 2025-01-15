using System;
using Microsoft.ProgramSynthesis.Split.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.PowerQuery
{
	// Token: 0x02001B33 RID: 6963
	public interface ILocalizedPowerQueryMStrings : Microsoft.ProgramSynthesis.Split.Translation.PowerQuery.ILocalizedPowerQueryMStrings, Microsoft.ProgramSynthesis.Translation.PowerQuery.ILocalizedPowerQueryMStrings
	{
		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x0600E4C3 RID: 58563
		string FilledMissingValues { get; }

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x0600E4C4 RID: 58564
		string Average { get; }

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x0600E4C5 RID: 58565
		string RoundedAverage { get; }

		// Token: 0x1700261F RID: 9759
		// (get) Token: 0x0600E4C6 RID: 58566
		string Mode { get; }

		// Token: 0x17002620 RID: 9760
		// (get) Token: 0x0600E4C7 RID: 58567
		string RemovedDuplicateRows { get; }

		// Token: 0x17002621 RID: 9761
		// (get) Token: 0x0600E4C8 RID: 58568
		string RemovedBlankRows { get; }
	}
}
