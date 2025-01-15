using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000190 RID: 400
	public class TokenValidationResult
	{
		// Token: 0x060011FA RID: 4602 RVA: 0x000430D9 File Offset: 0x000412D9
		public TokenValidationResult()
		{
			this.Initialize();
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000430F7 File Offset: 0x000412F7
		internal TokenValidationResult(SecurityToken securityToken, TokenHandler tokenHandler, TokenValidationParameters validationParameters, string issuer)
		{
			this._validationParameters = validationParameters;
			this._tokenHandler = tokenHandler;
			this.Issuer = issuer;
			this.SecurityToken = securityToken;
			this.Initialize();
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00043132 File Offset: 0x00041332
		public IDictionary<string, object> Claims
		{
			get
			{
				if (!this._hasIsValidOrExceptionBeenRead)
				{
					LogHelper.LogWarning("IDX10109: Warning: Claims is being accessed without first reading the properties TokenValidationResult.IsValid or TokenValidationResult.Exception. This could be a potential security issue.", Array.Empty<object>());
				}
				return this._claims.Value;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00043156 File Offset: 0x00041356
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x00043172 File Offset: 0x00041372
		public ClaimsIdentity ClaimsIdentity
		{
			get
			{
				if (this._claimsIdentity == null)
				{
					this._claimsIdentity = this.CreateClaimsIdentity();
				}
				return this._claimsIdentity;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._claimsIdentity = value;
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0004318C File Offset: 0x0004138C
		private ClaimsIdentity CreateClaimsIdentity()
		{
			if (this._validationParameters != null && this.SecurityToken != null && this._tokenHandler != null && this.Issuer != null)
			{
				return this._tokenHandler.CreateClaimsIdentityInternal(this.SecurityToken, this._validationParameters, this.Issuer);
			}
			return null;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000431D8 File Offset: 0x000413D8
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x000431E7 File Offset: 0x000413E7
		public Exception Exception
		{
			get
			{
				this._hasIsValidOrExceptionBeenRead = true;
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000431F0 File Offset: 0x000413F0
		private void Initialize()
		{
			this._claims = new Lazy<IDictionary<string, object>>(delegate
			{
				ClaimsIdentity claimsIdentity = this.ClaimsIdentity;
				return TokenUtilities.CreateDictionaryFromClaims((claimsIdentity != null) ? claimsIdentity.Claims : null);
			});
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00043209 File Offset: 0x00041409
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x00043211 File Offset: 0x00041411
		public string Issuer { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0004321A File Offset: 0x0004141A
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x00043229 File Offset: 0x00041429
		public bool IsValid
		{
			get
			{
				this._hasIsValidOrExceptionBeenRead = true;
				return this._isValid;
			}
			set
			{
				this._isValid = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00043232 File Offset: 0x00041432
		public IDictionary<string, object> PropertyBag { get; } = new Dictionary<string, object>(StringComparer.Ordinal);

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0004323A File Offset: 0x0004143A
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x00043242 File Offset: 0x00041442
		public SecurityToken SecurityToken { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004324B File Offset: 0x0004144B
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x00043253 File Offset: 0x00041453
		public SecurityToken TokenOnFailedValidation { get; internal set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0004325C File Offset: 0x0004145C
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00043264 File Offset: 0x00041464
		public CallContext TokenContext { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004326D File Offset: 0x0004146D
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00043275 File Offset: 0x00041475
		public string TokenType { get; set; }

		// Token: 0x040006D0 RID: 1744
		private Lazy<IDictionary<string, object>> _claims;

		// Token: 0x040006D1 RID: 1745
		private ClaimsIdentity _claimsIdentity;

		// Token: 0x040006D2 RID: 1746
		private Exception _exception;

		// Token: 0x040006D3 RID: 1747
		private bool _hasIsValidOrExceptionBeenRead;

		// Token: 0x040006D4 RID: 1748
		private bool _isValid;

		// Token: 0x040006D5 RID: 1749
		private TokenValidationParameters _validationParameters;

		// Token: 0x040006D6 RID: 1750
		private TokenHandler _tokenHandler;
	}
}
