using System;

namespace Microsoft.ProgramSynthesis.Split.Translation
{
	// Token: 0x020013FD RID: 5117
	internal struct Delimiter
	{
		// Token: 0x06009E0F RID: 40463 RVA: 0x00218663 File Offset: 0x00216863
		public Delimiter(string delimiter, bool isRegex)
		{
			this.DelimiterString = delimiter;
			this.IsRegex = isRegex;
		}

		// Token: 0x04003FFC RID: 16380
		public readonly string DelimiterString;

		// Token: 0x04003FFD RID: 16381
		public readonly bool IsRegex;
	}
}
