using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AzureClient.Authentication;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000010 RID: 16
	[Guid("A386B29B-A68B-4AA3-9B99-EA0EDED0C11B")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class PbiPremiumTokenHolder : IPbiPremiumTokenHolder
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000025F9 File Offset: 0x000007F9
		internal PbiPremiumTokenHolder(AuthenticationHandle handle, string pbiApiBaseUri, string workspaceName, string databaseName)
		{
			this.sourceHandle = handle;
			this.pbiApiBaseUri = pbiApiBaseUri;
			this.workspaceName = workspaceName;
			this.databaseName = databaseName;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002620 File Offset: 0x00000820
		public bool TryResolvePbiWorkspace(string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out string workspaceObjectId, out string pbiDedicatedRolloutFqdn, out string capacityObjectId, out ResolvePbiWorkspaceErrorReason errorReason, out string workspaceType, out WorkspaceCapacitySkuType201606 skuType, out string technicalDetails)
		{
			if (!PbiPremiumAuthenticationHandle.TryResolvePbiWorkspace(this.pbiApiBaseUri, this.workspaceName, this.sourceHandle, requestId, serviceToServiceToken, servicePrincipalProfileId, out workspaceObjectId, out pbiDedicatedRolloutFqdn, out capacityObjectId, out errorReason, out workspaceType, out skuType, out technicalDetails))
			{
				return false;
			}
			if (!PbiPremiumAuthenticationHandle.UseAadTokenInPublicXmlaEP)
			{
				this.workspaceObjectId = workspaceObjectId;
				this.capacityObjectId = capacityObjectId;
			}
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002674 File Offset: 0x00000874
		public bool TryGetDatabaseName(string workspaceId, string workspaceType, string databaseFriendlyName, string requestId, out string databaseName, out ArtifactCapacityState artifactCapacityState, out ResolveDatabaseNameErrorReason errorReason, out string technicalDetails)
		{
			if (string.IsNullOrEmpty(workspaceId))
			{
				throw new ArgumentNullException("workspaceId");
			}
			if (string.IsNullOrEmpty(workspaceType))
			{
				throw new ArgumentNullException("workspaceType");
			}
			if (string.IsNullOrEmpty(databaseFriendlyName))
			{
				throw new ArgumentNullException("databaseFriendlyName");
			}
			bool flag = PbiPremiumAuthenticationHandle.TryGetDatabaseName(this.pbiApiBaseUri, workspaceId, workspaceType, databaseFriendlyName, this.sourceHandle, requestId, out databaseName, out artifactCapacityState, out errorReason, out technicalDetails);
			if (!flag)
			{
				artifactCapacityState = ArtifactCapacityState.Unknown;
			}
			return flag;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026DF File Offset: 0x000008DF
		public string GetAsAzureRedirectedWorkspace(string aasInstance, string requestId, string serviceToServiceToken)
		{
			if (string.IsNullOrEmpty(aasInstance))
			{
				throw new ArgumentNullException("aasInstance");
			}
			return PbiPremiumAuthenticationHandle.GetAsAzureRedirectedWorkspace(this.pbiApiBaseUri, aasInstance, this.sourceHandle, requestId, serviceToServiceToken);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002708 File Offset: 0x00000908
		public string GetFriendlyDatasetNameForPowerBI()
		{
			return this.databaseName;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002710 File Offset: 0x00000910
		public ITokenHolder GetAadToken()
		{
			if (this.aadTokenHolder == null)
			{
				this.aadTokenHolder = new TokenHolder(this.sourceHandle);
			}
			return this.aadTokenHolder;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002734 File Offset: 0x00000934
		public ITokenHolder GetMwcToken(AuxiliaryPermissionInfo auxiliaryPermissionInfo)
		{
			if (PbiPremiumAuthenticationHandle.UseAadTokenInPublicXmlaEP)
			{
				return this.GetAadToken();
			}
			if (string.IsNullOrEmpty(this.workspaceObjectId) || string.IsNullOrEmpty(this.capacityObjectId))
			{
				throw new InvalidOperationException("TryResolvePbiPremiumWorkspace should be called before getting MWC token.");
			}
			auxiliaryPermissionInfo.Validate();
			if (this.mwcTokenHolder == null)
			{
				this.mwcTokenHolder = new TokenHolder(new PbiPremiumAuthenticationHandle(this.sourceHandle, this.pbiApiBaseUri, this.workspaceObjectId, this.capacityObjectId, this.databaseName, auxiliaryPermissionInfo));
			}
			return this.mwcTokenHolder;
		}

		// Token: 0x04000026 RID: 38
		private readonly AuthenticationHandle sourceHandle;

		// Token: 0x04000027 RID: 39
		private readonly string pbiApiBaseUri;

		// Token: 0x04000028 RID: 40
		private readonly string workspaceName;

		// Token: 0x04000029 RID: 41
		private readonly string databaseName;

		// Token: 0x0400002A RID: 42
		private ITokenHolder aadTokenHolder;

		// Token: 0x0400002B RID: 43
		private ITokenHolder mwcTokenHolder;

		// Token: 0x0400002C RID: 44
		private string workspaceObjectId;

		// Token: 0x0400002D RID: 45
		private string capacityObjectId;
	}
}
