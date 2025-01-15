using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200038F RID: 911
	internal class ServiceInfo
	{
		// Token: 0x06002027 RID: 8231 RVA: 0x00061DD8 File Offset: 0x0005FFD8
		private static IntPtr GetServiceHandle(string machine, string serviceName)
		{
			IntPtr intPtr = NativeMethods.OpenSCManager(machine, null, 983103U);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			IntPtr intPtr2 = NativeMethods.OpenService(intPtr, serviceName, 2U);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			return intPtr2;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00061E24 File Offset: 0x00060024
		internal static void SetServiceSID(string machine, string serviceName)
		{
			IntPtr serviceHandle = ServiceInfo.GetServiceHandle(machine, serviceName);
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			Marshal.StructureToPtr(new SERVICE_SID_INFO
			{
				dwServiceSidType = 1U
			}, intPtr, false);
			if (!NativeMethods.ChangeServiceConfig2(serviceHandle, 5, intPtr))
			{
				Marshal.FreeHGlobal(intPtr);
				throw new Win32Exception();
			}
			if (!NativeMethods.CloseServiceHandle(serviceHandle))
			{
				Marshal.FreeHGlobal(intPtr);
				throw new Win32Exception();
			}
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x040012FD RID: 4861
		private const uint SERVICE_SID_TYPE_UNRESTRICTED = 1U;

		// Token: 0x040012FE RID: 4862
		private const int SERVICE_CONFIG_SERVICE_SID_INFO = 5;

		// Token: 0x02000390 RID: 912
		private enum ServiceAccess : uint
		{
			// Token: 0x04001300 RID: 4864
			QUERY_CONFIG = 1U,
			// Token: 0x04001301 RID: 4865
			CHANGE_CONFIG
		}
	}
}
