using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000384 RID: 900
	internal static class GetComputerInfo
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x00060A10 File Offset: 0x0005EC10
		internal static string GetComputerDomainName(string machine)
		{
			string text;
			GetComputerInfo.GetJoinInformation(machine, out text);
			return text;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00060A28 File Offset: 0x0005EC28
		internal static NetJoinStatus GetJoinInformation(string machine, out string domainOrWorkgroupName)
		{
			IntPtr zero = IntPtr.Zero;
			NetJoinStatus netJoinStatus = NetJoinStatus.NetSetupUnknownStatus;
			try
			{
				int num = NativeMethods.NetGetJoinInformation(machine, out zero, out netJoinStatus);
				if (num != 0)
				{
					Win32Exception ex = new Win32Exception(num);
					string message = ex.Message;
					throw new DataCacheException(message, ex);
				}
				domainOrWorkgroupName = Marshal.PtrToStringAuto(zero);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					int num = NativeMethods.NetApiBufferFree(zero);
				}
			}
			if (domainOrWorkgroupName == null)
			{
				domainOrWorkgroupName = string.Empty;
			}
			return netJoinStatus;
		}
	}
}
