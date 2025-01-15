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
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000034 RID: 52
	internal static class ASAzureUtility
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000A51C File Offset: 0x0000871C
		public static void ExtractPbiPublicXmlaTenantAndWorkspace(string dataSource, out string tenant, out string workspace)
		{
			Uri uri = new Uri(dataSource);
			tenant = null;
			workspace = null;
			if (uri.Segments.Length <= 1)
			{
				throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat(dataSource));
			}
			if (string.Compare(uri.Segments[1].Trim(new char[] { '/' }), "v1.0", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ConnectionException(XmlaSR.XmlaClient_PbiPublicXmla_InvalidDataSourceUriFormat(dataSource));
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

		// Token: 0x060001AD RID: 429 RVA: 0x0000A5F0 File Offset: 0x000087F0
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
					throw new ConnectionException(XmlaSR.ConnectionString_ASAzure_InvalidLinkReferenceCustomPort(dataSourceUri), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
				}
				string text = ASAzureUtility.GetHttpDataAsString(uri).Trim();
				if (!ASAzureUtility.IsValidAsAzureUri(text))
				{
					throw new ConnectionException(XmlaSR.ConnectionString_ASAzure_InvalidLinkReferenceUri(dataSourceUri), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
				}
				text2 = text;
			}
			catch (WebException)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_ASAzure_FetchLinkReferenceFailed(dataSourceUri), null, ConnectionExceptionCause.LinkReferenceResolutionFailed);
			}
			return text2;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A674 File Offset: 0x00008874
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

		// Token: 0x060001AF RID: 431 RVA: 0x0000A6DE File Offset: 0x000088DE
		public static string ConstructAsAzureSecureServerConnUri(string dataSourceUri)
		{
			return new UriBuilder(dataSourceUri)
			{
				Scheme = Uri.UriSchemeHttps,
				Path = "/webapi/xmla"
			}.ToString();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A701 File Offset: 0x00008901
		public static string ConstructPbiPremiumServerConnUri(string rolloutFqdn)
		{
			return new UriBuilder(string.Format(CultureInfo.InvariantCulture, "https://{0}", rolloutFqdn))
			{
				Path = "/webapi/xmla"
			}.ToString();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A728 File Offset: 0x00008928
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

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A82C File Offset: 0x00008A2C
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
						throw new ConnectionException(ex.Message, ex);
					}
					ASAzureUtility.HandleAsAzureRedirectedToPowerBIWorkspaceResponse(httpWebResponse.StatusCode, aasInstance, null, ex, ref num, out flag, out keyValuePair);
				}
			}
			while (flag);
			ASAzureUtility.asAzureRedirectionCache.Insert(text, keyValuePair);
			tenantId = keyValuePair.Value;
			return keyValuePair.Key;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A954 File Offset: 0x00008B54
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

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A9FC File Offset: 0x00008BFC
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

		// Token: 0x060001B5 RID: 437 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public static Exception GetConnectionException(WebException ex, AsInstanceType asInstanceType)
		{
			return ASAzureUtility.GetConnectionException(null, true, ex, asInstanceType);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000AB04 File Offset: 0x00008D04
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

		// Token: 0x060001B7 RID: 439 RVA: 0x0000ABB8 File Offset: 0x00008DB8
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

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AC44 File Offset: 0x00008E44
		private static string HandlePbiDefaultTenant(string tenant)
		{
			tenant = HttpUtility.UrlDecode(tenant);
			if (string.Compare(tenant, "myorg", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			return tenant;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AC60 File Offset: 0x00008E60
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

		// Token: 0x060001BA RID: 442 RVA: 0x0000AC9C File Offset: 0x00008E9C
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

		// Token: 0x060001BB RID: 443 RVA: 0x0000AD8C File Offset: 0x00008F8C
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

		// Token: 0x060001BC RID: 444 RVA: 0x0000AE81 File Offset: 0x00009081
		private static bool SupportsExtendedErrorInformation(AsInstanceType asInstanceType)
		{
			return asInstanceType == AsInstanceType.PbiPremiumInternal || asInstanceType == AsInstanceType.AsAzure || asInstanceType == AsInstanceType.PbiPremiumXmlaEp;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000AE91 File Offset: 0x00009091
		private static bool SupportsErrorDetailsHeader(AsInstanceType asInstanceType)
		{
			return asInstanceType != AsInstanceType.PbiPremiumInternal;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000AE9C File Offset: 0x0000909C
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

		// Token: 0x060001BF RID: 447 RVA: 0x0000B000 File Offset: 0x00009200
		private static Exception CreateConnectionException(string message, ConnectionExceptionCause exceptionCause, IDictionary<string, object> extendedErrorInformation, Exception innerException)
		{
			Exception ex = ((innerException != null) ? new ConnectionException(message, innerException, exceptionCause) : new ConnectionException(message, exceptionCause));
			if (extendedErrorInformation != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in extendedErrorInformation)
				{
					ex.Data[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return ex;
		}

		// Token: 0x040001CB RID: 459
		private const int LinkReferenceUriPort = 443;

		// Token: 0x040001CC RID: 460
		private const string PbiDedicatedRolloutUriFormat = "https://{0}";

		// Token: 0x040001CD RID: 461
		private const string XmlaExecutionUriPath = "/webapi/xmla";

		// Token: 0x040001CE RID: 462
		private const string NameResolutionUriPath = "/webapi/clusterResolve";

		// Token: 0x040001CF RID: 463
		private const string DefaultPbiTenant = "myorg";

		// Token: 0x040001D0 RID: 464
		private const string AASErrorCode_ServerPausedOrScaling = "ServerPausedOrScaling";

		// Token: 0x040001D1 RID: 465
		private const string AASErrorCode_ServerNotReady = "ServerNotReady";

		// Token: 0x040001D2 RID: 466
		private const string AASErrorCode_ServerNotFoundForConnectionMode = "ServerNotFoundForConnectionMode";

		// Token: 0x040001D3 RID: 467
		private const string MwcErrorCode_XmlaEndpointDisabled = "XmlaEndpointDisabled";

		// Token: 0x040001D4 RID: 468
		private const string MwcErrorCode_CapacityNotActive = "CapacityNotActive";

		// Token: 0x040001D5 RID: 469
		private const string AsAzureRedirectionCacheKeyFormat = "{0}|{1}";

		// Token: 0x040001D6 RID: 470
		private const string PbiASAzureRedirectionCheckUriFormat = "https://{0}/AASRedirect/public/mappings/exists";

		// Token: 0x040001D7 RID: 471
		private const int ASAzureRedirectionMaxRetryAttempts = 2;

		// Token: 0x040001D8 RID: 472
		private const int ASAzureRedirectionDelayMsBetweenRetries = 1000;

		// Token: 0x040001D9 RID: 473
		private static readonly SharedMemoryCache asAzureRedirectionCache = SharedMemoryCache.Create("XmlaLibAsAzureRedirectionCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), null, null);

		// Token: 0x040001DA RID: 474
		private static readonly DataContractJsonSerializer ASPaaSErrorSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.ASPaaSError));

		// Token: 0x040001DB RID: 475
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceRequestSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.GetRedirectedWorkspaceRequest));

		// Token: 0x040001DC RID: 476
		private static readonly DataContractJsonSerializer getRedirectionExistenceResultSerializer = new DataContractJsonSerializer(typeof(ASAzureUtility.GetRedirectionExistenceResult));

		// Token: 0x0200016B RID: 363
		private static class ASPaaSExtendedErrorInformationMembers
		{
			// Token: 0x04000B98 RID: 2968
			public const string Code = "Code";

			// Token: 0x04000B99 RID: 2969
			public const string RootActivityId = "RootActivityId";

			// Token: 0x04000B9A RID: 2970
			public const string CurrentUtcDate = "UtcDate";

			// Token: 0x04000B9B RID: 2971
			public const string SubCode = "SubCode";

			// Token: 0x04000B9C RID: 2972
			public const string HttpStatusCode = "HttpStatusCode";
		}

		// Token: 0x0200016C RID: 364
		private static class PublicXmlaUriVersions
		{
			// Token: 0x04000B9D RID: 2973
			public const string V1 = "v1.0";
		}

		// Token: 0x0200016D RID: 365
		[DataContract]
		private sealed class GetRedirectedWorkspaceRequest
		{
			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0003F2DC File Offset: 0x0003D4DC
			// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0003F2E4 File Offset: 0x0003D4E4
			[DataMember(Name = "aasInstance", IsRequired = true)]
			public string AasInstance { get; set; }
		}

		// Token: 0x0200016E RID: 366
		[DataContract]
		private sealed class GetRedirectionExistenceResult
		{
			// Token: 0x170005F8 RID: 1528
			// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0003F2F5 File Offset: 0x0003D4F5
			// (set) Token: 0x060011F6 RID: 4598 RVA: 0x0003F2FD File Offset: 0x0003D4FD
			[DataMember(Name = "tenantId", IsRequired = true)]
			public string TenantId { get; set; }
		}

		// Token: 0x0200016F RID: 367
		[DataContract]
		private class NameResolutionRequest
		{
			// Token: 0x170005F9 RID: 1529
			// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0003F30E File Offset: 0x0003D50E
			// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0003F316 File Offset: 0x0003D516
			[DataMember(Name = "serverName")]
			public string ServerName { get; set; }

			// Token: 0x170005FA RID: 1530
			// (get) Token: 0x060011FA RID: 4602 RVA: 0x0003F31F File Offset: 0x0003D51F
			// (set) Token: 0x060011FB RID: 4603 RVA: 0x0003F327 File Offset: 0x0003D527
			[DataMember(Name = "databaseName")]
			public string DatabaseName { get; set; }

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x060011FC RID: 4604 RVA: 0x0003F330 File Offset: 0x0003D530
			// (set) Token: 0x060011FD RID: 4605 RVA: 0x0003F338 File Offset: 0x0003D538
			[DataMember(Name = "premiumPublicXmlaEndpoint")]
			public bool PremiumPublicXmlaEndpoint { get; set; }
		}

		// Token: 0x02000170 RID: 368
		[DataContract]
		private class NameResolutionResult
		{
			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x060011FF RID: 4607 RVA: 0x0003F349 File Offset: 0x0003D549
			// (set) Token: 0x06001200 RID: 4608 RVA: 0x0003F351 File Offset: 0x0003D551
			[DataMember(Name = "clusterFQDN")]
			public string ClusterFqdn { get; set; }

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001201 RID: 4609 RVA: 0x0003F35A File Offset: 0x0003D55A
			// (set) Token: 0x06001202 RID: 4610 RVA: 0x0003F362 File Offset: 0x0003D562
			[DataMember(Name = "coreServerName")]
			public string CoreServerName { get; set; }

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001203 RID: 4611 RVA: 0x0003F36B File Offset: 0x0003D56B
			// (set) Token: 0x06001204 RID: 4612 RVA: 0x0003F373 File Offset: 0x0003D573
			[DataMember(Name = "tenantId")]
			public string TenantId { get; set; }
		}

		// Token: 0x02000171 RID: 369
		[DataContract]
		private class ASPaaSError
		{
			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001206 RID: 4614 RVA: 0x0003F384 File Offset: 0x0003D584
			// (set) Token: 0x06001207 RID: 4615 RVA: 0x0003F38C File Offset: 0x0003D58C
			[DataMember(Name = "code")]
			public string Code { get; set; }

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001208 RID: 4616 RVA: 0x0003F395 File Offset: 0x0003D595
			// (set) Token: 0x06001209 RID: 4617 RVA: 0x0003F39D File Offset: 0x0003D59D
			[DataMember(Name = "message")]
			public string Message { get; set; }

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x0600120A RID: 4618 RVA: 0x0003F3A6 File Offset: 0x0003D5A6
			// (set) Token: 0x0600120B RID: 4619 RVA: 0x0003F3AE File Offset: 0x0003D5AE
			[DataMember(Name = "subCode", EmitDefaultValue = true)]
			public int? SubCode { get; set; }

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x0600120C RID: 4620 RVA: 0x0003F3B7 File Offset: 0x0003D5B7
			// (set) Token: 0x0600120D RID: 4621 RVA: 0x0003F3BF File Offset: 0x0003D5BF
			[DataMember(Name = "httpStatusCode", EmitDefaultValue = true)]
			public int? HttpStatusCode { get; set; }
		}

		// Token: 0x02000172 RID: 370
		[DataContract]
		private class PowerBIClusterResolutionResult
		{
			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x0600120F RID: 4623 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
			// (set) Token: 0x06001210 RID: 4624 RVA: 0x0003F3D8 File Offset: 0x0003D5D8
			[DataMember(Name = "DynamicClusterUri")]
			public string DynamicClusterUri { get; set; }

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001211 RID: 4625 RVA: 0x0003F3E1 File Offset: 0x0003D5E1
			// (set) Token: 0x06001212 RID: 4626 RVA: 0x0003F3E9 File Offset: 0x0003D5E9
			[DataMember(Name = "FixedClusterUri")]
			public string FixedClusterUri { get; set; }

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001213 RID: 4627 RVA: 0x0003F3F2 File Offset: 0x0003D5F2
			// (set) Token: 0x06001214 RID: 4628 RVA: 0x0003F3FA File Offset: 0x0003D5FA
			[DataMember(Name = "NewTenantId")]
			public string NewTenantId { get; set; }

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001215 RID: 4629 RVA: 0x0003F403 File Offset: 0x0003D603
			// (set) Token: 0x06001216 RID: 4630 RVA: 0x0003F40B File Offset: 0x0003D60B
			[DataMember(Name = "RuleDescription")]
			public string RuleDescription { get; set; }

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001217 RID: 4631 RVA: 0x0003F414 File Offset: 0x0003D614
			// (set) Token: 0x06001218 RID: 4632 RVA: 0x0003F41C File Offset: 0x0003D61C
			[DataMember(Name = "TTLSeconds")]
			public int TTLSeconds { get; set; }
		}

		// Token: 0x02000173 RID: 371
		[DataContract]
		private class PowerBIDiscoveryResult
		{
			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x0600121A RID: 4634 RVA: 0x0003F42D File Offset: 0x0003D62D
			// (set) Token: 0x0600121B RID: 4635 RVA: 0x0003F435 File Offset: 0x0003D635
			[DataMember(Name = "environments")]
			public ASAzureUtility.PowerBIEnvironment[] environments { get; set; }
		}

		// Token: 0x02000174 RID: 372
		[DataContract]
		private class PowerBIEnvironment
		{
			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x0600121D RID: 4637 RVA: 0x0003F446 File Offset: 0x0003D646
			// (set) Token: 0x0600121E RID: 4638 RVA: 0x0003F44E File Offset: 0x0003D64E
			[DataMember(Name = "cloudName")]
			public string cloudName { get; set; }

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x0600121F RID: 4639 RVA: 0x0003F457 File Offset: 0x0003D657
			// (set) Token: 0x06001220 RID: 4640 RVA: 0x0003F45F File Offset: 0x0003D65F
			[DataMember(Name = "services")]
			public ASAzureUtility.PowerBIService[] services { get; set; }
		}

		// Token: 0x02000175 RID: 373
		[DataContract]
		private class PowerBIService
		{
			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06001222 RID: 4642 RVA: 0x0003F470 File Offset: 0x0003D670
			// (set) Token: 0x06001223 RID: 4643 RVA: 0x0003F478 File Offset: 0x0003D678
			[DataMember(Name = "name")]
			public string name { get; set; }

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06001224 RID: 4644 RVA: 0x0003F481 File Offset: 0x0003D681
			// (set) Token: 0x06001225 RID: 4645 RVA: 0x0003F489 File Offset: 0x0003D689
			[DataMember(Name = "endpoint")]
			public string endpoint { get; set; }

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06001226 RID: 4646 RVA: 0x0003F492 File Offset: 0x0003D692
			// (set) Token: 0x06001227 RID: 4647 RVA: 0x0003F49A File Offset: 0x0003D69A
			[DataMember(Name = "resourceId")]
			public string resourceId { get; set; }

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06001228 RID: 4648 RVA: 0x0003F4A3 File Offset: 0x0003D6A3
			// (set) Token: 0x06001229 RID: 4649 RVA: 0x0003F4AB File Offset: 0x0003D6AB
			[DataMember(Name = "allowedDomains")]
			public object allowedDomains { get; set; }
		}
	}
}
