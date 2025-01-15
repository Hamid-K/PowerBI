using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200000D RID: 13
	internal class DirectoryServicesWrapper : IDirectoryServicesWrapper
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000028C5 File Offset: 0x00000AC5
		public bool CheckAccess(SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, string userName)
		{
			return Native.CheckAccess(itemType, secDesc, ref rightsMask, userName);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028D1 File Offset: 0x00000AD1
		public bool CheckAccess(SecurityItemType itemType, byte[] secDesc, ref uint rightsMask, IntPtr userToken)
		{
			return Native.CheckAccess(itemType, secDesc, ref rightsMask, userToken);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028DD File Offset: 0x00000ADD
		public bool IsAdmin(string userName)
		{
			return Native.IsAdmin(userName);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028E5 File Offset: 0x00000AE5
		public bool IsAdmin(IntPtr userToken)
		{
			return Native.IsAdmin(userToken);
		}
	}
}
