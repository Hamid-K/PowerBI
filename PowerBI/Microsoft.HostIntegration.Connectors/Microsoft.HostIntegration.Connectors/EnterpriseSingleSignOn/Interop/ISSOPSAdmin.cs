using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004AA RID: 1194
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("C35718F9-C35C-4cd4-8978-2B4CE1792F1B")]
	[CoClass(typeof(SSOPSAdmin))]
	[ComImport]
	public interface ISSOPSAdmin
	{
		// Token: 0x0600292D RID: 10541
		void AssignApplicationToAdapter(string applicationName, string adapterName);

		// Token: 0x0600292E RID: 10542
		void RemoveApplicationFromAdapter(string applicationName);

		// Token: 0x0600292F RID: 10543
		void GetApplicationsForAdapter(string adapterName, out string[] applications);

		// Token: 0x06002930 RID: 10544
		void AssignAdapterToAdapterGroup(string adapterName, string adapterGroupName);

		// Token: 0x06002931 RID: 10545
		void RemoveAdapterFromAdapterGroup(string adapterName);

		// Token: 0x06002932 RID: 10546
		void GetAdaptersForAdapterGroup(string adapterGroupName, out string[] adapters);

		// Token: 0x06002933 RID: 10547
		void SetAdapterProperties(string adapterName, IPropertyBag properties);

		// Token: 0x06002934 RID: 10548
		void ClearNotificationQueues(string adapterName);

		// Token: 0x06002935 RID: 10549
		void ClearDampingTable();
	}
}
