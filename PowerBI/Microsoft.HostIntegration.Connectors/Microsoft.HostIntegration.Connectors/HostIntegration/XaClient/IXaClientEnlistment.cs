using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000705 RID: 1797
	public interface IXaClientEnlistment
	{
		// Token: 0x060038F7 RID: 14583
		XaReturnCode Prepare(bool singlePhase);

		// Token: 0x060038F8 RID: 14584
		void Commit();

		// Token: 0x060038F9 RID: 14585
		void Rollback();
	}
}
