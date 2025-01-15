using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A7 RID: 1191
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("C177306C-A7A0-49E1-901E-D2BDFCFF4532")]
	[CoClass(typeof(SSOAdmin))]
	[ComImport]
	public interface ISSOAdmin
	{
		// Token: 0x0600291A RID: 10522
		void GetGlobalInfo(out int flags, out int auditAppDeleteMax, out int auditMappingDeleteMax, out int auditNtpLookupMax, out int auditXpLookupMax, out int ticketTimeout, out int credCacheTimeout, out string secretServer, out string SSOAdminGroup, out string affiliateAppMgrGroup);

		// Token: 0x0600291B RID: 10523
		void UpdateGlobalInfo(int flags, int flagMask, ref int auditAppDeleteMax, ref int auditMappingDeleteMax, ref int auditNtpLookupMax, ref int auditXpLookupMax, ref int ticketTimeout, ref int credCacheTimeout, string secretServer, string SSOAdminGroup, string affiliateAppMgrGroup);

		// Token: 0x0600291C RID: 10524
		void CreateApplication(string applicationName, string description, string contactInfo, string userGroupName, string adminGroupName, int flags, int numFields);

		// Token: 0x0600291D RID: 10525
		void DeleteApplication(string applicationName);

		// Token: 0x0600291E RID: 10526
		void GetApplicationInfo(string applicationName, out string description, out string contactInfo, out string userGroupName, out string adminGroupName, out int flags, out int numFields);

		// Token: 0x0600291F RID: 10527
		void UpdateApplication(string applicationName, string description, string contactInfo, string userGroupName, string adminGroupName, int flags, int flagMask);

		// Token: 0x06002920 RID: 10528
		void PurgeCacheForApplication(string applicationName);

		// Token: 0x06002921 RID: 10529
		void CreateFieldInfo(string applicationName, string label, int flags);
	}
}
