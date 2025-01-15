using System;
using System.Security.Claims;
using System.Xml;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000175 RID: 373
	public abstract class SecurityTokenHandler : TokenHandler, ISecurityTokenValidator
	{
		// Token: 0x060010D3 RID: 4307 RVA: 0x00040C71 File Offset: 0x0003EE71
		public virtual SecurityKeyIdentifierClause CreateSecurityTokenReference(SecurityToken token, bool attached)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00040C78 File Offset: 0x0003EE78
		public virtual SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00040C7F File Offset: 0x0003EE7F
		public virtual bool CanValidateToken
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00040C82 File Offset: 0x0003EE82
		public virtual bool CanWriteToken
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060010D7 RID: 4311
		public abstract Type TokenType { get; }

		// Token: 0x060010D8 RID: 4312 RVA: 0x00040C85 File Offset: 0x0003EE85
		public virtual bool CanReadToken(XmlReader reader)
		{
			return false;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00040C88 File Offset: 0x0003EE88
		public virtual bool CanReadToken(string tokenString)
		{
			return false;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00040C8B File Offset: 0x0003EE8B
		public virtual SecurityToken ReadToken(XmlReader reader)
		{
			return null;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00040C8E File Offset: 0x0003EE8E
		public virtual string WriteToken(SecurityToken token)
		{
			return null;
		}

		// Token: 0x060010DC RID: 4316
		public abstract void WriteToken(XmlWriter writer, SecurityToken token);

		// Token: 0x060010DD RID: 4317
		public abstract SecurityToken ReadToken(XmlReader reader, TokenValidationParameters validationParameters);

		// Token: 0x060010DE RID: 4318 RVA: 0x00040C91 File Offset: 0x0003EE91
		public virtual ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00040C98 File Offset: 0x0003EE98
		public virtual ClaimsPrincipal ValidateToken(XmlReader reader, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
		{
			throw new NotImplementedException();
		}
	}
}
