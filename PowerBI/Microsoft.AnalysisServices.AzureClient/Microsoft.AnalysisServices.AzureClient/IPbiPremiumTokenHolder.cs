using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x0200000F RID: 15
	[Guid("97358017-2B84-427F-8737-1E4CB04A7FD7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IPbiPremiumTokenHolder
	{
		// Token: 0x06000022 RID: 34
		bool TryResolvePbiWorkspace(string requestId, string serviceToServiceToken, string servicePrincipalProfileId, out string workspaceObjectId, out string pbiDedicatedRolloutFqdn, out string capacityObjectId, out ResolvePbiWorkspaceErrorReason errorReason, out string workspaceType, out WorkspaceCapacitySkuType201606 skuType, out string technicalDetails);

		// Token: 0x06000023 RID: 35
		bool TryGetDatabaseName(string workspaceId, string workspaceType, string databaseFriendlyName, string requestId, out string databaseName, out ArtifactCapacityState databaseCapacityState, out ResolveDatabaseNameErrorReason errorReason, out string technicalDetails);

		// Token: 0x06000024 RID: 36
		string GetAsAzureRedirectedWorkspace(string aasInstance, string requestId, string serviceToServiceToken);

		// Token: 0x06000025 RID: 37
		string GetFriendlyDatasetNameForPowerBI();

		// Token: 0x06000026 RID: 38
		ITokenHolder GetAadToken();

		// Token: 0x06000027 RID: 39
		ITokenHolder GetMwcToken(AuxiliaryPermissionInfo auxiliaryPermissionInfo);
	}
}
