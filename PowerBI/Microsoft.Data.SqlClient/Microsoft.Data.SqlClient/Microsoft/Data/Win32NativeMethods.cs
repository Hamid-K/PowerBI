using System;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data
{
	// Token: 0x0200000E RID: 14
	internal static class Win32NativeMethods
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x0000AA64 File Offset: 0x00008C64
		internal static bool IsTokenRestrictedWrapper(IntPtr token)
		{
			bool flag;
			uint num = SNINativeMethodWrapper.UnmanagedIsTokenRestricted(token, out flag);
			if (num != 0U)
			{
				Marshal.ThrowExceptionForHR((int)num);
			}
			return flag;
		}
	}
}
