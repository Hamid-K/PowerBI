using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200001F RID: 31
	public enum RefreshType
	{
		// Token: 0x0400008C RID: 140
		Full = 1,
		// Token: 0x0400008D RID: 141
		ClearValues,
		// Token: 0x0400008E RID: 142
		Calculate,
		// Token: 0x0400008F RID: 143
		DataOnly,
		// Token: 0x04000090 RID: 144
		Automatic,
		// Token: 0x04000091 RID: 145
		Add = 7,
		// Token: 0x04000092 RID: 146
		Defragment
	}
}
