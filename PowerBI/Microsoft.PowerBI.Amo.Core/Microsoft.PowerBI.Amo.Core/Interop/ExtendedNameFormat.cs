using System;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x02000129 RID: 297
	internal enum ExtendedNameFormat
	{
		// Token: 0x04000A73 RID: 2675
		NameUnknown,
		// Token: 0x04000A74 RID: 2676
		NameFullyQualifiedDN,
		// Token: 0x04000A75 RID: 2677
		NameSamCompatible,
		// Token: 0x04000A76 RID: 2678
		NameDisplay,
		// Token: 0x04000A77 RID: 2679
		NameUniqueId = 6,
		// Token: 0x04000A78 RID: 2680
		NameCanonical,
		// Token: 0x04000A79 RID: 2681
		NameUserPrincipal,
		// Token: 0x04000A7A RID: 2682
		NameCanonicalEx,
		// Token: 0x04000A7B RID: 2683
		NameServicePrincipal,
		// Token: 0x04000A7C RID: 2684
		NameDnsDomain = 12,
		// Token: 0x04000A7D RID: 2685
		NameGivenName,
		// Token: 0x04000A7E RID: 2686
		NameSurname
	}
}
