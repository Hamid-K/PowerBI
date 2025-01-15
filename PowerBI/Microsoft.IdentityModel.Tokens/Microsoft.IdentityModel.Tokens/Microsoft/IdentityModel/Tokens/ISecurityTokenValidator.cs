using System;
using System.Security.Claims;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000158 RID: 344
	public interface ISecurityTokenValidator
	{
		// Token: 0x06000FFD RID: 4093
		bool CanReadToken(string securityToken);

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000FFE RID: 4094
		bool CanValidateToken { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000FFF RID: 4095
		// (set) Token: 0x06001000 RID: 4096
		int MaximumTokenSizeInBytes { get; set; }

		// Token: 0x06001001 RID: 4097
		ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken);
	}
}
