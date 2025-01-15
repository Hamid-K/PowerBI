using System;
using System.IdentityModel.Tokens.Jwt;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000010 RID: 16
	// (Invoke) Token: 0x060000F3 RID: 243
	public delegate void IdTokenValidator(JwtSecurityToken idToken, OpenIdConnectProtocolValidationContext context);
}
