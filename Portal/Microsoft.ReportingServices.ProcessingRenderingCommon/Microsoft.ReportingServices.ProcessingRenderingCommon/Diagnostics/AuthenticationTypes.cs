using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000031 RID: 49
	[Flags]
	internal enum AuthenticationTypes
	{
		// Token: 0x040000AB RID: 171
		None = 0,
		// Token: 0x040000AC RID: 172
		RSWindowsNegotiate = 1,
		// Token: 0x040000AD RID: 173
		RSWindowsKerberos = 2,
		// Token: 0x040000AE RID: 174
		RSWindowsNTLM = 4,
		// Token: 0x040000AF RID: 175
		RSWindowsBasic = 8,
		// Token: 0x040000B0 RID: 176
		Custom = 16,
		// Token: 0x040000B1 RID: 177
		RSForms = 32,
		// Token: 0x040000B2 RID: 178
		OAuth = 64
	}
}
