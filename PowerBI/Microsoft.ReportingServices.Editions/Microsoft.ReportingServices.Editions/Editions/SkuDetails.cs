using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200000D RID: 13
	public class SkuDetails : Attribute
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002A21 File Offset: 0x00000C21
		public SkuDetails()
		{
			this.Guid = string.Empty;
			this.Product = ProductType.None;
			this.RequiresKey = false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A42 File Offset: 0x00000C42
		public SkuDetails(ProductType product, string guid, bool requiesKey)
		{
			this.Product = product;
			this.Guid = guid;
			this.RequiresKey = false;
		}

		// Token: 0x04000039 RID: 57
		public string Guid;

		// Token: 0x0400003A RID: 58
		public ProductType Product;

		// Token: 0x0400003B RID: 59
		public bool RequiresKey;
	}
}
