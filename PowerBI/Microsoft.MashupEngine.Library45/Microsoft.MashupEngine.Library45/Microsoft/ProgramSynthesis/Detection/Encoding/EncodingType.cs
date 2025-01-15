using System;

namespace Microsoft.ProgramSynthesis.Detection.Encoding
{
	// Token: 0x02000AD6 RID: 2774
	public enum EncodingType
	{
		// Token: 0x04001FBD RID: 8125
		Ascii = 1,
		// Token: 0x04001FBE RID: 8126
		Iso88591,
		// Token: 0x04001FBF RID: 8127
		Windows1252,
		// Token: 0x04001FC0 RID: 8128
		Utf8,
		// Token: 0x04001FC1 RID: 8129
		Utf16Le,
		// Token: 0x04001FC2 RID: 8130
		Utf16Be,
		// Token: 0x04001FC3 RID: 8131
		Utf32Le,
		// Token: 0x04001FC4 RID: 8132
		Utf32Be,
		// Token: 0x04001FC5 RID: 8133
		Unknown = 2147483647
	}
}
