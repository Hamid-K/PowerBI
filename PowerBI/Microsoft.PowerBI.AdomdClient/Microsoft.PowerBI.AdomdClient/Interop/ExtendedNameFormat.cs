using System;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000134 RID: 308
	internal enum ExtendedNameFormat
	{
		// Token: 0x04000AAD RID: 2733
		NameUnknown,
		// Token: 0x04000AAE RID: 2734
		NameFullyQualifiedDN,
		// Token: 0x04000AAF RID: 2735
		NameSamCompatible,
		// Token: 0x04000AB0 RID: 2736
		NameDisplay,
		// Token: 0x04000AB1 RID: 2737
		NameUniqueId = 6,
		// Token: 0x04000AB2 RID: 2738
		NameCanonical,
		// Token: 0x04000AB3 RID: 2739
		NameUserPrincipal,
		// Token: 0x04000AB4 RID: 2740
		NameCanonicalEx,
		// Token: 0x04000AB5 RID: 2741
		NameServicePrincipal,
		// Token: 0x04000AB6 RID: 2742
		NameDnsDomain = 12,
		// Token: 0x04000AB7 RID: 2743
		NameGivenName,
		// Token: 0x04000AB8 RID: 2744
		NameSurname
	}
}
