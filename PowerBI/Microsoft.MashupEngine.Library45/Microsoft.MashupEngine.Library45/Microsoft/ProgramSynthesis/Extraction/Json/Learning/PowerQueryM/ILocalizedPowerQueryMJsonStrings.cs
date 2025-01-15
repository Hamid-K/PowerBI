using System;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning.PowerQueryM
{
	// Token: 0x02000B91 RID: 2961
	public interface ILocalizedPowerQueryMJsonStrings : ILocalizedPowerQueryMStrings
	{
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06004B4A RID: 19274
		string ConvertedToTable { get; }

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06004B4B RID: 19275
		string ConvertedToList { get; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004B4C RID: 19276
		Func<string, string> Expanded { get; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004B4D RID: 19277
		string SplitColumn { get; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004B4E RID: 19278
		string TransformColumns { get; }
	}
}
