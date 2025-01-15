using System;
using System.Security.Principal;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000068 RID: 104
	public sealed class ImpersonationContext : IDisposable
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000AC38 File Offset: 0x00008E38
		public ImpersonationContext(string userName, SecureStringWrapper password, string domain)
		{
			this.m_oldContext = WindowsIdentity.Impersonate(IntPtr.Zero);
			try
			{
				LoginUtil.Login(userName, password, domain).Impersonate();
			}
			catch (Exception ex)
			{
				throw new LogonFailedException(ex, userName);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000AC84 File Offset: 0x00008E84
		public ImpersonationContext(string userName, string password, string domain)
		{
			this.m_oldContext = WindowsIdentity.Impersonate(IntPtr.Zero);
			try
			{
				LoginUtil.Login(userName, password, domain).Impersonate();
			}
			catch (Exception ex)
			{
				throw new LogonFailedException(ex, userName);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public ImpersonationContext(UserContext userContext)
		{
			RSTrace.SecurityTracer.Assert(userContext != null);
			if (userContext.AuthenticationType == AuthenticationType.Federation)
			{
				this.m_oldContext = WindowsIdentity.Impersonate(IntPtr.Zero);
				WindowsIdentity windowsIdentity = userContext.GetWindowsIdentity();
				if (windowsIdentity != null)
				{
					windowsIdentity.Impersonate();
					return;
				}
			}
			else
			{
				WindowsIdentity windowsIdentity2 = userContext.GetWindowsIdentity();
				if (windowsIdentity2 != null)
				{
					this.m_oldContext = WindowsIdentity.Impersonate(IntPtr.Zero);
					windowsIdentity2.Impersonate();
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AD3D File Offset: 0x00008F3D
		public ImpersonationContext()
		{
			this.m_oldContext = WindowsIdentity.Impersonate(IntPtr.Zero);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000AD55 File Offset: 0x00008F55
		public void Dispose()
		{
			if (this.m_oldContext != null)
			{
				this.m_oldContext.Undo();
			}
		}

		// Token: 0x04000178 RID: 376
		private WindowsImpersonationContext m_oldContext;

		// Token: 0x020000EE RID: 238
		public enum CredentialsType
		{
			// Token: 0x040004AF RID: 1199
			None,
			// Token: 0x040004B0 RID: 1200
			DataSource,
			// Token: 0x040004B1 RID: 1201
			Catalog,
			// Token: 0x040004B2 RID: 1202
			Unattended
		}
	}
}
