using System;

namespace Model
{
	// Token: 0x02000032 RID: 50
	public sealed class CatalogItemAccessToken
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00002A39 File Offset: 0x00000C39
		public CatalogItemAccessToken(byte[] token)
		{
			this.Token = token;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00002A50 File Offset: 0x00000C50
		public byte[] Token { get; private set; }
	}
}
