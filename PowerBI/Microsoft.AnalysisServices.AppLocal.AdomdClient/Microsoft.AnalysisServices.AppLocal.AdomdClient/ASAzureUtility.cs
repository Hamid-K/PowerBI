using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Web;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001C RID: 28
	internal static class ASAzureUtility
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00007608 File Offset: 0x00005808
		public static void ExtractPbiPublicXmlaTenantAndWorkspace(string dataSource, out string tenant, out string workspace)
		{
			Uri uri = new Uri(dataSource);
			tenant = null;
			workspace = null;
			if (uri.Segments.Length <= 1)
			{
				throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat(dataSource), null);
			}
			if (string.Compare(uri.Segments[1].Trim(new char[] { '/' }), "v1.0", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new AdomdConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat(dataSource), null);
			}
			if (uri.Segments.Length == 2)
			{
				return;
			}
			tenant = ASAzureUtility.HandlePbiDefaultTenant(uri.Segments[2].Trim(new char[] { '/' }));
			if (uri.Segments.Length == 3)
			{
				return;
			}
			workspace = string.Join("/", uri.Segments.Where((string seg, int i) => i > 2).ToArray<string>());
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000076F0 File Offset: 0x000058F0
		public static string ResolveServerBasedOnLinkReference(string dataSourceUri)
		{
			string text2;
			try
			{
				Uri uri = new UriBuilder(dataSourceUri)
				{
					Scheme = Uri.UriSchemeHttps
				}.Uri;
				if (uri.Port != 443)
				{
					throw new AdomdConnectionException(XmlaSR.ConnectionString_ASAzure_InvalidLinkReferenceCustomPort(dataSourceUri), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
				}
				string text = ASAzureUtility.GetHttpDataAsString(uri).Trim();
				if (!ASAzureUtility.IsValidAsAzureUri(text))
				{
					throw new AdomdConnectionException(XmlaSR.ConnectionString_ASAzure_InvalidLinkReferenceUri(dataSourceUri), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
				}
				text2 = text;
			}
			catch (WebException)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_ASAzure_FetchLinkReferenceFailed(dataSourceUri), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			return text2;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007774 File Offset: 0x00005974
		public static bool DataSourceUriWithOnlyServerName(string dataSourceUri, out string serverName)
		{
			Uri uri = new Uri(dataSourceUri);
			if (string.IsNullOrEmpty(uri.AbsolutePath) || !uri.AbsolutePath.StartsWith("/") || uri.AbsolutePath.Length == 1 || uri.AbsolutePath.IndexOf("/", 1, StringComparison.InvariantCulture) != -1)
			{
				serverName = null;
				return false;
			}
			serverName = uri.AbsolutePath.Substring(1);
			return true;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000077DE File Offset: 0x000059DE
		public static string ConstructAsAzureSecureServerConnUri(string dataSourceUri)
		{
			return new UriBuilder(dataSourceUri)
			{
				Scheme = Uri.UriSchemeHttps,
				Path = "/webapi/xmla"
			}.ToString();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007801 File Offset: 0x00005A01
		public static string ConstructPbiPremiumServerConnUri(string rolloutFqdn)
		{
			return new UriBuilder(string.Format(CultureInfo.InvariantCulture, "https://{0}", rolloutFqdn))
			{
				Path = "/webapi/xmla"
			}.ToString();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007828 File Offset: 0x00005A28
		public static AsPaasEndpointInfo ResolvePaaSConnectionEndpointDetail(AsInstanceType asInstanceType, Uri dataSourceUri, string serverName, string pbiDatabaseName, bool premiumPublicXmlaEndpoint, ref TimeoutUtils.TimeLeft timeLeft, TimeoutUtils.OnTimeoutAction timeoutAction, Guid parentActivityId)
		{
			AsPaasEndpointInfo asPaasEndpointInfo;
			if (!AsPaasEndpointInfo.TryGetEndpointInfoFromCache(dataSourceUri, serverName, out asPaasEndpointInfo))
			{
				try
				{
					using (new TimeoutUtils.TimeRestrictedMonitor(timeLeft, timeoutAction))
					{
						Uri uri = new UriBuilder(dataSourceUri)
						{
							Path = "/webapi/clusterResolve"
						}.Uri;
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						if (parentActivityId != Guid.Empty)
						{
							dictionary.Add("x-ms-parent-activity-id", parentActivityId.ToString());
						}
						ASAzureUtility.NameResolutionRequest nameResolutionRequest = new ASAzureUtility.NameResolutionRequest
						{
							ServerName = serverName,
							DatabaseName = pbiDatabaseName,
							PremiumPublicXmlaEndpoint = premiumPublicXmlaEndpoint
						};
						string text;
						ASAzureUtility.NameResolutionResult nameResolutionResult = ConnectivityHelper.ExecuteJsonBasedHttpPostRequest<ASAzureUtility.NameResolutionRequest, ASAzureUtility.NameResolutionResult>(uri, dictionary, nameResolutionRequest, ConnectivityHelper.JsonHttpRequestOptions.TargetingPaasInfra, timeLeft.TimeMs, null, null, out text);
						asPaasEndpointInfo = new AsPaasEndpointInfo(new UriBuilder(dataSourceUri)
						{
							Host = nameResolutionResult.ClusterFqdn
						}.Uri, nameResolutionResult.CoreServerName, nameResolutionResult.TenantId);
						AsPaasEndpointInfo.AddEndpointInfoToCache(dataSourceUri, serverName, asPaasEndpointInfo);
					}
				}
				catch (WebException ex)
				{
					throw ASAzureUtility.GetConnectionException(ex, asInstanceType);
				}
			}
			return asPaasEndpointInfo;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000792C File Offset: 0x00005B2C
		public static bool IsAsAzureRedirectedToPowerBIWorkspace(string pbiEndpoint, string aasInstance, Guid activityId, out string tenantId)
		{
			string text = string.Format("{0}|{1}", pbiEndpoint, aasInstance);
			KeyValuePair<bool, string> keyValuePair;
			if (ASAzureUtility.asAzureRedirectionCache.Lookup<KeyValuePair<bool, string>>(text, out keyValuePair))
			{
				tenantId = keyValuePair.Value;
				return keyValuePair.Key;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
			dictionary.Add("RequestId", activityId.ToString());
			Uri uri = new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/AASRedirect/public/mappings/exists", pbiEndpoint));
			ASAzureUtility.GetRedirectedWorkspaceRequest getRedirectedWorkspaceRequest = new ASAzureUtility.GetRedirectedWorkspaceRequest
			{
				AasInstance = aasInstance
			};
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					HttpStatusCode httpStatusCode;
					string text2;
					WebHeaderCollection webHeaderCollection;
					ASAzureUtility.GetRedirectionExistenceResult getRedirectionExistenceResult = ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<ASAzureUtility.GetRedirectedWorkspaceRequest, ASAzureUtility.GetRedirectionExistenceResult>(uri, "POST", dictionary, getRedirectedWorkspaceRequest, ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect, -1, ASAzureUtility.getRedirectedWorkspaceRequestSerializer, ASAzureUtility.getRedirectionExistenceResultSerializer, false, true, out httpStatusCode, out text2, out webHeaderCollection);
					ASAzureUtility.HandleAsAzureRedirectedToPowerBIWorkspaceResponse(httpStatusCode, aasInstance, getRedirectionExistenceResult, null, ref num, out flag, out keyValuePair);
				}
				catch (WebException ex)
				{
					HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
					if (httpWebResponse == null)
					{
						throw new AdomdConnectionException(ex.Message, ex);
					}
					ASAzureUtility.HandleAsAzureRedirectedToPowerBIWorkspaceResponse(httpWebResponse.StatusCode, aasInstance, null, ex, ref num, out flag, out keyValuePair);
				}
			}
			while (flag);
			ASAzureUtility.asAzureRedirectionCache.Insert(text, keyValuePair);
			tenantId = keyValuePair.Value;
			return keyValuePair.Key;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007A54 File Offset: 0x00005C54
		public static string ResolvePowerBICluster(Uri discoverUrl, string token, TimeoutUtils.TimeLeft timeLeft, AsInstanceType asInstanceType)
		{
			string text;
			try
			{
				TimeoutUtils.OnTimeoutAction onTimeoutAction = delegate(bool isOnDispose)
				{
					throw new ArgumentException(XmlaSR.CannotConnect);
				};
				string fixedClusterUri;
				using (new TimeoutUtils.TimeRestrictedMonitor(timeLeft, onTimeoutAction))
				{
					fixedClusterUri = ConnectivityHelper.ExecuteJsonBasedHttpPutRequest<string, ASAzureUtility.PowerBIClusterResolutionResult>(discoverUrl, new Dictionary<string, string> { 
					{
						"authorization",
						"Bearer " + token
					} }, string.Empty, ConnectivityHelper.JsonHttpRequestOptions.TargetingPaasInfra, timeLeft.TimeMs, null, null, out text).FixedClusterUri;
				}
				text = fixedClusterUri;
			}
			catch (WebException ex)
			{
				throw ASAzureUtility.GetConnectionException(ex, asInstanceType);
			}
			return text;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007AFC File Offset: 0x00005CFC
		public static bool AuthenticatePowerBIBackendEndpoint(Uri discoverUrl, Uri dataSourceUri, TimeoutUtils.TimeLeft timeLeft, AsInstanceType asInstanceType)
		{
			try
			{
				TimeoutUtils.OnTimeoutAction onTimeoutAction = (bool isOnDispose) => false;
				using (new TimeoutUtils.TimeRestrictedMonitor(timeLeft, onTimeoutAction))
				{
					string text;
					ASAzureUtility.PowerBIEnvironment[] environments = ConnectivityHelper.ExecuteJsonBasedHttpPostRequest<string, ASAzureUtility.PowerBIDiscoveryResult>(discoverUrl, null, string.Empty, ConnectivityHelper.JsonHttpRequestOptions.TargetingPaasInfra, timeLeft.TimeMs, null, null, out text).environments;
					for (int i = 0; i < environments.Length; i++)
					{
						foreach (ASAzureUtility.PowerBIService powerBIService in environments[i].services)
						{
							if (powerBIService.name.Equals("powerbi-backend", StringComparison.InvariantCultureIgnoreCase) && new Uri(powerBIService.endpoint).Host.Equals(dataSourceUri.Host, StringComparison.InvariantCultureIgnoreCase))
							{
								return true;
							}
						}
					}
				}
			}
			catch (WebException ex)
			{
				throw ASAzureUtility.GetConnectionException(ex, asInstanceType);
			}
			return false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public static Exception GetConnectionException(WebException ex, AsInstanceType asInstanceType)
		{
			return ASAzureUtility.GetConnectionException(null, true, ex, asInstanceType);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007C04 File Offset: 0x00005E04
		public static Exception GetConnectionException(HttpResponseMessage response, AsInstanceType asInstanceType)
		{
			string text = XmlaSR.HttpStream_ResponseWithFailedStatus(((int)response.StatusCode).ToString(), response.ReasonPhrase);
			ConnectionExceptionCause connectionExceptionCause = ConnectionExceptionCause.Unspecified;
			IDictionary<string, object> dictionary = null;
			Stream stream;
			if (ASAzureUtility.SupportsExtendedErrorInformation(asInstanceType) && response.TryGetResponseContent(out stream))
			{
				ASAzureUtility.GetASPaaSInfraExtendedErrorInformation(response.StatusCode, response.Headers.GetHttpHeaderValueOrDefault("x-ms-root-activity-id", null), response.Headers.GetHttpHeaderValueOrDefault("x-ms-current-utc-date", null), stream, ref text, out connectionExceptionCause, out dictionary);
			}
			if (ASAzureUtility.SupportsErrorDetailsHeader(asInstanceType))
			{
				string httpHeaderValueOrDefault = response.Headers.GetHttpHeaderValueOrDefault("x-ms-xmlaerror-extended", null);
				if (!string.IsNullOrEmpty(httpHeaderValueOrDefault))
				{
					text = httpHeaderValueOrDefault;
				}
			}
			text = string.Format("{0}{1}", text, AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(response));
			return ASAzureUtility.CreateConnectionException(text, connectionExceptionCause, dictionary, null);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007CB8 File Offset: 0x00005EB8
		private static string GetHttpDataAsString(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.Method = "GET";
			string text;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						text = streamReader.ReadLine();
					}
				}
			}
			return text;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007D44 File Offset: 0x00005F44
		private static string HandlePbiDefaultTenant(string tenant)
		{
			tenant = HttpUtility.UrlDecode(tenant);
			if (string.Compare(tenant, "myorg", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			return tenant;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007D60 File Offset: 0x00005F60
		private static bool IsValidAsAzureUri(string asAzureServerUri)
		{
			bool flag;
			try
			{
				flag = new Uri(asAzureServerUri).Scheme == "asazure";
			}
			catch (UriFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007D9C File Offset: 0x00005F9C
		private static void HandleAsAzureRedirectedToPowerBIWorkspaceResponse(HttpStatusCode httpStatusCode, string aasInstance, ASAzureUtility.GetRedirectionExistenceResult result, WebException ex, ref int retryAttempts, out bool needRetry, out KeyValuePair<bool, string> redirectionInfo)
		{
			needRetry = false;
			redirectionInfo = new KeyValuePair<bool, string>(false, null);
			if (httpStatusCode <= HttpStatusCode.NotFound)
			{
				if (httpStatusCode != HttpStatusCode.OK)
				{
					if (httpStatusCode == HttpStatusCode.NotFound)
					{
						goto IL_00A9;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(result.TenantId))
					{
						throw ASAzureUtility.GetConnectionException(XmlaSR.XmlaClient_ASAzureRedirectionResolutionMissingTenantId(aasInstance), false, ex, AsInstanceType.PbiPremiumXmlaEp);
					}
					redirectionInfo = new KeyValuePair<bool, string>(true, result.TenantId);
					goto IL_00A9;
				}
			}
			else
			{
				if (httpStatusCode == (HttpStatusCode)429)
				{
					throw ASAzureUtility.GetConnectionException(XmlaSR.XmlaClient_ASAzureRedirectionResolutionFailedWithThrottling, false, ex, AsInstanceType.PbiPremiumXmlaEp);
				}
				if (httpStatusCode == HttpStatusCode.InternalServerError || httpStatusCode - HttpStatusCode.BadGateway <= 1)
				{
					needRetry = true;
					goto IL_00A9;
				}
			}
			throw ASAzureUtility.GetConnectionException(XmlaSR.XmlaClient_ASAzureRedirectionResolutionFailedWithError(aasInstance, httpStatusCode.ToString()), false, ex, AsInstanceType.PbiPremiumXmlaEp);
			IL_00A9:
			if (needRetry)
			{
				int num = retryAttempts + 1;
				retryAttempts = num;
				if (num >= 2)
				{
					throw ASAzureUtility.GetConnectionException(XmlaSR.XmlaClient_ASAzureRedirectionResolutionFailedWithError(aasInstance, httpStatusCode.ToString()), false, ex, AsInstanceType.PbiPremiumXmlaEp);
				}
				Thread.Sleep(1000);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007E8C File Offset: 0x0000608C
		private static Exception GetConnectionException(string message, bool isPaasInfra, WebException ex, AsInstanceType asInstanceType)
		{
			if (string.IsNullOrEmpty(message))
			{
				message = ex.Message;
			}
			ConnectionExceptionCause connectionExceptionCause = ConnectionExceptionCause.Unspecified;
			IDictionary<string, object> dictionary = null;
			if (ex != null && ex.Response != null)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					if (isPaasInfra)
					{
						if (ASAzureUtility.SupportsExtendedErrorInformation(asInstanceType) && httpWebResponse.GetResponseStream() != null)
						{
							ASAzureUtility.GetASPaaSInfraExtendedErrorInformation(httpWebResponse.StatusCode, httpWebResponse.Headers["x-ms-root-activity-id"], httpWebResponse.Headers["x-ms-current-utc-date"], httpWebResponse.GetResponseStream(), ref message, out connectionExceptionCause, out dictionary);
							httpWebResponse.GetResponseStream().Seek(0L, SeekOrigin.Begin);
						}
						if (ASAzureUtility.SupportsErrorDetailsHeader(asInstanceType))
						{
							string text = httpWebResponse.Headers["x-ms-xmlaerror-extended"];
							if (!string.IsNullOrEmpty(text))
							{
								message = text;
							}
						}
						message = string.Format("{0}{1}", message, AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(httpWebResponse));
					}
					else
					{
						message = string.Format("{0}{1}", message, AsPaasHelper.GetTechnicalDetailsFromPbiSharedResponse(httpWebResponse));
					}
				}
			}
			return ASAzureUtility.CreateConnectionException(message, connectionExceptionCause, dictionary, ex);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007F81 File Offset: 0x00006181
		private static bool SupportsExtendedErrorInformation(AsInstanceType asInstanceType)
		{
			return asInstanceType == AsInstanceType.PbiPremiumInternal || asInstanceType == AsInstanceType.AsAzure || asInstanceType == AsInstanceType.PbiPremiumXmlaEp;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007F91 File Offset: 0x00006191
		private static bool SupportsErrorDetailsHeader(AsInstanceType asInstanceType)
		{
			return asInstanceType != AsInstanceType.PbiPremiumInternal;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007F9C File Offset: 0x0000619C
		private static void GetASPaaSInfraExtendedErrorInformation(HttpStatusCode statusCode, string rootActivityId, string utcDate, Stream responsePayload, ref string message, out ConnectionExceptionCause exceptionCause, out IDictionary<string, object> extendedErrorInformation)
		{
			try
			{
				ASAzureUtility.ASPaaSError aspaaSError = (ASAzureUtility.ASPaaSError)ASAzureUtility.ASPaaSErrorSerializer.ReadObject(responsePayload);
				if (aspaaSError != null)
				{
					string code = aspaaSError.Code;
					if (!(code == "ServerPausedOrScaling"))
					{
						if (!(code == "ServerNotReady") && !(code == "CapacityNotActive"))
						{
							if (!(code == "XmlaEndpointDisabled"))
							{
								if (!(code == "ServerNotFoundForConnectionMode"))
								{
									exceptionCause = ConnectionExceptionCause.Unspecified;
								}
								else
								{
									exceptionCause = ConnectionExceptionCause.ServerNotFoundInConnectionMode;
								}
							}
							else
							{
								exceptionCause = ConnectionExceptionCause.XmlaEndpointDisabled;
							}
						}
						else
						{
							exceptionCause = ConnectionExceptionCause.ServerNotReady;
						}
					}
					else
					{
						exceptionCause = ConnectionExceptionCause.ServerPausedOrScaling;
					}
					extendedErrorInformation = new Dictionary<string, object>();
					extendedErrorInformation["Code"] = aspaaSError.Code;
					extendedErrorInformation["RootActivityId"] = rootActivityId;
					extendedErrorInformation["UtcDate"] = utcDate;
					extendedErrorInformation["HttpStatusCode"] = ((aspaaSError.HttpStatusCode != null) ? aspaaSError.HttpStatusCode.Value : ((int)statusCode));
					if (aspaaSError.SubCode != null)
					{
						extendedErrorInformation["SubCode"] = aspaaSError.SubCode.Value;
					}
					message = aspaaSError.Message;
				}
				else
				{
					extendedErrorInformation = null;
					exceptionCause = ConnectionExceptionCause.Unspecified;
				}
			}
			catch
			{
				extendedErrorInformation = null;
				exceptionCause = ConnectionExceptionCause.Unspecified;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008100 File Offset: 0x00006300
		private static Exception CreateConnectionException(string message, ConnectionExceptionCause exceptionCause, IDictionary<string, object> extendedErrorInformation, Exception innerException)
		{
			Exception ex = ((innerException != null) ? new AdomdConnectionException(message, innerException, exceptionCause) : new AdomdConnectionException(message, new ConnectionExceptionCause?(exceptionCause)));
			if (extendedErrorInformation != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in extendedErrorInformation)
				{
					ex.Data[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return ex;
		}

		// Token: 0x04000186 RID: 390
		private const int LinkReferenceUriPort = 443;

		// Token: 0x04000187 RID: 391
		private const string PbiDedicatedRolloutUriFormat = "https://{0}";

		// Token: 0x04000188 RID: 392
		private const string XmlaExecutionUriPath = "/webapi/xmla";

		// Token: 0x04000189 RID: 393
		private const string NameResolutionUriPath = "/webapi/clusterResolve";

		// Token: 0x0400018A RID: 394
		private const string DefaultPbiTenant = "myorg";

		// Token: 0x0400018B RID: 395
		private const string AASErrorCode_ServerPausedOrScaling = "ServerPausedOrScaling";

		// Token: 0x0400018C RID: 396
		private const string AASErrorCode_ServerNotReady = "ServerNotReady";

		// Token: 0x0400018D RID: 397
		private const string AASErrorCode_ServerNotFoundForConnectionMode = "ServerNotFoundForConnectionMode";

		// Token: 0x0400018E RID: 398
		private const string MwcErrorCode_XmlaEndpointDisabled = "XmlaEndpointDisabled";

		// Token: 0x0400018F RID: 399
		private const string MwcErrorCode_CapacityNotActive = "CapacityNotActive";

		// Token: 0x04000190 RID: 400
		private const string AsAzureRedirectionCacheKeyFormat = "{0}|{1}";

		// Token: 0x04000191 RID: 401
		private const string PbiASAzureRedirectionCheckUriFormat = "https://{0}/AASRedirect/public/mappings/exists";

		// Token: 0x04000192 RID: 402
		private const int ASAzureRedirectionMaxRetryAttempts = 2;

		// Token: 0x04000193 RID: 403
		private const int ASAzureRedirectionDelayMsBetweenRetries = 1000;

		// Token: 0x04000194 RID: 404
		private static readonly SharedMemoryCache asAzureRedirectionCache = SharedMemoryCache.Create("XmlaLibAsAzureRedirectionCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), null, null);

		// Token: 0x04000195 RID: 405
		private static readonly DataContractJsonSerializer ASPaaSErrorSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.ASPaaSError));

		// Token: 0x04000196 RID: 406
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceRequestSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.GetRedirectedWorkspaceRequest));

		// Token: 0x04000197 RID: 407
		private static readonly DataContractJsonSerializer getRedirectionExistenceResultSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.GetRedirectionExistenceResult));

		// Token: 0x0200016F RID: 367
		private static class ASPaaSExtendedErrorInformationMembers
		{
			// Token: 0x04000BDD RID: 3037
			public const string Code = "Code";

			// Token: 0x04000BDE RID: 3038
			public const string RootActivityId = "RootActivityId";

			// Token: 0x04000BDF RID: 3039
			public const string CurrentUtcDate = "UtcDate";

			// Token: 0x04000BE0 RID: 3040
			public const string SubCode = "SubCode";

			// Token: 0x04000BE1 RID: 3041
			public const string HttpStatusCode = "HttpStatusCode";
		}

		// Token: 0x02000170 RID: 368
		private static class PublicXmlaUriVersions
		{
			// Token: 0x04000BE2 RID: 3042
			public const string V1 = "v1.0";
		}

		// Token: 0x02000171 RID: 369
		[DataContract]
		private sealed class GetRedirectedWorkspaceRequest
		{
			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06001154 RID: 4436 RVA: 0x0003CA7C File Offset: 0x0003AC7C
			// (set) Token: 0x06001155 RID: 4437 RVA: 0x0003CA84 File Offset: 0x0003AC84
			[DataMember(Name = "aasInstance", IsRequired = true)]
			public string AasInstance { get; set; }
		}

		// Token: 0x02000172 RID: 370
		[DataContract]
		private sealed class GetRedirectionExistenceResult
		{
			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06001157 RID: 4439 RVA: 0x0003CA95 File Offset: 0x0003AC95
			// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003CA9D File Offset: 0x0003AC9D
			[DataMember(Name = "tenantId", IsRequired = true)]
			public string TenantId { get; set; }
		}

		// Token: 0x02000173 RID: 371
		[DataContract]
		private class NameResolutionRequest
		{
			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x0600115A RID: 4442 RVA: 0x0003CAAE File Offset: 0x0003ACAE
			// (set) Token: 0x0600115B RID: 4443 RVA: 0x0003CAB6 File Offset: 0x0003ACB6
			[DataMember(Name = "serverName")]
			public string ServerName { get; set; }

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x0600115C RID: 4444 RVA: 0x0003CABF File Offset: 0x0003ACBF
			// (set) Token: 0x0600115D RID: 4445 RVA: 0x0003CAC7 File Offset: 0x0003ACC7
			[DataMember(Name = "databaseName")]
			public string DatabaseName { get; set; }

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x0600115E RID: 4446 RVA: 0x0003CAD0 File Offset: 0x0003ACD0
			// (set) Token: 0x0600115F RID: 4447 RVA: 0x0003CAD8 File Offset: 0x0003ACD8
			[DataMember(Name = "premiumPublicXmlaEndpoint")]
			public bool PremiumPublicXmlaEndpoint { get; set; }
		}

		// Token: 0x02000174 RID: 372
		[DataContract]
		private class NameResolutionResult
		{
			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003CAE9 File Offset: 0x0003ACE9
			// (set) Token: 0x06001162 RID: 4450 RVA: 0x0003CAF1 File Offset: 0x0003ACF1
			[DataMember(Name = "clusterFQDN")]
			public string ClusterFqdn { get; set; }

			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x06001163 RID: 4451 RVA: 0x0003CAFA File Offset: 0x0003ACFA
			// (set) Token: 0x06001164 RID: 4452 RVA: 0x0003CB02 File Offset: 0x0003AD02
			[DataMember(Name = "coreServerName")]
			public string CoreServerName { get; set; }

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x06001165 RID: 4453 RVA: 0x0003CB0B File Offset: 0x0003AD0B
			// (set) Token: 0x06001166 RID: 4454 RVA: 0x0003CB13 File Offset: 0x0003AD13
			[DataMember(Name = "tenantId")]
			public string TenantId { get; set; }
		}

		// Token: 0x02000175 RID: 373
		[DataContract]
		private class ASPaaSError
		{
			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06001168 RID: 4456 RVA: 0x0003CB24 File Offset: 0x0003AD24
			// (set) Token: 0x06001169 RID: 4457 RVA: 0x0003CB2C File Offset: 0x0003AD2C
			[DataMember(Name = "code")]
			public string Code { get; set; }

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x0600116A RID: 4458 RVA: 0x0003CB35 File Offset: 0x0003AD35
			// (set) Token: 0x0600116B RID: 4459 RVA: 0x0003CB3D File Offset: 0x0003AD3D
			[DataMember(Name = "message")]
			public string Message { get; set; }

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x0600116C RID: 4460 RVA: 0x0003CB46 File Offset: 0x0003AD46
			// (set) Token: 0x0600116D RID: 4461 RVA: 0x0003CB4E File Offset: 0x0003AD4E
			[DataMember(Name = "subCode", EmitDefaultValue = true)]
			public int? SubCode { get; set; }

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x0600116E RID: 4462 RVA: 0x0003CB57 File Offset: 0x0003AD57
			// (set) Token: 0x0600116F RID: 4463 RVA: 0x0003CB5F File Offset: 0x0003AD5F
			[DataMember(Name = "httpStatusCode", EmitDefaultValue = true)]
			public int? HttpStatusCode { get; set; }
		}

		// Token: 0x02000176 RID: 374
		[DataContract]
		private class PowerBIClusterResolutionResult
		{
			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001171 RID: 4465 RVA: 0x0003CB70 File Offset: 0x0003AD70
			// (set) Token: 0x06001172 RID: 4466 RVA: 0x0003CB78 File Offset: 0x0003AD78
			[DataMember(Name = "DynamicClusterUri")]
			public string DynamicClusterUri { get; set; }

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003CB81 File Offset: 0x0003AD81
			// (set) Token: 0x06001174 RID: 4468 RVA: 0x0003CB89 File Offset: 0x0003AD89
			[DataMember(Name = "FixedClusterUri")]
			public string FixedClusterUri { get; set; }

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001175 RID: 4469 RVA: 0x0003CB92 File Offset: 0x0003AD92
			// (set) Token: 0x06001176 RID: 4470 RVA: 0x0003CB9A File Offset: 0x0003AD9A
			[DataMember(Name = "NewTenantId")]
			public string NewTenantId { get; set; }

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003CBA3 File Offset: 0x0003ADA3
			// (set) Token: 0x06001178 RID: 4472 RVA: 0x0003CBAB File Offset: 0x0003ADAB
			[DataMember(Name = "RuleDescription")]
			public string RuleDescription { get; set; }

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06001179 RID: 4473 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
			// (set) Token: 0x0600117A RID: 4474 RVA: 0x0003CBBC File Offset: 0x0003ADBC
			[DataMember(Name = "TTLSeconds")]
			public int TTLSeconds { get; set; }
		}

		// Token: 0x02000177 RID: 375
		[DataContract]
		private class PowerBIDiscoveryResult
		{
			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x0600117C RID: 4476 RVA: 0x0003CBCD File Offset: 0x0003ADCD
			// (set) Token: 0x0600117D RID: 4477 RVA: 0x0003CBD5 File Offset: 0x0003ADD5
			[DataMember(Name = "environments")]
			public ASAzureUtility.PowerBIEnvironment[] environments { get; set; }
		}

		// Token: 0x02000178 RID: 376
		[DataContract]
		private class PowerBIEnvironment
		{
			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x0600117F RID: 4479 RVA: 0x0003CBE6 File Offset: 0x0003ADE6
			// (set) Token: 0x06001180 RID: 4480 RVA: 0x0003CBEE File Offset: 0x0003ADEE
			[DataMember(Name = "cloudName")]
			public string cloudName { get; set; }

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001181 RID: 4481 RVA: 0x0003CBF7 File Offset: 0x0003ADF7
			// (set) Token: 0x06001182 RID: 4482 RVA: 0x0003CBFF File Offset: 0x0003ADFF
			[DataMember(Name = "services")]
			public ASAzureUtility.PowerBIService[] services { get; set; }
		}

		// Token: 0x02000179 RID: 377
		[DataContract]
		private class PowerBIService
		{
			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001184 RID: 4484 RVA: 0x0003CC10 File Offset: 0x0003AE10
			// (set) Token: 0x06001185 RID: 4485 RVA: 0x0003CC18 File Offset: 0x0003AE18
			[DataMember(Name = "name")]
			public string name { get; set; }

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x06001186 RID: 4486 RVA: 0x0003CC21 File Offset: 0x0003AE21
			// (set) Token: 0x06001187 RID: 4487 RVA: 0x0003CC29 File Offset: 0x0003AE29
			[DataMember(Name = "endpoint")]
			public string endpoint { get; set; }

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x06001188 RID: 4488 RVA: 0x0003CC32 File Offset: 0x0003AE32
			// (set) Token: 0x06001189 RID: 4489 RVA: 0x0003CC3A File Offset: 0x0003AE3A
			[DataMember(Name = "resourceId")]
			public string resourceId { get; set; }

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x0600118A RID: 4490 RVA: 0x0003CC43 File Offset: 0x0003AE43
			// (set) Token: 0x0600118B RID: 4491 RVA: 0x0003CC4B File Offset: 0x0003AE4B
			[DataMember(Name = "allowedDomains")]
			public object allowedDomains { get; set; }
		}
	}
}
