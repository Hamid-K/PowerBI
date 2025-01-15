using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A6B RID: 2667
	internal static class OAuthCredentialExtensions
	{
		// Token: 0x06004AA9 RID: 19113 RVA: 0x000F8014 File Offset: 0x000F6214
		public static OAuthCredential RefreshTokenAsNeeded(this OAuthCredential credential, IEngineHost engineHost, IResource resource, bool forceRefresh = false)
		{
			ICredentialService credentialService = engineHost.QueryService<ICredentialService>();
			if (credential.RefreshToken != null)
			{
				ResourceCredentialCollection resourceCredentialCollection = credentialService.RefreshCredential(resource, forceRefresh);
				if (resourceCredentialCollection.Count == 0)
				{
					throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, null, null, null);
				}
				OAuthCredential oauthCredential = resourceCredentialCollection[0] as OAuthCredential;
				if (oauthCredential == null)
				{
					throw DataSourceException.NewInvalidCredentialsError(engineHost, resource, null, null, null);
				}
				credential = oauthCredential;
			}
			return credential;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x000F8068 File Offset: 0x000F6268
		public static string AccessTokenForResource(this OAuthCredential credential, string resource = null)
		{
			if (resource != null)
			{
				string text = TokenCredential.EncodeAccessTokenKey(resource);
				string text2;
				if (credential.Properties.TryGetValue(text, out text2))
				{
					return text2;
				}
			}
			return credential.AccessToken;
		}
	}
}
