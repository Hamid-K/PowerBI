using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Runtime;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000135 RID: 309
	internal static class AsPaasHelper
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x00039204 File Offset: 0x00037404
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

		// Token: 0x06001085 RID: 4229 RVA: 0x00039280 File Offset: 0x00037480
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

		// Token: 0x06001086 RID: 4230 RVA: 0x000392C0 File Offset: 0x000374C0
		public static string GetTechnicalDetailsWithWorkspaceInfo(string technicalDetails, string workspaceName)
		{
			if (string.IsNullOrEmpty(technicalDetails))
			{
				return technicalDetails;
			}
			string text = ClientHostingManager.MarkAsRestrictedInformation(workspaceName ?? "null", InfoRestrictionType.CCON);
			return RuntimeSR.AsPaasHelper_AdditionalTechnicalDetails_WorkspaceInfo(technicalDetails, text, Environment.NewLine);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000392F4 File Offset: 0x000374F4
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

		// Token: 0x06001088 RID: 4232 RVA: 0x00039348 File Offset: 0x00037548
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

		// Token: 0x06001089 RID: 4233 RVA: 0x000393A4 File Offset: 0x000375A4
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

		// Token: 0x0600108A RID: 4234 RVA: 0x00039400 File Offset: 0x00037600
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

		// Token: 0x0600108B RID: 4235 RVA: 0x00039490 File Offset: 0x00037690
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

		// Token: 0x0600108C RID: 4236 RVA: 0x0003959C File Offset: 0x0003779C
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

		// Token: 0x0600108D RID: 4237 RVA: 0x00039628 File Offset: 0x00037828
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

		// Token: 0x04000AB7 RID: 2743
		private const uint SCS_32BIT_BINARY = 0U;

		// Token: 0x04000AB8 RID: 2744
		private const uint SCS_64BIT_BINARY = 6U;

		// Token: 0x04000AB9 RID: 2745
		private static string hostProcessInfo;

		// Token: 0x04000ABA RID: 2746
		private static string hostAssemblyInfo;

		// Token: 0x020001D0 RID: 464
		public static class UriScheme
		{
			// Token: 0x04001152 RID: 4434
			public const string AsAzure = "asazure";

			// Token: 0x04001153 RID: 4435
			public const string PbiDedicated = "pbidedicated";

			// Token: 0x04001154 RID: 4436
			public const string PbiPublicXmla = "powerbi";

			// Token: 0x04001155 RID: 4437
			public const string PbiDataset = "pbiazure";

			// Token: 0x04001156 RID: 4438
			public const string Link = "link";
		}

		// Token: 0x020001D1 RID: 465
		public static class AixlEndpoints
		{
			// Token: 0x04001157 RID: 4439
			public const string ResourceForProd = "https://analysis.windows.net/powerbi/api";

			// Token: 0x04001158 RID: 4440
			public const string ResourceForPpe = "https://analysis.windows-int.net/powerbi/api";
		}

		// Token: 0x020001D2 RID: 466
		public static class ClusterResolutionEndpoint
		{
			// Token: 0x06001400 RID: 5120 RVA: 0x00044CDB File Offset: 0x00042EDB
			public static bool IsTrustedClusterResolutionEndpoint(Uri dataSource)
			{
				return AsPaasHelper.ClusterResolutionEndpoint.trustedEndpoints.Contains(dataSource.AbsoluteUri);
			}

			// Token: 0x06001401 RID: 5121 RVA: 0x00044CF0 File Offset: 0x00042EF0
			private static ICollection<string> GetTrustedEndpoints()
			{
				return new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"https://api.powerbi.com/", "https://df-msit-scus-redirect.analysis.windows.net/", "https://wabi-daily-us-east2-redirect.analysis.windows.net/", "https://dailyapi.powerbi.com/", "https://wabi-staging-us-east-redirect.analysis.windows.net/", "https://dxtapi.powerbi.com/", "https://biazure-int-edog-redirect.analysis-df.windows.net/", "https://powerbiapi.analysis-df.windows.net/", "https://onebox-redirect.analysis.windows-int.net/", "https://api.powerbi.cn",
					"https://api.powerbigov.us", "https://api.high.powerbigov.us", "https://api.mil.powerbigov.us", "https://api.powerbi.eaglex.ic.gov", "https://api.powerbi.microsoft.scloud"
				};
			}

			// Token: 0x04001159 RID: 4441
			private static readonly ICollection<string> trustedEndpoints = AsPaasHelper.ClusterResolutionEndpoint.GetTrustedEndpoints();

			// Token: 0x0400115A RID: 4442
			public const string PbiProd = "https://api.powerbi.com/";

			// Token: 0x0400115B RID: 4443
			public const string PbiMsit = "https://df-msit-scus-redirect.analysis.windows.net/";

			// Token: 0x0400115C RID: 4444
			public const string PbiDaily = "https://wabi-daily-us-east2-redirect.analysis.windows.net/";

			// Token: 0x0400115D RID: 4445
			public const string PbiDxt = "https://wabi-staging-us-east-redirect.analysis.windows.net/";

			// Token: 0x0400115E RID: 4446
			public const string PbiEdog = "https://biazure-int-edog-redirect.analysis-df.windows.net/";

			// Token: 0x0400115F RID: 4447
			public const string PbiOneBox = "https://onebox-redirect.analysis.windows-int.net/";

			// Token: 0x04001160 RID: 4448
			public const string PbiDailyApi = "https://dailyapi.powerbi.com/";

			// Token: 0x04001161 RID: 4449
			public const string PbiDxtApi = "https://dxtapi.powerbi.com/";

			// Token: 0x04001162 RID: 4450
			public const string PbiEdogApi = "https://powerbiapi.analysis-df.windows.net/";

			// Token: 0x04001163 RID: 4451
			public const string PbiMooncakeApi = "https://api.powerbi.cn";

			// Token: 0x04001164 RID: 4452
			public const string PbiFairfaxApi = "https://api.powerbigov.us";

			// Token: 0x04001165 RID: 4453
			public const string PbiTrailBlazerApi = "https://api.high.powerbigov.us";

			// Token: 0x04001166 RID: 4454
			public const string PbiPathFinderApi = "https://api.mil.powerbigov.us";

			// Token: 0x04001167 RID: 4455
			public const string PbiUSNatApi = "https://api.powerbi.eaglex.ic.gov";

			// Token: 0x04001168 RID: 4456
			public const string PbiUSSecApi = "https://api.powerbi.microsoft.scloud";

			// Token: 0x04001169 RID: 4457
			public const string PbiPath = "spglobalservice/GetOrInsertClusterUrisByTenantLocation";
		}

		// Token: 0x020001D3 RID: 467
		public static class AixlToPublicXmlaConversion
		{
			// Token: 0x06001403 RID: 5123 RVA: 0x00044DC7 File Offset: 0x00042FC7
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

			// Token: 0x0400116A RID: 4458
			public const string AadPpeIdentityProvider = "AADPPE";

			// Token: 0x0400116B RID: 4459
			private const string PBI_AIXL_CLIENT_ID = "929d0ec0-7a41-4b1e-bc7c-b754a28bddcc";

			// Token: 0x0400116C RID: 4460
			private const string PBI_AIXL_CLIENT_ID_FAIRFAX = "7b12a2ce-4704-4349-9388-d80a8e23116d";

			// Token: 0x0400116D RID: 4461
			private const string PBI_AS_CLIENT_ID = "cf710c6e-dfcc-4fa8-a093-d47294e44c66";

			// Token: 0x0400116E RID: 4462
			private const string PBI_AS_CLIENT_ID_FAIRFAX = "ec3681c2-6e7d-472a-b23b-8be15bd25c15";

			// Token: 0x0400116F RID: 4463
			private const string POWERBI_DESKTOP_CLIENT_ID = "7f67af8a-fedc-4b08-8b4e-37c4d127b6cf";

			// Token: 0x04001170 RID: 4464
			private const string POWERBI_DESKTOP_CLIENT_ID_FAIRFAX = "6807062e-abc9-480a-ae93-9f7deee6b470";

			// Token: 0x04001171 RID: 4465
			private const string POWERBI_REPORT_BUILDER_CLIENT_ID = "f0b72488-7082-488a-a7e8-eada97bd842d";

			// Token: 0x04001172 RID: 4466
			private static HashSet<string> ConversionAllowedForApps = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "929d0ec0-7a41-4b1e-bc7c-b754a28bddcc", "7b12a2ce-4704-4349-9388-d80a8e23116d", "cf710c6e-dfcc-4fa8-a093-d47294e44c66", "ec3681c2-6e7d-472a-b23b-8be15bd25c15" };

			// Token: 0x04001173 RID: 4467
			private static HashSet<string> ConversionAllowedForAppsWithBypassBuildPerm = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "f0b72488-7082-488a-a7e8-eada97bd842d" };
		}

		// Token: 0x020001D4 RID: 468
		public static class DiscoveryEndpoint
		{
			// Token: 0x04001174 RID: 4468
			public const string PbiPath = "powerbi/globalservice/v201606/environments/discover";

			// Token: 0x04001175 RID: 4469
			public const string PbiQuery = "client=powerbi-msolap";
		}

		// Token: 0x020001D5 RID: 469
		public static class XmlaHttpHeaders
		{
			// Token: 0x04001176 RID: 4470
			public const string WrongNode = "x-ms-wrong-node";

			// Token: 0x04001177 RID: 4471
			public const string Server = "x-ms-xmlaserver";

			// Token: 0x04001178 RID: 4472
			public const string Database = "x-ms-xmladatabase";

			// Token: 0x04001179 RID: 4473
			public const string BypassAuthorization = "x-ms-xmlabypassauthorization";

			// Token: 0x0400117A RID: 4474
			public const string User = "x-ms-xmlauser";

			// Token: 0x0400117B RID: 4475
			public const string WorkspaceObjectId = "x-ms-xmlaworkspaceobjectid";

			// Token: 0x0400117C RID: 4476
			public const string Roles = "x-ms-xmlaroles";

			// Token: 0x0400117D RID: 4477
			public const string IntendedUsage = "x-ms-xmlaintendedusage";

			// Token: 0x0400117E RID: 4478
			public const string ContextualIdentity = "x-ms-xmlacontextualidentity";

			// Token: 0x0400117F RID: 4479
			public const string CapabilitiesNegotiationFlags = "x-ms-xmlacaps-negotiation-flags";

			// Token: 0x04001180 RID: 4480
			public const string SessionId = "x-ms-xmlasession-id";

			// Token: 0x04001181 RID: 4481
			public const string AppGeneralInfo = "x-ms-xmlaapp-general-info";

			// Token: 0x04001182 RID: 4482
			public const string ParentActivityId = "x-ms-parent-activity-id";

			// Token: 0x04001183 RID: 4483
			public const string RootActivityId = "x-ms-root-activity-id";

			// Token: 0x04001184 RID: 4484
			public const string RequestRegistrationId = "x-ms-request-registration-id";

			// Token: 0x04001185 RID: 4485
			public const string AcceptsContinuations = "x-ms-accepts-continuations";

			// Token: 0x04001186 RID: 4486
			public const string RoundTripId = "x-ms-round-trip-id";

			// Token: 0x04001187 RID: 4487
			public const string DedicatedConnection = "x-ms-xmladedicatedconnection";

			// Token: 0x04001188 RID: 4488
			public const string WorkloadResourceMoniker = "x-ms-workload-resource-moniker";

			// Token: 0x04001189 RID: 4489
			public const string RoutingScenario = "x-ms-routing-scenario";

			// Token: 0x0400118A RID: 4490
			public const string RoutingHint = "x-ms-routing-hint";

			// Token: 0x0400118B RID: 4491
			public const string TransientModelMode = "x-ms-xmlatransientmodelmode";

			// Token: 0x0400118C RID: 4492
			public const string ReadOnly = "x-ms-xmlareadonly";

			// Token: 0x0400118D RID: 4493
			public const string ServiceToServiceToken = "x-ms-xls2stoken";

			// Token: 0x0400118E RID: 4494
			public const string CurrentUtcDate = "x-ms-current-utc-date";

			// Token: 0x0400118F RID: 4495
			public const string ErrorDetails = "x-ms-xmlaerror-extended";

			// Token: 0x04001190 RID: 4496
			public const string XmlaClientTraits = "x-ms-xmlaclienttraits";

			// Token: 0x04001191 RID: 4497
			public const string ScaleOutAutoSync = "x-ms-xmlascaleoutautosync";

			// Token: 0x04001192 RID: 4498
			public const string SourceCapacityObjectId = "x-ms-src-capacity-id";

			// Token: 0x04001193 RID: 4499
			public const string ServicePrincipalProfileId = "X-PowerBI-profile-id";
		}

		// Token: 0x020001D6 RID: 470
		public static class XmlaHttpHeaderValues
		{
			// Token: 0x04001194 RID: 4500
			public const string WrongNodeIndication = "1";
		}

		// Token: 0x020001D7 RID: 471
		public static class PbiHttpHeaders
		{
			// Token: 0x04001195 RID: 4501
			public const string RequestId = "RequestId";
		}

		// Token: 0x020001D8 RID: 472
		public static class DataverseHttpHeaders
		{
			// Token: 0x04001196 RID: 4502
			public const string ServiceRequestId = "x-ms-service-request-id";

			// Token: 0x04001197 RID: 4503
			public const string ClientRequestId = "x-ms-client-request-id";
		}
	}
}
