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
		// Token: 0x06000113 RID: 275 RVA: 0x00007308 File Offset: 0x00005508
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

		// Token: 0x06000114 RID: 276 RVA: 0x000073F0 File Offset: 0x000055F0
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

		// Token: 0x06000115 RID: 277 RVA: 0x00007474 File Offset: 0x00005674
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

		// Token: 0x06000116 RID: 278 RVA: 0x000074DE File Offset: 0x000056DE
		public static string ConstructAsAzureSecureServerConnUri(string dataSourceUri)
		{
			return new UriBuilder(dataSourceUri)
			{
				Scheme = Uri.UriSchemeHttps,
				Path = "/webapi/xmla"
			}.ToString();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007501 File Offset: 0x00005701
		public static string ConstructPbiPremiumServerConnUri(string rolloutFqdn)
		{
			return new UriBuilder(string.Format(CultureInfo.InvariantCulture, "https://{0}", rolloutFqdn))
			{
				Path = "/webapi/xmla"
			}.ToString();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007528 File Offset: 0x00005728
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

		// Token: 0x06000119 RID: 281 RVA: 0x0000762C File Offset: 0x0000582C
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

		// Token: 0x0600011A RID: 282 RVA: 0x00007754 File Offset: 0x00005954
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

		// Token: 0x0600011B RID: 283 RVA: 0x000077FC File Offset: 0x000059FC
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

		// Token: 0x0600011C RID: 284 RVA: 0x000078F8 File Offset: 0x00005AF8
		public static Exception GetConnectionException(WebException ex, AsInstanceType asInstanceType)
		{
			return ASAzureUtility.GetConnectionException(null, true, ex, asInstanceType);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007904 File Offset: 0x00005B04
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

		// Token: 0x0600011E RID: 286 RVA: 0x000079B8 File Offset: 0x00005BB8
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

		// Token: 0x0600011F RID: 287 RVA: 0x00007A44 File Offset: 0x00005C44
		private static string HandlePbiDefaultTenant(string tenant)
		{
			tenant = HttpUtility.UrlDecode(tenant);
			if (string.Compare(tenant, "myorg", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			return tenant;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007A60 File Offset: 0x00005C60
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

		// Token: 0x06000121 RID: 289 RVA: 0x00007A9C File Offset: 0x00005C9C
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

		// Token: 0x06000122 RID: 290 RVA: 0x00007B8C File Offset: 0x00005D8C
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

		// Token: 0x06000123 RID: 291 RVA: 0x00007C81 File Offset: 0x00005E81
		private static bool SupportsExtendedErrorInformation(AsInstanceType asInstanceType)
		{
			return asInstanceType == AsInstanceType.PbiPremiumInternal || asInstanceType == AsInstanceType.AsAzure || asInstanceType == AsInstanceType.PbiPremiumXmlaEp;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007C91 File Offset: 0x00005E91
		private static bool SupportsErrorDetailsHeader(AsInstanceType asInstanceType)
		{
			return asInstanceType != AsInstanceType.PbiPremiumInternal;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007C9C File Offset: 0x00005E9C
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

		// Token: 0x06000126 RID: 294 RVA: 0x00007E00 File Offset: 0x00006000
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

		// Token: 0x04000179 RID: 377
		private const int LinkReferenceUriPort = 443;

		// Token: 0x0400017A RID: 378
		private const string PbiDedicatedRolloutUriFormat = "https://{0}";

		// Token: 0x0400017B RID: 379
		private const string XmlaExecutionUriPath = "/webapi/xmla";

		// Token: 0x0400017C RID: 380
		private const string NameResolutionUriPath = "/webapi/clusterResolve";

		// Token: 0x0400017D RID: 381
		private const string DefaultPbiTenant = "myorg";

		// Token: 0x0400017E RID: 382
		private const string AASErrorCode_ServerPausedOrScaling = "ServerPausedOrScaling";

		// Token: 0x0400017F RID: 383
		private const string AASErrorCode_ServerNotReady = "ServerNotReady";

		// Token: 0x04000180 RID: 384
		private const string AASErrorCode_ServerNotFoundForConnectionMode = "ServerNotFoundForConnectionMode";

		// Token: 0x04000181 RID: 385
		private const string MwcErrorCode_XmlaEndpointDisabled = "XmlaEndpointDisabled";

		// Token: 0x04000182 RID: 386
		private const string MwcErrorCode_CapacityNotActive = "CapacityNotActive";

		// Token: 0x04000183 RID: 387
		private const string AsAzureRedirectionCacheKeyFormat = "{0}|{1}";

		// Token: 0x04000184 RID: 388
		private const string PbiASAzureRedirectionCheckUriFormat = "https://{0}/AASRedirect/public/mappings/exists";

		// Token: 0x04000185 RID: 389
		private const int ASAzureRedirectionMaxRetryAttempts = 2;

		// Token: 0x04000186 RID: 390
		private const int ASAzureRedirectionDelayMsBetweenRetries = 1000;

		// Token: 0x04000187 RID: 391
		private static readonly SharedMemoryCache asAzureRedirectionCache = SharedMemoryCache.Create("XmlaLibAsAzureRedirectionCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), null, null);

		// Token: 0x04000188 RID: 392
		private static readonly DataContractJsonSerializer ASPaaSErrorSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.ASPaaSError));

		// Token: 0x04000189 RID: 393
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceRequestSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.GetRedirectedWorkspaceRequest));

		// Token: 0x0400018A RID: 394
		private static readonly DataContractJsonSerializer getRedirectionExistenceResultSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.GetRedirectionExistenceResult));

		// Token: 0x0200016F RID: 367
		private static class ASPaaSExtendedErrorInformationMembers
		{
			// Token: 0x04000BCD RID: 3021
			public const string Code = "Code";

			// Token: 0x04000BCE RID: 3022
			public const string RootActivityId = "RootActivityId";

			// Token: 0x04000BCF RID: 3023
			public const string CurrentUtcDate = "UtcDate";

			// Token: 0x04000BD0 RID: 3024
			public const string SubCode = "SubCode";

			// Token: 0x04000BD1 RID: 3025
			public const string HttpStatusCode = "HttpStatusCode";
		}

		// Token: 0x02000170 RID: 368
		private static class PublicXmlaUriVersions
		{
			// Token: 0x04000BD2 RID: 3026
			public const string V1 = "v1.0";
		}

		// Token: 0x02000171 RID: 369
		[DataContract]
		private sealed class GetRedirectedWorkspaceRequest
		{
			// Token: 0x1700062D RID: 1581
			// (get) Token: 0x06001147 RID: 4423 RVA: 0x0003C74C File Offset: 0x0003A94C
			// (set) Token: 0x06001148 RID: 4424 RVA: 0x0003C754 File Offset: 0x0003A954
			[DataMember(Name = "aasInstance", IsRequired = true)]
			public string AasInstance { get; set; }
		}

		// Token: 0x02000172 RID: 370
		[DataContract]
		private sealed class GetRedirectionExistenceResult
		{
			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x0600114A RID: 4426 RVA: 0x0003C765 File Offset: 0x0003A965
			// (set) Token: 0x0600114B RID: 4427 RVA: 0x0003C76D File Offset: 0x0003A96D
			[DataMember(Name = "tenantId", IsRequired = true)]
			public string TenantId { get; set; }
		}

		// Token: 0x02000173 RID: 371
		[DataContract]
		private class NameResolutionRequest
		{
			// Token: 0x1700062F RID: 1583
			// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003C77E File Offset: 0x0003A97E
			// (set) Token: 0x0600114E RID: 4430 RVA: 0x0003C786 File Offset: 0x0003A986
			[DataMember(Name = "serverName")]
			public string ServerName { get; set; }

			// Token: 0x17000630 RID: 1584
			// (get) Token: 0x0600114F RID: 4431 RVA: 0x0003C78F File Offset: 0x0003A98F
			// (set) Token: 0x06001150 RID: 4432 RVA: 0x0003C797 File Offset: 0x0003A997
			[DataMember(Name = "databaseName")]
			public string DatabaseName { get; set; }

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x06001151 RID: 4433 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
			// (set) Token: 0x06001152 RID: 4434 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
			[DataMember(Name = "premiumPublicXmlaEndpoint")]
			public bool PremiumPublicXmlaEndpoint { get; set; }
		}

		// Token: 0x02000174 RID: 372
		[DataContract]
		private class NameResolutionResult
		{
			// Token: 0x17000632 RID: 1586
			// (get) Token: 0x06001154 RID: 4436 RVA: 0x0003C7B9 File Offset: 0x0003A9B9
			// (set) Token: 0x06001155 RID: 4437 RVA: 0x0003C7C1 File Offset: 0x0003A9C1
			[DataMember(Name = "clusterFQDN")]
			public string ClusterFqdn { get; set; }

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06001156 RID: 4438 RVA: 0x0003C7CA File Offset: 0x0003A9CA
			// (set) Token: 0x06001157 RID: 4439 RVA: 0x0003C7D2 File Offset: 0x0003A9D2
			[DataMember(Name = "coreServerName")]
			public string CoreServerName { get; set; }

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06001158 RID: 4440 RVA: 0x0003C7DB File Offset: 0x0003A9DB
			// (set) Token: 0x06001159 RID: 4441 RVA: 0x0003C7E3 File Offset: 0x0003A9E3
			[DataMember(Name = "tenantId")]
			public string TenantId { get; set; }
		}

		// Token: 0x02000175 RID: 373
		[DataContract]
		private class ASPaaSError
		{
			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x0600115B RID: 4443 RVA: 0x0003C7F4 File Offset: 0x0003A9F4
			// (set) Token: 0x0600115C RID: 4444 RVA: 0x0003C7FC File Offset: 0x0003A9FC
			[DataMember(Name = "code")]
			public string Code { get; set; }

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x0600115D RID: 4445 RVA: 0x0003C805 File Offset: 0x0003AA05
			// (set) Token: 0x0600115E RID: 4446 RVA: 0x0003C80D File Offset: 0x0003AA0D
			[DataMember(Name = "message")]
			public string Message { get; set; }

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x0600115F RID: 4447 RVA: 0x0003C816 File Offset: 0x0003AA16
			// (set) Token: 0x06001160 RID: 4448 RVA: 0x0003C81E File Offset: 0x0003AA1E
			[DataMember(Name = "subCode", EmitDefaultValue = true)]
			public int? SubCode { get; set; }

			// Token: 0x17000638 RID: 1592
			// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003C827 File Offset: 0x0003AA27
			// (set) Token: 0x06001162 RID: 4450 RVA: 0x0003C82F File Offset: 0x0003AA2F
			[DataMember(Name = "httpStatusCode", EmitDefaultValue = true)]
			public int? HttpStatusCode { get; set; }
		}

		// Token: 0x02000176 RID: 374
		[DataContract]
		private class PowerBIClusterResolutionResult
		{
			// Token: 0x17000639 RID: 1593
			// (get) Token: 0x06001164 RID: 4452 RVA: 0x0003C840 File Offset: 0x0003AA40
			// (set) Token: 0x06001165 RID: 4453 RVA: 0x0003C848 File Offset: 0x0003AA48
			[DataMember(Name = "DynamicClusterUri")]
			public string DynamicClusterUri { get; set; }

			// Token: 0x1700063A RID: 1594
			// (get) Token: 0x06001166 RID: 4454 RVA: 0x0003C851 File Offset: 0x0003AA51
			// (set) Token: 0x06001167 RID: 4455 RVA: 0x0003C859 File Offset: 0x0003AA59
			[DataMember(Name = "FixedClusterUri")]
			public string FixedClusterUri { get; set; }

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06001168 RID: 4456 RVA: 0x0003C862 File Offset: 0x0003AA62
			// (set) Token: 0x06001169 RID: 4457 RVA: 0x0003C86A File Offset: 0x0003AA6A
			[DataMember(Name = "NewTenantId")]
			public string NewTenantId { get; set; }

			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x0600116A RID: 4458 RVA: 0x0003C873 File Offset: 0x0003AA73
			// (set) Token: 0x0600116B RID: 4459 RVA: 0x0003C87B File Offset: 0x0003AA7B
			[DataMember(Name = "RuleDescription")]
			public string RuleDescription { get; set; }

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x0600116C RID: 4460 RVA: 0x0003C884 File Offset: 0x0003AA84
			// (set) Token: 0x0600116D RID: 4461 RVA: 0x0003C88C File Offset: 0x0003AA8C
			[DataMember(Name = "TTLSeconds")]
			public int TTLSeconds { get; set; }
		}

		// Token: 0x02000177 RID: 375
		[DataContract]
		private class PowerBIDiscoveryResult
		{
			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x0600116F RID: 4463 RVA: 0x0003C89D File Offset: 0x0003AA9D
			// (set) Token: 0x06001170 RID: 4464 RVA: 0x0003C8A5 File Offset: 0x0003AAA5
			[DataMember(Name = "environments")]
			public ASAzureUtility.PowerBIEnvironment[] environments { get; set; }
		}

		// Token: 0x02000178 RID: 376
		[DataContract]
		private class PowerBIEnvironment
		{
			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001172 RID: 4466 RVA: 0x0003C8B6 File Offset: 0x0003AAB6
			// (set) Token: 0x06001173 RID: 4467 RVA: 0x0003C8BE File Offset: 0x0003AABE
			[DataMember(Name = "cloudName")]
			public string cloudName { get; set; }

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001174 RID: 4468 RVA: 0x0003C8C7 File Offset: 0x0003AAC7
			// (set) Token: 0x06001175 RID: 4469 RVA: 0x0003C8CF File Offset: 0x0003AACF
			[DataMember(Name = "services")]
			public ASAzureUtility.PowerBIService[] services { get; set; }
		}

		// Token: 0x02000179 RID: 377
		[DataContract]
		private class PowerBIService
		{
			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003C8E0 File Offset: 0x0003AAE0
			// (set) Token: 0x06001178 RID: 4472 RVA: 0x0003C8E8 File Offset: 0x0003AAE8
			[DataMember(Name = "name")]
			public string name { get; set; }

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06001179 RID: 4473 RVA: 0x0003C8F1 File Offset: 0x0003AAF1
			// (set) Token: 0x0600117A RID: 4474 RVA: 0x0003C8F9 File Offset: 0x0003AAF9
			[DataMember(Name = "endpoint")]
			public string endpoint { get; set; }

			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x0600117B RID: 4475 RVA: 0x0003C902 File Offset: 0x0003AB02
			// (set) Token: 0x0600117C RID: 4476 RVA: 0x0003C90A File Offset: 0x0003AB0A
			[DataMember(Name = "resourceId")]
			public string resourceId { get; set; }

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x0600117D RID: 4477 RVA: 0x0003C913 File Offset: 0x0003AB13
			// (set) Token: 0x0600117E RID: 4478 RVA: 0x0003C91B File Offset: 0x0003AB1B
			[DataMember(Name = "allowedDomains")]
			public object allowedDomains { get; set; }
		}
	}
}
