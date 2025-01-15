using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A9 RID: 169
	internal class LoginUtil
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x00010590 File Offset: 0x0000E790
		internal static WindowsIdentity Login(string userName, SecureStringWrapper userPwd, string domain)
		{
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			WindowsIdentity windowsIdentity;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(userPwd.GetUnderlyingSecureString());
				windowsIdentity = LoginUtil.Login(userName, intPtr, domain);
			}
			finally
			{
				if (IntPtr.Zero != intPtr)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
			}
			return windowsIdentity;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000105E4 File Offset: 0x0000E7E4
		internal static WindowsIdentity Login(string userName, string userPwd, string domain)
		{
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			WindowsIdentity windowsIdentity;
			try
			{
				intPtr = Marshal.StringToHGlobalUni(userPwd);
				windowsIdentity = LoginUtil.Login(userName, intPtr, domain);
			}
			finally
			{
				if (IntPtr.Zero != intPtr)
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
			}
			return windowsIdentity;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00010634 File Offset: 0x0000E834
		private static WindowsIdentity Login(string userName, IntPtr ptrPwd, string domain)
		{
			LoginUtil.SafeUserToken safeUserToken = null;
			WindowsIdentity windowsIdentity;
			try
			{
				if (!LoginUtil.LogonUser(userName, domain, ptrPwd, 2, 0, out safeUserToken))
				{
					Win32Exception ex = new Win32Exception();
					if (ex.NativeErrorCode != 1380 && ex.NativeErrorCode != 1385 && (ex.NativeErrorCode & 65535) != 830)
					{
						throw ex;
					}
					if (!LoginUtil.LogonUser(userName, domain, ptrPwd, 4, 0, out safeUserToken))
					{
						throw new Win32Exception();
					}
				}
				windowsIdentity = new WindowsIdentity(safeUserToken.DangerousGetHandle());
			}
			finally
			{
				if (safeUserToken != null && !safeUserToken.IsInvalid)
				{
					safeUserToken.Close();
				}
			}
			return windowsIdentity;
		}

		// Token: 0x06000559 RID: 1369
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool LogonUser(string lpszUsername, string lpszDomain, IntPtr lpszPassword, int dwLogonType, int dwLogonProvider, out LoginUtil.SafeUserToken hToken);

		// Token: 0x0600055A RID: 1370 RVA: 0x000106CC File Offset: 0x0000E8CC
		internal static SecurityIdentifier GetAccountSID(string accountName)
		{
			NTAccount ntaccount = new NTAccount(accountName);
			if ("LocalSystem".Equals(accountName, StringComparison.OrdinalIgnoreCase))
			{
				return new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			}
			return (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001070C File Offset: 0x0000E90C
		internal static NTAccount CreateWellKnownAccount(WellKnownSidType sidType)
		{
			return (NTAccount)new SecurityIdentifier(sidType, null).Translate(typeof(NTAccount));
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001072C File Offset: 0x0000E92C
		internal static bool IsWellKnownAccount(string accountName)
		{
			bool flag = false;
			try
			{
				SecurityIdentifier accountSID = LoginUtil.GetAccountSID(accountName);
				if (accountSID.IsWellKnown(WellKnownSidType.NetworkServiceSid) || accountSID.IsWellKnown(WellKnownSidType.LocalSystemSid) || accountSID.IsWellKnown(WellKnownSidType.LocalServiceSid))
				{
					flag = true;
				}
			}
			catch (Exception)
			{
			}
			return flag;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00010778 File Offset: 0x0000E978
		public static string MakeAccount(string domain, string userName)
		{
			return domain + "\\" + userName;
		}

		// Token: 0x040002FE RID: 766
		internal const string NTServiceDomain = "NT Service";

		// Token: 0x040002FF RID: 767
		internal const string NTAuthorityDomain = "NT Authority";

		// Token: 0x04000300 RID: 768
		internal const string LocalServiceUserName = "LocalService";

		// Token: 0x04000301 RID: 769
		internal const string NetworkServiceUserName = "NetworkService";

		// Token: 0x04000302 RID: 770
		public static readonly string LocalServiceAccount = LoginUtil.MakeAccount("NT Authority", "LocalService");

		// Token: 0x04000303 RID: 771
		public static readonly string NetworkServiceAccount = LoginUtil.MakeAccount("NT Authority", "NetworkService");

		// Token: 0x04000304 RID: 772
		internal const string LocalSystemWmiAccount = "LocalSystem";

		// Token: 0x02000102 RID: 258
		internal sealed class SafeUserToken : SafeHandle
		{
			// Token: 0x06000800 RID: 2048 RVA: 0x00014F1D File Offset: 0x0001311D
			public SafeUserToken()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x00014F2B File Offset: 0x0001312B
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x00014F3D File Offset: 0x0001313D
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			protected override bool ReleaseHandle()
			{
				return LoginUtil.SafeUserToken.CloseHandle(this.handle);
			}

			// Token: 0x06000803 RID: 2051
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			private static extern bool CloseHandle(IntPtr hToken);
		}
	}
}
