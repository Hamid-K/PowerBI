using System;

namespace Microsoft.Identity.Client.Cache.Keys
{
	// Token: 0x020002B2 RID: 690
	internal interface IiOSKey
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060019CD RID: 6605
		string iOSAccount { get; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060019CE RID: 6606
		string iOSGeneric { get; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060019CF RID: 6607
		string iOSService { get; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060019D0 RID: 6608
		int iOSType { get; }
	}
}
