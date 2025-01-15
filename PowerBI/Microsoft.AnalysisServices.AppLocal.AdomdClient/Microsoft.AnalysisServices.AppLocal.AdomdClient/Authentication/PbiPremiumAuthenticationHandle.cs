using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x0200010A RID: 266
	internal sealed class PbiPremiumAuthenticationHandle : AuthenticationHandle
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x000337A8 File Offset: 0x000319A8
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

		// Token: 0x06000F2C RID: 3884 RVA: 0x00033818 File Offset: 0x00031A18
		static PbiPremiumAuthenticationHandle()
		{
			object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
			PbiPremiumAuthenticationHandle.clientVersion = ((customAttributes != null && customAttributes.Length != 0) ? ((AssemblyFileVersionAttribute)customAttributes[0]).Version : "0.0.0.0");
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000339C9 File Offset: 0x00031BC9
		public static bool UseAadTokenInPublicXmlaEP
		{
			get
			{
				return PbiPremiumAuthenticationHandle.useAadTokenInPublicXmlaEP;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000339D0 File Offset: 0x00031BD0
		public override string Principal
		{
			get
			{
				return this.handle.Principal;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x000339DD File Offset: 0x00031BDD
		public override string AuthenticationScheme
		{
			get
			{
				return "MwcToken";
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000339E4 File Offset: 0x00031BE4
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

		// Token: 0x06000F31 RID: 3889 RVA: 0x00033AA4 File Offset: 0x00031CA4
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

		// Token: 0x06000F32 RID: 3890 RVA: 0x00033B48 File Offset: 0x00031D48
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

		// Token: 0x06000F33 RID: 3891 RVA: 0x00033BD0 File Offset: 0x00031DD0
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

		// Token: 0x06000F34 RID: 3892 RVA: 0x00033C38 File Offset: 0x00031E38
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
						throw new AdomdConnectionException(AuthenticationSR.Exception_RedirectionFailWithThrottling, ex);
					}
					if (handle is ExternalAuthenticationHandle && handle.AuthenticationScheme == "Bearer" && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
					{
						throw new AdomdConnectionException(AuthenticationSR.Exception_RedirectionTokenAsPasswordIsNotSupported, ex);
					}
				}
				throw PbiPremiumAuthenticationHandle.ConvertPbiRequestErrorToConnectionException(AuthenticationSR.PbiRequest_GetRedirectInfo, ex);
			}
			return pbiWorkspace;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00033D48 File Offset: 0x00031F48
		public override string GetAccessToken()
		{
			if (!(this.handle is ExternalAuthenticationHandle) && this.refreshByTimeAsFileTime < DateTimeOffset.Now.ToFileTime())
			{
				this.token = this.GetMwcToken();
				this.refreshByTimeAsFileTime = this.handle.GetRefreshByTimeAsFileTime();
			}
			return this.token.Token;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00033D9F File Offset: 0x00031F9F
		public override long GetRefreshByTimeAsFileTime()
		{
			return this.refreshByTimeAsFileTime;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00033DA7 File Offset: 0x00031FA7
		internal override void AddUserRelatedProperties(AdomdPropertyCollection properties)
		{
			this.handle.AddUserRelatedProperties(properties);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00033DB8 File Offset: 0x00031FB8
		private static bool TryResolvePbiWorkspaceImpl(string pbiApiBaseUri, string workspaceName, AuthenticationHandle handle, string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out PbiPremiumAuthenticationHandle.Workspace201606 workspace, out ResolvePbiWorkspaceErrorReason errorReason, out string technicalDetails)
		{
			if (string.Compare(handle.AuthenticationScheme, "EmbedToken", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return PbiPremiumAuthenticationHandle.TryResolveSinglePbiWorkspace(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, out workspace, out errorReason, out technicalDetails);
			}
			return PbiPremiumAuthenticationHandle.TryResolveWorkspaceWithWorkspaceResolver(pbiApiBaseUri, workspaceName, handle, requestId, serviceToServiceToken, servicePrincipalProfileId, out workspace, out errorReason, out technicalDetails);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00033E00 File Offset: 0x00032000
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

		// Token: 0x06000F3A RID: 3898 RVA: 0x00033EC0 File Offset: 0x000320C0
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

		// Token: 0x06000F3B RID: 3899 RVA: 0x00033F80 File Offset: 0x00032180
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

		// Token: 0x06000F3C RID: 3900 RVA: 0x000340B0 File Offset: 0x000322B0
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

		// Token: 0x06000F3D RID: 3901 RVA: 0x000341AC File Offset: 0x000323AC
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

		// Token: 0x06000F3E RID: 3902 RVA: 0x000342B4 File Offset: 0x000324B4
		private static TResult ExecuteJsonBasedHttpGetRequestWithPreferClientRouting<TResult>(Uri request, IDictionary<string, string> headers, AuthenticationHandle handle, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			return PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<object, TResult>(request, "GET", headers, handle, null, null, responseSerializer, getTechnicalDetails, out technicalDetails, out status);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000342D8 File Offset: 0x000324D8
		private static TResult ExecuteJsonBasedHttpPostRequestWithPreferClientRouting<TRequest, TResult>(Uri request, IDictionary<string, string> headers, AuthenticationHandle handle, TRequest requestBody, DataContractJsonSerializer requestSerializer, DataContractJsonSerializer responseSerializer, bool getTechnicalDetails, out string technicalDetails, out HttpStatusCode status)
		{
			return PbiPremiumAuthenticationHandle.ExecuteJsonBasedHttpRequestWithPreferClientRoutingImpl<TRequest, TResult>(request, "POST", headers, handle, requestBody, requestSerializer, responseSerializer, getTechnicalDetails, out technicalDetails, out status);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00034300 File Offset: 0x00032500
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

		// Token: 0x06000F41 RID: 3905 RVA: 0x00034474 File Offset: 0x00032674
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

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003455C File Offset: 0x0003275C
		private static string BuildDatabaseNameCacheKey(string pbiApiBaseUri, string principal, string tenant, string aadToken, string workspaceId, string databaseFriendlyName)
		{
			if (!string.IsNullOrEmpty(principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "dbname|{0}|{1}|{2}|{3}|{4}", new object[] { principal, tenant, pbiApiBaseUri, workspaceId, databaseFriendlyName });
			}
			return string.Format(CultureInfo.InvariantCulture, "dbname|{0}|{1}|{2}|{3}", new object[] { aadToken, pbiApiBaseUri, workspaceId, databaseFriendlyName });
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000345C4 File Offset: 0x000327C4
		private static string BuildAsAzureRedirectedWorkspaceCacheKey(string pbiApiBaseUri, AuthenticationHandle handle, string aasInstance, string serviceToServiceToken)
		{
			string text = (string.IsNullOrEmpty(serviceToServiceToken) ? string.Empty : serviceToServiceToken.GetHashCode().ToString());
			string text2 = (string.IsNullOrEmpty(handle.Principal) ? handle.GetAccessToken() : string.Format("{0}|{1}", handle.Principal, handle.Tenant));
			return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}", new object[] { pbiApiBaseUri, text2, aasInstance, text });
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00034640 File Offset: 0x00032840
		private static string BuildHomeTenantUriCacheKey(AuthenticationHandle handle)
		{
			return string.Format(CultureInfo.InvariantCulture, "hometenanturi|{0}", string.IsNullOrEmpty(handle.Principal) ? handle.GetAccessToken() : string.Format("{0}|{1}", handle.Principal, handle.Tenant));
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003467C File Offset: 0x0003287C
		private static string BuildDatasetDetailsForAixlCacheKey(string pbiApiBaseUrl, AuthenticationHandle handle, string databaseObjectId)
		{
			databaseObjectId = databaseObjectId.ToLower();
			if (!string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "aixl|{0}|{1}|{2}", handle.Principal, handle.Tenant, databaseObjectId);
			}
			return string.Format(CultureInfo.InvariantCulture, "aixl|{0}|{1}", handle.GetAccessToken(), databaseObjectId);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000346D4 File Offset: 0x000328D4
		private static string BuildSensitivityLabelCacheKey(string pbiApiBaseUrl, AuthenticationHandle handle, string datasetFriendlyName)
		{
			datasetFriendlyName = datasetFriendlyName.ToLower();
			if (!string.IsNullOrEmpty(handle.Principal))
			{
				return string.Format(CultureInfo.InvariantCulture, "senlab|{0}|{1}|{2}|{3}", new object[] { pbiApiBaseUrl, handle.Principal, handle.Tenant, datasetFriendlyName });
			}
			return string.Format(CultureInfo.InvariantCulture, "senlab|{0}|{1}|{2}", pbiApiBaseUrl, handle.GetAccessToken(), datasetFriendlyName);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00034740 File Offset: 0x00032940
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

		// Token: 0x06000F48 RID: 3912 RVA: 0x00034780 File Offset: 0x00032980
		private static Exception ConvertPbiRequestErrorToConnectionException(string action, WebException ex)
		{
			string responseFromWebException = AsPaasHelper.GetResponseFromWebException(ex);
			return new AdomdConnectionException(AuthenticationSR.Exception_PbiRequestFailed(action, responseFromWebException, AsPaasHelper.GetTechnicalDetailsFromPbiSharedResponse(ex.Response), Environment.NewLine), ex);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000347B4 File Offset: 0x000329B4
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

		// Token: 0x06000F4A RID: 3914 RVA: 0x00034968 File Offset: 0x00032B68
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

		// Token: 0x040008BD RID: 2237
		internal const string EnvVariable_UseAadTokenInPublicXmlaEp = "AS_USE_AAD_TOKEN_PUBLIC_XMLA_EP";

		// Token: 0x040008BE RID: 2238
		private const string AuthorizationHeaderFormat = "{0} {1}";

		// Token: 0x040008BF RID: 2239
		private const string ClientRoutingUriQuery = "PreferClientRouting=true";

		// Token: 0x040008C0 RID: 2240
		private const int ClientRoutingRetryCount = 3;

		// Token: 0x040008C1 RID: 2241
		private const string XmlaEndpointApiDnsNameDefault = "api.powerbi.com";

		// Token: 0x040008C2 RID: 2242
		private const string PbiGetMultiWorkspacesRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces";

		// Token: 0x040008C3 RID: 2243
		private const string PbiGetSingleWorkspaceRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces/{1}";

		// Token: 0x040008C4 RID: 2244
		private const string PbiGenerateMwcTokenUriFormat = "https://{0}/metadata/v201606/generateastoken";

		// Token: 0x040008C5 RID: 2245
		private const string PbiGetDatabaseNameRequestUriFormat = "https://{0}/powerbi/databases/v201606/workspaces/{1}/getDatabaseName";

		// Token: 0x040008C6 RID: 2246
		private const string PbiGetAsAzureRedirectedWorkspaceUriFormat = "https://{0}/AASRedirect/mappings/lookup";

		// Token: 0x040008C7 RID: 2247
		private const string PbiGetDatasetDetailsUriFormat = "https://{0}/powerbi/databases/v201606/datasets/{1}/getDatasetDetailsForAnalyzeInExcel";

		// Token: 0x040008C8 RID: 2248
		private const string MultiWorkspacesCacheKeyFromUpnFormat = "workspaces|{0}|{1}|{2}|{3}|{4}";

		// Token: 0x040008C9 RID: 2249
		private const string MultiWorkspacesCacheKeyFromTokenFormat = "workspaces|{0}|{1}|{2}|{3}";

		// Token: 0x040008CA RID: 2250
		private const string SingleWorkspaceCacheKeyFromTokenFormat = "workspace|{0}|{1}|{2}|{3}";

		// Token: 0x040008CB RID: 2251
		private const string DatabaseCacheKeyFromUpnFormat = "dbname|{0}|{1}|{2}|{3}|{4}";

		// Token: 0x040008CC RID: 2252
		private const string DatabaseCacheKeyFromTokenFormat = "dbname|{0}|{1}|{2}|{3}";

		// Token: 0x040008CD RID: 2253
		private const string MwcTokenCacheKeyFormat = "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}";

		// Token: 0x040008CE RID: 2254
		private const string MwcTokenWithS2SKeyFormat = "mwctoken|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}";

		// Token: 0x040008CF RID: 2255
		private const string AsAzureRedirectedWorkspaceCacheKeyFormat = "{0}|{1}|{2}|{3}";

		// Token: 0x040008D0 RID: 2256
		private const string HomeTenantUriCacheKeyFormat = "hometenanturi|{0}";

		// Token: 0x040008D1 RID: 2257
		private const string DatasetDetailsCacheKeyFromUpnFormat = "aixl|{0}|{1}|{2}";

		// Token: 0x040008D2 RID: 2258
		private const string DatasetDetailsCacheKeyFromTokenFormat = "aixl|{0}|{1}";

		// Token: 0x040008D3 RID: 2259
		private const string SensitivityLabelCacheKeyFromUpnFormat = "senlab|{0}|{1}|{2}|{3}";

		// Token: 0x040008D4 RID: 2260
		private const string SensitivityLabelCacheKeyFromTokenFormat = "senlab|{0}|{1}|{2}";

		// Token: 0x040008D5 RID: 2261
		private static MemoryCache artifactCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)));

		// Token: 0x040008D6 RID: 2262
		private static MemoryCache tokenCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(5.0)));

		// Token: 0x040008D7 RID: 2263
		private static MemoryCache sensitivityLabelCache = new MemoryCache(MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)));

		// Token: 0x040008D8 RID: 2264
		private static SharedMemoryCache asAzureRedirectedWorkspaceCache = SharedMemoryCache.Create("XmlaLibAsAzureRedirectedWorkspaceCache", MemoryCacheRetentionPolicy.BuildAbsoluteExpirationPolicy(TimeSpan.FromMinutes(10.0)), null, null);

		// Token: 0x040008D9 RID: 2265
		private static readonly DataContractJsonSerializer workspacesSerializer = new DataContractJsonSerializer(typeof(IList<PbiPremiumAuthenticationHandle.Workspace201606>));

		// Token: 0x040008DA RID: 2266
		private static readonly DataContractJsonSerializer workspaceSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.Workspace201606));

		// Token: 0x040008DB RID: 2267
		private static readonly DataContractJsonSerializer getDBsByDatasetNameRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetDatabasesByDatasetNameRequest));

		// Token: 0x040008DC RID: 2268
		private static readonly DataContractJsonSerializer datasetsSerializer = new DataContractJsonSerializer(typeof(IList<PbiPremiumAuthenticationHandle.Dataset201606>));

		// Token: 0x040008DD RID: 2269
		private static readonly DataContractJsonSerializer mwcTokenRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.MWCASTokenRequest));

		// Token: 0x040008DE RID: 2270
		private static readonly DataContractJsonSerializer mwcTokenSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.MWCToken));

		// Token: 0x040008DF RID: 2271
		private static readonly DataContractJsonSerializer getDatasetDetailsForAnalyzeInExcelRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetDatasetDetailsForAnalyzeInExcelRequest));

		// Token: 0x040008E0 RID: 2272
		private static readonly DataContractJsonSerializer workspaceWithDatasetSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.DatasetWithWorkspace201606));

		// Token: 0x040008E1 RID: 2273
		private static readonly DataContractJsonSerializer informationProtectionSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.ArtifactInformationProtectionV202002));

		// Token: 0x040008E2 RID: 2274
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceRequestSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceRequest));

		// Token: 0x040008E3 RID: 2275
		private static readonly DataContractJsonSerializer getRedirectedWorkspaceResponseSerializer = new DataContractJsonSerializer(typeof(PbiPremiumAuthenticationHandle.GetRedirectedWorkspaceResponse));

		// Token: 0x040008E4 RID: 2276
		private static readonly bool useAadTokenInPublicXmlaEP = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AS_USE_AAD_TOKEN_PUBLIC_XMLA_EP"));

		// Token: 0x040008E5 RID: 2277
		private static readonly string clientVersion;

		// Token: 0x040008E6 RID: 2278
		private readonly AuthenticationHandle handle;

		// Token: 0x040008E7 RID: 2279
		private readonly string pbiApiBaseUri;

		// Token: 0x040008E8 RID: 2280
		private readonly string targetWorkspaceObjectId;

		// Token: 0x040008E9 RID: 2281
		private readonly string targetCapacityObjectId;

		// Token: 0x040008EA RID: 2282
		private readonly string targetDatabaseName;

		// Token: 0x040008EB RID: 2283
		private readonly AuxiliaryPermissionInfo permissionInfo;

		// Token: 0x040008EC RID: 2284
		private PbiPremiumAuthenticationHandle.MWCToken token;

		// Token: 0x040008ED RID: 2285
		private long refreshByTimeAsFileTime;

		// Token: 0x020001DF RID: 479
		[DataContract]
		private enum WorkspaceType201606
		{
			// Token: 0x04000E59 RID: 3673
			[EnumMember(Value = "User")]
			User,
			// Token: 0x04000E5A RID: 3674
			[EnumMember(Value = "Group")]
			Group,
			// Token: 0x04000E5B RID: 3675
			[EnumMember(Value = "Folder")]
			Folder
		}

		// Token: 0x020001E0 RID: 480
		private struct ArtifactCacheItem
		{
			// Token: 0x06001436 RID: 5174 RVA: 0x0004634B File Offset: 0x0004454B
			public ArtifactCacheItem(string artifactName, ArtifactCapacityState capacityState)
			{
				this.ArtifactName = artifactName;
				this.CapacityState = capacityState;
			}

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004635B File Offset: 0x0004455B
			public string ArtifactName { get; }

			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x06001438 RID: 5176 RVA: 0x00046363 File Offset: 0x00044563
			public ArtifactCapacityState CapacityState { get; }
		}

		// Token: 0x020001E1 RID: 481
		[DataContract]
		private sealed class Workspace201606
		{
			// Token: 0x170006FE RID: 1790
			// (get) Token: 0x06001439 RID: 5177 RVA: 0x0004636B File Offset: 0x0004456B
			// (set) Token: 0x0600143A RID: 5178 RVA: 0x00046373 File Offset: 0x00044573
			[DataMember(Order = 0, Name = "id")]
			public string Id { get; set; }

			// Token: 0x170006FF RID: 1791
			// (get) Token: 0x0600143B RID: 5179 RVA: 0x0004637C File Offset: 0x0004457C
			// (set) Token: 0x0600143C RID: 5180 RVA: 0x00046384 File Offset: 0x00044584
			[DataMember(Order = 10, Name = "name")]
			public string Name { get; set; }

			// Token: 0x17000700 RID: 1792
			// (get) Token: 0x0600143D RID: 5181 RVA: 0x0004638D File Offset: 0x0004458D
			// (set) Token: 0x0600143E RID: 5182 RVA: 0x00046395 File Offset: 0x00044595
			[DataMember(Order = 20, Name = "type")]
			public string Type { get; set; }

			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x0600143F RID: 5183 RVA: 0x0004639E File Offset: 0x0004459E
			// (set) Token: 0x06001440 RID: 5184 RVA: 0x000463A6 File Offset: 0x000445A6
			[DataMember(Order = 30, Name = "capacitySku")]
			public string CapacitySku { get; set; }

			// Token: 0x17000702 RID: 1794
			// (get) Token: 0x06001441 RID: 5185 RVA: 0x000463AF File Offset: 0x000445AF
			// (set) Token: 0x06001442 RID: 5186 RVA: 0x000463B7 File Offset: 0x000445B7
			[DataMember(Order = 40, Name = "capacityUri", EmitDefaultValue = true)]
			public string CapacityUri { get; set; }

			// Token: 0x06001443 RID: 5187 RVA: 0x000463C0 File Offset: 0x000445C0
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 GetWorkspaceType()
			{
				return (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), this.Type);
			}

			// Token: 0x06001444 RID: 5188 RVA: 0x000463DC File Offset: 0x000445DC
			public WorkspaceCapacitySkuType201606 GetCapacitySku()
			{
				return (WorkspaceCapacitySkuType201606)Enum.Parse(typeof(WorkspaceCapacitySkuType201606), this.CapacitySku);
			}
		}

		// Token: 0x020001E2 RID: 482
		[DataContract]
		private sealed class DatasetWithWorkspace201606
		{
			// Token: 0x17000703 RID: 1795
			// (get) Token: 0x06001446 RID: 5190 RVA: 0x00046400 File Offset: 0x00044600
			// (set) Token: 0x06001447 RID: 5191 RVA: 0x00046408 File Offset: 0x00044608
			[DataMember(Order = 0, Name = "workspaceObjectId")]
			public string WorkspaceObjectId { get; set; }

			// Token: 0x17000704 RID: 1796
			// (get) Token: 0x06001448 RID: 5192 RVA: 0x00046411 File Offset: 0x00044611
			// (set) Token: 0x06001449 RID: 5193 RVA: 0x00046419 File Offset: 0x00044619
			[DataMember(Order = 10, Name = "workspaceFriendlyName")]
			public string WorkspaceFriendlyName { get; set; }

			// Token: 0x17000705 RID: 1797
			// (get) Token: 0x0600144A RID: 5194 RVA: 0x00046422 File Offset: 0x00044622
			// (set) Token: 0x0600144B RID: 5195 RVA: 0x0004642A File Offset: 0x0004462A
			[DataMember(Order = 20, Name = "workspaceType")]
			public string WorkspaceType { get; set; }

			// Token: 0x17000706 RID: 1798
			// (get) Token: 0x0600144C RID: 5196 RVA: 0x00046433 File Offset: 0x00044633
			// (set) Token: 0x0600144D RID: 5197 RVA: 0x0004643B File Offset: 0x0004463B
			[DataMember(Order = 30, Name = "datasetObjectId")]
			public string DatasetObjectId { get; set; }

			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x0600144E RID: 5198 RVA: 0x00046444 File Offset: 0x00044644
			// (set) Token: 0x0600144F RID: 5199 RVA: 0x0004644C File Offset: 0x0004464C
			[DataMember(Order = 40, Name = "datasetFriendlyName")]
			public string DatasetFriendlyName { get; set; }

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x06001450 RID: 5200 RVA: 0x00046455 File Offset: 0x00044655
			// (set) Token: 0x06001451 RID: 5201 RVA: 0x0004645D File Offset: 0x0004465D
			[DataMember(Order = 50, Name = "virtualServerName")]
			public string VirtualServerName { get; set; }

			// Token: 0x17000709 RID: 1801
			// (get) Token: 0x06001452 RID: 5202 RVA: 0x00046466 File Offset: 0x00044666
			// (set) Token: 0x06001453 RID: 5203 RVA: 0x0004646E File Offset: 0x0004466E
			[DataMember(Order = 60, Name = "isAvailableOnFabric", EmitDefaultValue = false, IsRequired = false)]
			public bool? IsAvailableOnFabric { get; set; }

			// Token: 0x1700070A RID: 1802
			// (get) Token: 0x06001454 RID: 5204 RVA: 0x00046477 File Offset: 0x00044677
			// (set) Token: 0x06001455 RID: 5205 RVA: 0x0004647F File Offset: 0x0004467F
			[DataMember(Order = 70, Name = "xmlaEndpointApiDNSName", EmitDefaultValue = false, IsRequired = false)]
			public string XmlaEndpointApiDNSName { get; set; }

			// Token: 0x06001456 RID: 5206 RVA: 0x00046488 File Offset: 0x00044688
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 GetWorkspaceType()
			{
				return (PbiPremiumAuthenticationHandle.WorkspaceType201606)Enum.Parse(typeof(PbiPremiumAuthenticationHandle.WorkspaceType201606), this.WorkspaceType);
			}
		}

		// Token: 0x020001E3 RID: 483
		[DataContract]
		private sealed class GetDatasetDetailsForAnalyzeInExcelRequest
		{
			// Token: 0x1700070B RID: 1803
			// (get) Token: 0x06001458 RID: 5208 RVA: 0x000464AC File Offset: 0x000446AC
			// (set) Token: 0x06001459 RID: 5209 RVA: 0x000464B4 File Offset: 0x000446B4
			[DataMember(Order = 10, Name = "clientVersion")]
			public string ClientVersion { get; set; }
		}

		// Token: 0x020001E4 RID: 484
		[DataContract]
		public class ArtifactInformationProtectionV202002
		{
			// Token: 0x1700070C RID: 1804
			// (get) Token: 0x0600145B RID: 5211 RVA: 0x000464C5 File Offset: 0x000446C5
			// (set) Token: 0x0600145C RID: 5212 RVA: 0x000464CD File Offset: 0x000446CD
			[DataMember(Order = 10, Name = "labelId", EmitDefaultValue = false)]
			public Guid LabelId { get; set; }

			// Token: 0x1700070D RID: 1805
			// (get) Token: 0x0600145D RID: 5213 RVA: 0x000464D6 File Offset: 0x000446D6
			// (set) Token: 0x0600145E RID: 5214 RVA: 0x000464DE File Offset: 0x000446DE
			[DataMember(Order = 40, Name = "artifactId", EmitDefaultValue = false)]
			public long ArtifactId { get; set; }
		}

		// Token: 0x020001E5 RID: 485
		private struct AixlCacheItem
		{
			// Token: 0x06001460 RID: 5216 RVA: 0x000464EF File Offset: 0x000446EF
			public AixlCacheItem(string workspaceFriendlyName, string datasetFriendlyName, string xmlaEndpointApiDnsName, ArtifactCapacityState artifactCapacityState)
			{
				this.WorkspaceFriendlyName = workspaceFriendlyName;
				this.DatasetFriendlyName = datasetFriendlyName;
				this.XmlaEndpointApiDnsName = xmlaEndpointApiDnsName;
				this.CapacityState = artifactCapacityState;
			}

			// Token: 0x1700070E RID: 1806
			// (get) Token: 0x06001461 RID: 5217 RVA: 0x0004650E File Offset: 0x0004470E
			public string WorkspaceFriendlyName { get; }

			// Token: 0x1700070F RID: 1807
			// (get) Token: 0x06001462 RID: 5218 RVA: 0x00046516 File Offset: 0x00044716
			public string DatasetFriendlyName { get; }

			// Token: 0x17000710 RID: 1808
			// (get) Token: 0x06001463 RID: 5219 RVA: 0x0004651E File Offset: 0x0004471E
			public string XmlaEndpointApiDnsName { get; }

			// Token: 0x17000711 RID: 1809
			// (get) Token: 0x06001464 RID: 5220 RVA: 0x00046526 File Offset: 0x00044726
			public ArtifactCapacityState CapacityState { get; }
		}

		// Token: 0x020001E6 RID: 486
		private struct SensitivityLabelCacheItem
		{
			// Token: 0x06001465 RID: 5221 RVA: 0x0004652E File Offset: 0x0004472E
			public SensitivityLabelCacheItem(string labelId, int statusCode)
			{
				this.LabelId = labelId;
				this.StatusCode = statusCode;
			}

			// Token: 0x17000712 RID: 1810
			// (get) Token: 0x06001466 RID: 5222 RVA: 0x0004653E File Offset: 0x0004473E
			public string LabelId { get; }

			// Token: 0x17000713 RID: 1811
			// (get) Token: 0x06001467 RID: 5223 RVA: 0x00046546 File Offset: 0x00044746
			public int StatusCode { get; }
		}

		// Token: 0x020001E7 RID: 487
		[DataContract]
		private sealed class GetDatabasesByDatasetNameRequest
		{
			// Token: 0x17000714 RID: 1812
			// (get) Token: 0x06001468 RID: 5224 RVA: 0x0004654E File Offset: 0x0004474E
			// (set) Token: 0x06001469 RID: 5225 RVA: 0x00046556 File Offset: 0x00044756
			[DataMember(Order = 10, Name = "datasetName")]
			public string DatasetName { get; set; }

			// Token: 0x17000715 RID: 1813
			// (get) Token: 0x0600146A RID: 5226 RVA: 0x0004655F File Offset: 0x0004475F
			// (set) Token: 0x0600146B RID: 5227 RVA: 0x00046567 File Offset: 0x00044767
			[DataMember(Order = 20, Name = "workspaceType")]
			public PbiPremiumAuthenticationHandle.WorkspaceType201606 WorkspaceType201606 { get; set; }
		}

		// Token: 0x020001E8 RID: 488
		[DataContract]
		private sealed class Dataset201606
		{
			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x0600146D RID: 5229 RVA: 0x00046578 File Offset: 0x00044778
			// (set) Token: 0x0600146E RID: 5230 RVA: 0x00046580 File Offset: 0x00044780
			[DataMember(Order = 0, Name = "databaseName")]
			public string DatabaseName { get; set; }

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x0600146F RID: 5231 RVA: 0x00046589 File Offset: 0x00044789
			// (set) Token: 0x06001470 RID: 5232 RVA: 0x00046591 File Offset: 0x00044791
			[DataMember(Order = 10, Name = "datasetName")]
			public string DatasetName { get; set; }

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004659A File Offset: 0x0004479A
			// (set) Token: 0x06001472 RID: 5234 RVA: 0x000465A2 File Offset: 0x000447A2
			[DataMember(Order = 20, Name = "versionEtag")]
			public string VersionEtag { get; set; }

			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06001473 RID: 5235 RVA: 0x000465AB File Offset: 0x000447AB
			// (set) Token: 0x06001474 RID: 5236 RVA: 0x000465B3 File Offset: 0x000447B3
			[DataMember(Order = 30, Name = "virtualServerName")]
			public string VirtualServerName { get; set; }

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06001475 RID: 5237 RVA: 0x000465BC File Offset: 0x000447BC
			// (set) Token: 0x06001476 RID: 5238 RVA: 0x000465C4 File Offset: 0x000447C4
			[DataMember(Order = 40, Name = "creatorUserPrincipalName")]
			public string CreatorUserPrincipalName { get; set; }

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06001477 RID: 5239 RVA: 0x000465CD File Offset: 0x000447CD
			// (set) Token: 0x06001478 RID: 5240 RVA: 0x000465D5 File Offset: 0x000447D5
			[DataMember(Order = 50, Name = "isAvailableOnPremiumCapacity", IsRequired = false)]
			public bool? IsAvailableOnPremiumCapacity { get; set; }
		}

		// Token: 0x020001E9 RID: 489
		[DataContract]
		private sealed class MWCASTokenRequest
		{
			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x0600147A RID: 5242 RVA: 0x000465E6 File Offset: 0x000447E6
			// (set) Token: 0x0600147B RID: 5243 RVA: 0x000465EE File Offset: 0x000447EE
			[DataMember(Name = "capacityObjectId", IsRequired = true)]
			public string CapacityObjectId { get; set; }

			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x0600147C RID: 5244 RVA: 0x000465F7 File Offset: 0x000447F7
			// (set) Token: 0x0600147D RID: 5245 RVA: 0x000465FF File Offset: 0x000447FF
			[DataMember(Name = "workspaceObjectId", IsRequired = true)]
			public string WorkspaceObjectId { get; set; }

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x0600147E RID: 5246 RVA: 0x00046608 File Offset: 0x00044808
			// (set) Token: 0x0600147F RID: 5247 RVA: 0x00046610 File Offset: 0x00044810
			[DataMember(Name = "datasetName", IsRequired = false)]
			public string DatasetName { get; set; }

			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x06001480 RID: 5248 RVA: 0x00046619 File Offset: 0x00044819
			// (set) Token: 0x06001481 RID: 5249 RVA: 0x00046621 File Offset: 0x00044821
			[DataMember(Name = "applyAuxiliaryPermission", IsRequired = false)]
			public bool ApplyAuxiliaryPermission { get; set; }

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004662A File Offset: 0x0004482A
			// (set) Token: 0x06001483 RID: 5251 RVA: 0x00046632 File Offset: 0x00044832
			[DataMember(Name = "auxiliaryPermissionOwner", IsRequired = false)]
			public string AuxiliaryPermissionOwner { get; set; }

			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x06001484 RID: 5252 RVA: 0x0004663B File Offset: 0x0004483B
			// (set) Token: 0x06001485 RID: 5253 RVA: 0x00046643 File Offset: 0x00044843
			[DataMember(Name = "bypassBuildPermission", IsRequired = false)]
			public bool BypassBuildPermission { get; set; }

			// Token: 0x17000722 RID: 1826
			// (get) Token: 0x06001486 RID: 5254 RVA: 0x0004664C File Offset: 0x0004484C
			// (set) Token: 0x06001487 RID: 5255 RVA: 0x00046654 File Offset: 0x00044854
			[DataMember(Name = "intendedUsage", IsRequired = false)]
			public int IntendedUsage { get; set; }

			// Token: 0x17000723 RID: 1827
			// (get) Token: 0x06001488 RID: 5256 RVA: 0x0004665D File Offset: 0x0004485D
			// (set) Token: 0x06001489 RID: 5257 RVA: 0x00046665 File Offset: 0x00044865
			[DataMember(Name = "sourceCapacityObjectId", IsRequired = false)]
			public string SourceCapacityObjectId { get; set; }
		}

		// Token: 0x020001EA RID: 490
		[DataContract]
		private sealed class MWCToken
		{
			// Token: 0x17000724 RID: 1828
			// (get) Token: 0x0600148B RID: 5259 RVA: 0x00046676 File Offset: 0x00044876
			// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004667E File Offset: 0x0004487E
			[DataMember]
			public string Token { get; set; }
		}

		// Token: 0x020001EB RID: 491
		[DataContract]
		private sealed class GetRedirectedWorkspaceRequest
		{
			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x0600148E RID: 5262 RVA: 0x0004668F File Offset: 0x0004488F
			// (set) Token: 0x0600148F RID: 5263 RVA: 0x00046697 File Offset: 0x00044897
			[DataMember(Name = "aasInstance", IsRequired = true)]
			public string AasInstance { get; set; }
		}

		// Token: 0x020001EC RID: 492
		[DataContract]
		private sealed class GetRedirectedWorkspaceResponse
		{
			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x06001491 RID: 5265 RVA: 0x000466A8 File Offset: 0x000448A8
			// (set) Token: 0x06001492 RID: 5266 RVA: 0x000466B0 File Offset: 0x000448B0
			[DataMember(Name = "pbiWorkspace")]
			public string PbiWorkspace { get; set; }
		}

		// Token: 0x020001ED RID: 493
		private sealed class WorkspaceResolver
		{
			// Token: 0x06001494 RID: 5268 RVA: 0x000466C1 File Offset: 0x000448C1
			public WorkspaceResolver(IList<PbiPremiumAuthenticationHandle.Workspace201606> workspaces)
			{
				this.Initialize(workspaces);
			}

			// Token: 0x06001495 RID: 5269 RVA: 0x000466D0 File Offset: 0x000448D0
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

			// Token: 0x06001496 RID: 5270 RVA: 0x0004672D File Offset: 0x0004492D
			private static string GetConflictResolverWorkspaceName(string workspaceName, string workspaceId)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", workspaceName, workspaceId);
			}

			// Token: 0x06001497 RID: 5271 RVA: 0x00046740 File Offset: 0x00044940
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

			// Token: 0x04000E87 RID: 3719
			private const string WorkspaceNameResolverFormat = "{0}-{1}";

			// Token: 0x04000E88 RID: 3720
			private Dictionary<string, PbiPremiumAuthenticationHandle.Workspace201606> friendlyNameMap;

			// Token: 0x04000E89 RID: 3721
			private HashSet<string> conflictedNames;

			// Token: 0x04000E8A RID: 3722
			private PbiPremiumAuthenticationHandle.Workspace201606 personalWorkspace;
		}
	}
}
