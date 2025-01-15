using System;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A1 RID: 673
	internal class AdalUserForMsalEntry
	{
		// Token: 0x06001967 RID: 6503 RVA: 0x00053330 File Offset: 0x00051530
		public AdalUserForMsalEntry(string clientId, string authority, string clientInfo, AdalUserInfo userInfo)
		{
			if (clientId == null)
			{
				throw new ArgumentNullException("clientId");
			}
			this.ClientId = clientId;
			this.Authority = authority;
			this.ClientInfo = clientInfo;
			if (userInfo == null)
			{
				throw new ArgumentNullException("userInfo");
			}
			this.UserInfo = userInfo;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0005337E File Offset: 0x0005157E
		public string ClientId { get; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00053386 File Offset: 0x00051586
		public string Authority { get; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0005338E File Offset: 0x0005158E
		public string ClientInfo { get; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x00053396 File Offset: 0x00051596
		public AdalUserInfo UserInfo { get; }
	}
}
