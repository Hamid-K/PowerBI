using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200000D RID: 13
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public sealed class ImpersonationContext : IDisposable
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000028F9 File Offset: 0x00000AF9
		public static IDisposable EnterServiceAccountContext()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal).Assert();
			CodeAccessPermission.RevertAssert();
			return new ImpersonationContext(IntPtr.Zero);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002919 File Offset: 0x00000B19
		public static IDisposable EnterUserContext(AccountCredentials accountCredentials)
		{
			if (accountCredentials == null)
			{
				throw new ArgumentException("Null account credentials");
			}
			return new ImpersonationContext(accountCredentials);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000292F File Offset: 0x00000B2F
		public static IDisposable EnterUserContext(IntPtr userToken)
		{
			if (userToken == IntPtr.Zero)
			{
				throw new ArgumentException("[0] is not a valid USER token.  If you want to revert to service credentials, use EnterServiceAccountContext()");
			}
			return new ImpersonationContext(userToken);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002950 File Offset: 0x00000B50
		private ImpersonationContext(AccountCredentials accountCredentials)
		{
			if (!ImpersonationContext.LogonUser(accountCredentials.UserId, accountCredentials.Domain, accountCredentials.Password, 8, 0, out this._handle))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, "Could not impersonate the user.  LogonUser returned error code {0}.", lastWin32Error));
			}
			this._context = WindowsIdentity.Impersonate(this._handle.DangerousGetHandle());
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029BB File Offset: 0x00000BBB
		private ImpersonationContext(IntPtr userToken)
		{
			this._context = WindowsIdentity.Impersonate(userToken);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029D0 File Offset: 0x00000BD0
		public void Dispose()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (this._context != null)
				{
					this._context.Undo();
					this._context.Dispose();
				}
				if (this._handle != null)
				{
					this._handle.Dispose();
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A20 File Offset: 0x00000C20
		~ImpersonationContext()
		{
			if (!this._disposed)
			{
				Logger.Error("Impersonation Context Not Disposed - (use all instances of Impersonation context in a using() block)", Array.Empty<object>());
			}
		}

		// Token: 0x06000042 RID: 66
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

		// Token: 0x04000045 RID: 69
		private readonly SafeTokenHandle _handle;

		// Token: 0x04000046 RID: 70
		private readonly WindowsImpersonationContext _context;

		// Token: 0x04000047 RID: 71
		private bool _disposed;

		// Token: 0x04000048 RID: 72
		private const int Logon32_Logon_Network_Cleartext = 8;

		// Token: 0x04000049 RID: 73
		public static readonly IntPtr SystemContext = IntPtr.Zero;
	}
}
