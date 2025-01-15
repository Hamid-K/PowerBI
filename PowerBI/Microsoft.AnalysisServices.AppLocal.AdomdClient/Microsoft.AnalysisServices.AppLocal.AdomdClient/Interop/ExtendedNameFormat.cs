using System;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000134 RID: 308
	internal enum ExtendedNameFormat
	{
		// Token: 0x04000ABA RID: 2746
		NameUnknown,
		// Token: 0x04000ABB RID: 2747
		NameFullyQualifiedDN,
		// Token: 0x04000ABC RID: 2748
		NameSamCompatible,
		// Token: 0x04000ABD RID: 2749
		NameDisplay,
		// Token: 0x04000ABE RID: 2750
		NameUniqueId = 6,
		// Token: 0x04000ABF RID: 2751
		NameCanonical,
		// Token: 0x04000AC0 RID: 2752
		NameUserPrincipal,
		// Token: 0x04000AC1 RID: 2753
		NameCanonicalEx,
		// Token: 0x04000AC2 RID: 2754
		NameServicePrincipal,
		// Token: 0x04000AC3 RID: 2755
		NameDnsDomain = 12,
		// Token: 0x04000AC4 RID: 2756
		NameGivenName,
		// Token: 0x04000AC5 RID: 2757
		NameSurname
	}
}
