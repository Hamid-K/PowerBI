using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000015 RID: 21
	public class DefaultSecurityTokenHandler : ISecurityTokenHandler, ISecurityTokenValidator
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000290E File Offset: 0x00000B0E
		public string WriteToken(SecurityToken token)
		{
			return this._handler.WriteToken(token);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000291C File Offset: 0x00000B1C
		public bool CanReadToken(string securityToken)
		{
			return this._handler.CanReadToken(securityToken);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000292A File Offset: 0x00000B2A
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002937 File Offset: 0x00000B37
		public int MaximumTokenSizeInBytes
		{
			get
			{
				return this._handler.MaximumTokenSizeInBytes;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000214F File Offset: 0x0000034F
		public bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000293E File Offset: 0x00000B3E
		public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
		{
			return this._handler.ValidateToken(securityToken, validationParameters, out validatedToken);
		}

		// Token: 0x0400003B RID: 59
		private readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();
	}
}
