using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Services.Extensions;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x0200001D RID: 29
	internal static class ServicesUtil
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000C414 File Offset: 0x0000A614
		internal static RSService CreateRsService(IPrincipal principal)
		{
			IUserContextContainer userContextContainer = principal.Identity as IUserContextContainer;
			UserContext userContext;
			if (userContextContainer != null)
			{
				userContext = userContextContainer.ToUserContext();
			}
			else
			{
				userContext = principal.Identity.ToUserContext();
			}
			return new RSService(userContext.UserName, userContext.UserToken, userContext.AuthenticationType, RSRequestInspector.Instance);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000C463 File Offset: 0x0000A663
		internal static ListRolesAction CreateListRolesAction(RSService rsService, SecurityScopeEnum securityScope)
		{
			ListRolesAction listRolesAction = rsService.ListRolesAction;
			listRolesAction.ActionParameters.Scope = securityScope;
			return listRolesAction;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000C477 File Offset: 0x0000A677
		internal static GetSystemPermissionsAction CreateGetSystemPermissionsAction(RSService rsService)
		{
			return rsService.GetSystemPermissionsAction;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000C47F File Offset: 0x0000A67F
		internal static GetPermissionsAction CreateGetItemPermissionsAction(RSService rsService, string itemPath)
		{
			GetPermissionsAction getPermissionsAction = rsService.GetPermissionsAction;
			getPermissionsAction.ActionParameters.ItemPath = itemPath;
			return getPermissionsAction;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000C494 File Offset: 0x0000A694
		internal static string HashBytes(byte[] bytes)
		{
			string text;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				text = string.Concat(from x in sha256Cng.ComputeHash(bytes)
					select x.ToString("x2"));
			}
			return text;
		}
	}
}
