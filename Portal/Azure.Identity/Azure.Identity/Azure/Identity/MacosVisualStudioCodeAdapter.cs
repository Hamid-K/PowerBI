using System;
using System.IO;
using System.Runtime.InteropServices;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x0200006E RID: 110
	internal sealed class MacosVisualStudioCodeAdapter : IVisualStudioCodeAdapter
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0000B194 File Offset: 0x00009394
		public string GetUserSettingsPath()
		{
			return MacosVisualStudioCodeAdapter.s_userSettingsJsonPath;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B19C File Offset: 0x0000939C
		public string GetCredentials(string serviceName, string accountName)
		{
			Argument.AssertNotNullOrEmpty(serviceName, "serviceName");
			Argument.AssertNotNullOrEmpty(accountName, "accountName");
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			string text;
			try
			{
				int num;
				MacosNativeMethods.SecKeychainFindGenericPassword(IntPtr.Zero, serviceName, accountName, out num, out zero, out zero2);
				if (num <= 0)
				{
					throw new InvalidOperationException("No password found");
				}
				text = Marshal.PtrToStringAnsi(zero, num);
			}
			finally
			{
				try
				{
					MacosNativeMethods.SecKeychainItemFreeContent(IntPtr.Zero, zero);
				}
				finally
				{
					MacosNativeMethods.CFRelease(zero2);
				}
			}
			return text;
		}

		// Token: 0x0400022B RID: 555
		private static readonly string s_userSettingsJsonPath = Path.Combine(new string[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
			"Library",
			"Application Support",
			"Code",
			"User",
			"settings.json"
		});
	}
}
