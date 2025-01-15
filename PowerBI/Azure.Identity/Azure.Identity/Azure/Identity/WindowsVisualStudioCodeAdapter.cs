using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Azure.Identity
{
	// Token: 0x0200008D RID: 141
	internal sealed class WindowsVisualStudioCodeAdapter : IVisualStudioCodeAdapter
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public string GetUserSettingsPath()
		{
			return WindowsVisualStudioCodeAdapter.s_userSettingsJsonPath;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		public string GetCredentials(string serviceName, string accountName)
		{
			IntPtr intPtr = WindowsNativeMethods.CredRead(serviceName + "/" + accountName, WindowsNativeMethods.CRED_TYPE.GENERIC);
			string text;
			try
			{
				WindowsNativeMethods.CredentialData credentialData = Marshal.PtrToStructure<WindowsNativeMethods.CredentialData>(intPtr);
				text = Marshal.PtrToStringAnsi(credentialData.CredentialBlob, (int)credentialData.CredentialBlobSize);
			}
			finally
			{
				WindowsNativeMethods.CredFree(intPtr);
			}
			return text;
		}

		// Token: 0x04000295 RID: 661
		private static readonly string s_userSettingsJsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Code", "User", "settings.json");
	}
}
