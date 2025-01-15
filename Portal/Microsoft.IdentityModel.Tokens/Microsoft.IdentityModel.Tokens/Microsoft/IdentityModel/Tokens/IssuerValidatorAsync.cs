using System;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000186 RID: 390
	// (Invoke) Token: 0x0600115D RID: 4445
	internal delegate Task<string> IssuerValidatorAsync(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters);
}
