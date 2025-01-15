using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000011 RID: 17
	internal class SkuSupportsDatabasesCreatedBy : Attribute
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002F92 File Offset: 0x00001192
		public SkuSupportsDatabasesCreatedBy()
		{
			this.SupportsDatabasesCreatedBy = new SkuType[0];
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FA6 File Offset: 0x000011A6
		public SkuSupportsDatabasesCreatedBy(SkuType[] supportsDatabasesCreatedBy)
		{
			this.SupportsDatabasesCreatedBy = supportsDatabasesCreatedBy;
		}

		// Token: 0x04000055 RID: 85
		public SkuType[] SupportsDatabasesCreatedBy;
	}
}
