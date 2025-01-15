using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000223 RID: 547
	internal sealed class AuthenticationExtensionFactory
	{
		// Token: 0x060013C7 RID: 5063 RVA: 0x0004A198 File Offset: 0x00048398
		public static IAuthenticationExtension2 GetAuthenticationExtension(AuthenticationType authType)
		{
			object newInstanceExtensionClass = ExtensionClassFactory.GetNewInstanceExtensionClass(WebConfigUtil.WebServerAuthMode.ToString(), "Authentication");
			IWindowsAuthenticationExtension windowsAuthenticationExtension = newInstanceExtensionClass as IWindowsAuthenticationExtension;
			IAuthenticationExtension2 authenticationExtension;
			if (windowsAuthenticationExtension != null)
			{
				authenticationExtension = new WindowsAuthenticationBackCompatProxy(windowsAuthenticationExtension);
			}
			else
			{
				IAuthenticationExtension authenticationExtension2 = newInstanceExtensionClass as IAuthenticationExtension;
				if (authenticationExtension2 != null)
				{
					authenticationExtension = new AuthenticationExtensionBackCompatProxy(authenticationExtension2);
				}
				else
				{
					authenticationExtension = (IAuthenticationExtension2)newInstanceExtensionClass;
				}
			}
			if (authenticationExtension == null)
			{
				throw new ServerConfigurationErrorException("Could not load Authentication extension");
			}
			return authenticationExtension;
		}

		// Token: 0x040006F5 RID: 1781
		internal const string RSSharePointWindowsAuthenticationExtensionClassName = "Microsoft.ReportingServices.SharePoint.Server.SharePointWindowsAuthenticationExtension";

		// Token: 0x040006F6 RID: 1782
		internal const string RSSharePointTrustedUserAuthenticationExtensionClassName = "Microsoft.ReportingServices.SharePoint.Server.SharePointTrustedUserAuthenticationExtension";
	}
}
