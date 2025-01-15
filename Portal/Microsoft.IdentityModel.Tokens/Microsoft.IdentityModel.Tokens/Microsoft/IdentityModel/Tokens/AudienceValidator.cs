using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200017F RID: 383
	// (Invoke) Token: 0x06001141 RID: 4417
	public delegate bool AudienceValidator(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters);
}
