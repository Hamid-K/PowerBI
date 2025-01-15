using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000150 RID: 336
	public interface IConfidentialClientApplication : IClientApplicationBase, IApplicationBase
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060010BA RID: 4282
		ITokenCache AppTokenCache { get; }

		// Token: 0x060010BB RID: 4283
		AcquireTokenByAuthorizationCodeParameterBuilder AcquireTokenByAuthorizationCode(IEnumerable<string> scopes, string authorizationCode);

		// Token: 0x060010BC RID: 4284
		AcquireTokenForClientParameterBuilder AcquireTokenForClient(IEnumerable<string> scopes);

		// Token: 0x060010BD RID: 4285
		AcquireTokenOnBehalfOfParameterBuilder AcquireTokenOnBehalfOf(IEnumerable<string> scopes, UserAssertion userAssertion);

		// Token: 0x060010BE RID: 4286
		GetAuthorizationRequestUrlParameterBuilder GetAuthorizationRequestUrl(IEnumerable<string> scopes);

		// Token: 0x060010BF RID: 4287
		[Obsolete("In confidential client apps use AcquireTokenSilent(scopes, account) instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, string loginHint);

		// Token: 0x060010C0 RID: 4288
		[Obsolete("Use GetAccountAsync(identifier) in web apps and web APIs, and use a token cache serializer for better security and performance. See https://aka.ms/msal-net-cca-token-cache-serialization.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<IEnumerable<IAccount>> GetAccountsAsync();

		// Token: 0x060010C1 RID: 4289
		[Obsolete("Use AcquireTokenOnBehalfOf instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenOnBehalfOfAsync(IEnumerable<string> scopes, UserAssertion userAssertion);

		// Token: 0x060010C2 RID: 4290
		[Obsolete("Use AcquireTokenOnBehalfOf instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenOnBehalfOfAsync(IEnumerable<string> scopes, UserAssertion userAssertion, string authority);

		// Token: 0x060010C3 RID: 4291
		[Obsolete("Use AcquireTokenByAuthorizationCode instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Task<AuthenticationResult> AcquireTokenByAuthorizationCodeAsync(string authorizationCode, IEnumerable<string> scopes);

		// Token: 0x060010C4 RID: 4292
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenForClientAsync(IEnumerable<string> scopes);

		// Token: 0x060010C5 RID: 4293
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenForClientAsync(IEnumerable<string> scopes, bool forceRefresh);

		// Token: 0x060010C6 RID: 4294
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetAuthorizationRequestUrl instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<Uri> GetAuthorizationRequestUrlAsync(IEnumerable<string> scopes, string loginHint, string extraQueryParameters);

		// Token: 0x060010C7 RID: 4295
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetAuthorizationRequestUrl instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<Uri> GetAuthorizationRequestUrlAsync(IEnumerable<string> scopes, string redirectUri, string loginHint, string extraQueryParameters, IEnumerable<string> extraScopesToConsent, string authority);
	}
}
