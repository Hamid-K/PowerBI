using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;

namespace Microsoft.PowerBI.ReportServer.WebApi.Logon
{
	// Token: 0x02000031 RID: 49
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public class UserService : IUserService
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00004756 File Offset: 0x00002956
		public WindowsIdentity Logon(UserCredentials usercredentials)
		{
			return this.Logon(usercredentials.UserNameOnly, usercredentials.Password, usercredentials.DomainName);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004770 File Offset: 0x00002970
		public string GetUserPrincipalName(IIdentity identity)
		{
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				using (windowsIdentity.Impersonate())
				{
					StringBuilder stringBuilder = new StringBuilder(1024);
					int capacity = stringBuilder.Capacity;
					if (UserService.GetUserNameEx(UserService.ExtendedNameFormat.NameUserPrincipal, stringBuilder, ref capacity) != 0)
					{
						return stringBuilder.ToString();
					}
					throw new Exception("Cannot resolve UPN for user " + identity.Name);
				}
			}
			return identity.Name;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000047F0 File Offset: 0x000029F0
		public bool IsWindowsIdentity(IIdentity identity)
		{
			return identity is WindowsIdentity;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000047FB File Offset: 0x000029FB
		public IIdentity GetCurrentWindowsIdentity()
		{
			return WindowsIdentity.GetCurrent();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004804 File Offset: 0x00002A04
		private WindowsIdentity Logon(string userName, SecureString userPwd, string domain)
		{
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			WindowsIdentity windowsIdentity;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(userPwd);
				windowsIdentity = UserService.Logon(userName, intPtr, domain);
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

		// Token: 0x060000E6 RID: 230 RVA: 0x00004854 File Offset: 0x00002A54
		[PrincipalPermission(SecurityAction.Demand, Role = "BUILTIN\\Administrators")]
		private static WindowsIdentity Logon(string userName, IntPtr ptrPwd, string domain)
		{
			UserService.SafeUserToken safeUserToken = null;
			WindowsIdentity windowsIdentity;
			try
			{
				if (!UserService.LogonUser(userName, domain, ptrPwd, 8, 0, out safeUserToken))
				{
					throw new Win32Exception();
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

		// Token: 0x060000E7 RID: 231
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int GetUserNameEx(UserService.ExtendedNameFormat nameFormat, StringBuilder userName, ref int userNameSize);

		// Token: 0x060000E8 RID: 232
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool LogonUser(string lpszUsername, string lpszDomain, IntPtr lpszPassword, int dwLogonType, int dwLogonProvider, out UserService.SafeUserToken hToken);

		// Token: 0x02000068 RID: 104
		internal enum ExtendedNameFormat
		{
			// Token: 0x040001D7 RID: 471
			NameUserPrincipal = 8
		}

		// Token: 0x02000069 RID: 105
		internal sealed class SafeUserToken : SafeHandle
		{
			// Token: 0x060001A1 RID: 417 RVA: 0x0000C0AA File Offset: 0x0000A2AA
			public SafeUserToken()
				: base(IntPtr.Zero, true)
			{
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x0000C0CA File Offset: 0x0000A2CA
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			protected override bool ReleaseHandle()
			{
				return UserService.SafeUserToken.CloseHandle(this.handle);
			}

			// Token: 0x060001A4 RID: 420
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			private static extern bool CloseHandle(IntPtr hToken);
		}
	}
}
