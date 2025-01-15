using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002EC RID: 748
	public static class ExtendedWindowsIdentity
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x00044E60 File Offset: 0x00043060
		public static WindowsImpersonationContext ImpersonateUser([NotNull] string username, string domainName, string password, ImpersonateUserOptions options)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(username, "username");
			LogonType logonType;
			LogonProvider logonProvider;
			SecurityImpersonationLevel securityImpersonationLevel;
			if (options != ImpersonateUserOptions.Interactive)
			{
				if (options != ImpersonateUserOptions.Outbound)
				{
					throw new ArgumentOutOfRangeException("options");
				}
				logonType = LogonType.LOGON32_LOGON_NEW_CREDENTIALS;
				logonProvider = LogonProvider.LOGON32_PROVIDER_WINNT50;
				securityImpersonationLevel = SecurityImpersonationLevel.SecurityImpersonation;
			}
			else
			{
				logonType = LogonType.LOGON32_LOGON_INTERACTIVE;
				logonProvider = LogonProvider.LOGON32_PROVIDER_DEFAULT;
				securityImpersonationLevel = SecurityImpersonationLevel.SecurityImpersonation;
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			WindowsImpersonationContext windowsImpersonationContext;
			try
			{
				if (!ExtendedWindowsIdentity.NativeMethods.LogonUser(username, domainName, password, (int)logonType, (int)logonProvider, ref zero))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Call to LogonUser() failed");
				}
				if (!ExtendedWindowsIdentity.NativeMethods.DuplicateToken(zero, (int)securityImpersonationLevel, ref zero2))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Call to DuplicateToken() failed");
				}
				windowsImpersonationContext = new WindowsIdentity(zero2).Impersonate();
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					ExtendedWindowsIdentity.NativeMethods.CloseHandle(zero);
				}
				if (zero2 != IntPtr.Zero)
				{
					ExtendedWindowsIdentity.NativeMethods.CloseHandle(zero2);
				}
			}
			return windowsImpersonationContext;
		}

		// Token: 0x0200078C RID: 1932
		private static class NativeMethods
		{
			// Token: 0x060030B4 RID: 12468
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

			// Token: 0x060030B5 RID: 12469
			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

			// Token: 0x060030B6 RID: 12470
			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool RevertToSelf();

			// Token: 0x060030B7 RID: 12471
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern bool CloseHandle(IntPtr handle);
		}
	}
}
