using System;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x02000767 RID: 1895
	public class ParseConfig
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x00072F2A File Offset: 0x0007112A
		public RepetitionMode RepetitionMode { get; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x00072F32 File Offset: 0x00071132
		public UnionMatchingMode UnionMatchingMode { get; }

		// Token: 0x0600288E RID: 10382 RVA: 0x00072F3A File Offset: 0x0007113A
		public ParseConfig(RepetitionMode repetitionMode, UnionMatchingMode unionMatchingMode)
		{
			this.RepetitionMode = repetitionMode;
			this.UnionMatchingMode = unionMatchingMode;
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x00072F50 File Offset: 0x00071150
		public static ParseConfig Default { get; } = new ParseConfig(RepetitionMode.Greedy, UnionMatchingMode.Exhaustive);
	}
}
