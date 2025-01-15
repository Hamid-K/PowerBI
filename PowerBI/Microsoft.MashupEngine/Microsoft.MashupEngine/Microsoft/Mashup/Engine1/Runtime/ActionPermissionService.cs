using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001255 RID: 4693
	internal static class ActionPermissionService
	{
		// Token: 0x06007BAB RID: 31659 RVA: 0x001AA834 File Offset: 0x001A8A34
		public static bool IsActionPermitted(this IEngineHost engineHost, IResource resource)
		{
			if (engineHost != null)
			{
				IActionPermissionService actionPermissionService = engineHost.QueryService<IActionPermissionService>();
				if (actionPermissionService != null)
				{
					return actionPermissionService.IsActionPermitted(resource);
				}
			}
			return false;
		}

		// Token: 0x06007BAC RID: 31660 RVA: 0x001AA857 File Offset: 0x001A8A57
		public static void VerifyActionPermitted(this IEngineHost hostEnvironment, IResource resource)
		{
			if (!hostEnvironment.IsActionPermitted(resource))
			{
				throw DataSourceException.NewUnpermittedActionError(hostEnvironment, resource, Strings.ActionNotPermitted, null, null);
			}
		}

		// Token: 0x06007BAD RID: 31661 RVA: 0x001AA878 File Offset: 0x001A8A78
		public static bool AreActionsAvailable(this IEngineHost engineHost)
		{
			if (engineHost != null)
			{
				IActionPermissionService actionPermissionService = engineHost.QueryService<IActionPermissionService>();
				if (actionPermissionService != null)
				{
					return actionPermissionService.AreActionsAvailable;
				}
			}
			return false;
		}
	}
}
