using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Editions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000057 RID: 87
	internal sealed class RevokedPermissionsForSku
	{
		// Token: 0x040002CD RID: 717
		internal SkuType Sku;

		// Token: 0x040002CE RID: 718
		internal List<string> RevokedGeneralPermissions = new List<string>();

		// Token: 0x040002CF RID: 719
		internal List<string> RevokedModelPermissions = new List<string>();

		// Token: 0x040002D0 RID: 720
		internal List<string> RevokedSystemPermissions = new List<string>();
	}
}
