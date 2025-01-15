using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200050D RID: 1293
	internal static class ResourceHelpers
	{
		// Token: 0x060029F5 RID: 10741 RVA: 0x0007D764 File Offset: 0x0007B964
		public static OAuthResource GetAdlsResource(OAuthServices services, string url)
		{
			OAuthResource oauthResource;
			try
			{
				oauthResource = AadOAuthProvider.CreateResourceForUrl(services, url, null);
			}
			catch (OAuthException)
			{
				oauthResource = AadOAuthProvider.CreateResourceForId(services, "https://datalake.azure.net/", null);
			}
			return oauthResource;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0007D7A0 File Offset: 0x0007B9A0
		public static OAuthResource AadForStorage(OAuthServices services, string url, IEnumerable<KeyValuePair<string, string>> headers = null)
		{
			OAuthSettings oauthSettings = AadSettings.CommonSettings;
			ISecureTokenService secureTokenService;
			if (AadOAuthProvider.GetAuthenticationUrl(services, url, headers, out secureTokenService))
			{
				oauthSettings = new OAuthSettings(new ISecureTokenService[] { secureTokenService });
			}
			else
			{
				string host = new Uri(url).Host;
				foreach (KeyValuePair<string, OAuthSettings> keyValuePair in ResourceHelpers.azureStorageResources)
				{
					if (host.EndsWith(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						oauthSettings = keyValuePair.Value;
						break;
					}
				}
			}
			return AadOAuthProvider.CreateResourceForId(services, "https://storage.azure.com", oauthSettings);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0007D844 File Offset: 0x0007BA44
		public static bool IsAzureStorageResource(this IResource resource)
		{
			string text = ((resource != null) ? resource.Kind : null);
			return text == "AzureDataLakeStorage" || text == "AzureBlobs" || text == "HDInsight";
		}

		// Token: 0x0400123B RID: 4667
		public static AuthenticationInfo AnonymousAuth = new ImplicitAuthenticationInfo();

		// Token: 0x0400123C RID: 4668
		public static AuthenticationInfo BasicAuth = new UsernamePasswordAuthenticationInfo();

		// Token: 0x0400123D RID: 4669
		public static AuthenticationInfo SqlAuth = new UsernamePasswordAuthenticationInfo();

		// Token: 0x0400123E RID: 4670
		public static AuthenticationInfo WindowsAuth = new WindowsAuthenticationInfo();

		// Token: 0x0400123F RID: 4671
		public static AuthenticationInfo WindowsAuthAlternateCredentials = new WindowsAuthenticationInfo
		{
			SupportsAlternateCredentials = true
		};

		// Token: 0x04001240 RID: 4672
		public static ParameterizedAuthenticationInfo SasAuthInfo = new ParameterizedAuthenticationInfo("SAS", null)
		{
			Label = Strings.Auth_SAS,
			Properties = new CredentialProperty[]
			{
				new CredentialProperty
				{
					Name = "Token",
					Label = Strings.Auth_Token,
					IsRequired = true,
					IsSecret = true,
					PropertyType = typeof(string)
				}
			}
		};

		// Token: 0x04001241 RID: 4673
		private static readonly Dictionary<string, OAuthSettings> azureStorageResources = new Dictionary<string, OAuthSettings>
		{
			{
				".core.cloudapi.de",
				AadSettings.CommonDeSettings
			},
			{
				".core.usgovcloudapi.net",
				AadSettings.CommonUsGovSettings
			},
			{
				".core.chinacloudapi.cn",
				AadSettings.CommonCnSettings
			}
		};
	}
}
