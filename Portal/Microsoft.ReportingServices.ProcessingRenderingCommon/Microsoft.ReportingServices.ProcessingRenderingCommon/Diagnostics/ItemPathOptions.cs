using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000053 RID: 83
	[Flags]
	internal enum ItemPathOptions
	{
		// Token: 0x04000119 RID: 281
		None = 0,
		// Token: 0x0400011A RID: 282
		Validate = 1,
		// Token: 0x0400011B RID: 283
		Convert = 2,
		// Token: 0x0400011C RID: 284
		Translate = 4,
		// Token: 0x0400011D RID: 285
		AllowEditSessionSyntax = 8,
		// Token: 0x0400011E RID: 286
		IgnoreValidateEditSession = 16,
		// Token: 0x0400011F RID: 287
		Default = 7
	}
}
