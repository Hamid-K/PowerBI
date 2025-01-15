using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x0200006D RID: 109
	public class OAuthClientConfigurationService : IOAuthClientConfigurationService
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0001520A File Offset: 0x0001340A
		public OAuthClientConfigurationService(IPortalConfigurationManager portalConfigurationManager)
		{
			this._portalConfigurationManager = portalConfigurationManager;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001521C File Offset: 0x0001341C
		public string GetConfigInfoScript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			IOAuthConfiguration oauthConfiguration = this._portalConfigurationManager.Current.OAuthConfiguration;
			stringBuilder.AppendLine("'use strict';");
			if (this.IsOAuthRequired())
			{
				stringBuilder.AppendLine("var __oAuthConfigData__ = ");
				stringBuilder.AppendLine(JsonConvert.SerializeObject(new
				{
					instance = oauthConfiguration.TokenUrl,
					clientId = oauthConfiguration.ClientId,
					tenant = oauthConfiguration.TenantId
				}, Formatting.Indented) + ";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00015294 File Offset: 0x00013494
		public bool IsScriptRequest(string path)
		{
			return !string.IsNullOrEmpty(path) && path.Equals("/assets/js/oauth.js", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000152AC File Offset: 0x000134AC
		public bool IsOAuthRequired()
		{
			AuthenticationTypes authenticationTypes = (AuthenticationTypes)this._portalConfigurationManager.Current.AuthenticationTypes;
			return authenticationTypes.HasFlag(AuthenticationTypes.OAuth);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000152DC File Offset: 0x000134DC
		public IOAuthConfiguration Configuration
		{
			get
			{
				if (this.IsOAuthRequired())
				{
					return this._portalConfigurationManager.Current.OAuthConfiguration;
				}
				return null;
			}
		}

		// Token: 0x040000E1 RID: 225
		private readonly IPortalConfigurationManager _portalConfigurationManager;
	}
}
