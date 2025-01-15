using System;

namespace Microsoft.Data.SqlClient.DataClassification
{
	// Token: 0x02000155 RID: 341
	public sealed class InformationType
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0006BA73 File Offset: 0x00069C73
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x0006BA7B File Offset: 0x00069C7B
		public string Name { get; private set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0006BA84 File Offset: 0x00069C84
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x0006BA8C File Offset: 0x00069C8C
		public string Id { get; private set; }

		// Token: 0x06001A3E RID: 6718 RVA: 0x0006BA95 File Offset: 0x00069C95
		public InformationType(string name, string id)
		{
			this.Name = name;
			this.Id = id;
		}
	}
}
