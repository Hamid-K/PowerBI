using System;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000009 RID: 9
	public class ProductInfoEntity
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000025AD File Offset: 0x000007AD
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000025B5 File Offset: 0x000007B5
		public string DbSchemaHash { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000025BE File Offset: 0x000007BE
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000025C6 File Offset: 0x000007C6
		public string Sku { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000025CF File Offset: 0x000007CF
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000025D7 File Offset: 0x000007D7
		public string BuildNumber { get; set; }
	}
}
