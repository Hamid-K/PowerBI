using System;
using System.Runtime.InteropServices;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F5 RID: 245
	internal sealed class InOutOfProcHelper
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0004C9BC File Offset: 0x0004ABBC
		private InOutOfProcHelper()
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return;
			}
			IntPtr moduleHandle = SafeNativeMethods.GetModuleHandle(null);
			if (IntPtr.Zero != moduleHandle)
			{
				if (IntPtr.Zero != SafeNativeMethods.GetProcAddress(moduleHandle, "_______SQL______Process______Available@0"))
				{
					this._inProc = true;
					return;
				}
				if (IntPtr.Zero != SafeNativeMethods.GetProcAddress(moduleHandle, "______SQL______Process______Available"))
				{
					this._inProc = true;
				}
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0004CA2D File Offset: 0x0004AC2D
		internal static bool InProc
		{
			get
			{
				return InOutOfProcHelper.SingletonInstance._inProc;
			}
		}

		// Token: 0x040007DF RID: 2015
		private static readonly InOutOfProcHelper SingletonInstance = new InOutOfProcHelper();

		// Token: 0x040007E0 RID: 2016
		private bool _inProc;
	}
}
