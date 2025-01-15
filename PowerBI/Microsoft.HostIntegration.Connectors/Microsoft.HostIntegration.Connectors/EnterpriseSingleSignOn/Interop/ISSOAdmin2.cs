using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A8 RID: 1192
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("CB878AF5-9BE9-4451-9492-9D228A3B65E7")]
	[CoClass(typeof(SSOAdmin))]
	[ComImport]
	public interface ISSOAdmin2
	{
		// Token: 0x06002922 RID: 10530
		void GetGlobalInfo(out int flags, out int auditAppDeleteMax, out int auditMappingDeleteMax, out int auditNtpLookupMax, out int auditXpLookupMax, out int ticketTimeout, out int credCacheTimeout, out string secretServer, out string SSOAdminGroup, out string affiliateAppMgrGroup);

		// Token: 0x06002923 RID: 10531
		void UpdateGlobalInfo(int flags, int flagMask, ref int auditAppDeleteMax, ref int auditMappingDeleteMax, ref int auditNtpLookupMax, ref int auditXpLookupMax, ref int ticketTimeout, ref int credCacheTimeout, string secretServer, string SSOAdminGroup, string affiliateAppMgrGroup);

		// Token: 0x06002924 RID: 10532
		void CreateApplication(string applicationName, string description, string contactInfo, string userGroupName, string adminGroupName, int flags, int numFields);

		// Token: 0x06002925 RID: 10533
		void DeleteApplication(string applicationName);

		// Token: 0x06002926 RID: 10534
		void GetApplicationInfo(string applicationName, out string description, out string contactInfo, out string userGroupName, out string adminGroupName, out int flags, out int numFields);

		// Token: 0x06002927 RID: 10535
		void UpdateApplication(string applicationName, string description, string contactInfo, string userGroupName, string adminGroupName, int flags, int flagMask);

		// Token: 0x06002928 RID: 10536
		void PurgeCacheForApplication(string applicationName);

		// Token: 0x06002929 RID: 10537
		void CreateFieldInfo(string applicationName, string label, int flags);

		// Token: 0x0600292A RID: 10538
		void GetApplicationInfo2(string applicationName, IPropertyBag appInfoProps);

		// Token: 0x0600292B RID: 10539
		void UpdateApplication2(string applicationName, IPropertyBag appInfoProps);
	}
}
