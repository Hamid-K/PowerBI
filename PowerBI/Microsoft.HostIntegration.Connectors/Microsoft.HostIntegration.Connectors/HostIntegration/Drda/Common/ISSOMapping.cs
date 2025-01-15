using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200085E RID: 2142
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("CB6B05A8-B806-43CC-B441-9E5B5EFB881C")]
	[CoClass(typeof(SSOMapping))]
	[ComImport]
	public interface ISSOMapping
	{
		// Token: 0x0600442D RID: 17453
		void Create(int flags);

		// Token: 0x0600442E RID: 17454
		void Delete();

		// Token: 0x0600442F RID: 17455
		void Enable(int flags);

		// Token: 0x06004430 RID: 17456
		void Disable();

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06004431 RID: 17457
		// (set) Token: 0x06004432 RID: 17458
		string WindowsDomainName { get; set; }

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06004433 RID: 17459
		// (set) Token: 0x06004434 RID: 17460
		string WindowsUserName { get; set; }

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06004435 RID: 17461
		// (set) Token: 0x06004436 RID: 17462
		string ApplicationName { get; set; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06004437 RID: 17463
		// (set) Token: 0x06004438 RID: 17464
		string ExternalUserName { get; set; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06004439 RID: 17465
		// (set) Token: 0x0600443A RID: 17466
		int Flags { get; set; }
	}
}
