using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.AnalysisServices.Interop;

namespace Microsoft.AnalysisServices.Network
{
	// Token: 0x02000117 RID: 279
	internal static class NetworkHelper
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x000385B0 File Offset: 0x000367B0
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

		// Token: 0x0600102A RID: 4138 RVA: 0x00038604 File Offset: 0x00036804
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

		// Token: 0x040009EF RID: 2543
		private const uint MAX_COMPUTERNAME_LENGTH = 15U;

		// Token: 0x040009F0 RID: 2544
		private const int DEFAULT_USERNAME_SIZE = 64;
	}
}
