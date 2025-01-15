using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000188 RID: 392
	// (Invoke) Token: 0x06001165 RID: 4453
	public delegate bool TokenReplayValidator(DateTime? expirationTime, string securityToken, TokenValidationParameters validationParameters);
}
