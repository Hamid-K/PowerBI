using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A2 RID: 1186
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("5AC332CB-773F-4925-95FE-39CF2C670C24")]
	[CoClass(typeof(SSOConfigOM))]
	[ComImport]
	public interface ISSOConfigOM
	{
		// Token: 0x0600290E RID: 10510
		void DiscoverServers(out string[] servers);

		// Token: 0x0600290F RID: 10511
		void GetServerStatus(int flags, out string ssoServerName, out string sqlServer, out string sqlDatabase, out string serviceAccount, out string computerNameCluster, out string computerNameNode, out int eventCountInformational, out int eventCountWarning, out int eventCountError, out int versionInfoM, out int versionInfoL, out int auditLevelN, out int auditLevelP, out int passwordSyncAge, out int statusFlags);
	}
}
