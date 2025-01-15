using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.Runtime;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000140 RID: 320
	internal static class AsPaasHelper
	{
		// Token: 0x06000FE9 RID: 4073 RVA: 0x000365D0 File Offset: 0x000347D0
		public static string GetResponseFromWebException(WebException ex)
		{
			string text;
			if (((ex != null) ? ex.Response : null) == null)
			{
				text = string.Empty;
			}
			else
			{
				Stream responseStream = ex.Response.GetResponseStream();
				try
				{
					if (responseStream != null)
					{
						using (TextReader textReader = new StreamReader(responseStream))
						{
							return textReader.ReadToEnd();
						}
					}
					text = string.Empty;
				}
				finally
				{
					if (responseStream != null)
					{
						responseStream.Close();
					}
				}
			}
			return text;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003664C File Offset: 0x0003484C
		public static string GetTechnicalDetailsFromPbiSharedResponse(WebResponse response)
		{
			if (response == null)
			{
				return string.Empty;
			}
			string text = response.Headers["RequestId"];
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return RuntimeSR.AsPaasHelper_TechnicalDetails_PbiShared(text, Environment.NewLine);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003668C File Offset: 0x0003488C
		public static string GetTechnicalDetailsWithWorkspaceInfo(string technicalDetails, string workspaceName)
		{
			if (string.IsNullOrEmpty(technicalDetails))
			{
				return technicalDetails;
			}
			string text = ClientHostingManager.MarkAsRestrictedInformation(workspaceName ?? "null", InfoRestrictionType.CCON);
			return RuntimeSR.AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(technicalDetails, text, Environment.NewLine);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000366C0 File Offset: 0x000348C0
		public static string GetTechnicalDetailsFromDataverseResponse(WebResponse response, IDictionary<string, string> requestHeaders)
		{
			string text = ((response != null) ? response.Headers["x-ms-service-request-id"] : null) ?? string.Empty;
			string empty;
			if (requestHeaders == null || !requestHeaders.TryGetValue("x-ms-client-request-id", out empty))
			{
				empty = string.Empty;
			}
			return RuntimeSR.AsPaasHelper_TechnicalDetails_Dataverse(empty, text, Environment.NewLine);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00036714 File Offset: 0x00034914
		public static string GetTechnicalDetailsFromPaasInfraResponse(HttpWebResponse response)
		{
			if (response == null)
			{
				return string.Empty;
			}
			string text = response.Headers["x-ms-root-activity-id"];
			string text2 = response.Headers["x-ms-current-utc-date"];
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				return string.Empty;
			}
			return RuntimeSR.AsPaasHelper_TechnicalDetails_AaPaasInfra(text, text2, Environment.NewLine);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00036770 File Offset: 0x00034970
		public static string GetTechnicalDetailsFromPaasInfraResponse(HttpResponseMessage response)
		{
			if (response == null)
			{
				return string.Empty;
			}
			string httpHeaderValueOrDefault = response.Headers.GetHttpHeaderValueOrDefault("x-ms-root-activity-id", null);
			string httpHeaderValueOrDefault2 = response.Headers.GetHttpHeaderValueOrDefault("x-ms-current-utc-date", null);
			if (string.IsNullOrEmpty(httpHeaderValueOrDefault) || string.IsNullOrEmpty(httpHeaderValueOrDefault2))
			{
				return string.Empty;
			}
			return RuntimeSR.AsPaasHelper_TechnicalDetails_AaPaasInfra(httpHeaderValueOrDefault, httpHeaderValueOrDefault2, Environment.NewLine);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000367CC File Offset: 0x000349CC
		public static string BuildGeneralInfoHeader(string aadClient, int cacheUsageIndication, bool hasUserID, bool isLinkFile, string applicationName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(AsPaasHelper.GetHostProcessInfo());
			stringBuilder.Append(AsPaasHelper.GetHostAssemblyInfo(Assembly.GetExecutingAssembly()));
			stringBuilder.AppendFormat(",AadClient={0}", aadClient);
			stringBuilder.AppendFormat(",CacheUsed={0}", cacheUsageIndication);
			stringBuilder.AppendFormat(",ConStrId={0}", (!hasUserID) ? 1 : 0);
			if (isLinkFile)
			{
				stringBuilder.Append(",LinkFileProtocol=true");
			}
			if (!string.IsNullOrEmpty(applicationName))
			{
				stringBuilder.AppendFormat(",sspropInitApp={0}", applicationName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003685C File Offset: 0x00034A5C
		private static string GetHostProcessInfo()
		{
			if (!string.IsNullOrEmpty(AsPaasHelper.hostProcessInfo))
			{
				return AsPaasHelper.hostProcessInfo;
			}
			int num;
			string text;
			string text2;
			bool flag2;
			bool flag = HostRuntimeHelper.TryGetHostingProcessInfo(out num, out text, out text2, out flag2);
			StringBuilder stringBuilder = new StringBuilder();
			if (flag || !string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat("AppName={0}.exe", text);
			}
			else
			{
				stringBuilder.Append("AppName=");
			}
			if (flag || !string.IsNullOrEmpty(text2))
			{
				stringBuilder.AppendFormat(",AppVer={0}", text2);
			}
			else
			{
				stringBuilder.Append(",AppVer=");
			}
			stringBuilder.AppendFormat(",AppBinaryType={0}", flag2 ? 6U : 0U);
			Version version;
			if (FrameworkRuntimeHelper.TryGetRuntimeVersion(out version))
			{
				stringBuilder.Append(",HostRuntime='");
				if (FrameworkRuntimeHelper.IsNetCoreDomain)
				{
					if (version.Major >= 5)
					{
						stringBuilder.Append(".NET ");
					}
					else
					{
						stringBuilder.Append(".NET Core ");
					}
				}
				else
				{
					stringBuilder.Append(".NET Framework ");
				}
				stringBuilder.AppendFormat("{0}'", version);
			}
			if (flag)
			{
				AsPaasHelper.hostProcessInfo = stringBuilder.ToString();
				return AsPaasHelper.hostProcessInfo;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00036968 File Offset: 0x00034B68
		private static string GetHostAssemblyInfo(Assembly assembly)
		{
			if (!string.IsNullOrEmpty(AsPaasHelper.hostAssemblyInfo))
			{
				return AsPaasHelper.hostAssemblyInfo;
			}
			string name = assembly.GetName().Name;
			object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
			string text = ((customAttributes != null && customAttributes.Length != 0) ? ((AssemblyFileVersionAttribute)customAttributes[0]).Version : null);
			bool globalAssemblyCache = assembly.GlobalAssemblyCache;
			StringBuilder stringBuilder = new StringBuilder();
			AsPaasHelper.AddHostAssemblyVersionInfo(stringBuilder, "ManagedVer", name, text, globalAssemblyCache);
			AsPaasHelper.AddHostAssemblyVersionInfo(stringBuilder, "ClientLibVer", name, text, globalAssemblyCache);
			AsPaasHelper.hostAssemblyInfo = stringBuilder.ToString();
			return AsPaasHelper.hostAssemblyInfo;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000369F4 File Offset: 0x00034BF4
		private static void AddHostAssemblyVersionInfo(StringBuilder assemblyInfo, string property, string assemblyName, string version, bool isGacAssembly)
		{
			assemblyInfo.AppendFormat(",{0}={1}", property, assemblyName);
			if (!string.IsNullOrEmpty(version))
			{
				assemblyInfo.AppendFormat(":{0}", version);
			}
			if (isGacAssembly)
			{
				assemblyInfo.Append(" [GAC]");
			}
		}

		// Token: 0x04000AF1 RID: 2801
		private const uint SCS_32BIT_BINARY = 0U;

		// Token: 0x04000AF2 RID: 2802
		private const uint SCS_64BIT_BINARY = 6U;

		// Token: 0x04000AF3 RID: 2803
		private static string hostProcessInfo;

		// Token: 0x04000AF4 RID: 2804
		private static string hostAssemblyInfo;

		// Token: 0x020001F3 RID: 499
		public static class UriScheme
		{
			// Token: 0x04000E86 RID: 3718
			public const string AsAzure = "asazure";

			// Token: 0x04000E87 RID: 3719
			public const string PbiDedicated = "pbidedicated";

			// Token: 0x04000E88 RID: 3720
			public const string PbiPublicXmla = "powerbi";

			// Token: 0x04000E89 RID: 3721
			public const string PbiDataset = "pbiazure";

			// Token: 0x04000E8A RID: 3722
			public const string Link = "link";
		}

		// Token: 0x020001F4 RID: 500
		public static class AixlEndpoints
		{
			// Token: 0x04000E8B RID: 3723
			public const string ResourceForProd = "https://analysis.windows.net/powerbi/api";

			// Token: 0x04000E8C RID: 3724
			public const string ResourceForPpe = "https://analysis.windows-int.net/powerbi/api";
		}

		// Token: 0x020001F5 RID: 501
		public static class ClusterResolutionEndpoint
		{
			// Token: 0x06001498 RID: 5272 RVA: 0x0004657F File Offset: 0x0004477F
			public static bool IsTrustedClusterResolutionEndpoint(Uri dataSource)
			{
				return AsPaasHelper.ClusterResolutionEndpoint.trustedEndpoints.Contains(dataSource.AbsoluteUri);
			}

			// Token: 0x06001499 RID: 5273 RVA: 0x00046594 File Offset: 0x00044794
			private static ICollection<string> GetTrustedEndpoints()
			{
				return new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"https://api.powerbi.com/", "https://df-msit-scus-redirect.analysis.windows.net/", "https://wabi-daily-us-east2-redirect.analysis.windows.net/", "https://dailyapi.powerbi.com/", "https://wabi-staging-us-east-redirect.analysis.windows.net/", "https://dxtapi.powerbi.com/", "https://biazure-int-edog-redirect.analysis-df.windows.net/", "https://powerbiapi.analysis-df.windows.net/", "https://onebox-redirect.analysis.windows-int.net/", "https://api.powerbi.cn",
					"https://api.powerbigov.us", "https://api.high.powerbigov.us", "https://api.mil.powerbigov.us", "https://api.powerbi.eaglex.ic.gov", "https://api.powerbi.microsoft.scloud"
				};
			}

			// Token: 0x04000E8D RID: 3725
			private static readonly ICollection<string> trustedEndpoints = AsPaasHelper.ClusterResolutionEndpoint.GetTrustedEndpoints();

			// Token: 0x04000E8E RID: 3726
			public const string PbiProd = "https://api.powerbi.com/";

			// Token: 0x04000E8F RID: 3727
			public const string PbiMsit = "https://df-msit-scus-redirect.analysis.windows.net/";

			// Token: 0x04000E90 RID: 3728
			public const string PbiDaily = "https://wabi-daily-us-east2-redirect.analysis.windows.net/";

			// Token: 0x04000E91 RID: 3729
			public const string PbiDxt = "https://wabi-staging-us-east-redirect.analysis.windows.net/";

			// Token: 0x04000E92 RID: 3730
			public const string PbiEdog = "https://biazure-int-edog-redirect.analysis-df.windows.net/";

			// Token: 0x04000E93 RID: 3731
			public const string PbiOneBox = "https://onebox-redirect.analysis.windows-int.net/";

			// Token: 0x04000E94 RID: 3732
			public const string PbiDailyApi = "https://dailyapi.powerbi.com/";

			// Token: 0x04000E95 RID: 3733
			public const string PbiDxtApi = "https://dxtapi.powerbi.com/";

			// Token: 0x04000E96 RID: 3734
			public const string PbiEdogApi = "https://powerbiapi.analysis-df.windows.net/";

			// Token: 0x04000E97 RID: 3735
			public const string PbiMooncakeApi = "https://api.powerbi.cn";

			// Token: 0x04000E98 RID: 3736
			public const string PbiFairfaxApi = "https://api.powerbigov.us";

			// Token: 0x04000E99 RID: 3737
			public const string PbiTrailBlazerApi = "https://api.high.powerbigov.us";

			// Token: 0x04000E9A RID: 3738
			public const string PbiPathFinderApi = "https://api.mil.powerbigov.us";

			// Token: 0x04000E9B RID: 3739
			public const string PbiUSNatApi = "https://api.powerbi.eaglex.ic.gov";

			// Token: 0x04000E9C RID: 3740
			public const string PbiUSSecApi = "https://api.powerbi.microsoft.scloud";

			// Token: 0x04000E9D RID: 3741
			public const string PbiPath = "spglobalservice/GetOrInsertClusterUrisByTenantLocation";
		}

		// Token: 0x020001F6 RID: 502
		public static class AixlToPublicXmlaConversion
		{
			// Token: 0x0600149B RID: 5275 RVA: 0x0004666B File Offset: 0x0004486B
			public static bool IsAixlToPublicXmlaConversionAllowedForApp(string applicationId, bool bypassBuildPermission, bool hasServicePrincipalProfileId)
			{
				if (hasServicePrincipalProfileId)
				{
					return false;
				}
				if (AsPaasHelper.AixlToPublicXmlaConversion.ConversionAllowedForAppsWithBypassBuildPerm.Contains(applicationId))
				{
					return bypassBuildPermission;
				}
				return AsPaasHelper.AixlToPublicXmlaConversion.ConversionAllowedForApps.Contains(applicationId) || string.Compare(applicationId, "AADPPE", StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04000E9E RID: 3742
			public const string AadPpeIdentityProvider = "AADPPE";

			// Token: 0x04000E9F RID: 3743
			private const string PBI_AIXL_CLIENT_ID = "929d0ec0-7a41-4b1e-bc7c-b754a28bddcc";

			// Token: 0x04000EA0 RID: 3744
			private const string PBI_AIXL_CLIENT_ID_FAIRFAX = "7b12a2ce-4704-4349-9388-d80a8e23116d";

			// Token: 0x04000EA1 RID: 3745
			private const string PBI_AS_CLIENT_ID = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";

			// Token: 0x04000EA2 RID: 3746
			private const string PBI_AS_CLIENT_ID_FAIRFAX = "ec3681c2-6e7d-472a-b23b-8be15bd25c15";

			// Token: 0x04000EA3 RID: 3747
			private const string POWERBI_DESKTOP_CLIENT_ID = "7f67af8a-fedc-4b08-8b4e-37c4d127b6cf";

			// Token: 0x04000EA4 RID: 3748
			private const string POWERBI_DESKTOP_CLIENT_ID_FAIRFAX = "6807062e-abc9-480a-ae93-9f7deee6b470";

			// Token: 0x04000EA5 RID: 3749
			private const string POWERBI_REPORT_BUILDER_CLIENT_ID = "f0b72488-7082-488a-a7e8-eada97bd842d";

			// Token: 0x04000EA6 RID: 3750
			private static HashSet<string> ConversionAllowedForApps = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "929d0ec0-7a41-4b1e-bc7c-b754a28bddcc", "7b12a2ce-4704-4349-9388-d80a8e23116d", "cf710c6e-dfcc-4fa8-a093-d47294e44c66", "ec3681c2-6e7d-472a-b23b-8be15bd25c15" };

			// Token: 0x04000EA7 RID: 3751
			private static HashSet<string> ConversionAllowedForAppsWithBypassBuildPerm = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "f0b72488-7082-488a-a7e8-eada97bd842d" };
		}

		// Token: 0x020001F7 RID: 503
		public static class DiscoveryEndpoint
		{
			// Token: 0x04000EA8 RID: 3752
			public const string PbiPath = "powerbi/globalservice/v201606/environments/discover";

			// Token: 0x04000EA9 RID: 3753
			public const string PbiQuery = "client=powerbi-msolap";
		}

		// Token: 0x020001F8 RID: 504
		public static class XmlaHttpHeaders
		{
			// Token: 0x04000EAA RID: 3754
			public const string WrongNode = "x-ms-wrong-node";

			// Token: 0x04000EAB RID: 3755
			public const string Server = "x-ms-xmlaserver";

			// Token: 0x04000EAC RID: 3756
			public const string Database = "x-ms-xmladatabase";

			// Token: 0x04000EAD RID: 3757
			public const string BypassAuthorization = "x-ms-xmlabypassauthorization";

			// Token: 0x04000EAE RID: 3758
			public const string User = "x-ms-xmlauser";

			// Token: 0x04000EAF RID: 3759
			public const string WorkspaceObjectId = "x-ms-xmlaworkspaceobjectid";

			// Token: 0x04000EB0 RID: 3760
			public const string Roles = "x-ms-xmlaroles";

			// Token: 0x04000EB1 RID: 3761
			public const string IntendedUsage = "x-ms-xmlaintendedusage";

			// Token: 0x04000EB2 RID: 3762
			public const string ContextualIdentity = "x-ms-xmlacontextualidentity";

			// Token: 0x04000EB3 RID: 3763
			public const string CapabilitiesNegotiationFlags = "x-ms-xmlacaps-negotiation-flags";

			// Token: 0x04000EB4 RID: 3764
			public const string SessionId = "x-ms-xmlasession-id";

			// Token: 0x04000EB5 RID: 3765
			public const string AppGeneralInfo = "x-ms-xmlaapp-general-info";

			// Token: 0x04000EB6 RID: 3766
			public const string ParentActivityId = "x-ms-parent-activity-id";

			// Token: 0x04000EB7 RID: 3767
			public const string RootActivityId = "x-ms-root-activity-id";

			// Token: 0x04000EB8 RID: 3768
			public const string RequestRegistrationId = "x-ms-request-registration-id";

			// Token: 0x04000EB9 RID: 3769
			public const string AcceptsContinuations = "x-ms-accepts-continuations";

			// Token: 0x04000EBA RID: 3770
			public const string RoundTripId = "x-ms-round-trip-id";

			// Token: 0x04000EBB RID: 3771
			public const string DedicatedConnection = "x-ms-xmladedicatedconnection";

			// Token: 0x04000EBC RID: 3772
			public const string WorkloadResourceMoniker = "x-ms-workload-resource-moniker";

			// Token: 0x04000EBD RID: 3773
			public const string RoutingScenario = "x-ms-routing-scenario";

			// Token: 0x04000EBE RID: 3774
			public const string RoutingHint = "x-ms-routing-hint";

			// Token: 0x04000EBF RID: 3775
			public const string TransientModelMode = "x-ms-xmlatransientmodelmode";

			// Token: 0x04000EC0 RID: 3776
			public const string ReadOnly = "x-ms-xmlareadonly";

			// Token: 0x04000EC1 RID: 3777
			public const string ServiceToServiceToken = "x-ms-xls2stoken";

			// Token: 0x04000EC2 RID: 3778
			public const string CurrentUtcDate = "x-ms-current-utc-date";

			// Token: 0x04000EC3 RID: 3779
			public const string ErrorDetails = "x-ms-xmlaerror-extended";

			// Token: 0x04000EC4 RID: 3780
			public const string XmlaClientTraits = "x-ms-xmlaclienttraits";

			// Token: 0x04000EC5 RID: 3781
			public const string ScaleOutAutoSync = "x-ms-xmlascaleoutautosync";

			// Token: 0x04000EC6 RID: 3782
			public const string SourceCapacityObjectId = "x-ms-src-capacity-id";

			// Token: 0x04000EC7 RID: 3783
			public const string ServicePrincipalProfileId = "X-PowerBI-profile-id";
		}

		// Token: 0x020001F9 RID: 505
		public static class XmlaHttpHeaderValues
		{
			// Token: 0x04000EC8 RID: 3784
			public const string WrongNodeIndication = "1";
		}

		// Token: 0x020001FA RID: 506
		public static class PbiHttpHeaders
		{
			// Token: 0x04000EC9 RID: 3785
			public const string RequestId = "RequestId";
		}

		// Token: 0x020001FB RID: 507
		public static class DataverseHttpHeaders
		{
			// Token: 0x04000ECA RID: 3786
			public const string ServiceRequestId = "x-ms-service-request-id";

			// Token: 0x04000ECB RID: 3787
			public const string ClientRequestId = "x-ms-client-request-id";
		}
	}
}
