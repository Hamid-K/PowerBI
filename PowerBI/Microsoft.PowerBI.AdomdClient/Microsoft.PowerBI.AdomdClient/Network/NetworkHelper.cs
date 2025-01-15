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
		// Token: 0x06000F8E RID: 3982 RVA: 0x0003597C File Offset: 0x00033B7C
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

		// Token: 0x06000F8F RID: 3983 RVA: 0x000359D0 File Offset: 0x00033BD0
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

		// Token: 0x04000A29 RID: 2601
		private const uint MAX_COMPUTERNAME_LENGTH = 15U;

		// Token: 0x04000A2A RID: 2602
		private const int DEFAULT_USERNAME_SIZE = 64;
	}
}
