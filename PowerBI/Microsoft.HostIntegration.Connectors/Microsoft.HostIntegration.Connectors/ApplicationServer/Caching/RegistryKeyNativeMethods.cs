using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200036C RID: 876
	internal class RegistryKeyNativeMethods
	{
		// Token: 0x06001EC4 RID: 7876
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int RegConnectRegistry(string lpmachineName, IntPtr hKey, ref IntPtr phKResult);

		// Token: 0x06001EC5 RID: 7877
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int RegOpenKeyEx(IntPtr hKey, string subKey, uint ulOptions, int samDesired, out IntPtr hkResult);

		// Token: 0x06001EC6 RID: 7878
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern int RegNotifyChangeKeyValue(IntPtr hKey, [MarshalAs(UnmanagedType.Bool)] bool bWatchSubtree, RegChangeNotifyFilter dwNotifyFilter, SafeWaitHandle hEvent, [MarshalAs(UnmanagedType.Bool)] bool fAsynchronous);

		// Token: 0x06001EC7 RID: 7879
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern long RegGetValue(IntPtr hKey, string lpSubKey, string lpValue, uint dwFlags, out uint pdwType, StringBuilder pvData, ref uint pcbData);

		// Token: 0x06001EC8 RID: 7880
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int RegSetValueEx(IntPtr hKey, string lpValueName, int Reserved, RegistryValueKind dwType, string lpData, int cbData);

		// Token: 0x06001EC9 RID: 7881
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x0400116F RID: 4463
		internal const int KEY_QUERY_VALUE = 1;

		// Token: 0x04001170 RID: 4464
		internal const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x04001171 RID: 4465
		internal const int KEY_NOTIFY = 16;

		// Token: 0x04001172 RID: 4466
		internal const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x04001173 RID: 4467
		internal const int KEY_READ = 131097;

		// Token: 0x04001174 RID: 4468
		internal const int KEY_WRITE = 131078;

		// Token: 0x04001175 RID: 4469
		internal const int KEY_WOW64_64KEY = 256;

		// Token: 0x04001176 RID: 4470
		internal const uint RRF_RT_REG_SZ = 2U;

		// Token: 0x04001177 RID: 4471
		internal const int ReadWriteWow64Key = 131359;

		// Token: 0x04001178 RID: 4472
		internal static readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(int.MinValue);

		// Token: 0x04001179 RID: 4473
		internal static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);

		// Token: 0x0400117A RID: 4474
		internal static readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);

		// Token: 0x0400117B RID: 4475
		internal static readonly IntPtr HKEY_USERS = new IntPtr(-2147483645);

		// Token: 0x0400117C RID: 4476
		internal static readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);

		// Token: 0x0400117D RID: 4477
		internal static readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);

		// Token: 0x0400117E RID: 4478
		internal static readonly IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);
	}
}
