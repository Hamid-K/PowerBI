using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B3 RID: 1203
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("A146E1CC-043D-4D33-9B82-16392AD36E7D")]
	[CoClass(typeof(SSOMapper))]
	[ComImport]
	public interface ISSOMapper
	{
		// Token: 0x06002942 RID: 10562
		object[] GetMappingsForWindowsUser(string windowsDomainName, string windowsUserName, string applicationName);

		// Token: 0x06002943 RID: 10563
		object[] GetMappingsForExternalUser(string applicationName, string externalUserName);

		// Token: 0x06002944 RID: 10564
		void SetWindowsPassword(string windowsPassword);

		// Token: 0x06002945 RID: 10565
		void SetExternalCredentials(string applicationName, string externalUserName, ref string[] externalCredentials);

		// Token: 0x06002946 RID: 10566
		void GetApplications(out string[] applications, out string[] descriptions, out string[] contactInfo);

		// Token: 0x06002947 RID: 10567
		void GetFieldInfo(string applicationName, out string[] labels, out int[] flags);
	}
}
