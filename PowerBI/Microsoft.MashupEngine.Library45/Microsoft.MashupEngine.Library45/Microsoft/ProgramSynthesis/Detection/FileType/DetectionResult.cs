using System;

namespace Microsoft.ProgramSynthesis.Detection.FileType
{
	// Token: 0x02000AB9 RID: 2745
	internal struct DetectionResult
	{
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000D7BE5 File Offset: 0x000D5DE5
		internal readonly FileType FileType { get; }

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x000D7BED File Offset: 0x000D5DED
		internal readonly int Precedence { get; }

		// Token: 0x060044E5 RID: 17637 RVA: 0x000D7BF5 File Offset: 0x000D5DF5
		internal DetectionResult(FileType fileType, int precedence)
		{
			this.FileType = fileType;
			this.Precedence = precedence;
		}
	}
}
