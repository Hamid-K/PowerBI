using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200000F RID: 15
	internal interface IDirectoryServicesWrapper
	{
		// Token: 0x06000037 RID: 55
		bool CheckAccess(SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, string userName);

		// Token: 0x06000038 RID: 56
		bool CheckAccess(SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, IntPtr userToken);

		// Token: 0x06000039 RID: 57
		bool IsAdmin(string userName);

		// Token: 0x0600003A RID: 58
		bool IsAdmin(IntPtr userToken);
	}
}
