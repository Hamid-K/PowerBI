using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B6 RID: 1206
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("CB6B05A8-B806-43CC-B441-9E5B5EFB881C")]
	[CoClass(typeof(SSOMapping))]
	[ComImport]
	public interface ISSOMapping
	{
		// Token: 0x06002950 RID: 10576
		void Create(int flags);

		// Token: 0x06002951 RID: 10577
		void Delete();

		// Token: 0x06002952 RID: 10578
		void Enable(int flags);

		// Token: 0x06002953 RID: 10579
		void Disable();

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002954 RID: 10580
		// (set) Token: 0x06002955 RID: 10581
		string WindowsDomainName { get; set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002956 RID: 10582
		// (set) Token: 0x06002957 RID: 10583
		string WindowsUserName { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002958 RID: 10584
		// (set) Token: 0x06002959 RID: 10585
		string ApplicationName { get; set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600295A RID: 10586
		// (set) Token: 0x0600295B RID: 10587
		string ExternalUserName { get; set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600295C RID: 10588
		// (set) Token: 0x0600295D RID: 10589
		int Flags { get; set; }
	}
}
