using System;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Owin.Common.Middleware;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x02000066 RID: 102
	internal static class IdentityExtensions
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00014E74 File Offset: 0x00013074
		public static UserContext ToUserContext(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				object obj = windowsIdentity.Token;
				return new UserContext(identity.Name, obj, AuthenticationType.Windows);
			}
			if (identity.AuthenticationType.Equals("Basic", StringComparison.OrdinalIgnoreCase))
			{
				return new UserContext(identity.Name, null, AuthenticationType.Windows);
			}
			if (identity.IsOAuthAuthenticated())
			{
				return new UserContext(identity.Name, null, AuthenticationType.OAuth);
			}
			throw new InvalidOperationException("Unknown Identity Type");
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00014EF8 File Offset: 0x000130F8
		public static UserContext ToUserContext(this IUserContextContainer identity)
		{
			if (identity != null)
			{
				RsUserContext rsUserContext = (RsUserContext)identity.UserContext;
				return new UserContext(rsUserContext.UserName, rsUserContext.Token, rsUserContext.RSInterfacesAuthenticationType);
			}
			throw new InvalidOperationException("Unknown Identity Type");
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00014F36 File Offset: 0x00013136
		public static bool IsOAuthAuthenticated(this IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return identity.AuthenticationType.Equals("JWT", StringComparison.OrdinalIgnoreCase) || identity.AuthenticationType.Equals("Federation", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00014F6C File Offset: 0x0001316C
		public static AuthenticationType ToAuthenticationType(this IIdentity identity)
		{
			IUserContextContainer userContextContainer = identity as IUserContextContainer;
			if (userContextContainer == null)
			{
				return identity.ToUserContext().AuthenticationType;
			}
			return userContextContainer.ToUserContext().AuthenticationType;
		}
	}
}
