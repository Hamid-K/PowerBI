using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000181 RID: 385
	// (Invoke) Token: 0x06001149 RID: 4425
	public delegate IEnumerable<SecurityKey> IssuerSigningKeyResolverUsingConfiguration(string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters, BaseConfiguration configuration);
}
