using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200008D RID: 141
	public static class RevertImpersonationContext
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x0000D298 File Offset: 0x0000B498
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static void Run(ContextBody callback)
		{
			SecurityContext.Run(SecurityContext.Capture(), delegate(object state)
			{
				WindowsImpersonationContext windowsImpersonationContext = null;
				try
				{
					try
					{
						windowsImpersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
						callback();
					}
					finally
					{
						if (windowsImpersonationContext != null)
						{
							windowsImpersonationContext.Undo();
						}
					}
				}
				catch
				{
					throw;
				}
			}, null);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public static void RunFromRestrictedCasContext(ContextBody callback)
		{
			SecurityContext.Run(SecurityContext.Capture(), delegate(object state)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal).Assert();
				WindowsImpersonationContext windowsImpersonationContext = null;
				try
				{
					try
					{
						windowsImpersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
						CodeAccessPermission.RevertAssert();
						callback();
					}
					finally
					{
						if (windowsImpersonationContext != null)
						{
							windowsImpersonationContext.Undo();
						}
					}
				}
				catch
				{
					throw;
				}
			}, null);
		}

		// Token: 0x0400028F RID: 655
		internal const int SkipStackFrames = 8;
	}
}
