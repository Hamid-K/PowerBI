using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000254 RID: 596
	[Serializable]
	public class AccountIsLockedException : ServiceRemoteException
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x0003CC23 File Offset: 0x0003BC23
		public AccountIsLockedException(string message, Uri accountUnlockUrl, Exception innerException)
			: base(message, innerException)
		{
			this.AccountUnlockUrl = accountUnlockUrl;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0003CC34 File Offset: 0x0003BC34
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x0003CC3C File Offset: 0x0003BC3C
		public Uri AccountUnlockUrl { get; private set; }
	}
}
