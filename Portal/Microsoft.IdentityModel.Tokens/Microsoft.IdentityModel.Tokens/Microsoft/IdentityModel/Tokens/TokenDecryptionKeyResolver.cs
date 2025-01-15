using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200018C RID: 396
	// (Invoke) Token: 0x06001175 RID: 4469
	public delegate IEnumerable<SecurityKey> TokenDecryptionKeyResolver(string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters);
}
