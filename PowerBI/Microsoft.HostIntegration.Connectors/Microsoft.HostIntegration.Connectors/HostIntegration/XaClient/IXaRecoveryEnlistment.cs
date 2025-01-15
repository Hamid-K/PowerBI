using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000707 RID: 1799
	public interface IXaRecoveryEnlistment
	{
		// Token: 0x17000CAB RID: 3243
		// (set) Token: 0x060038FE RID: 14590
		int ResourceManagerId { set; }

		// Token: 0x060038FF RID: 14591
		XaReturnCode Open(string xaInfo, XaFlags flags);

		// Token: 0x06003900 RID: 14592
		XaReturnCode Close(string xaInfo, XaFlags flags);

		// Token: 0x06003901 RID: 14593
		XaReturnCode Start(Xid xid, XaFlags flags);

		// Token: 0x06003902 RID: 14594
		XaReturnCode End(Xid xid, XaFlags flags);

		// Token: 0x06003903 RID: 14595
		XaReturnCode Rollback(Xid xid, XaFlags flags);

		// Token: 0x06003904 RID: 14596
		XaReturnCode Prepare(Xid xid, XaFlags flags);

		// Token: 0x06003905 RID: 14597
		XaReturnCode Commit(Xid xid, XaFlags flags);

		// Token: 0x06003906 RID: 14598
		XaReturnCode Forget(Xid xid, XaFlags flags);

		// Token: 0x06003907 RID: 14599
		XaReturnCode Recover(XaFlags flags, int maximumNumberOfXids, out Xid[] xids);

		// Token: 0x06003908 RID: 14600
		XaReturnCode Complete(int handle, XaFlags flags, out int returnValue);
	}
}
