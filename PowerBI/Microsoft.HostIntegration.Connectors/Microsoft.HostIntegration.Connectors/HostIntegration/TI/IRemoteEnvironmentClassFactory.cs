using System;
using System.Runtime.InteropServices;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000740 RID: 1856
	[Guid("DEA3BE3F-35F5-4e83-9C33-77B05BB56116")]
	public interface IRemoteEnvironmentClassFactory
	{
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003A5A RID: 14938
		RemoteEnvironmentClass[] RemoteEnvironmentClasses { get; }

		// Token: 0x06003A5B RID: 14939
		string GetTransportNameByType(long Transport);

		// Token: 0x06003A5C RID: 14940
		IRemoteEnvironmentClass GetRemoteEnvironmentClassByType(long RemoteEnvironmentType);

		// Token: 0x06003A5D RID: 14941
		IRemoteEnvironmentClass GetRemoteEnvironmentClassByID(string RemoteEnvironmentClassID);

		// Token: 0x06003A5E RID: 14942
		void GetVendorInfoList(out Guid[] VendorIDs, out string[] VendorNames, out RemoteEnvironmentClass[] RemoteEnvironmentClassList, out bool HasManagedREC);

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06003A5F RID: 14943
		HostEnvironmentInfo[] HostEnvironmentInfoList { get; }
	}
}
