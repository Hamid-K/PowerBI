using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B4 RID: 1204
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("2D56DCAB-FA6C-435b-835A-B135177DE8B0")]
	[CoClass(typeof(SSOMapper))]
	[ComImport]
	public interface ISSOMapper2
	{
		// Token: 0x06002948 RID: 10568
		object[] GetMappingsForWindowsUser(string windowsDomainName, string windowsUserName, string applicationName);

		// Token: 0x06002949 RID: 10569
		object[] GetMappingsForExternalUser(string applicationName, string externalUserName);

		// Token: 0x0600294A RID: 10570
		void SetWindowsPassword(string windowsPassword);

		// Token: 0x0600294B RID: 10571
		void SetExternalCredentials(string applicationName, string externalUserName, ref string[] externalCredentials);

		// Token: 0x0600294C RID: 10572
		void GetApplications(out string[] applications, out string[] descriptions, out string[] contactInfo);

		// Token: 0x0600294D RID: 10573
		void GetFieldInfo(string applicationName, out string[] labels, out int[] flags);

		// Token: 0x0600294E RID: 10574
		void GetApplications2(out string[] applications, out string[] descriptions, out string[] contactInfo, out string[] userAccounts, out string[] adminAccounts, out int[] flags);
	}
}
