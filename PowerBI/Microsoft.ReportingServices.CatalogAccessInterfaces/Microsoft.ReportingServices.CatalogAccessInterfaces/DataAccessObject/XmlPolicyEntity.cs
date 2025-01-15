using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000019 RID: 25
	public class XmlPolicyEntity
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002745 File Offset: 0x00000945
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000274D File Offset: 0x0000094D
		public string XmlDescription { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000275E File Offset: 0x0000095E
		public int PolicyRoot { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000276F File Offset: 0x0000096F
		public int Type { get; set; }
	}
}
