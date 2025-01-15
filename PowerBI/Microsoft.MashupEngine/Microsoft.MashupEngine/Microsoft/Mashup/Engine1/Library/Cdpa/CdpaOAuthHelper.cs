using System;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E15 RID: 3605
	internal static class CdpaOAuthHelper
	{
		// Token: 0x06006102 RID: 24834 RVA: 0x0014A8F4 File Offset: 0x00148AF4
		public static OAuthResource GetOAuthResource(OAuthServices services, string url)
		{
			CdpaEndpointConfig cdpaEndpointConfig;
			if (CdpaEndpointConfig.TryGetEndpointConfig(url, out cdpaEndpointConfig))
			{
				return AadOAuthProvider.CreateResourceForId(services, cdpaEndpointConfig.ResourceUri, cdpaEndpointConfig.AadSettings);
			}
			throw new OAuthException(Strings.Cdpa_UriNotSupported(url));
		}
	}
}
