using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000157 RID: 343
	internal static class InternalValidators
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x0003E50A File Offset: 0x0003C70A
		internal static void ValidateAfterSignatureFailed(SecurityToken securityToken, DateTime? notBefore, DateTime? expires, IEnumerable<string> audiences, TokenValidationParameters validationParameters, BaseConfiguration configuration)
		{
			Validators.ValidateLifetime(notBefore, expires, securityToken, validationParameters);
			Validators.ValidateIssuer(securityToken.Issuer, securityToken, validationParameters, configuration);
			Validators.ValidateAudience(audiences, securityToken, validationParameters);
		}
	}
}
