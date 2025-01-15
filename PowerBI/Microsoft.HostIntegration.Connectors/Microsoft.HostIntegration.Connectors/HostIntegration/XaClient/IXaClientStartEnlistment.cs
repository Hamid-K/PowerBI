using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000706 RID: 1798
	public interface IXaClientStartEnlistment
	{
		// Token: 0x060038FA RID: 14586
		void Start(Xid xid);

		// Token: 0x060038FB RID: 14587
		XaReturnCode Prepare(bool singlePhase);

		// Token: 0x060038FC RID: 14588
		void Commit();

		// Token: 0x060038FD RID: 14589
		void Rollback();
	}
}
