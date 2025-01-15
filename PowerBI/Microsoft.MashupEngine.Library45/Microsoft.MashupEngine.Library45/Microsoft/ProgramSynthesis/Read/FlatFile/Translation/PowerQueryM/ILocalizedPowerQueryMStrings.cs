using System;
using Microsoft.ProgramSynthesis.Extraction.Text.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM
{
	// Token: 0x020012D8 RID: 4824
	public interface ILocalizedPowerQueryMStrings : Microsoft.ProgramSynthesis.Extraction.Text.Translation.ILocalizedPowerQueryMStrings, Microsoft.ProgramSynthesis.Translation.PowerQuery.ILocalizedPowerQueryMStrings
	{
		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x0600918F RID: 37263
		string FromRows { get; }

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06009190 RID: 37264
		string ParsedCsv { get; }

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06009191 RID: 37265
		string RemovedBottomRows { get; }

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06009192 RID: 37266
		string SplitColumnByRanges { get; }

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06009193 RID: 37267
		string TrimmedAllCells { get; }
	}
}
