using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000FF RID: 255
	internal sealed class PbiPremiumAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x000360BC File Offset: 0x000342BC
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

		// Token: 0x06000FBB RID: 4027 RVA: 0x0003612C File Offset: 0x0003432C
		static PbiPremiumAuthenticationHandle()
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
			PbiPremiumAuthenticationHandle.clientVersion = ((customAttributes != null && customAttributes.Length != 0) ? ((AssemblyFileVersionAttribute)customAttributes[0]).Version : "0.0.0.0");
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x000362DD File Offset: 0x000344DD
		public static bool UseAadTokenInPublicXmlaEP
		{
			get
			{
				return PbiPremiumAuthenticationHandle.useAadTokenInPublicXmlaEP;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000362E4 File Offset: 0x000344E4
		public override string Principal
		{
			get
			{
				return this.handle.Principal;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000362F1 File Offset: 0x000344F1
		public override string AuthenticationScheme
		{
			get
			{
				return "MwcToken";
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000362F8 File Offset: 0x000344F8
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

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000363B8 File Offset: 0x000345B8
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

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003645C File Offset: 0x0003465C
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

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000364E4 File Offset: 0x000346E4
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

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003654C File Offset: 0x0003474C
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
				PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse getRedirectedWorkspaceResponse = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest, PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/AASRedirect/mappings/lookup", pbiApiBaseUri)), httpHeadersFromRequestIdAndS2SToken, handle, getRedirectedWorkspaceRequest, PbiPremiumAuthenticationHandle.getRedirectedWorkspaceRequestSerializer, PbiPremiumAuthenticationHandle.getRedirectedWorkspaceResponseSerializer, false, out pbiWorkspace, out httpStatusCode);
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
						throw new ConnectionException(AuthenticationSR.Exception_RedirectionFailWithThrottling, ex);
					}
					if (handle is ExternalAuthenticationHandle && handle.AuthenticationScheme == "Bearer" && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
					{
						throw new ConnectionException(AuthenticationSR.Exception_RedirectionTokenAsPasswordIsNotSupported, ex);
					}
				}
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_GetRedirectInfo, ex);
			}
			return pbiWorkspace;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003665C File Offset: 0x0003485C
		public override string GetAccessToken()
		{
			if (!(this.handle is ExternalAuthenticationHandle) && this.refreshByTimeAsFileTime < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetMwcToken();
				this.refreshByTimeAsFileTime = this.handle.GetRefreshByTimeAsFileTime();
			}
			return this.token.Token;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000366B3 File Offset: 0x000348B3
		public override long GetRefreshByTimeAsFileTime()
		{
			return this.refreshByTimeAsFileTime;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000366BC File Offset: 0x000348BC
		private static bool TryResolvePbiWorkspaceImpl(string pbiApiBaseUri, string workspaceName, AuthenticationHandle handle, string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out PbiPremiumAuthenticationHandle.Workspace201606 workspace, out ResolvePbiWorkspaceErrorReason errorReason, out string technicalDetails)
		{
			if (string.Compare(handle.AuthenticationScheme, "EmbedToken", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return PbiPremiumAuthenticationHandle.TryResolveSinglePbiWorkspace(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, out workspace, out errorReason, out technicalDetails);
			}
			return PbiPremiumAuthenticationHandle.TryResolveWorkspaceWithWorkspaceResolver(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, servicePrincipalProfileId, out workspace, out errorReason, out technicalDetails);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00036704 File Offset: 0x00034904
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
				workspaceResolver = new PbiPremiumAuthenticationHandle.WorkspaceResolver(PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<IList<PbiPremiumAuthenticationHandle.Workspace201606>>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/workspaces", pbiApiBaseUri)), httpHeadersFromRequestIdAndS2SToken, handle, PbiPremiumAuthenticationHandle.workspacesSerializer, true, out technicalDetails, out httpStatusCode));
				PbiPremiumAuthenticationHandle.artifactCache.Insert(text, workspaceResolver);
			}
			catch (WebException ex)
			{
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_ResolveWorkspace, ex);
			}
			return workspaceResolver.TryResolvePbiWorkspace(workspaceName, out workspace, out errorReason);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000367C4 File Offset: 0x000349C4
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
				workspace = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.Workspace201606>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/workspaces/{1}", pbiApiBaseUri, workspaceName)), httpHeadersFromRequestIdAndS2SToken, handle, PbiPremiumAuthenticationHandle.workspaceSerializer, true, out technicalDetails, out httpStatusCode);
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

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00036884 File Offset: 0x00034A84
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
				IEnumerable<PbiPremiumAuthenticationHandle.Dataset201606> enumerable = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest, IList<PbiPremiumAuthenticationHandle.Dataset201606>>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/workspaces/{1}/getDatabaseName", pbiApiBaseUri, workspaceId)), dictionary, handle, getDatabasesByDatasetNameRequest, PbiPremiumAuthenticationHandle.getDBsByDatasetNameRequestSerializer, PbiPremiumAuthenticationHandle.datasetsSerializer, true, out technicalDetails, out httpStatusCode);
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

		// Token: 0x06000FCA RID: 4042 RVA: 0x000369B4 File Offset: 0x00034BB4
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
				PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606 datasetWithWorkspace = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest, PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/powerbi/databases/v201606/datasets/{1}/getDatasetDetailsForAnalyzeInExcel", pbiApiBaseUri, datasetObjectId)), httpHeadersFromRequestIdAndS2SToken, handle, getDatasetDetailsForAnalyzeInExcelRequest, PbiPremiumAuthenticationHandle.getDatasetDetailsForAnalyzeInExcelRequestSerializer, PbiPremiumAuthenticationHandle.workspaceWithDatasetSerializer, true, out text, out httpStatusCode);
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

		// Token: 0x06000FCB RID: 4043 RVA: 0x00036AB0 File Offset: 0x00034CB0
		private static bool TryGetSensitivityLabelImpl(string pbiInformationProtectionBaseUri, string datasetFriendlyName, AuthenticationHandle handle, string requestId, out string labelId, out int statusCode)
		{
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1) { { "RequestId", requestId } };
				string text;
				HttpStatusCode httpStatusCode;
				PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002 artifactInformationProtectionV = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002>(new Uri(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", pbiInformationProtectionBaseUri.TrimEnd(new char[] { '/' }), datasetFriendlyName)), dictionary, handle, PbiPremiumAuthenticationHandle.informationProtectionSerializer, true, out text, out httpStatusCode);
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

		// Token: 0x06000FCC RID: 4044 RVA: 0x00036BB8 File Offset: 0x00034DB8
		private static TResult ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<TResult>(Uri request, IDictionary<string, string> headers, AuthenticationHandle handle, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			return PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<object, TResult>(request, "GET", headers, handle, null, null, responseSerializer, getTechnicalDetails, out technicalDetails, out status);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00036BDC File Offset: 0x00034DDC
		private static TResult ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<TRequest, TResult>(Uri request, IDictionary<string, string> headers, AuthenticationHandle handle, TRequest requestBody, DataContractJsonSerializer requestSerializer, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			return PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<TRequest, TResult>(request, "POST", headers, handle, requestBody, requestSerializer, responseSerializer, getTechnicalDetails, out technicalDetails, out status);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00036C04 File Offset: 0x00034E04
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

		// Token: 0x06000FCF RID: 4047 RVA: 0x00036D78 File Offset: 0x00034F78
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

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00036E60 File Offset: 0x00035060
		private static string BuildDatabaseNameCacheKey(string pbiApiBaseUri, string principal, string tenant, string aadToken, string workspaceId, string databaseFriendlyName)
		{
			if (!string.IsNullOrEmpty(principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "dbname|{0}|{1}|{2}|{3}|{4}", new object[] { principal, tenant, pbiApiBaseUri, workspaceId, databaseFriendlyName });
			}
			return string.Format(CultureInfo.InvariantCulture, "dbname|{0}|{1}|{2}|{3}", new object[] { aadToken, pbiApiBaseUri, workspaceId, databaseFriendlyName });
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00036EC8 File Offset: 0x000350C8
		private static string BuildAsAzureRedirectedWorkspaceCacheKey(string pbiApiBaseUri, AuthenticationHandle handle, string aasInstance, string serviceToServiceToken)
		{
			string text = (string.IsNullOrEmpty(serviceToServiceToken) ? string.Empty : serviceToServiceToken.GetHashCode().ToString());
			string text2 = (string.IsNullOrEmpty(handle.Principal) ? handle.GetAccessToken() : string.Format("{0}|{1}", handle.Principal, handle.Tenant));
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}", new object[] { pbiApiBaseUri, text2, aasInstance, text });
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00036F44 File Offset: 0x00035144
		private static string BuildHomeTenantUriCacheKey(AuthenticationHandle handle)
		{
			return string.Format(CultureInfo.InvariantCulture, "hometenanturi|{0}", string.IsNullOrEmpty(handle.Principal) ? handle.GetAccessToken() : string.Format("{0}|{1}", handle.Principal, handle.Tenant));
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00036F80 File Offset: 0x00035180
		private static string BuildDatasetDetailsForAixlCacheKey(string pbiApiBaseUrl, AuthenticationHandle handle, string databaseObjectId)
		{
			databaseObjectId = databaseObjectId.ToLower();
			if (!string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "aixl|{0}|{1}|{2}", handle.Principal, handle.Tenant, databaseObjectId);
			}
			return string.Format(CultureInfo.InvariantCulture, "aixl|{0}|{1}", handle.GetAccessToken(), databaseObjectId);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00036FD8 File Offset: 0x000351D8
		private static string BuildSensitivityLabelCacheKey(string pbiApiBaseUrl, AuthenticationHandle handle, string datasetFriendlyName)
		{
			datasetFriendlyName = datasetFriendlyName.ToLower();
			if (!string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "senlab|{0}|{1}|{2}|{3}", new object[] { pbiApiBaseUrl, handle.Principal, handle.Tenant, datasetFriendlyName });
			}
			return string.Format(CultureInfo.InvariantCulture, "senlab|{0}|{1}|{2}", pbiApiBaseUrl, handle.GetAccessToken(), datasetFriendlyName);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00037044 File Offset: 0x00035244
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

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00037084 File Offset: 0x00035284
		private static Exception ConvertPbiRequestErrorToConnectionException(string action, WebException ex)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new ConnectionException(AuthenticationSR.Exception_PbiRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromPbiSharedResponse(ex.Response), Environment.NewLine), ex);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000370B8 File Offset: 0x000352B8
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
				mwctoken = PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<PbiPremiumAuthenticationHandle.MWCASTokenRequest, PbiPremiumAuthenticationHandle.MWCToken>(new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}/metadata/v201606/generateastoken", this.pbiApiBaseUri)), dictionary, this.handle, mwcastokenRequest, PbiPremiumAuthenticationHandle.mwcTokenRequestSerializer, PbiPremiumAuthenticationHandle.mwcTokenSerializer, false, out text2, out httpStatusCode);
			}
			catch (WebException ex)
			{
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_GetMwcToken, ex);
			}
			PbiPremiumAuthenticationHandle.tokenCache.Insert(text, mwctoken);
			return mwctoken;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003726C File Offset: 0x0003546C
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

		// Token: 0x04000876 RID: 2166
		internal const string EnvVariable_UseAadTokenInPublicXmlaEp = "AS_USE_AAD_TOKEN_PUBLIC_XMLA_EP";

		// Token: 0x04000877 RID: 2167
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x04000878 RID: 2168
		private const string ClientRoutingUriQuery = "PreferClientRouting=true";

		// Token: 0x04000879 RID: 2169
		private const int ClientRoutingRetryCount = 3;

		// Token: 0x0400087A RID: 2170
		private const string XmlaEndpointApiDnsNameDefault = "api.powerbi.com";

		// Token: 0x0400087B RID: 2171
		private const string PbiGetMultiWorkspacesRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces";

		// Token: 0x0400087C RID: 2172
		private const string PbiGetSingleWorkspaceRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces/{1}";

		// Token: 0x0400087D RID: 2173
		private const string PbiGenerateMwcTokenUriFormat = "https://{0}/metadata/v201606/generateastoken";

		// Token: 0x0400087E RID: 2174
		private const string PbiGetDatabaseNameRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces/{1}/getDatabaseName";

		// Token: 0x0400087F RID: 2175
		private const string PbiGetAsAzureRedirectedWorkspaceUriFormat = "https://{0}/AASRedirect/mappings/lookup";

		// Token: 0x04000880 RID: 2176
		private const string PbiGetDatasetDetailsUriFormat = "https://{0}/powerbi/databases/v201606/datasets/{1}/getDatasetDetailsForAnalyzeInExcel";

		// Token: 0x04000881 RID: 2177
		private const string MultiWorkspacesCacheKeyFromUpnFormat = "workspaces|{0}|{1}|{2}|{3}|{4}";

		// Token: 0x04000882 RID: 2178
		private const string MultiWorkspacesCacheKeyFromTokenFormat = "workspaces|{0}|{1}|{2}|{3}";

		// Token: 0x04000883 RID: 2179
		private const string SingleWorkspaceCacheKeyFromTokenFormat = "workspace|{0}|{1}|{2}|{3}";

		// Token: 0x04000884 RID: 2180
		private const string DatabaseCacheKeyFromUpnFormat = "dbname|{0}|{1}|{2}|{3}|{4}";

		// Token: 0x04000885 RID: 2181
		private const string DatabaseCacheKeyFromTokenFormat = "dbname|{0}|{1}|{2}|{3}";

		// Token: 0x04000886 RID: 2182
		private const string MwcTokenCacheKeyFormat = "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}";

		// Token: 0x04000887 RID: 2183
		private const string MwcTokenWithS2SKeyFormat = "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}";

		// Token: 0x04000888 RID: 2184
		private const string AsAzureRedirectedWorkspaceCacheKeyFormat = "{0}|{1}|{2}|{3}";

		// Token: 0x04000889 RID: 2185
		private const string HomeTenantUriCacheKeyFormat = "hometenanturi|{0}";

		// Token: 0x0400088A RID: 2186
		private const string DatasetDetailsCacheKeyFromUpnFormat = "aixl|{0}|{1}|{2}";

		// Token: 0x0400088B RID: 2187
		private const string DatasetDetailsCacheKeyFromTokenFormat = "aixl|{0}|{1}";

		// Token: 0x0400088C RID: 2188
		private const string SensitivityLabelCacheKeyFromUpnFormat = "senlab|{0}|{1}|{2}|{3}";

		// Token: 0x0400088D RID: 2189
		private const string SensitivityLabelCacheKeyFromTokenFormat = "senlab|{0}|{1}|{2}";

		// Token: 0x0400088E RID: 2190
		private static MemoryCache artifactCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)));

		// Token: 0x0400088F RID: 2191
		private static MemoryCache tokenCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(5.0)));

		// Token: 0x04000890 RID: 2192
		private static MemoryCache sensitivityLabelCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)));

		// Token: 0x04000891 RID: 2193
		private static SharedMemoryCache asAzureRedirectedWorkspaceCache = SharedMemoryCache.Create("XmlaLibAsAzureRedirectedWorkspaceCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), null, null);

		// Token: 0x04000892 RID: 2194
		private static readonly DataContractJsonSerializer workspacesSerializer = new DataContractJsonSerializer(typeof(IList<PbiPremiumAuthenticationHandle.Workspace201606>));

		// Token: 0x04000893 RID: 2195
		private static readonly DataContractJsonSerializer workspaceSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.Workspace201606));

		// Token: 0x04000894 RID: 2196
		private static readonly DataContractJsonSerializer getDBsByDatasetNameRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest));

		// Token: 0x04000895 RID: 2197
		private static readonly DataContractJsonSerializer datasetsSerializer = new DataContractJsonSerializer(typeof(IList<PbiPremiumAuthenticationHandle.Dataset201606>));

		// Token: 0x04000896 RID: 2198
		private static readonly DataContractJsonSerializer mwcTokenRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.MWCASTokenRequest));

		// Token: 0x04000897 RID: 2199
		private static readonly DataContractJsonSerializer mwcTokenSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.MWCToken));

		// Token: 0x04000898 RID: 2200
		private static readonly DataContractJsonSerializer getDatasetDetailsForAnalyzeInExcelRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest));

		// Token: 0x04000899 RID: 2201
		private static readonly DataContractJsonSerializer workspaceWithDatasetSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606));

		// Token: 0x0400089A RID: 2202
		private static readonly DataContractJsonSerializer informationProtectionSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002));

		// Token: 0x0400089B RID: 2203
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest));

		// Token: 0x0400089C RID: 2204
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceResponseSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse));

		// Token: 0x0400089D RID: 2205
		private static readonly bool useAadTokenInPublicXmlaEP = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AS_USE_AAD_TOKEN_PUBLIC_XMLA_EP"));

		// Token: 0x0400089E RID: 2206
		private static readonly string clientVersion;

		// Token: 0x0400089F RID: 2207
		private readonly AuthenticationHandle handle;

		// Token: 0x040008A0 RID: 2208
		private readonly string pbiApiBaseUri;

		// Token: 0x040008A1 RID: 2209
		private readonly string targetWorkspaceObjectId;

		// Token: 0x040008A2 RID: 2210
		private readonly string targetCapacityObjectId;

		// Token: 0x040008A3 RID: 2211
		private readonly string targetDatabaseName;

		// Token: 0x040008A4 RID: 2212
		private readonly AuxiliaryPermissionInfo permissionInfo;

		// Token: 0x040008A5 RID: 2213
		private PbiPremiumAuthenticationHandle.MWCToken token;

		// Token: 0x040008A6 RID: 2214
		private long refreshByTimeAsFileTime;

		// Token: 0x020001BC RID: 444
		[DataContract]
		private enum WorkspaceType201606
		{
			// Token: 0x04001114 RID: 4372
			[EnumMember(Value = "User")]
			User,
			// Token: 0x04001115 RID: 4373
			[EnumMember(Value = "Group")]
			Group,
			// Token: 0x04001116 RID: 4374
			[EnumMember(Value = "Folder")]
			Folder
		}

		// Token: 0x020001BD RID: 445
		private struct ArtifactCacheItem
		{
			// Token: 0x06001391 RID: 5009 RVA: 0x0004456B File Offset: 0x0004276B
			public ArtifactCacheItem(string artifactName, ArtifactCapacityState capacityState)
			{
				this.ArtifactName = artifactName;
				this.CapacityState = capacityState;
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001392 RID: 5010 RVA: 0x0004457B File Offset: 0x0004277B
			public string ArtifactName { get; }

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06001393 RID: 5011 RVA: 0x00044583 File Offset: 0x00042783
			public ArtifactCapacityState CapacityState { get; }
		}

		// Token: 0x020001BE RID: 446
		[DataContract]
		private sealed class Workspace201606
		{
			// Token: 0x17000643 RID: 1603
			// (get) Token: 0x06001394 RID: 5012 RVA: 0x0004458B File Offset: 0x0004278B
			// (set) Token: 0x06001395 RID: 5013 RVA: 0x00044593 File Offset: 0x00042793
			[DataMember(Order = 0, Name = "id")]
			public string Id { get; set; }

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06001396 RID: 5014 RVA: 0x0004459C File Offset: 0x0004279C
			// (set) Token: 0x06001397 RID: 5015 RVA: 0x000445A4 File Offset: 0x000427A4
			[DataMember(Order = 10, Name = "name")]
			public string Name { get; set; }

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001398 RID: 5016 RVA: 0x000445AD File Offset: 0x000427AD
			// (set) Token: 0x06001399 RID: 5017 RVA: 0x000445B5 File Offset: 0x000427B5
			[DataMember(Order = 20, Name = "type")]
			public string Type { get; set; }

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x0600139A RID: 5018 RVA: 0x000445BE File Offset: 0x000427BE
			// (set) Token: 0x0600139B RID: 5019 RVA: 0x000445C6 File Offset: 0x000427C6
			[DataMember(Order = 30, Name = "capacitySku")]
			public string CapacitySku { get; set; }

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x000445CF File Offset: 0x000427CF
			// (set) Token: 0x0600139D RID: 5021 RVA: 0x000445D7 File Offset: 0x000427D7
			[DataMember(Order = 40, Name = "capacityUri", EmitDefaultValue = true)]
			public string CapacityUri { get; set; }

			// Token: 0x0600139E RID: 5022 RVA: 0x000445E0 File Offset: 0x000427E0
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 GetWorkspaceType()
			{
				return (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), this.Type);
			}

			// Token: 0x0600139F RID: 5023 RVA: 0x000445FC File Offset: 0x000427FC
			public WorkspaceCapacitySkuType201606 GetCapacitySku()
			{
				return (WorkspaceCapacitySkuType201606)Enum.Parse(typeof(WorkspaceCapacitySkuType201606), this.CapacitySku);
			}
		}

		// Token: 0x020001BF RID: 447
		[DataContract]
		private sealed class DatasetWithWorkspace201606
		{
			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00044620 File Offset: 0x00042820
			// (set) Token: 0x060013A2 RID: 5026 RVA: 0x00044628 File Offset: 0x00042828
			[DataMember(Order = 0, Name = "workspaceObjectId")]
			public string WorkspaceObjectId { get; set; }

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00044631 File Offset: 0x00042831
			// (set) Token: 0x060013A4 RID: 5028 RVA: 0x00044639 File Offset: 0x00042839
			[DataMember(Order = 10, Name = "workspaceFriendlyName")]
			public string WorkspaceFriendlyName { get; set; }

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00044642 File Offset: 0x00042842
			// (set) Token: 0x060013A6 RID: 5030 RVA: 0x0004464A File Offset: 0x0004284A
			[DataMember(Order = 20, Name = "workspaceType")]
			public string WorkspaceType { get; set; }

			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00044653 File Offset: 0x00042853
			// (set) Token: 0x060013A8 RID: 5032 RVA: 0x0004465B File Offset: 0x0004285B
			[DataMember(Order = 30, Name = "datasetObjectId")]
			public string DatasetObjectId { get; set; }

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00044664 File Offset: 0x00042864
			// (set) Token: 0x060013AA RID: 5034 RVA: 0x0004466C File Offset: 0x0004286C
			[DataMember(Order = 40, Name = "datasetFriendlyName")]
			public string DatasetFriendlyName { get; set; }

			// Token: 0x1700064D RID: 1613
			// (get) Token: 0x060013AB RID: 5035 RVA: 0x00044675 File Offset: 0x00042875
			// (set) Token: 0x060013AC RID: 5036 RVA: 0x0004467D File Offset: 0x0004287D
			[DataMember(Order = 50, Name = "virtualServerName")]
			public string VirtualServerName { get; set; }

			// Token: 0x1700064E RID: 1614
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x00044686 File Offset: 0x00042886
			// (set) Token: 0x060013AE RID: 5038 RVA: 0x0004468E File Offset: 0x0004288E
			[DataMember(Order = 60, Name = "isAvailableOnFabric", EmitDefaultValue = false, IsRequired = false)]
			public bool? IsAvailableOnFabric { get; set; }

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x060013AF RID: 5039 RVA: 0x00044697 File Offset: 0x00042897
			// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0004469F File Offset: 0x0004289F
			[DataMember(Order = 70, Name = "xmlaEndpointApiDNSName", EmitDefaultValue = false, IsRequired = false)]
			public string XmlaEndpointApiDNSName { get; set; }

			// Token: 0x060013B1 RID: 5041 RVA: 0x000446A8 File Offset: 0x000428A8
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 GetWorkspaceType()
			{
				return (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), this.WorkspaceType);
			}
		}

		// Token: 0x020001C0 RID: 448
		[DataContract]
		private sealed class GetDatasetDetailsForAnalyzeInExcelRequest
		{
			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x060013B3 RID: 5043 RVA: 0x000446CC File Offset: 0x000428CC
			// (set) Token: 0x060013B4 RID: 5044 RVA: 0x000446D4 File Offset: 0x000428D4
			[DataMember(Order = 10, Name = "clientVersion")]
			public string ClientVersion { get; set; }
		}

		// Token: 0x020001C1 RID: 449
		[DataContract]
		public class ArtifactInformationProtectionV202002
		{
			// Token: 0x17000651 RID: 1617
			// (get) Token: 0x060013B6 RID: 5046 RVA: 0x000446E5 File Offset: 0x000428E5
			// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000446ED File Offset: 0x000428ED
			[DataMember(Order = 10, Name = "labelId", EmitDefaultValue = false)]
			public Guid LabelId { get; set; }

			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000446F6 File Offset: 0x000428F6
			// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000446FE File Offset: 0x000428FE
			[DataMember(Order = 40, Name = "artifactId", EmitDefaultValue = false)]
			public long ArtifactId { get; set; }
		}

		// Token: 0x020001C2 RID: 450
		private struct AixlCacheItem
		{
			// Token: 0x060013BB RID: 5051 RVA: 0x0004470F File Offset: 0x0004290F
			public AixlCacheItem(string workspaceFriendlyName, string datasetFriendlyName, string xmlaEndpointApiDnsName, ArtifactCapacityState artifactCapacityState)
			{
				this.WorkspaceFriendlyName = workspaceFriendlyName;
				this.DatasetFriendlyName = datasetFriendlyName;
				this.XmlaEndpointApiDnsName = xmlaEndpointApiDnsName;
				this.CapacityState = artifactCapacityState;
			}

			// Token: 0x17000653 RID: 1619
			// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004472E File Offset: 0x0004292E
			public string WorkspaceFriendlyName { get; }

			// Token: 0x17000654 RID: 1620
			// (get) Token: 0x060013BD RID: 5053 RVA: 0x00044736 File Offset: 0x00042936
			public string DatasetFriendlyName { get; }

			// Token: 0x17000655 RID: 1621
			// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004473E File Offset: 0x0004293E
			public string XmlaEndpointApiDnsName { get; }

			// Token: 0x17000656 RID: 1622
			// (get) Token: 0x060013BF RID: 5055 RVA: 0x00044746 File Offset: 0x00042946
			public ArtifactCapacityState CapacityState { get; }
		}

		// Token: 0x020001C3 RID: 451
		private struct SensitivityLabelCacheItem
		{
			// Token: 0x060013C0 RID: 5056 RVA: 0x0004474E File Offset: 0x0004294E
			public SensitivityLabelCacheItem(string labelId, int statusCode)
			{
				this.LabelId = labelId;
				this.StatusCode = statusCode;
			}

			// Token: 0x17000657 RID: 1623
			// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0004475E File Offset: 0x0004295E
			public string LabelId { get; }

			// Token: 0x17000658 RID: 1624
			// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00044766 File Offset: 0x00042966
			public int StatusCode { get; }
		}

		// Token: 0x020001C4 RID: 452
		[DataContract]
		private sealed class GetDatabasesByDatasetNameRequest
		{
			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0004476E File Offset: 0x0004296E
			// (set) Token: 0x060013C4 RID: 5060 RVA: 0x00044776 File Offset: 0x00042976
			[DataMember(Order = 10, Name = "datasetName")]
			public string DatasetName { get; set; }

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0004477F File Offset: 0x0004297F
			// (set) Token: 0x060013C6 RID: 5062 RVA: 0x00044787 File Offset: 0x00042987
			[DataMember(Order = 20, Name = "workspaceType")]
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 WorkspaceType201606 { get; set; }
		}

		// Token: 0x020001C5 RID: 453
		[DataContract]
		private sealed class Dataset201606
		{
			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00044798 File Offset: 0x00042998
			// (set) Token: 0x060013C9 RID: 5065 RVA: 0x000447A0 File Offset: 0x000429A0
			[DataMember(Order = 0, Name = "databaseName")]
			public string DatabaseName { get; set; }

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x060013CA RID: 5066 RVA: 0x000447A9 File Offset: 0x000429A9
			// (set) Token: 0x060013CB RID: 5067 RVA: 0x000447B1 File Offset: 0x000429B1
			[DataMember(Order = 10, Name = "datasetName")]
			public string DatasetName { get; set; }

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x060013CC RID: 5068 RVA: 0x000447BA File Offset: 0x000429BA
			// (set) Token: 0x060013CD RID: 5069 RVA: 0x000447C2 File Offset: 0x000429C2
			[DataMember(Order = 20, Name = "versionEtag")]
			public string VersionEtag { get; set; }

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x060013CE RID: 5070 RVA: 0x000447CB File Offset: 0x000429CB
			// (set) Token: 0x060013CF RID: 5071 RVA: 0x000447D3 File Offset: 0x000429D3
			[DataMember(Order = 30, Name = "virtualServerName")]
			public string VirtualServerName { get; set; }

			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000447DC File Offset: 0x000429DC
			// (set) Token: 0x060013D1 RID: 5073 RVA: 0x000447E4 File Offset: 0x000429E4
			[DataMember(Order = 40, Name = "creatorUserPrincipalName")]
			public string CreatorUserPrincipalName { get; set; }

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x060013D2 RID: 5074 RVA: 0x000447ED File Offset: 0x000429ED
			// (set) Token: 0x060013D3 RID: 5075 RVA: 0x000447F5 File Offset: 0x000429F5
			[DataMember(Order = 50, Name = "isAvailableOnPremiumCapacity", IsRequired = false)]
			public bool? IsAvailableOnPremiumCapacity { get; set; }
		}

		// Token: 0x020001C6 RID: 454
		[DataContract]
		private sealed class MWCASTokenRequest
		{
			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00044806 File Offset: 0x00042A06
			// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0004480E File Offset: 0x00042A0E
			[DataMember(Name = "capacityObjectId", IsRequired = true)]
			public string CapacityObjectId { get; set; }

			// Token: 0x17000662 RID: 1634
			// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00044817 File Offset: 0x00042A17
			// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0004481F File Offset: 0x00042A1F
			[DataMember(Name = "workspaceObjectId", IsRequired = true)]
			public string WorkspaceObjectId { get; set; }

			// Token: 0x17000663 RID: 1635
			// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00044828 File Offset: 0x00042A28
			// (set) Token: 0x060013DA RID: 5082 RVA: 0x00044830 File Offset: 0x00042A30
			[DataMember(Name = "datasetName", IsRequired = false)]
			public string DatasetName { get; set; }

			// Token: 0x17000664 RID: 1636
			// (get) Token: 0x060013DB RID: 5083 RVA: 0x00044839 File Offset: 0x00042A39
			// (set) Token: 0x060013DC RID: 5084 RVA: 0x00044841 File Offset: 0x00042A41
			[DataMember(Name = "applyAuxiliaryPermission", IsRequired = false)]
			public bool ApplyAuxiliaryPermission { get; set; }

			// Token: 0x17000665 RID: 1637
			// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004484A File Offset: 0x00042A4A
			// (set) Token: 0x060013DE RID: 5086 RVA: 0x00044852 File Offset: 0x00042A52
			[DataMember(Name = "auxiliaryPermissionOwner", IsRequired = false)]
			public string AuxiliaryPermissionOwner { get; set; }

			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004485B File Offset: 0x00042A5B
			// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00044863 File Offset: 0x00042A63
			[DataMember(Name = "bypassBuildPermission", IsRequired = false)]
			public bool BypassBuildPermission { get; set; }

			// Token: 0x17000667 RID: 1639
			// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0004486C File Offset: 0x00042A6C
			// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00044874 File Offset: 0x00042A74
			[DataMember(Name = "intendedUsage", IsRequired = false)]
			public int IntendedUsage { get; set; }

			// Token: 0x17000668 RID: 1640
			// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0004487D File Offset: 0x00042A7D
			// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00044885 File Offset: 0x00042A85
			[DataMember(Name = "sourceCapacityObjectId", IsRequired = false)]
			public string SourceCapacityObjectId { get; set; }
		}

		// Token: 0x020001C7 RID: 455
		[DataContract]
		private sealed class MWCToken
		{
			// Token: 0x17000669 RID: 1641
			// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00044896 File Offset: 0x00042A96
			// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0004489E File Offset: 0x00042A9E
			[DataMember]
			public string Token { get; set; }
		}

		// Token: 0x020001C8 RID: 456
		[DataContract]
		private sealed class GetRedirectedWorkspaceRequest
		{
			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000448AF File Offset: 0x00042AAF
			// (set) Token: 0x060013EA RID: 5098 RVA: 0x000448B7 File Offset: 0x00042AB7
			[DataMember(Name = "aasInstance", IsRequired = true)]
			public string AasInstance { get; set; }
		}

		// Token: 0x020001C9 RID: 457
		[DataContract]
		private sealed class GetRedirectedWorkspaceResponse
		{
			// Token: 0x1700066B RID: 1643
			// (get) Token: 0x060013EC RID: 5100 RVA: 0x000448C8 File Offset: 0x00042AC8
			// (set) Token: 0x060013ED RID: 5101 RVA: 0x000448D0 File Offset: 0x00042AD0
			[DataMember(Name = "pbiWorkspace")]
			public string PbiWorkspace { get; set; }
		}

		// Token: 0x020001CA RID: 458
		private sealed class WorkspaceResolver
		{
			// Token: 0x060013EF RID: 5103 RVA: 0x000448E1 File Offset: 0x00042AE1
			public WorkspaceResolver(IList<PbiPremiumAuthenticationHandle.Workspace201606> workspaces)
			{
				this.Initialize(workspaces);
			}

			// Token: 0x060013F0 RID: 5104 RVA: 0x000448F0 File Offset: 0x00042AF0
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

			// Token: 0x060013F1 RID: 5105 RVA: 0x0004494D File Offset: 0x00042B4D
			private static string GetConflictResolverWorkspaceName(string workspaceName, string workspaceId)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", workspaceName, workspaceId);
			}

			// Token: 0x060013F2 RID: 5106 RVA: 0x00044960 File Offset: 0x00042B60
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

			// Token: 0x04001142 RID: 4418
			private const string WorkspaceNameResolverFormat = "{0}-{1}";

			// Token: 0x04001143 RID: 4419
			private Dictionary<string, PbiPremiumAuthenticationHandle.Workspace201606> friendlyNameMap;

			// Token: 0x04001144 RID: 4420
			private HashSet<string> conflictedNames;

			// Token: 0x04001145 RID: 4421
			private PbiPremiumAuthenticationHandle.Workspace201606 personalWorkspace;
		}
	}
}
