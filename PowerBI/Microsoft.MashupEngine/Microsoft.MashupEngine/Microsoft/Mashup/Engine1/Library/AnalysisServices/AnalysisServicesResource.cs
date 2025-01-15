using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F31 RID: 3889
	internal class AnalysisServicesResource
	{
		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x00162036 File Offset: 0x00160236
		public static ResourceKindInfo ResourceKindInfo
		{
			get
			{
				return AnalysisServicesResource.resourceKindInfo;
			}
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x00162040 File Offset: 0x00160240
		public static bool IsCloudAnalysisServices(IResource resource)
		{
			OAuthResource oauthResource;
			return AnalysisServicesResource.TryCreateCloudAnalysisServicesSettings(OAuthServices.Empty, resource.Path, out oauthResource);
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x00162060 File Offset: 0x00160260
		public static bool ShouldEnforceReadOnlyAccessMode(string serverUri)
		{
			if (!AnalysisServicesResource.IsPowerBIDatasetXmlaServer(serverUri))
			{
				return AnalysisServicesResource.IsAsAzureServer(serverUri) && !serverUri.EndsWith(":rw", StringComparison.OrdinalIgnoreCase);
			}
			if (!serverUri.Contains("?"))
			{
				return true;
			}
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(new Uri(serverUri).Query);
			if (nameValueCollection.AllKeys.Length == 0 || nameValueCollection[null] == null)
			{
				return true;
			}
			string[] array = nameValueCollection[null].Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Equals("readwrite", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x001620FC File Offset: 0x001602FC
		private static bool IsPowerBIDatasetXmlaServer(string serverUri)
		{
			return serverUri.StartsWith("powerbi" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x00162114 File Offset: 0x00160314
		private static bool IsAsAzureServer(string serverUri)
		{
			return serverUri.StartsWith("asazure" + Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0016212C File Offset: 0x0016032C
		private static bool TryCreateCloudAnalysisServicesSettings(OAuthServices services, string server, out OAuthResource resource)
		{
			try
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
				dbConnectionStringBuilder["Data Source"] = Regex.Split(server, "(?<!\\%);")[0];
				dbConnectionStringBuilder["UID"] = string.Empty;
				dbConnectionStringBuilder["PWD"] = "ACCESS_TOKEN";
				CloudConnectionAuthenticationProperties cloudConnectionAuthenticationProperties = new AdomdConnection(dbConnectionStringBuilder.ToString()).GetCloudConnectionAuthenticationProperties();
				resource = AadOAuthProvider.CreateResourceForAuthorizationUrl(services, cloudConnectionAuthenticationProperties.Authority, cloudConnectionAuthenticationProperties.ResourceId, null);
				return true;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["Exception"] = ex;
				services.WriteTrace("AnalysisServicesResource/TryCreateCloudAnalysisServicesSettings", TraceEventType.Error, dictionary, true);
			}
			resource = null;
			return false;
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x001621E4 File Offset: 0x001603E4
		private static OAuthResource AadForAnalysisServicesCloud(OAuthServices services, string url)
		{
			OAuthResource oauthResource;
			if (!AnalysisServicesResource.TryCreateCloudAnalysisServicesSettings(services, url, out oauthResource))
			{
				throw new OAuthException(Strings.AnalysisServices_AadIsPaasOnly);
			}
			return oauthResource;
		}

		// Token: 0x04003892 RID: 14482
		private static ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("AnalysisServices", Strings.AnalysisServicesChallengeTitle, false, false, true, new AuthenticationInfo[]
		{
			new WindowsAuthenticationInfo
			{
				Description = Strings.AnalysisServices_WindowsAuth,
				SupportsAlternateCredentials = true
			},
			new UsernamePasswordAuthenticationInfo(),
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Required,
				Label = Strings.Microsoft_OAuth,
				ProviderFactory = new OAuthFactory(new Func<OAuthServices, string, OAuthResource>(AnalysisServicesResource.AadForAnalysisServicesCloud), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, AnalysisServicesResource.AadForAnalysisServicesCloud(services, url)))
			}
		}, null, new string[] { "EffectiveUserName", "CustomData" }, new DataSourceLocationFactory[] { AnalysisServicesDataSourceLocation.Factory });

		// Token: 0x04003893 RID: 14483
		private const string PowerBIAccessModeReadWrite = "readwrite";

		// Token: 0x04003894 RID: 14484
		private const string PowerBIDatasetXmlaScheme = "powerbi";

		// Token: 0x04003895 RID: 14485
		private const string AsAzureServerXmlaScheme = "asazure";

		// Token: 0x04003896 RID: 14486
		private const string AsAzureAccessModeReadWriteSuffix = ":rw";
	}
}
