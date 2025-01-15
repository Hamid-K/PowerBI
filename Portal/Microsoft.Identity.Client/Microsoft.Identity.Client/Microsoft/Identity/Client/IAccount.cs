using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200014C RID: 332
	public interface IAccount
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600109F RID: 4255
		string Username { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060010A0 RID: 4256
		string Environment { get; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060010A1 RID: 4257
		AccountId HomeAccountId { get; }
	}
}
