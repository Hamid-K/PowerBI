using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x020000A5 RID: 165
	public interface ICatalogItem
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600053C RID: 1340
		// (set) Token: 0x0600053D RID: 1341
		string Id { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600053E RID: 1342
		// (set) Token: 0x0600053F RID: 1343
		string Name { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000540 RID: 1344
		// (set) Token: 0x06000541 RID: 1345
		string Path { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000542 RID: 1346
		// (set) Token: 0x06000543 RID: 1347
		CatalogItemType Type { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000544 RID: 1348
		// (set) Token: 0x06000545 RID: 1349
		int Size { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000546 RID: 1350
		// (set) Token: 0x06000547 RID: 1351
		string Description { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000548 RID: 1352
		// (set) Token: 0x06000549 RID: 1353
		bool Hidden { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600054A RID: 1354
		// (set) Token: 0x0600054B RID: 1355
		DateTime CreatedDate { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600054C RID: 1356
		// (set) Token: 0x0600054D RID: 1357
		DateTime ModifiedDate { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600054E RID: 1358
		// (set) Token: 0x0600054F RID: 1359
		string CreatedBy { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000550 RID: 1360
		// (set) Token: 0x06000551 RID: 1361
		string ModifiedBy { get; set; }
	}
}
