using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x0200009A RID: 154
	public interface IMobileReportManifestItem
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600050D RID: 1293
		// (set) Token: 0x0600050E RID: 1294
		Guid Id { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600050F RID: 1295
		// (set) Token: 0x06000510 RID: 1296
		string Path { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000511 RID: 1297
		// (set) Token: 0x06000512 RID: 1298
		string Name { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000513 RID: 1299
		// (set) Token: 0x06000514 RID: 1300
		string Hash { get; set; }
	}
}
