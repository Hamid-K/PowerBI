using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x0200001D RID: 29
	public class SubscriptionEntity
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00002844 File Offset: 0x00000A44
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000284C File Offset: 0x00000A4C
		public Guid SubscriptionID { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00002855 File Offset: 0x00000A55
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000285D File Offset: 0x00000A5D
		public Guid Report_OID { get; set; }
	}
}
