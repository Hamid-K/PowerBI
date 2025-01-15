using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	internal sealed class AuthenticationExtensionUnauthorizedException : RSException
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000053AB File Offset: 0x000035AB
		// (set) Token: 0x06000295 RID: 661 RVA: 0x000053B3 File Offset: 0x000035B3
		public KeyValuePair<string, string> HttpResponseHeader { get; private set; }

		// Token: 0x06000296 RID: 662 RVA: 0x000053BC File Offset: 0x000035BC
		public AuthenticationExtensionUnauthorizedException(string authorizationUri, string resourceId, string nativeClientId, string oauthLogoutUrl)
			: base(ErrorCode.rsAuthorizationHeaderNotFound, ErrorStringsWrapper.rsAuthorizationTokenInvalidOrExpired, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
			this.HttpResponseHeader = new KeyValuePair<string, string>("WWW-authenticate", this.GetAuthenticateHeader(authorizationUri, resourceId, nativeClientId, oauthLogoutUrl));
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000540C File Offset: 0x0000360C
		public AuthenticationExtensionUnauthorizedException(Exception innerException, string authorizationUri, string resourceId, string nativeClientId, string oauthLogoutUrl)
			: base(ErrorCode.rsAuthorizationTokenInvalidOrExpired, ErrorStringsWrapper.rsAuthorizationTokenInvalidOrExpired, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
			this.HttpResponseHeader = new KeyValuePair<string, string>("WWW-authenticate", this.GetAuthenticateHeader(authorizationUri, resourceId, nativeClientId, oauthLogoutUrl));
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000545B File Offset: 0x0000365B
		private AuthenticationExtensionUnauthorizedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00005465 File Offset: 0x00003665
		private string GetAuthenticateHeader(string authorizationUri, string resourceId, string nativeClientId, string oauthLogoutUrl)
		{
			return string.Format("Bearer authorization_uri={0},resource_id={1},nativeclient_id={2},oauthLogoutUrl_uri={3}", new object[] { authorizationUri, resourceId, nativeClientId, oauthLogoutUrl });
		}
	}
}
