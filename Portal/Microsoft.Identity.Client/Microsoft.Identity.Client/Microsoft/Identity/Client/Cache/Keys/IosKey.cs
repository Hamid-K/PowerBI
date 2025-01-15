using System;

namespace Microsoft.Identity.Client.Cache.Keys
{
	// Token: 0x020002B4 RID: 692
	internal struct IosKey : IiOSKey
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00054A97 File Offset: 0x00052C97
		public readonly string iOSAccount { get; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00054A9F File Offset: 0x00052C9F
		public readonly string iOSGeneric { get; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00054AA7 File Offset: 0x00052CA7
		public readonly string iOSService { get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00054AAF File Offset: 0x00052CAF
		public readonly int iOSType { get; }

		// Token: 0x060019DB RID: 6619 RVA: 0x00054AB7 File Offset: 0x00052CB7
		internal IosKey(string iOSAccount, string iOSService, string iOSGeneric, int iOSType)
		{
			this.iOSAccount = iOSAccount;
			this.iOSGeneric = iOSGeneric;
			this.iOSService = iOSService;
			this.iOSType = iOSType;
		}
	}
}
