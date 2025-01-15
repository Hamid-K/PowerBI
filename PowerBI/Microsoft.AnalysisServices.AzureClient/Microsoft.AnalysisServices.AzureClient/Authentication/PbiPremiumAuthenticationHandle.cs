using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.AzureClient.Utilities;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000023 RID: 35
	internal sealed class PbiPremiumAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00004834 File Offset: 0x00002A34
		public PbiPremiumAuthenticationHandle(AuthenticationHandle handle, string pbiApiBaseUri, string workspaceObjectId, string capacityObjectId, string databaseName, AuxiliaryPermissionInfo permissionInfo)
			: base(handle.Endpoint, handle.Provider, handle.Tenant)
		{
			this.handle = handle;
			this.pbiApiBaseUri = pbiApiBaseUri;
			this.targetWorkspaceObjectId = workspaceObjectId;
			this.targetCapacityObjectId = capacityObjectId;
			this.targetDatabaseName = databaseName;
			this.permissionInfo = permissionInfo;
			this.token = this.GetMwcToken();
			this.refreshByTimeAsFileTime = this.handle.GetRefreshByTimeAsFileTime();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000048A4 File Offset: 0x00002AA4
		static PbiPremiumAuthenticationHandle()
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
			PbiPremiumAuthenticationHandle.clientVersion = ((customAttributes != null && customAttributes.Length != 0) ? ((AssemblyFileVersionAttribute)customAttributes[0]).Version : "0.0.0.0");
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004A55 File Offset: 0x00002C55
		public static bool UseAadTokenInPublicXmlaEP
		{
			get
			{
				return PbiPremiumAuthenticationHandle.useAadTokenInPublicXmlaEP;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004A5C File Offset: 0x00002C5C
		public override string Principal
		{
			get
			{
				return this.handle.Principal;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004A69 File Offset: 0x00002C69
		public override string AuthenticationScheme
		{
			get
			{
				return "MwcToken";
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004A70 File Offset: 0x00002C70
		public static bool TryResolvePbiWorkspace(string pbiApiBaseUri, string workspaceName, AuthenticationHandle handle, string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out string workspaceObjectId, out string pbiDedicatedRolloutFqdn, out string capacityObjectId, out ResolvePbiWorkspaceErrorReason errorReason, out string workspaceType, out WorkspaceCapacitySkuType201606 skuType, out string technicalDetails)
		{
			if (!string.IsNullOrEmpty(workspaceName))
			{
				workspaceName = Uri.UnescapeDataString(workspaceName).ToLower();
			}
			PbiPremiumAuthenticationHandle.Workspace201606 workspace;
			if (!PbiPremiumAuthenticationHandle.TryResolvePbiWorkspaceImpl(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, servicePrincipalProfileId, out workspace, out errorReason, out technicalDetails))
			{
				workspaceObjectId = null;
				capacityObjectId = null;
				pbiDedicatedRolloutFqdn = null;
				workspaceType = null;
				skuType = WorkspaceCapacitySkuType201606.Premium;
				technicalDetails = AsPaasHelper.GetTechnicalDetailsWithWorkspaceInfo(technicalDetails, workspaceName);
				return false;
			}
			workspaceObjectId = workspace.Id;
			workspaceType = workspace.Type;
			skuType = workspace.GetCapacitySku();
			if (skuType == WorkspaceCapacitySkuType201606.Premium)
			{
				if (string.IsNullOrEmpty(workspace.CapacityUri))
				{
					throw new InvalidDataException(AuthenticationSR.Exception_InvalidPBIPCapacity);
				}
				Uri uri = new Uri(workspace.CapacityUri);
				capacityObjectId = uri.Segments[1];
				pbiDedicatedRolloutFqdn = uri.DnsSafeHost;
			}
			else
			{
				capacityObjectId = null;
				pbiDedicatedRolloutFqdn = null;
			}
			return true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004B30 File Offset: 0x00002D30
		public static bool TryGetDatabaseName(string pbiApiBaseUri, string workspaceId, string workspaceType, string databaseFriendlyName, AuthenticationHandle handle, string requestId, out string databaseName, out ArtifactCapacityState artifactCapacityState, out ResolveDatabaseNameErrorReason errorReason, out string technicalDetails)
		{
			string text = PbiPremiumAuthenticationHandle.BuildDatabaseNameCacheKey(pbiApiBaseUri, handle.Principal, handle.Tenant, handle.GetAccessToken(), workspaceId, databaseFriendlyName);
			PbiPremiumAuthenticationHandle.ArtifactCacheItem artifactCacheItem;
			if (PbiPremiumAuthenticationHandle.artifactCache.Lookup<PbiPremiumAuthenticationHandle.ArtifactCacheItem>(text, out artifactCacheItem))
			{
				databaseName = artifactCacheItem.ArtifactName;
				artifactCapacityState = artifactCacheItem.CapacityState;
				errorReason = ResolveDatabaseNameErrorReason.None;
				technicalDetails = null;
				return true;
			}
			if (!PbiPremiumAuthenticationHandle.TryGetDatabaseNameImpl(pbiApiBaseUri, workspaceId, (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), workspaceType, true), databaseFriendlyName, handle, requestId, out databaseName, out artifactCapacityState, out errorReason, out technicalDetails))
			{
				return false;
			}
			PbiPremiumAuthenticationHandle.artifactCache.Insert(text, new PbiPremiumAuthenticationHandle.ArtifactCacheItem(databaseName, artifactCapacityState));
			return true;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public static bool TryGetDatasetDetailsForAnalyzeInExcel(string pbiApiBaseUri, string datasetObjectId, AuthenticationHandle handle, AuthenticationHandle s2sHandle, string requestId, out string datasetFriendlyName, out string workspaceFriendlyName, out string xmlaEndpointApiDnsName, out ArtifactCapacityState artifactCapacityState)
		{
			string text = PbiPremiumAuthenticationHandle.BuildDatasetDetailsForAixlCacheKey(pbiApiBaseUri, handle, datasetObjectId);
			PbiPremiumAuthenticationHandle.AixlCacheItem aixlCacheItem;
			if (PbiPremiumAuthenticationHandle.artifactCache.Lookup<PbiPremiumAuthenticationHandle.AixlCacheItem>(text, out aixlCacheItem))
			{
				datasetFriendlyName = aixlCacheItem.DatasetFriendlyName;
				workspaceFriendlyName = aixlCacheItem.WorkspaceFriendlyName;
				artifactCapacityState = aixlCacheItem.CapacityState;
				xmlaEndpointApiDnsName = aixlCacheItem.XmlaEndpointApiDnsName;
				return true;
			}
			if (!PbiPremiumAuthenticationHandle.TryGetDatasetDetailsForAnalyzeInExcelImpl(pbiApiBaseUri, datasetObjectId, handle, s2sHandle, requestId, out datasetFriendlyName, out workspaceFriendlyName, out xmlaEndpointApiDnsName, out artifactCapacityState))
			{
				return false;
			}
			PbiPremiumAuthenticationHandle.artifactCache.Insert(text, new PbiPremiumAuthenticationHandle.AixlCacheItem(workspaceFriendlyName, datasetFriendlyName, xmlaEndpointApiDnsName, artifactCapacityState));
			return true;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004C5C File Offset: 0x00002E5C
		public static bool TryGetSensitivityLabel(string pbiInformationProtectionBaseUri, string datasetFriendlyName, AuthenticationHandle handle, string requestId, out string labelId, out int statusCode)
		{
			string text = PbiPremiumAuthenticationHandle.BuildSensitivityLabelCacheKey(pbiInformationProtectionBaseUri, handle, datasetFriendlyName);
			PbiPremiumAuthenticationHandle.SensitivityLabelCacheItem sensitivityLabelCacheItem;
			if (PbiPremiumAuthenticationHandle.sensitivityLabelCache.Lookup<PbiPremiumAuthenticationHandle.SensitivityLabelCacheItem>(text, out sensitivityLabelCacheItem))
			{
				labelId = sensitivityLabelCacheItem.LabelId;
				statusCode = sensitivityLabelCacheItem.StatusCode;
				return true;
			}
			if (!PbiPremiumAuthenticationHandle.TryGetSensitivityLabelImpl(pbiInformationProtectionBaseUri, datasetFriendlyName, handle, requestId, out labelId, out statusCode))
			{
				return false;
			}
			PbiPremiumAuthenticationHandle.sensitivityLabelCache.Insert(text, new PbiPremiumAuthenticationHandle.SensitivityLabelCacheItem(labelId, statusCode));
			return true;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public static string GetAsAzureRedirectedWorkspace(string pbiApiBaseUri, string aasInstance, AuthenticationHandle handle, string requestId, string serviceToServiceToken)
		{
			string text = PbiPremiumAuthenticationHandle.BuildAsAzureRedirectedWorkspaceCacheKey(pbiApiBaseUri, handle, aasInstance, serviceToServiceToken);
			string text2;
			if (PbiPremiumAuthenticationHandle.asAzureRedirectedWorkspaceCache.Lookup<string>(text, out text2))
			{
				return text2;
			}
			string pbiWorkspace;
			try
			{
				IDictionary<string, string> httpHeadersFromRequestIdAndS2SToken = PbiPremiumAuthenticationHandle.GetHttpHeadersFromRequestIdAndS2SToken(requestId, serviceToServiceToken);
				PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest getRedirectedWorkspaceRequest = new PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest
				{
					AasInstance = aasInstance
				};
				HttpStatusCode httpStatusCode;
				PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse getRedirectedWorkspaceResponse = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest, PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/AASRedirect/mappings/lookup", new object[] { pbiApiBaseUri })), httpHeadersFromRequestIdAndS2SToken, handle, getRedirectedWorkspaceRequest, PbiPremiumAuthenticationHandle.getRedirectedWorkspaceRequestSerializer, PbiPremiumAuthenticationHandle.getRedirectedWorkspaceResponseSerializer, false, out pbiWorkspace, out httpStatusCode);
				PbiPremiumAuthenticationHandle.asAzureRedirectedWorkspaceCache.Insert(text, getRedirectedWorkspaceResponse.PbiWorkspace);
				pbiWorkspace = getRedirectedWorkspaceResponse.PbiWorkspace;
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					if (httpWebResponse.StatusCode == (HttpStatusCode)429)
					{
						throw new ASAzureAdalWrapperException(AuthenticationSR.Exception_RedirectionFailWithThrottling, ex);
					}
					if (handle is ExternalAuthenticationHandle && handle.AuthenticationScheme == "Bearer" && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
					{
						throw new ASAzureAdalWrapperException(AuthenticationSR.Exception_RedirectionTokenAsPasswordIsNotSupported, ex);
					}
				}
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_GetRedirectInfo, ex);
			}
			return pbiWorkspace;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004DDC File Offset: 0x00002FDC
		public override string GetAccessToken()
		{
			if (!(this.handle is ExternalAuthenticationHandle) && this.refreshByTimeAsFileTime < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetMwcToken();
				this.refreshByTimeAsFileTime = this.handle.GetRefreshByTimeAsFileTime();
			}
			return this.token.Token;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004E33 File Offset: 0x00003033
		public override long GetRefreshByTimeAsFileTime()
		{
			return this.refreshByTimeAsFileTime;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004E3C File Offset: 0x0000303C
		private static bool TryResolvePbiWorkspaceImpl(string pbiApiBaseUri, string workspaceName, AuthenticationHandle handle, string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out PbiPremiumAuthenticationHandle.Workspace201606 workspace, out ResolvePbiWorkspaceErrorReason errorReason, out string technicalDetails)
		{
			if (string.Compare(handle.AuthenticationScheme, "EmbedToken", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return PbiPremiumAuthenticationHandle.TryResolveSinglePbiWorkspace(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, out workspace, out errorReason, out technicalDetails);
			}
			return PbiPremiumAuthenticationHandle.TryResolveWorkspaceWithWorkspaceResolver(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, servicePrincipalProfileId, out workspace, out errorReason, out technicalDetails);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004E84 File Offset: 0x00003084
		private static bool TryResolveWorkspaceWithWorkspaceResolver(string pbiApiBaseUri, string workspaceName, AuthenticationHandle handle, string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out PbiPremiumAuthenticationHandle.Workspace201606 workspace, out ResolvePbiWorkspaceErrorReason errorReason, out string technicalDetails)
		{
			string text = PbiPremiumAuthenticationHandle.BuildWorkspaceCacheKey(pbiApiBaseUri, handle, workspaceName, serviceToServiceToken, servicePrincipalProfileId);
			PbiPremiumAuthenticationHandle.WorkspaceResolver workspaceResolver;
			if (PbiPremiumAuthenticationHandle.artifactCache.Lookup<PbiPremiumAuthenticationHandle.WorkspaceResolver>(text, out workspaceResolver) && workspaceResolver.TryResolvePbiWorkspace(workspaceName, out workspace, out errorReason))
			{
				technicalDetails = null;
				return true;
			}
			try
			{
				IDictionary<string, string> httpHeadersFromRequestIdAndS2SToken = PbiPremiumAuthenticationHandle.GetHttpHeadersFromRequestIdAndS2SToken(requestId, serviceToServiceToken);
				if (!string.IsNullOrEmpty(servicePrincipalProfileId))
				{
					httpHeadersFromRequestIdAndS2SToken.Add("X-PowerBI-profile-id", servicePrincipalProfileId);
				}
				HttpStatusCode httpStatusCode;
				workspaceResolver = new PbiPremiumAuthenticationHandle.WorkspaceResolver(PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<IList<PbiPremiumAuthenticationHandle.Workspace201606>>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/workspaces", new object[] { pbiApiBaseUri })), httpHeadersFromRequestIdAndS2SToken, handle, PbiPremiumAuthenticationHandle.workspacesSerializer, true, out technicalDetails, out httpStatusCode));
				PbiPremiumAuthenticationHandle.artifactCache.Insert(text, workspaceResolver);
			}
			catch (WebException ex)
			{
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_ResolveWorkspace, ex);
			}
			return workspaceResolver.TryResolvePbiWorkspace(workspaceName, out workspace, out errorReason);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004F50 File Offset: 0x00003150
		private static bool TryResolveSinglePbiWorkspace(string pbiApiBaseUri, string workspaceName, AuthenticationHandle handle, string requestId, string serviceToServiceToken, out PbiPremiumAuthenticationHandle.Workspace201606 workspace, out ResolvePbiWorkspaceErrorReason errorReason, out string technicalDetails)
		{
			string text = PbiPremiumAuthenticationHandle.BuildWorkspaceCacheKey(pbiApiBaseUri, handle, workspaceName, serviceToServiceToken, string.Empty);
			errorReason = ResolvePbiWorkspaceErrorReason.None;
			if (PbiPremiumAuthenticationHandle.artifactCache.Lookup<PbiPremiumAuthenticationHandle.Workspace201606>(text, out workspace))
			{
				technicalDetails = null;
				return true;
			}
			bool flag;
			try
			{
				IDictionary<string, string> httpHeadersFromRequestIdAndS2SToken = PbiPremiumAuthenticationHandle.GetHttpHeadersFromRequestIdAndS2SToken(requestId, serviceToServiceToken);
				HttpStatusCode httpStatusCode;
				workspace = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.Workspace201606>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/workspaces/{1}", new object[] { pbiApiBaseUri, workspaceName })), httpHeadersFromRequestIdAndS2SToken, handle, PbiPremiumAuthenticationHandle.workspaceSerializer, true, out technicalDetails, out httpStatusCode);
				if (!string.IsNullOrEmpty(workspace.Name))
				{
					workspace.Name = workspace.Name.ToLower();
				}
				PbiPremiumAuthenticationHandle.artifactCache.Insert(text, workspace);
				flag = true;
			}
			catch (WebException ex)
			{
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_ResolveWorkspace, ex);
			}
			return flag;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000501C File Offset: 0x0000321C
		private static bool TryGetDatabaseNameImpl(string pbiApiBaseUri, string workspaceId, PbiPremiumAuthenticationHandle.WorkspaceType201606 workspaceType, string datasetFriendlyName, AuthenticationHandle handle, string requestId, out string databaseName, out ArtifactCapacityState artifactCapacityState, out ResolveDatabaseNameErrorReason errorReason, out string technicalDetails)
		{
			artifactCapacityState = ArtifactCapacityState.Unknown;
			bool flag;
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
				dictionary.Add("RequestId", requestId);
				PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest getDatabasesByDatasetNameRequest = new PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest
				{
					DatasetName = datasetFriendlyName,
					WorkspaceType201606 = workspaceType
				};
				HttpStatusCode httpStatusCode;
				IEnumerable<PbiPremiumAuthenticationHandle.Dataset201606> enumerable = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest, IList<PbiPremiumAuthenticationHandle.Dataset201606>>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/workspaces/{1}/getDatabaseName", new object[] { pbiApiBaseUri, workspaceId })), dictionary, handle, getDatabasesByDatasetNameRequest, PbiPremiumAuthenticationHandle.getDBsByDatasetNameRequestSerializer, PbiPremiumAuthenticationHandle.datasetsSerializer, true, out technicalDetails, out httpStatusCode);
				databaseName = null;
				foreach (PbiPremiumAuthenticationHandle.Dataset201606 dataset in enumerable)
				{
					if (dataset != null && !string.IsNullOrEmpty(dataset.DatabaseName))
					{
						if (databaseName != null)
						{
							errorReason = ResolveDatabaseNameErrorReason.DatabaseNameDuplicated;
							databaseName = null;
							artifactCapacityState = ArtifactCapacityState.Unknown;
							return false;
						}
						databaseName = dataset.DatabaseName;
						if (dataset.IsAvailableOnPremiumCapacity != null)
						{
							artifactCapacityState = (dataset.IsAvailableOnPremiumCapacity.Value ? ArtifactCapacityState.AssignedToCapacity : ArtifactCapacityState.NotAssignedToCapacity);
						}
					}
				}
				if (databaseName == null)
				{
					errorReason = ResolveDatabaseNameErrorReason.DatabaseNotFound;
					flag = false;
				}
				else
				{
					errorReason = ResolveDatabaseNameErrorReason.None;
					flag = true;
				}
			}
			catch (WebException ex)
			{
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_GetDatabaseName, ex);
			}
			return flag;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005158 File Offset: 0x00003358
		private static bool TryGetDatasetDetailsForAnalyzeInExcelImpl(string pbiApiBaseUri, string datasetObjectId, AuthenticationHandle handle, AuthenticationHandle s2sHandle, string requestId, out string datasetFriendlyName, out string workspaceFriendlyName, out string xmlaEndpointApiDnsName, out ArtifactCapacityState artifactCapacityState)
		{
			datasetFriendlyName = null;
			workspaceFriendlyName = null;
			xmlaEndpointApiDnsName = null;
			artifactCapacityState = ArtifactCapacityState.Unknown;
			bool flag;
			try
			{
				IDictionary<string, string> httpHeadersFromRequestIdAndS2SToken = PbiPremiumAuthenticationHandle.GetHttpHeadersFromRequestIdAndS2SToken(requestId, (s2sHandle != null) ? s2sHandle.GetAccessToken() : null);
				PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest getDatasetDetailsForAnalyzeInExcelRequest = new PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest
				{
					ClientVersion = PbiPremiumAuthenticationHandle.clientVersion
				};
				string text;
				HttpStatusCode httpStatusCode;
				PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606 datasetWithWorkspace = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest, PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/datasets/{1}/getDatasetDetailsForAnalyzeInExcel", new object[] { pbiApiBaseUri, datasetObjectId })), httpHeadersFromRequestIdAndS2SToken, handle, getDatasetDetailsForAnalyzeInExcelRequest, PbiPremiumAuthenticationHandle.getDatasetDetailsForAnalyzeInExcelRequestSerializer, PbiPremiumAuthenticationHandle.workspaceWithDatasetSerializer, true, out text, out httpStatusCode);
				if (datasetWithWorkspace == null || datasetWithWorkspace.GetWorkspaceType() == PbiPremiumAuthenticationHandle.WorkspaceType201606.Group)
				{
					flag = false;
				}
				else
				{
					datasetFriendlyName = datasetWithWorkspace.DatasetFriendlyName;
					workspaceFriendlyName = ((datasetWithWorkspace.GetWorkspaceType() == PbiPremiumAuthenticationHandle.WorkspaceType201606.User) ? string.Empty : datasetWithWorkspace.WorkspaceFriendlyName);
					xmlaEndpointApiDnsName = datasetWithWorkspace.XmlaEndpointApiDNSName ?? "api.powerbi.com";
					if (datasetWithWorkspace.IsAvailableOnFabric != null)
					{
						artifactCapacityState = (datasetWithWorkspace.IsAvailableOnFabric.Value ? ArtifactCapacityState.AssignedToCapacity : ArtifactCapacityState.NotAssignedToCapacity);
					}
					flag = true;
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005260 File Offset: 0x00003460
		private static bool TryGetSensitivityLabelImpl(string pbiInformationProtectionBaseUri, string datasetFriendlyName, AuthenticationHandle handle, string requestId, out string labelId, out int statusCode)
		{
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1) { { "RequestId", requestId } };
				string text;
				HttpStatusCode httpStatusCode;
				PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002 artifactInformationProtectionV = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002>(new Uri(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					pbiInformationProtectionBaseUri.TrimEnd(new char[] { '/' }),
					datasetFriendlyName
				})), dictionary, handle, PbiPremiumAuthenticationHandle.informationProtectionSerializer, true, out text, out httpStatusCode);
				Guid? guid = ((artifactInformationProtectionV != null) ? new Guid?(artifactInformationProtectionV.LabelId) : null);
				Guid empty = Guid.Empty;
				if (guid != empty)
				{
					Guid? guid2;
					if (artifactInformationProtectionV == null)
					{
						guid = null;
						guid2 = guid;
					}
					else
					{
						guid2 = new Guid?(artifactInformationProtectionV.LabelId);
					}
					guid = guid2;
					labelId = ((guid != null) ? guid.GetValueOrDefault().ToString() : null);
					statusCode = (int)httpStatusCode;
					return true;
				}
			}
			catch (Exception)
			{
			}
			labelId = null;
			statusCode = 300;
			return false;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005374 File Offset: 0x00003574
		private static TResult ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<TResult>(Uri request, IDictionary<string, string> headers, AuthenticationHandle handle, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			return PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<object, TResult>(request, "GET", headers, handle, null, null, responseSerializer, getTechnicalDetails, out technicalDetails, out status);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005398 File Offset: 0x00003598
		private static TResult ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<TRequest, TResult>(Uri request, IDictionary<string, string> headers, AuthenticationHandle handle, TRequest requestBody, DataContractJsonSerializer requestSerializer, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			return PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<TRequest, TResult>(request, "POST", headers, handle, requestBody, requestSerializer, responseSerializer, getTechnicalDetails, out technicalDetails, out status);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000053C0 File Offset: 0x000035C0
		private static TResult ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<TRequest, TResult>(Uri request, string method, IDictionary<string, string> headers, AuthenticationHandle handle, TRequest requestBody, DataContractJsonSerializer requestSerializer, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			status = HttpStatusCode.Unused;
			headers = ((headers == null) ? new Dictionary<string, string>(1) : new Dictionary<string, string>(headers));
			headers.Add("Authorization", string.Format("{0} {1}", handle.AuthenticationScheme, handle.GetAccessToken()));
			technicalDetails = null;
			TResult tresult = default(TResult);
			bool flag = false;
			try
			{
				string text = PbiPremiumAuthenticationHandle.BuildHomeTenantUriCacheKey(handle);
				UriBuilder uriBuilder = new UriBuilder(request);
				uriBuilder.Query = "PreferClientRouting=true";
				string text2;
				if (PbiPremiumAuthenticationHandle.tokenCache.Lookup<string>(text, out text2))
				{
					uriBuilder.Host = text2;
				}
				ConnectivityHelper.JsonHttpRequestOptions jsonHttpRequestOptions = ConnectivityHelper.JsonHttpRequestOptions.SetContentLength | ConnectivityHelper.JsonHttpRequestOptions.RetryOnServiceUnavailable | ConnectivityHelper.JsonHttpRequestOptions.TargetingPbiShared;
				if (getTechnicalDetails)
				{
					jsonHttpRequestOptions |= ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails;
				}
				int num = 0;
				do
				{
					WebHeaderCollection webHeaderCollection;
					tresult = ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uriBuilder.Uri, method, headers, requestBody, jsonHttpRequestOptions, -1, requestSerializer, responseSerializer, true, true, out status, out technicalDetails, out webHeaderCollection);
					if (!string.IsNullOrEmpty(webHeaderCollection["Location"]))
					{
						uriBuilder = new UriBuilder(webHeaderCollection["Location"]);
					}
				}
				while (status == HttpStatusCode.TemporaryRedirect && ++num < 3);
				if (status == HttpStatusCode.TemporaryRedirect && num == 3)
				{
					flag = true;
				}
				else if (string.Compare(uriBuilder.Host, request.Host, StringComparison.OrdinalIgnoreCase) != 0)
				{
					PbiPremiumAuthenticationHandle.tokenCache.Insert(text, uriBuilder.Host);
				}
			}
			catch (WebException)
			{
				flag = true;
			}
			if (flag)
			{
				ConnectivityHelper.JsonHttpRequestOptions jsonHttpRequestOptions2 = ConnectivityHelper.JsonHttpRequestOptions.SetContentLength | ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect | ConnectivityHelper.JsonHttpRequestOptions.TargetingPbiShared;
				if (getTechnicalDetails)
				{
					jsonHttpRequestOptions2 |= ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails;
				}
				WebHeaderCollection webHeaderCollection2;
				tresult = ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(request, method, headers, requestBody, jsonHttpRequestOptions2, -1, requestSerializer, responseSerializer, false, true, out status, out technicalDetails, out webHeaderCollection2);
			}
			return tresult;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005534 File Offset: 0x00003734
		private static string BuildWorkspaceCacheKey(string pbiApiBaseUri, AuthenticationHandle handle, string workspaceName, string serviceToServiceToken, string servicePrincipalProfileId)
		{
			string text = (string.IsNullOrEmpty(serviceToServiceToken) ? string.Empty : serviceToServiceToken.GetHashCode().ToString());
			string text2 = (string.IsNullOrEmpty(servicePrincipalProfileId) ? string.Empty : servicePrincipalProfileId);
			if (string.Compare(handle.AuthenticationScheme, "EmbedToken", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "workspace|{0}|{1}|{2}|{3}", new object[]
				{
					handle.GetAccessToken(),
					pbiApiBaseUri,
					workspaceName,
					text
				});
			}
			if (string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "workspaces|{0}|{1}|{2}|{3}", new object[]
				{
					handle.GetAccessToken(),
					pbiApiBaseUri,
					text,
					text2
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "workspaces|{0}|{1}|{2}|{3}|{4}", new object[] { handle.Principal, handle.Tenant, pbiApiBaseUri, text, text2 });
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000561C File Offset: 0x0000381C
		private static string BuildDatabaseNameCacheKey(string pbiApiBaseUri, string principal, string tenant, string aadToken, string workspaceId, string databaseFriendlyName)
		{
			if (!string.IsNullOrEmpty(principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "dbname|{0}|{1}|{2}|{3}|{4}", new object[] { principal, tenant, pbiApiBaseUri, workspaceId, databaseFriendlyName });
			}
			return string.Format(CultureInfo.InvariantCulture, "dbname|{0}|{1}|{2}|{3}", new object[] { aadToken, pbiApiBaseUri, workspaceId, databaseFriendlyName });
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005684 File Offset: 0x00003884
		private static string BuildAsAzureRedirectedWorkspaceCacheKey(string pbiApiBaseUri, AuthenticationHandle handle, string aasInstance, string serviceToServiceToken)
		{
			string text = (string.IsNullOrEmpty(serviceToServiceToken) ? string.Empty : serviceToServiceToken.GetHashCode().ToString());
			string text2 = (string.IsNullOrEmpty(handle.Principal) ? handle.GetAccessToken() : string.Format("{0}|{1}", handle.Principal, handle.Tenant));
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}", new object[] { pbiApiBaseUri, text2, aasInstance, text });
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005700 File Offset: 0x00003900
		private static string BuildHomeTenantUriCacheKey(AuthenticationHandle handle)
		{
			return string.Format(CultureInfo.InvariantCulture, "hometenanturi|{0}", new object[] { string.IsNullOrEmpty(handle.Principal) ? handle.GetAccessToken() : string.Format("{0}|{1}", handle.Principal, handle.Tenant) });
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005750 File Offset: 0x00003950
		private static string BuildDatasetDetailsForAixlCacheKey(string pbiApiBaseUrl, AuthenticationHandle handle, string databaseObjectId)
		{
			databaseObjectId = databaseObjectId.ToLower();
			if (!string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "aixl|{0}|{1}|{2}", new object[] { handle.Principal, handle.Tenant, databaseObjectId });
			}
			return string.Format(CultureInfo.InvariantCulture, "aixl|{0}|{1}", new object[]
			{
				handle.GetAccessToken(),
				databaseObjectId
			});
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057C0 File Offset: 0x000039C0
		private static string BuildSensitivityLabelCacheKey(string pbiApiBaseUrl, AuthenticationHandle handle, string datasetFriendlyName)
		{
			datasetFriendlyName = datasetFriendlyName.ToLower();
			if (!string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "senlab|{0}|{1}|{2}|{3}", new object[] { pbiApiBaseUrl, handle.Principal, handle.Tenant, datasetFriendlyName });
			}
			return string.Format(CultureInfo.InvariantCulture, "senlab|{0}|{1}|{2}", new object[]
			{
				pbiApiBaseUrl,
				handle.GetAccessToken(),
				datasetFriendlyName
			});
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005838 File Offset: 0x00003A38
		private static IDictionary<string, string> GetHttpHeadersFromRequestIdAndS2SToken(string requestId, string serviceToServiceToken)
		{
			bool flag = !string.IsNullOrEmpty(serviceToServiceToken);
			IDictionary<string, string> dictionary = new Dictionary<string, string>(flag ? 2 : 1);
			dictionary.Add("RequestId", requestId);
			if (flag)
			{
				dictionary.Add("x-ms-xls2stoken", serviceToServiceToken);
			}
			return dictionary;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005878 File Offset: 0x00003A78
		private static Exception ConvertPbiRequestErrorToConnectionException(string action, WebException ex)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new ASAzureAdalWrapperException(AuthenticationSR.Exception_PbiRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromPbiSharedResponse(ex.Response), Environment.NewLine), ex);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000058AC File Offset: 0x00003AAC
		private PbiPremiumAuthenticationHandle.MWCToken GetMwcToken()
		{
			string text = this.BuildMwcTokenCacheKey();
			PbiPremiumAuthenticationHandle.MWCToken mwctoken;
			if (PbiPremiumAuthenticationHandle.tokenCache.Lookup<PbiPremiumAuthenticationHandle.MWCToken>(text, out mwctoken))
			{
				return mwctoken;
			}
			try
			{
				IDictionary<string, string> dictionary = ((this.permissionInfo.RequireServiceToServiceToken || this.permissionInfo.HasServicePrincipalProfile) ? new Dictionary<string, string>(2) : null);
				if (this.permissionInfo.RequireServiceToServiceToken)
				{
					dictionary.Add("x-ms-xls2stoken", this.permissionInfo.ServiceToServiceToken);
				}
				if (this.permissionInfo.HasServicePrincipalProfile)
				{
					dictionary.Add("X-PowerBI-profile-id", this.permissionInfo.ServicePrincipalProfileId);
				}
				PbiPremiumAuthenticationHandle.MWCASTokenRequest mwcastokenRequest = new PbiPremiumAuthenticationHandle.MWCASTokenRequest
				{
					CapacityObjectId = this.targetCapacityObjectId,
					WorkspaceObjectId = this.targetWorkspaceObjectId,
					DatasetName = this.targetDatabaseName,
					ApplyAuxiliaryPermission = this.permissionInfo.ApplyAuxiliaryPermission,
					AuxiliaryPermissionOwner = (this.permissionInfo.ApplyAuxiliaryPermission ? this.permissionInfo.AuxiliaryPermissionOwner : null),
					BypassBuildPermission = this.permissionInfo.BypassBuildPermission,
					IntendedUsage = this.permissionInfo.IntendedUsage,
					SourceCapacityObjectId = (this.permissionInfo.SkipThrottling ? this.permissionInfo.SourceCapacityObjectId : null)
				};
				string text2;
				HttpStatusCode httpStatusCode;
				mwctoken = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.MWCASTokenRequest, PbiPremiumAuthenticationHandle.MWCToken>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/metadata/v201606/generateastoken", new object[] { this.pbiApiBaseUri })), dictionary, this.handle, mwcastokenRequest, PbiPremiumAuthenticationHandle.mwcTokenRequestSerializer, PbiPremiumAuthenticationHandle.mwcTokenSerializer, false, out text2, out httpStatusCode);
			}
			catch (WebException ex)
			{
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_GetMwcToken, ex);
			}
			PbiPremiumAuthenticationHandle.tokenCache.Insert(text, mwctoken);
			return mwctoken;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005A6C File Offset: 0x00003C6C
		private string BuildMwcTokenCacheKey()
		{
			if (this.permissionInfo.RequireServiceToServiceToken)
			{
				return string.Format(CultureInfo.InvariantCulture, "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", new object[]
				{
					this.pbiApiBaseUri,
					this.targetWorkspaceObjectId,
					this.targetCapacityObjectId,
					this.handle.GetAccessToken(),
					this.targetDatabaseName,
					this.permissionInfo.AuxiliaryPermissionOwner,
					this.permissionInfo.ServiceToServiceToken.GetHashCode(),
					this.permissionInfo.IntendedUsage,
					this.permissionInfo.ServicePrincipalProfileId,
					this.permissionInfo.BypassBuildPermission
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}", new object[]
			{
				this.pbiApiBaseUri,
				this.targetWorkspaceObjectId,
				this.targetCapacityObjectId,
				this.handle.GetAccessToken(),
				this.targetDatabaseName,
				this.permissionInfo.ServicePrincipalProfileId
			});
		}

		// Token: 0x0400008E RID: 142
		internal const string EnvVariable_UseAadTokenInPublicXmlaEp = "AS_USE_AAD_TOKEN_PUBLIC_XMLA_EP";

		// Token: 0x0400008F RID: 143
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x04000090 RID: 144
		private const string ClientRoutingUriQuery = "PreferClientRouting=true";

		// Token: 0x04000091 RID: 145
		private const int ClientRoutingRetryCount = 3;

		// Token: 0x04000092 RID: 146
		private const string XmlaEndpointApiDnsNameDefault = "api.powerbi.com";

		// Token: 0x04000093 RID: 147
		private const string PbiGetMultiWorkspacesRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces";

		// Token: 0x04000094 RID: 148
		private const string PbiGetSingleWorkspaceRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces/{1}";

		// Token: 0x04000095 RID: 149
		private const string PbiGenerateMwcTokenUriFormat = "https://{0}/metadata/v201606/generateastoken";

		// Token: 0x04000096 RID: 150
		private const string PbiGetDatabaseNameRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces/{1}/getDatabaseName";

		// Token: 0x04000097 RID: 151
		private const string PbiGetAsAzureRedirectedWorkspaceUriFormat = "https://{0}/AASRedirect/mappings/lookup";

		// Token: 0x04000098 RID: 152
		private const string PbiGetDatasetDetailsUriFormat = "https://{0}/powerbi/databases/v201606/datasets/{1}/getDatasetDetailsForAnalyzeInExcel";

		// Token: 0x04000099 RID: 153
		private const string MultiWorkspacesCacheKeyFromUpnFormat = "workspaces|{0}|{1}|{2}|{3}|{4}";

		// Token: 0x0400009A RID: 154
		private const string MultiWorkspacesCacheKeyFromTokenFormat = "workspaces|{0}|{1}|{2}|{3}";

		// Token: 0x0400009B RID: 155
		private const string SingleWorkspaceCacheKeyFromTokenFormat = "workspace|{0}|{1}|{2}|{3}";

		// Token: 0x0400009C RID: 156
		private const string DatabaseCacheKeyFromUpnFormat = "dbname|{0}|{1}|{2}|{3}|{4}";

		// Token: 0x0400009D RID: 157
		private const string DatabaseCacheKeyFromTokenFormat = "dbname|{0}|{1}|{2}|{3}";

		// Token: 0x0400009E RID: 158
		private const string MwcTokenCacheKeyFormat = "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}";

		// Token: 0x0400009F RID: 159
		private const string MwcTokenWithS2SKeyFormat = "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}";

		// Token: 0x040000A0 RID: 160
		private const string AsAzureRedirectedWorkspaceCacheKeyFormat = "{0}|{1}|{2}|{3}";

		// Token: 0x040000A1 RID: 161
		private const string HomeTenantUriCacheKeyFormat = "hometenanturi|{0}";

		// Token: 0x040000A2 RID: 162
		private const string DatasetDetailsCacheKeyFromUpnFormat = "aixl|{0}|{1}|{2}";

		// Token: 0x040000A3 RID: 163
		private const string DatasetDetailsCacheKeyFromTokenFormat = "aixl|{0}|{1}";

		// Token: 0x040000A4 RID: 164
		private const string SensitivityLabelCacheKeyFromUpnFormat = "senlab|{0}|{1}|{2}|{3}";

		// Token: 0x040000A5 RID: 165
		private const string SensitivityLabelCacheKeyFromTokenFormat = "senlab|{0}|{1}|{2}";

		// Token: 0x040000A6 RID: 166
		private static MemoryCache artifactCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)));

		// Token: 0x040000A7 RID: 167
		private static MemoryCache tokenCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(5.0)));

		// Token: 0x040000A8 RID: 168
		private static MemoryCache sensitivityLabelCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)));

		// Token: 0x040000A9 RID: 169
		private static SharedMemoryCache asAzureRedirectedWorkspaceCache = SharedMemoryCache.Create("XmlaLibAsAzureRedirectedWorkspaceCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), null, null);

		// Token: 0x040000AA RID: 170
		private static readonly DataContractJsonSerializer workspacesSerializer = new DataContractJsonSerializer(typeof(IList<PbiPremiumAuthenticationHandle.Workspace201606>));

		// Token: 0x040000AB RID: 171
		private static readonly DataContractJsonSerializer workspaceSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.Workspace201606));

		// Token: 0x040000AC RID: 172
		private static readonly DataContractJsonSerializer getDBsByDatasetNameRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest));

		// Token: 0x040000AD RID: 173
		private static readonly DataContractJsonSerializer datasetsSerializer = new DataContractJsonSerializer(typeof(IList<PbiPremiumAuthenticationHandle.Dataset201606>));

		// Token: 0x040000AE RID: 174
		private static readonly DataContractJsonSerializer mwcTokenRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.MWCASTokenRequest));

		// Token: 0x040000AF RID: 175
		private static readonly DataContractJsonSerializer mwcTokenSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.MWCToken));

		// Token: 0x040000B0 RID: 176
		private static readonly DataContractJsonSerializer getDatasetDetailsForAnalyzeInExcelRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest));

		// Token: 0x040000B1 RID: 177
		private static readonly DataContractJsonSerializer workspaceWithDatasetSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606));

		// Token: 0x040000B2 RID: 178
		private static readonly DataContractJsonSerializer informationProtectionSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002));

		// Token: 0x040000B3 RID: 179
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest));

		// Token: 0x040000B4 RID: 180
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceResponseSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse));

		// Token: 0x040000B5 RID: 181
		private static readonly bool useAadTokenInPublicXmlaEP = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AS_USE_AAD_TOKEN_PUBLIC_XMLA_EP"));

		// Token: 0x040000B6 RID: 182
		private static readonly string clientVersion;

		// Token: 0x040000B7 RID: 183
		private readonly AuthenticationHandle handle;

		// Token: 0x040000B8 RID: 184
		private readonly string pbiApiBaseUri;

		// Token: 0x040000B9 RID: 185
		private readonly string targetWorkspaceObjectId;

		// Token: 0x040000BA RID: 186
		private readonly string targetCapacityObjectId;

		// Token: 0x040000BB RID: 187
		private readonly string targetDatabaseName;

		// Token: 0x040000BC RID: 188
		private readonly AuxiliaryPermissionInfo permissionInfo;

		// Token: 0x040000BD RID: 189
		private PbiPremiumAuthenticationHandle.MWCToken token;

		// Token: 0x040000BE RID: 190
		private long refreshByTimeAsFileTime;

		// Token: 0x02000059 RID: 89
		[DataContract]
		private enum WorkspaceType201606
		{
			// Token: 0x040001B4 RID: 436
			[EnumMember(Value = "User")]
			User,
			// Token: 0x040001B5 RID: 437
			[EnumMember(Value = "Group")]
			Group,
			// Token: 0x040001B6 RID: 438
			[EnumMember(Value = "Folder")]
			Folder
		}

		// Token: 0x0200005A RID: 90
		private struct ArtifactCacheItem
		{
			// Token: 0x06000274 RID: 628 RVA: 0x0000BB73 File Offset: 0x00009D73
			public ArtifactCacheItem(string artifactName, ArtifactCapacityState capacityState)
			{
				this.ArtifactName = artifactName;
				this.CapacityState = capacityState;
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000275 RID: 629 RVA: 0x0000BB83 File Offset: 0x00009D83
			public string ArtifactName { get; }

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000276 RID: 630 RVA: 0x0000BB8B File Offset: 0x00009D8B
			public ArtifactCapacityState CapacityState { get; }
		}

		// Token: 0x0200005B RID: 91
		[DataContract]
		private sealed class Workspace201606
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000277 RID: 631 RVA: 0x0000BB93 File Offset: 0x00009D93
			// (set) Token: 0x06000278 RID: 632 RVA: 0x0000BB9B File Offset: 0x00009D9B
			[DataMember(Order = 0, Name = "id")]
			public string Id { get; set; }

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000279 RID: 633 RVA: 0x0000BBA4 File Offset: 0x00009DA4
			// (set) Token: 0x0600027A RID: 634 RVA: 0x0000BBAC File Offset: 0x00009DAC
			[DataMember(Order = 10, Name = "name")]
			public string Name { get; set; }

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600027B RID: 635 RVA: 0x0000BBB5 File Offset: 0x00009DB5
			// (set) Token: 0x0600027C RID: 636 RVA: 0x0000BBBD File Offset: 0x00009DBD
			[DataMember(Order = 20, Name = "type")]
			public string Type { get; set; }

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BBC6 File Offset: 0x00009DC6
			// (set) Token: 0x0600027E RID: 638 RVA: 0x0000BBCE File Offset: 0x00009DCE
			[DataMember(Order = 30, Name = "capacitySku")]
			public string CapacitySku { get; set; }

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000BBD7 File Offset: 0x00009DD7
			// (set) Token: 0x06000280 RID: 640 RVA: 0x0000BBDF File Offset: 0x00009DDF
			[DataMember(Order = 40, Name = "capacityUri", EmitDefaultValue = true)]
			public string CapacityUri { get; set; }

			// Token: 0x06000281 RID: 641 RVA: 0x0000BBE8 File Offset: 0x00009DE8
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 GetWorkspaceType()
			{
				return (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), this.Type);
			}

			// Token: 0x06000282 RID: 642 RVA: 0x0000BC04 File Offset: 0x00009E04
			public WorkspaceCapacitySkuType201606 GetCapacitySku()
			{
				return (WorkspaceCapacitySkuType201606)Enum.Parse(typeof(WorkspaceCapacitySkuType201606), this.CapacitySku);
			}
		}

		// Token: 0x0200005C RID: 92
		[DataContract]
		private sealed class DatasetWithWorkspace201606
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000284 RID: 644 RVA: 0x0000BC28 File Offset: 0x00009E28
			// (set) Token: 0x06000285 RID: 645 RVA: 0x0000BC30 File Offset: 0x00009E30
			[DataMember(Order = 0, Name = "workspaceObjectId")]
			public string WorkspaceObjectId { get; set; }

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000286 RID: 646 RVA: 0x0000BC39 File Offset: 0x00009E39
			// (set) Token: 0x06000287 RID: 647 RVA: 0x0000BC41 File Offset: 0x00009E41
			[DataMember(Order = 10, Name = "workspaceFriendlyName")]
			public string WorkspaceFriendlyName { get; set; }

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000288 RID: 648 RVA: 0x0000BC4A File Offset: 0x00009E4A
			// (set) Token: 0x06000289 RID: 649 RVA: 0x0000BC52 File Offset: 0x00009E52
			[DataMember(Order = 20, Name = "workspaceType")]
			public string WorkspaceType { get; set; }

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600028A RID: 650 RVA: 0x0000BC5B File Offset: 0x00009E5B
			// (set) Token: 0x0600028B RID: 651 RVA: 0x0000BC63 File Offset: 0x00009E63
			[DataMember(Order = 30, Name = "datasetObjectId")]
			public string DatasetObjectId { get; set; }

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x0600028C RID: 652 RVA: 0x0000BC6C File Offset: 0x00009E6C
			// (set) Token: 0x0600028D RID: 653 RVA: 0x0000BC74 File Offset: 0x00009E74
			[DataMember(Order = 40, Name = "datasetFriendlyName")]
			public string DatasetFriendlyName { get; set; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x0600028E RID: 654 RVA: 0x0000BC7D File Offset: 0x00009E7D
			// (set) Token: 0x0600028F RID: 655 RVA: 0x0000BC85 File Offset: 0x00009E85
			[DataMember(Order = 50, Name = "virtualServerName")]
			public string VirtualServerName { get; set; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000290 RID: 656 RVA: 0x0000BC8E File Offset: 0x00009E8E
			// (set) Token: 0x06000291 RID: 657 RVA: 0x0000BC96 File Offset: 0x00009E96
			[DataMember(Order = 60, Name = "isAvailableOnFabric", EmitDefaultValue = false, IsRequired = false)]
			public bool? IsAvailableOnFabric { get; set; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000292 RID: 658 RVA: 0x0000BC9F File Offset: 0x00009E9F
			// (set) Token: 0x06000293 RID: 659 RVA: 0x0000BCA7 File Offset: 0x00009EA7
			[DataMember(Order = 70, Name = "xmlaEndpointApiDNSName", EmitDefaultValue = false, IsRequired = false)]
			public string XmlaEndpointApiDNSName { get; set; }

			// Token: 0x06000294 RID: 660 RVA: 0x0000BCB0 File Offset: 0x00009EB0
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 GetWorkspaceType()
			{
				return (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), this.WorkspaceType);
			}
		}

		// Token: 0x0200005D RID: 93
		[DataContract]
		private sealed class GetDatasetDetailsForAnalyzeInExcelRequest
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x06000296 RID: 662 RVA: 0x0000BCD4 File Offset: 0x00009ED4
			// (set) Token: 0x06000297 RID: 663 RVA: 0x0000BCDC File Offset: 0x00009EDC
			[DataMember(Order = 10, Name = "clientVersion")]
			public string ClientVersion { get; set; }
		}

		// Token: 0x0200005E RID: 94
		[DataContract]
		public class ArtifactInformationProtectionV202002
		{
			// Token: 0x17000076 RID: 118
			// (get) Token: 0x06000299 RID: 665 RVA: 0x0000BCED File Offset: 0x00009EED
			// (set) Token: 0x0600029A RID: 666 RVA: 0x0000BCF5 File Offset: 0x00009EF5
			[DataMember(Order = 10, Name = "labelId", EmitDefaultValue = false)]
			public Guid LabelId { get; set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600029B RID: 667 RVA: 0x0000BCFE File Offset: 0x00009EFE
			// (set) Token: 0x0600029C RID: 668 RVA: 0x0000BD06 File Offset: 0x00009F06
			[DataMember(Order = 40, Name = "artifactId", EmitDefaultValue = false)]
			public long ArtifactId { get; set; }
		}

		// Token: 0x0200005F RID: 95
		private struct AixlCacheItem
		{
			// Token: 0x0600029E RID: 670 RVA: 0x0000BD17 File Offset: 0x00009F17
			public AixlCacheItem(string workspaceFriendlyName, string datasetFriendlyName, string xmlaEndpointApiDnsName, ArtifactCapacityState artifactCapacityState)
			{
				this.WorkspaceFriendlyName = workspaceFriendlyName;
				this.DatasetFriendlyName = datasetFriendlyName;
				this.XmlaEndpointApiDnsName = xmlaEndpointApiDnsName;
				this.CapacityState = artifactCapacityState;
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x0600029F RID: 671 RVA: 0x0000BD36 File Offset: 0x00009F36
			public string WorkspaceFriendlyName { get; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000BD3E File Offset: 0x00009F3E
			public string DatasetFriendlyName { get; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000BD46 File Offset: 0x00009F46
			public string XmlaEndpointApiDnsName { get; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000BD4E File Offset: 0x00009F4E
			public ArtifactCapacityState CapacityState { get; }
		}

		// Token: 0x02000060 RID: 96
		private struct SensitivityLabelCacheItem
		{
			// Token: 0x060002A3 RID: 675 RVA: 0x0000BD56 File Offset: 0x00009F56
			public SensitivityLabelCacheItem(string labelId, int statusCode)
			{
				this.LabelId = labelId;
				this.StatusCode = statusCode;
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000BD66 File Offset: 0x00009F66
			public string LabelId { get; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000BD6E File Offset: 0x00009F6E
			public int StatusCode { get; }
		}

		// Token: 0x02000061 RID: 97
		[DataContract]
		private sealed class GetDatabasesByDatasetNameRequest
		{
			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000BD76 File Offset: 0x00009F76
			// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000BD7E File Offset: 0x00009F7E
			[DataMember(Order = 10, Name = "datasetName")]
			public string DatasetName { get; set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000BD87 File Offset: 0x00009F87
			// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000BD8F File Offset: 0x00009F8F
			[DataMember(Order = 20, Name = "workspaceType")]
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 WorkspaceType201606 { get; set; }
		}

		// Token: 0x02000062 RID: 98
		[DataContract]
		private sealed class Dataset201606
		{
			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060002AB RID: 683 RVA: 0x0000BDA0 File Offset: 0x00009FA0
			// (set) Token: 0x060002AC RID: 684 RVA: 0x0000BDA8 File Offset: 0x00009FA8
			[DataMember(Order = 0, Name = "databaseName")]
			public string DatabaseName { get; set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060002AD RID: 685 RVA: 0x0000BDB1 File Offset: 0x00009FB1
			// (set) Token: 0x060002AE RID: 686 RVA: 0x0000BDB9 File Offset: 0x00009FB9
			[DataMember(Order = 10, Name = "datasetName")]
			public string DatasetName { get; set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BDC2 File Offset: 0x00009FC2
			// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000BDCA File Offset: 0x00009FCA
			[DataMember(Order = 20, Name = "versionEtag")]
			public string VersionEtag { get; set; }

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BDD3 File Offset: 0x00009FD3
			// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000BDDB File Offset: 0x00009FDB
			[DataMember(Order = 30, Name = "virtualServerName")]
			public string VirtualServerName { get; set; }

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000BDE4 File Offset: 0x00009FE4
			// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000BDEC File Offset: 0x00009FEC
			[DataMember(Order = 40, Name = "creatorUserPrincipalName")]
			public string CreatorUserPrincipalName { get; set; }

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000BDF5 File Offset: 0x00009FF5
			// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000BDFD File Offset: 0x00009FFD
			[DataMember(Order = 50, Name = "isAvailableOnPremiumCapacity", IsRequired = false)]
			public bool? IsAvailableOnPremiumCapacity { get; set; }
		}

		// Token: 0x02000063 RID: 99
		[DataContract]
		private sealed class MWCASTokenRequest
		{
			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000BE0E File Offset: 0x0000A00E
			// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000BE16 File Offset: 0x0000A016
			[DataMember(Name = "capacityObjectId", IsRequired = true)]
			public string CapacityObjectId { get; set; }

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060002BA RID: 698 RVA: 0x0000BE1F File Offset: 0x0000A01F
			// (set) Token: 0x060002BB RID: 699 RVA: 0x0000BE27 File Offset: 0x0000A027
			[DataMember(Name = "workspaceObjectId", IsRequired = true)]
			public string WorkspaceObjectId { get; set; }

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000BE30 File Offset: 0x0000A030
			// (set) Token: 0x060002BD RID: 701 RVA: 0x0000BE38 File Offset: 0x0000A038
			[DataMember(Name = "datasetName", IsRequired = false)]
			public string DatasetName { get; set; }

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BE41 File Offset: 0x0000A041
			// (set) Token: 0x060002BF RID: 703 RVA: 0x0000BE49 File Offset: 0x0000A049
			[DataMember(Name = "applyAuxiliaryPermission", IsRequired = false)]
			public bool ApplyAuxiliaryPermission { get; set; }

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000BE52 File Offset: 0x0000A052
			// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000BE5A File Offset: 0x0000A05A
			[DataMember(Name = "auxiliaryPermissionOwner", IsRequired = false)]
			public string AuxiliaryPermissionOwner { get; set; }

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000BE63 File Offset: 0x0000A063
			// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000BE6B File Offset: 0x0000A06B
			[DataMember(Name = "bypassBuildPermission", IsRequired = false)]
			public bool BypassBuildPermission { get; set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000BE74 File Offset: 0x0000A074
			// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000BE7C File Offset: 0x0000A07C
			[DataMember(Name = "intendedUsage", IsRequired = false)]
			public int IntendedUsage { get; set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000BE85 File Offset: 0x0000A085
			// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000BE8D File Offset: 0x0000A08D
			[DataMember(Name = "sourceCapacityObjectId", IsRequired = false)]
			public string SourceCapacityObjectId { get; set; }
		}

		// Token: 0x02000064 RID: 100
		[DataContract]
		private sealed class MWCToken
		{
			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000BE9E File Offset: 0x0000A09E
			// (set) Token: 0x060002CA RID: 714 RVA: 0x0000BEA6 File Offset: 0x0000A0A6
			[DataMember]
			public string Token { get; set; }
		}

		// Token: 0x02000065 RID: 101
		[DataContract]
		private sealed class GetRedirectedWorkspaceRequest
		{
			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BEB7 File Offset: 0x0000A0B7
			// (set) Token: 0x060002CD RID: 717 RVA: 0x0000BEBF File Offset: 0x0000A0BF
			[DataMember(Name = "aasInstance", IsRequired = true)]
			public string AasInstance { get; set; }
		}

		// Token: 0x02000066 RID: 102
		[DataContract]
		private sealed class GetRedirectedWorkspaceResponse
		{
			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060002CF RID: 719 RVA: 0x0000BED0 File Offset: 0x0000A0D0
			// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000BED8 File Offset: 0x0000A0D8
			[DataMember(Name = "pbiWorkspace")]
			public string PbiWorkspace { get; set; }
		}

		// Token: 0x02000067 RID: 103
		private sealed class WorkspaceResolver
		{
			// Token: 0x060002D2 RID: 722 RVA: 0x0000BEE9 File Offset: 0x0000A0E9
			public WorkspaceResolver(IList<PbiPremiumAuthenticationHandle.Workspace201606> workspaces)
			{
				this.Initialize(workspaces);
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
			public bool TryResolvePbiWorkspace(string workspaceName, out PbiPremiumAuthenticationHandle.Workspace201606 workspace, out ResolvePbiWorkspaceErrorReason errorReason)
			{
				if (string.IsNullOrEmpty(workspaceName))
				{
					if (this.personalWorkspace == null)
					{
						workspace = null;
						errorReason = ResolvePbiWorkspaceErrorReason.WorkspaceNotFound;
						return false;
					}
					workspace = this.personalWorkspace;
				}
				else
				{
					if (this.conflictedNames.Contains(workspaceName))
					{
						workspace = null;
						errorReason = ResolvePbiWorkspaceErrorReason.WorkspaceNameDuplicated;
						return false;
					}
					if (!this.friendlyNameMap.TryGetValue(workspaceName, out workspace))
					{
						errorReason = ResolvePbiWorkspaceErrorReason.WorkspaceNotFound;
						return false;
					}
				}
				errorReason = ResolvePbiWorkspaceErrorReason.None;
				return true;
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000BF55 File Offset: 0x0000A155
			private static string GetConflictResolverWorkspaceName(string workspaceName, string workspaceId)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[] { workspaceName, workspaceId });
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x0000BF74 File Offset: 0x0000A174
			private void Initialize(IList<PbiPremiumAuthenticationHandle.Workspace201606> workspaces)
			{
				this.friendlyNameMap = new Dictionary<string, PbiPremiumAuthenticationHandle.Workspace201606>();
				this.conflictedNames = new HashSet<string>();
				foreach (PbiPremiumAuthenticationHandle.Workspace201606 workspace in workspaces)
				{
					if (workspace.GetWorkspaceType() == PbiPremiumAuthenticationHandle.WorkspaceType201606.User)
					{
						this.personalWorkspace = workspace;
					}
					else if (!string.IsNullOrEmpty(workspace.Name))
					{
						workspace.Name = workspace.Name.ToLower();
						if (this.conflictedNames.Contains(workspace.Name) || this.friendlyNameMap.ContainsKey(workspace.Name))
						{
							PbiPremiumAuthenticationHandle.Workspace201606 workspace2;
							if (this.friendlyNameMap.TryGetValue(workspace.Name, out workspace2))
							{
								this.conflictedNames.Add(workspace.Name);
								this.friendlyNameMap.Remove(workspace.Name);
								string conflictResolverWorkspaceName = PbiPremiumAuthenticationHandle.WorkspaceResolver.GetConflictResolverWorkspaceName(workspace2.Name, workspace2.Id);
								if (this.friendlyNameMap.ContainsKey(conflictResolverWorkspaceName))
								{
									this.conflictedNames.Add(conflictResolverWorkspaceName);
									this.friendlyNameMap.Remove(conflictResolverWorkspaceName);
								}
								else if (!this.conflictedNames.Contains(conflictResolverWorkspaceName))
								{
									this.friendlyNameMap[conflictResolverWorkspaceName] = workspace2;
								}
							}
							string conflictResolverWorkspaceName2 = PbiPremiumAuthenticationHandle.WorkspaceResolver.GetConflictResolverWorkspaceName(workspace.Name, workspace.Id);
							if (this.friendlyNameMap.ContainsKey(conflictResolverWorkspaceName2))
							{
								this.conflictedNames.Add(conflictResolverWorkspaceName2);
								this.friendlyNameMap.Remove(conflictResolverWorkspaceName2);
							}
							else if (!this.conflictedNames.Contains(conflictResolverWorkspaceName2))
							{
								this.friendlyNameMap[conflictResolverWorkspaceName2] = workspace;
							}
						}
						else
						{
							this.friendlyNameMap[workspace.Name] = workspace;
						}
					}
				}
			}

			// Token: 0x040001E2 RID: 482
			private const string WorkspaceNameResolverFormat = "{0}-{1}";

			// Token: 0x040001E3 RID: 483
			private Dictionary<string, PbiPremiumAuthenticationHandle.Workspace201606> friendlyNameMap;

			// Token: 0x040001E4 RID: 484
			private HashSet<string> conflictedNames;

			// Token: 0x040001E5 RID: 485
			private PbiPremiumAuthenticationHandle.Workspace201606 personalWorkspace;
		}
	}
}
