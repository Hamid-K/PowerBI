using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A3 RID: 1187
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("EF1D1B7A-A661-42b0-8154-6B0F3A2FF5C0")]
	[CoClass(typeof(SSOConfigOM))]
	[ComImport]
	public interface ISSOConfigDB
	{
		// Token: 0x06002910 RID: 10512
		void GetDBInfo(string sqlServer, string sqlDatabase, out bool exists, out bool isConfigured, out bool needsUpgrade, out string secretServer, out string ssoAdminAccount, out string ssoAffilateAdminAccount);

		// Token: 0x06002911 RID: 10513
		void CreateDatabase(string sqlServer, string sqlDatabase, bool configureOnly, string secretServer, string ssoAdminAccount, string ssoAffilateAdminAccount);

		// Token: 0x06002912 RID: 10514
		void UpgradeDatabase(string sqlServer, string sqlDatabase);
	}
}
