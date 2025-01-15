using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000187 RID: 391
	// (Invoke) Token: 0x06001161 RID: 4449
	public delegate bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters);
}
