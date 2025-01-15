using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200085C RID: 2140
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("A146E1CC-043D-4D33-9B82-16392AD36E7D")]
	[CoClass(typeof(SSOMapper))]
	[ComImport]
	public interface ISSOMapper
	{
		// Token: 0x06004426 RID: 17446
		object[] GetMappingsForWindowsUser(string windowsDomainName, string windowsUserName, string applicationName);

		// Token: 0x06004427 RID: 17447
		object[] GetMappingsForExternalUser(string applicationName, string externalUserName);

		// Token: 0x06004428 RID: 17448
		void SetWindowsPassword(string windowsPassword);

		// Token: 0x06004429 RID: 17449
		void SetExternalCredentials(string applicationName, string externalUserName, ref string[] externalCredentials);

		// Token: 0x0600442A RID: 17450
		void GetApplications(out string[] applications, out string[] descriptions, out string[] contactInfo);

		// Token: 0x0600442B RID: 17451
		void GetFieldInfo(string applicationName, out string[] labels, out int[] flags);
	}
}
