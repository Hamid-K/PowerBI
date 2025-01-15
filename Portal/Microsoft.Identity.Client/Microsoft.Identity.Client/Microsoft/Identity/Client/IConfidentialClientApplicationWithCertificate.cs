using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000162 RID: 354
	public interface IConfidentialClientApplicationWithCertificate
	{
		// Token: 0x06001176 RID: 4470
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenForClientWithCertificateAsync(IEnumerable<string> scopes);

		// Token: 0x06001177 RID: 4471
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenForClientWithCertificateAsync(IEnumerable<string> scopes, bool forceRefresh);

		// Token: 0x06001178 RID: 4472
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenOnBehalfOfWithCertificateAsync(IEnumerable<string> scopes, UserAssertion userAssertion);

		// Token: 0x06001179 RID: 4473
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenForClient instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenOnBehalfOfWithCertificateAsync(IEnumerable<string> scopes, UserAssertion userAssertion, string authority);
	}
}
