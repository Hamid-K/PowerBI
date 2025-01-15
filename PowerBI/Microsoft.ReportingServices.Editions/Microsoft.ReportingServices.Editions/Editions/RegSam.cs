using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000017 RID: 23
	public enum RegSam
	{
		// Token: 0x0400006B RID: 107
		QueryValue = 1,
		// Token: 0x0400006C RID: 108
		SetValue,
		// Token: 0x0400006D RID: 109
		CreateSubKey = 4,
		// Token: 0x0400006E RID: 110
		EnumerateSubKeys = 8,
		// Token: 0x0400006F RID: 111
		Notify = 16,
		// Token: 0x04000070 RID: 112
		CreateLink = 32,
		// Token: 0x04000071 RID: 113
		Wow6432Key = 512,
		// Token: 0x04000072 RID: 114
		Wow6464Key = 256,
		// Token: 0x04000073 RID: 115
		Wow64Res = 768,
		// Token: 0x04000074 RID: 116
		Read = 131097,
		// Token: 0x04000075 RID: 117
		Write = 131078,
		// Token: 0x04000076 RID: 118
		Execute = 131097,
		// Token: 0x04000077 RID: 119
		AllAccess = 983103
	}
}
