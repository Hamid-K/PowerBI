using System;
using System.Globalization;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200015B RID: 347
	internal static class LogMessages
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x0003B7EE File Offset: 0x000399EE
		public static string ErrorReturnedInBrokerResponse(string error)
		{
			return "Error " + error + " returned in broker response. ";
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003B800 File Offset: 0x00039A00
		public static string UsingXScopesForRefreshTokenRequest(int numScopes)
		{
			return string.Format(CultureInfo.InvariantCulture, "Using {0} scopes for acquire token by refresh token request", numScopes);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003B817 File Offset: 0x00039A17
		public static string CustomWebUiCallingAcquireAuthorizationCodePii(Uri authorizationUri, Uri redirectUri)
		{
			return string.Format(CultureInfo.InvariantCulture, "calling CustomWebUi.AcquireAuthorizationCode authUri({0}) redirectUri({1})", authorizationUri, redirectUri);
		}

		// Token: 0x0400051D RID: 1309
		public const string BeginningAcquireByRefreshToken = "Begin acquire token by refresh token...";

		// Token: 0x0400051E RID: 1310
		public const string NoScopesProvidedForRefreshTokenRequest = "No scopes provided for acquire token by refresh token request. Using default scope instead.";

		// Token: 0x0400051F RID: 1311
		public const string CustomWebUiAcquiringAuthorizationCode = "Using CustomWebUi to acquire the authorization code";

		// Token: 0x04000520 RID: 1312
		public const string CustomWebUiRedirectUriMatched = "Redirect Uri was matched.  Returning success from CustomWebUiHandler.";

		// Token: 0x04000521 RID: 1313
		public const string CustomWebUiOperationCancelled = "CustomWebUi AcquireAuthorizationCode was canceled";

		// Token: 0x04000522 RID: 1314
		public const string CustomWebUiCallingAcquireAuthorizationCodeNoPii = "Calling CustomWebUi.AcquireAuthorizationCode";

		// Token: 0x04000523 RID: 1315
		public const string ClientAssertionDoesNotExistOrNearExpiry = "Client Assertion does not exist or near expiry. ";

		// Token: 0x04000524 RID: 1316
		public const string ReusingTheUnexpiredClientAssertion = "Reusing the unexpired Client Assertion...";

		// Token: 0x04000525 RID: 1317
		public const string ResolvingAuthorityEndpointsTrue = "Resolving authority endpoints... Already resolved? - TRUE";

		// Token: 0x04000526 RID: 1318
		public const string ResolvingAuthorityEndpointsFalse = "Resolving authority endpoints... Already resolved? - FALSE";

		// Token: 0x04000527 RID: 1319
		public const string CheckMsalTokenResponseReturnedFromBroker = "Checking MsalTokenResponse returned from broker. ";

		// Token: 0x04000528 RID: 1320
		public const string UnknownErrorReturnedInBrokerResponse = "Unknown error returned in broker response. ";

		// Token: 0x04000529 RID: 1321
		public const string BrokerInvocationRequired = "Based on auth code received from STS, broker invocation is required. ";

		// Token: 0x0400052A RID: 1322
		public const string AddBrokerInstallUrlToPayload = "Broker is required for authentication and broker is not installed on the device. Adding BrokerInstallUrl to broker payload. ";

		// Token: 0x0400052B RID: 1323
		public const string BrokerInvocationNotRequired = "Based on auth code received from STS, broker invocation is not required. ";

		// Token: 0x0400052C RID: 1324
		public const string CanInvokeBrokerAcquireTokenWithBroker = "Can invoke broker. Will attempt to acquire token with broker. ";

		// Token: 0x0400052D RID: 1325
		public const string AuthenticationWithBrokerDidNotSucceed = "Broker authentication did not succeed, or the broker install failed. See https://aka.ms/msal-net-brokers for more information. ";

		// Token: 0x0400052E RID: 1326
		public const string UserCancelledAuthentication = "Authorization result status returned user cancelled authentication. ";

		// Token: 0x0400052F RID: 1327
		public const string AuthorizationResultWasNotSuccessful = "Authorization result was not successful. See error message for more details. ";

		// Token: 0x04000530 RID: 1328
		public const string WsTrustRequestFailed = "Ws-Trust request failed. See error message for more details.";
	}
}
