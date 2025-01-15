using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x0200000D RID: 13
	[Guid("2E32D74C-1F87-48BF-B307-3F2A10C2F015")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IAadAuthenticator
	{
		// Token: 0x06000018 RID: 24
		ITokenHolder AcquireToken(string resource, string dataSource, string identityProvider, string tenantId, string userId, string password, AadAuthenticationOptions options);

		// Token: 0x06000019 RID: 25
		IPbiPremiumTokenHolder AcquireTokenForPbiPremium(string workspaceName, string resource, string dataSource, string identityProvider, string tenantId, string userId, string password, string databaseName, AadAuthenticationOptions options);

		// Token: 0x0600001A RID: 26
		IPbiPremiumTokenHolder AcquireTokenForAsAzureRedirection(string pbiApiBaseUri, string dataSource, string tenantId, string userId, string password, AadAuthenticationOptions options);

		// Token: 0x0600001B RID: 27
		bool TryGetDatasetDetails(string pbiApiBaseUri, string token, string catalog, string requestId, string serviceToServiceToken, out string datasetFriendlyName, out string workspaceFriendlyName, out string xmlaEndpointApiDnsName, out bool isAvailableOnFabric);

		// Token: 0x0600001C RID: 28
		bool TryGetSensitivityLabel(string pbiInformationProtectionBaseUri, string token, string datasetFriendlyName, string requestId, out string labelId, out int statusCode);
	}
}
