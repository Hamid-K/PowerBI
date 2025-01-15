using System;
using System.Collections.Generic;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x0200009B RID: 155
	public interface IMobileReportManifestResourceGroup
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000515 RID: 1301
		// (set) Token: 0x06000516 RID: 1302
		string Name { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000517 RID: 1303
		// (set) Token: 0x06000518 RID: 1304
		MobileReportResourceGroupType Type { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000519 RID: 1305
		IEnumerable<IMobileReportManifestResourceItem> Items { get; }
	}
}
