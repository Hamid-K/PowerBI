using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AzureClient.Authentication;
using Microsoft.AnalysisServices.AzureClient.Hosting;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x0200000B RID: 11
	[Guid("92DB0183-4232-4F12-82C7-5BBE8FDDD4E1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class AadAuthenticator : IAadAuthenticator, IConnectivityOwner
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021D9 File Offset: 0x000003D9
		public AadAuthenticator()
		{
			AuthenticationManager.EnableStrongSecurityProtocols();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E8 File Offset: 0x000003E8
		public ITokenHolder AcquireToken(string resource, string dataSource, string identityProvider, string tenantId, string userId, string password, AadAuthenticationOptions options)
		{
			if (string.IsNullOrEmpty(resource))
			{
				throw new ArgumentNullException("resource");
			}
			if (string.IsNullOrEmpty(dataSource))
			{
				throw new ArgumentNullException("dataSource");
			}
			AuthenticationInformation authenticationInformation;
			return new TokenHolder(AadAuthenticator.AcquireTokenImpl(this, resource, dataSource, identityProvider, tenantId, userId, password, null, null, options, false, out authenticationInformation));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002238 File Offset: 0x00000438
		public IPbiPremiumTokenHolder AcquireTokenForPbiPremium(string workspaceName, string resource, string dataSource, string identityProvider, string tenantId, string userId, string password, string databaseName, AadAuthenticationOptions options)
		{
			if (string.IsNullOrEmpty(resource))
			{
				throw new ArgumentNullException("resource");
			}
			if (string.IsNullOrEmpty(dataSource))
			{
				throw new ArgumentNullException("dataSource");
			}
			AuthenticationInformation authenticationInformation;
			AuthenticationHandle authenticationHandle = AadAuthenticator.AcquireTokenImpl(this, resource, dataSource, identityProvider, tenantId, userId, password, workspaceName, databaseName, options, false, out authenticationInformation);
			if (AuthenticationInformation.IsDataverseIdentityProvider(identityProvider))
			{
				DataverseAuthenticationHandle dataverseAuthenticationHandle = authenticationHandle as DataverseAuthenticationHandle;
				if (dataverseAuthenticationHandle == null)
				{
					throw new InvalidOperationException("Invalid authentication handle for dataverse");
				}
				DataverseAuthenticationHandle.PowerBIDatasetDetails powerBIDatasetDetails = dataverseAuthenticationHandle.GetPowerBIDatasetDetails();
				workspaceName = powerBIDatasetDetails.PowerBIWorkspaceName;
				databaseName = powerBIDatasetDetails.PowerBIDatasetName;
			}
			return new PbiPremiumTokenHolder(authenticationHandle, AadAuthenticator.GetPbiApiBaseUrl(identityProvider, dataSource, authenticationInformation), workspaceName, databaseName);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C8 File Offset: 0x000004C8
		public IPbiPremiumTokenHolder AcquireTokenForAsAzureRedirection(string pbiApiBaseUri, string dataSource, string tenantId, string userId, string password, AadAuthenticationOptions options)
		{
			if (string.IsNullOrEmpty(pbiApiBaseUri))
			{
				throw new ArgumentNullException("pbiApiBaseUri");
			}
			if (string.IsNullOrEmpty(dataSource))
			{
				throw new ArgumentNullException("dataSource");
			}
			AuthenticationInformation authenticationInformation;
			return new PbiPremiumTokenHolder(AadAuthenticator.AcquireTokenImpl(this, dataSource, dataSource, null, tenantId, userId, password, null, null, options, true, out authenticationInformation), pbiApiBaseUri, null, null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002318 File Offset: 0x00000518
		public bool TryGetDatasetDetails(string pbiApiBaseUri, string token, string catalog, string requestId, string serviceToServiceToken, out string datasetFriendlyName, out string workspaceFriendlyName, out string xmlaEndpointApiDnsName, out bool isAvailableOnFabric)
		{
			if (string.IsNullOrEmpty(pbiApiBaseUri))
			{
				throw new ArgumentNullException("pbiApiBaseUri");
			}
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentNullException("token");
			}
			if (string.IsNullOrEmpty(catalog))
			{
				throw new ArgumentNullException("catalog");
			}
			isAvailableOnFabric = false;
			ExternalAuthenticationHandle externalAuthenticationHandle = new ExternalAuthenticationHandle(token, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(null));
			ExternalAuthenticationHandle externalAuthenticationHandle2 = ((!string.IsNullOrEmpty(serviceToServiceToken)) ? new ExternalAuthenticationHandle(serviceToServiceToken, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(null)) : null);
			ArtifactCapacityState artifactCapacityState;
			if (!PbiPremiumAuthenticationHandle.TryGetDatasetDetailsForAnalyzeInExcel(pbiApiBaseUri, catalog, externalAuthenticationHandle, externalAuthenticationHandle2, requestId, out datasetFriendlyName, out workspaceFriendlyName, out xmlaEndpointApiDnsName, out artifactCapacityState))
			{
				return false;
			}
			isAvailableOnFabric = artifactCapacityState == ArtifactCapacityState.AssignedToCapacity;
			return true;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023A8 File Offset: 0x000005A8
		public bool TryGetSensitivityLabel(string pbiInformationProtectionBaseUri, string token, string datasetFriendlyName, string requestId, out string labelId, out int statusCode)
		{
			if (string.IsNullOrEmpty(pbiInformationProtectionBaseUri))
			{
				throw new ArgumentNullException("pbiInformationProtectionBaseUri");
			}
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentNullException("token");
			}
			if (string.IsNullOrEmpty(datasetFriendlyName))
			{
				throw new ArgumentNullException("datasetFriendlyName");
			}
			ExternalAuthenticationHandle externalAuthenticationHandle = new ExternalAuthenticationHandle(token, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(null));
			return PbiPremiumAuthenticationHandle.TryGetSensitivityLabel(pbiInformationProtectionBaseUri, datasetFriendlyName, externalAuthenticationHandle, requestId, out labelId, out statusCode);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002410 File Offset: 0x00000610
		private static AuthenticationHandle AcquireTokenImpl(IConnectivityOwner owner, string resource, string dataSource, string identityProvider, string tenantId, string userId, string password, string workspaceName, string databaseName, AadAuthenticationOptions options, bool isForAsAzureRedirection, out AuthenticationInformation authenticationInformation)
		{
			authenticationInformation = null;
			if (userId == null && password != null)
			{
				return new ExternalAuthenticationHandle(password, AuthenticationHandle.ConvertIdentityProviderToTokenScheme(identityProvider));
			}
			AuthenticationHandle authenticationHandle;
			using (AuthenticationTracer.StartScope("AadAuthenticator.AcquireToken"))
			{
				try
				{
					AuthenticationTracer.TraceInformation("AadAuthenticator.AcquireToken - resource:'{0}', dataSource:'{1}', identityProvider:'{2}', tenantId:'{3}', userId:'{4}', options=[useTokenCache:{5}, ssoMode:{6}], isForAsAzureRedirection:{7}", new object[] { resource, dataSource, identityProvider, tenantId, userId, options.UseTokenCache, options.SsoMode, isForAsAzureRedirection });
					authenticationHandle = AuthenticationManager.Authenticate(new AuthenticationOptions(owner)
					{
						UseTokenCache = options.UseTokenCache,
						SsoMode = options.SsoMode,
						BypassAuthInfoValidation = (!string.IsNullOrEmpty(identityProvider) && (AadAuthenticator.HasUriProtocolScheme(dataSource, "https") || AadAuthenticator.HasUriProtocolScheme(dataSource, "pbiazure"))),
						HasServicePrincipalProfile = options.HasServicePrincipalProfile
					}, identityProvider, resource, tenantId, userId, password, workspaceName, databaseName, isForAsAzureRedirection, out authenticationInformation);
				}
				catch (Exception ex)
				{
					AuthenticationTracer.TraceError("AadAuthenticator.AcquireToken failed; Exception: {0}", new object[] { ex });
					throw;
				}
			}
			return authenticationHandle;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002540 File Offset: 0x00000740
		private static string GetPbiApiBaseUrl(string identityProvider, string dataSource, AuthenticationInformation authInfo)
		{
			if (!AuthenticationInformation.IsDataverseIdentityProvider(identityProvider))
			{
				return new Uri(dataSource).Authority;
			}
			string text = ((authInfo != null) ? authInfo.PowerBIEndpoint : null);
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("pbiApiBaseUrl");
			}
			return text;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002578 File Offset: 0x00000778
		private static bool HasUriProtocolScheme(string url, string scheme)
		{
			return !string.IsNullOrEmpty(url) && (url.StartsWith(scheme, StringComparison.InvariantCultureIgnoreCase) && url.Length > scheme.Length + "://".Length) && string.Compare(url, scheme.Length, "://", 0, "://".Length, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x04000024 RID: 36
		private const string PbiAzureScheme = "pbiazure";

		// Token: 0x04000025 RID: 37
		private const string HttpsScheme = "https";
	}
}
