using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200014E RID: 334
	public interface IByRefreshToken
	{
		// Token: 0x060010A2 RID: 4258
		AcquireTokenByRefreshTokenParameterBuilder AcquireTokenByRefreshToken(IEnumerable<string> scopes, string refreshToken);

		// Token: 0x060010A3 RID: 4259
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use AcquireTokenByRefreshToken instead. See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		Task<AuthenticationResult> AcquireTokenByRefreshTokenAsync(IEnumerable<string> scopes, string refreshToken);
	}
}
