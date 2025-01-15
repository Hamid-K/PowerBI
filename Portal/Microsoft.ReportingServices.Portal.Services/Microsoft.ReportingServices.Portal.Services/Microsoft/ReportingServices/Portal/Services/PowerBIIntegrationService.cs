using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Hybrid.OAuth;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Model;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x0200001B RID: 27
	internal sealed class PowerBIIntegrationService : IPowerBIIntegrationService
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000C2A7 File Offset: 0x0000A4A7
		internal PowerBIIntegrationService(ILogger logger)
		{
			this.logger = logger;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000C2C1 File Offset: 0x0000A4C1
		public bool IsPowerBIEnabled()
		{
			return Globals.Configuration.EnablePowerBIFeatures;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public PowerBIUserInfo GetUserInfo(IPrincipal userPrincipal)
		{
			Microsoft.ReportingServices.Library.Soap.Property[] array = new Microsoft.ReportingServices.Library.Soap.Property[]
			{
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = this.aadAuthToken
				}
			};
			GetUserSettingsAction getUserSettingsAction = ServicesUtil.CreateRsService(userPrincipal).GetUserSettingsAction;
			getUserSettingsAction.ActionParameters.RequestedProperties = array;
			getUserSettingsAction.Execute();
			Microsoft.ReportingServices.Library.Soap.Property[] userProperties = getUserSettingsAction.ActionParameters.UserProperties;
			Microsoft.ReportingServices.Library.Soap.Property property = ((userProperties == null) ? null : userProperties.FirstOrDefault<Microsoft.ReportingServices.Library.Soap.Property>());
			string text = null;
			PowerBIUserStatus powerBIUserStatus = PowerBIUserStatus.SignedOut;
			if (property != null)
			{
				string value = property.Value;
				if (!string.IsNullOrEmpty(value))
				{
					ServiceToken serviceToken = ServiceToken.FromJson(value);
					if (serviceToken != null && serviceToken.IdToken != null)
					{
						text = AadOAuthHelper.GetIdTokenFromResponseString(serviceToken.IdToken).UniqueName;
						powerBIUserStatus = PowerBIUserStatus.SignedIn;
					}
				}
				else
				{
					powerBIUserStatus = PowerBIUserStatus.Expired;
				}
			}
			return new PowerBIUserInfo
			{
				UserName = text,
				Status = powerBIUserStatus
			};
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000C38C File Offset: 0x0000A58C
		public bool LogOutUser(IPrincipal userPrincipal)
		{
			bool flag;
			try
			{
				Microsoft.ReportingServices.Library.Soap.Property[] array = new Microsoft.ReportingServices.Library.Soap.Property[]
				{
					new Microsoft.ReportingServices.Library.Soap.Property
					{
						Name = this.aadAuthToken
					}
				};
				SetUserSettingsAction setUserSettingsAction = ServicesUtil.CreateRsService(userPrincipal).SetUserSettingsAction;
				setUserSettingsAction.ActionParameters.Properties = array;
				setUserSettingsAction.Execute();
				flag = true;
			}
			catch (Exception ex)
			{
				this.logger.Trace(TraceLevel.Error, "Logout failed with error: {0}" + ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000079 RID: 121
		private readonly string aadAuthToken = "AADAuthToken";

		// Token: 0x0400007A RID: 122
		private readonly ILogger logger;
	}
}
