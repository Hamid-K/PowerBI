using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient.Interop;

namespace Microsoft.AnalysisServices.AdomdClient.Network
{
	// Token: 0x02000122 RID: 290
	internal static class NetworkHelper
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x00035CAC File Offset: 0x00033EAC
		public static string GetComputerName(ComputerNameFormat nameFormat)
		{
			uint num = 15U;
			StringBuilder stringBuilder = new StringBuilder((int)num);
			if (!NativeMethods.GetComputerNameEx(nameFormat, stringBuilder, ref num))
			{
				if (Marshal.GetLastWin32Error() != 234)
				{
					throw new Win32Exception();
				}
				stringBuilder = new StringBuilder((int)num);
				if (!NativeMethods.GetComputerNameEx(nameFormat, stringBuilder, ref num))
				{
					throw new Win32Exception();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00035D00 File Offset: 0x00033F00
		public static string GetUserName(ExtendedNameFormat nameFormat)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			uint num = 64U;
			if (!NativeMethods.GetUserNameEx(nameFormat, stringBuilder, ref num))
			{
				if (Marshal.GetLastWin32Error() != 234)
				{
					throw new Win32Exception();
				}
				stringBuilder = new StringBuilder((int)num);
				if (!NativeMethods.GetUserNameEx(nameFormat, stringBuilder, ref num))
				{
					throw new Win32Exception();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A36 RID: 2614
		private const uint MAX_COMPUTERNAME_LENGTH = 15U;

		// Token: 0x04000A37 RID: 2615
		private const int DEFAULT_USERNAME_SIZE = 64;
	}
}
