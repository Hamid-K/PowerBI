using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001248 RID: 4680
	public class LearningOptions
	{
		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06008CCA RID: 36042 RVA: 0x001D92A9 File Offset: 0x001D74A9
		// (set) Token: 0x06008CCB RID: 36043 RVA: 0x001D92B1 File Offset: 0x001D74B1
		public int LinesToLearn { get; set; } = 200;

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06008CCC RID: 36044 RVA: 0x001D92BA File Offset: 0x001D74BA
		// (set) Token: 0x06008CCD RID: 36045 RVA: 0x001D92C2 File Offset: 0x001D74C2
		public ColumnNameCleaningType ColumnNameCleaning { get; set; } = ColumnNameCleaningType.AsciiAlphaNumeric;

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06008CCE RID: 36046 RVA: 0x001D92CB File Offset: 0x001D74CB
		// (set) Token: 0x06008CCF RID: 36047 RVA: 0x001D92D3 File Offset: 0x001D74D3
		public TimeSpan? TimeLimit { get; set; }
	}
}
