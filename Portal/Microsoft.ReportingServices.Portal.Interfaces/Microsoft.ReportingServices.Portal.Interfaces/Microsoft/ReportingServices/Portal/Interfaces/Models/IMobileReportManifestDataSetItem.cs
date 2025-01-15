using System;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x02000099 RID: 153
	public interface IMobileReportManifestDataSetItem : IMobileReportManifestItem
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000505 RID: 1285
		// (set) Token: 0x06000506 RID: 1286
		MobileReportDataSetType Type { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000507 RID: 1287
		// (set) Token: 0x06000508 RID: 1288
		string TimeUnit { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000509 RID: 1289
		// (set) Token: 0x0600050A RID: 1290
		string DateTimeColumn { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600050B RID: 1291
		// (set) Token: 0x0600050C RID: 1292
		bool IsParameterized { get; set; }
	}
}
