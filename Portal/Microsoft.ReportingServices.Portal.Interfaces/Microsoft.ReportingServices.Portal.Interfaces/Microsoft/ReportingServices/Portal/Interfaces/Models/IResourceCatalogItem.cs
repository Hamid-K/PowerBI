using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x020000A3 RID: 163
	public interface IResourceCatalogItem : ICatalogItem
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000538 RID: 1336
		// (set) Token: 0x06000539 RID: 1337
		byte[] Content { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600053A RID: 1338
		// (set) Token: 0x0600053B RID: 1339
		string ContentType { get; set; }
	}
}
