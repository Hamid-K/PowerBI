using System;

namespace Model
{
	// Token: 0x02000033 RID: 51
	public sealed class CatalogItemAccessTokenContent
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00002A59 File Offset: 0x00000C59
		public CatalogItemAccessTokenContent(string userName, string authenticationType, Guid catalogItemId)
		{
			this.UserName = userName;
			this.AuthenticationType = authenticationType;
			this.CatalogItemId = catalogItemId;
			this.Timestamp = DateTime.UtcNow;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002A81 File Offset: 0x00000C81
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00002A89 File Offset: 0x00000C89
		public string UserName { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002A92 File Offset: 0x00000C92
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002A9A File Offset: 0x00000C9A
		public string AuthenticationType { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00002AA3 File Offset: 0x00000CA3
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00002AAB File Offset: 0x00000CAB
		public Guid CatalogItemId { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002AB4 File Offset: 0x00000CB4
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00002ABC File Offset: 0x00000CBC
		public DateTime Timestamp { get; private set; }
	}
}
