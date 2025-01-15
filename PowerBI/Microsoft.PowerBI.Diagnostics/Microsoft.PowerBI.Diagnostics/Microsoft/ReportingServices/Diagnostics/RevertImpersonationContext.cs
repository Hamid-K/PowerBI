using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000015 RID: 21
	internal static class RevertImpersonationContext
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000032C4 File Offset: 0x000014C4
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

		// Token: 0x0600006E RID: 110 RVA: 0x000032F8 File Offset: 0x000014F8
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

		// Token: 0x0400005E RID: 94
		internal const int SkipStackFrames = 8;
	}
}
