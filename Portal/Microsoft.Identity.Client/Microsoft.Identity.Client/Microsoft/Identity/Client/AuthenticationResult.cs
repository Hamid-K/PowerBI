using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000145 RID: 325
	public class AuthenticationResult
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		public AuthenticationResult(string accessToken, bool isExtendedLifeTimeToken, string uniqueId, DateTimeOffset expiresOn, DateTimeOffset extendedExpiresOn, string tenantId, IAccount account, string idToken, IEnumerable<string> scopes, Guid correlationId, string tokenType = "Bearer", AuthenticationResultMetadata authenticationResultMetadata = null, ClaimsPrincipal claimsPrincipal = null, string spaAuthCode = null, IReadOnlyDictionary<string, string> additionalResponseParameters = null)
		{
			this.AccessToken = accessToken;
			this.IsExtendedLifeTimeToken = isExtendedLifeTimeToken;
			this.ExtendedExpiresOn = extendedExpiresOn;
			this.UniqueId = uniqueId;
			this.ExpiresOn = expiresOn;
			this.TenantId = tenantId;
			this.Account = account;
			this.IdToken = idToken;
			this.Scopes = scopes;
			this.CorrelationId = correlationId;
			this.TokenType = tokenType;
			this.AuthenticationResultMetadata = authenticationResultMetadata;
			this.ClaimsPrincipal = claimsPrincipal;
			this.SpaAuthCode = spaAuthCode;
			this.AdditionalResponseParameters = additionalResponseParameters;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003AC5C File Offset: 0x00038E5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AuthenticationResult(string accessToken, bool isExtendedLifeTimeToken, string uniqueId, DateTimeOffset expiresOn, DateTimeOffset extendedExpiresOn, string tenantId, IAccount account, string idToken, IEnumerable<string> scopes, Guid correlationId, AuthenticationResultMetadata authenticationResultMetadata, string tokenType = "Bearer")
			: this(accessToken, isExtendedLifeTimeToken, uniqueId, expiresOn, extendedExpiresOn, tenantId, account, idToken, scopes, correlationId, tokenType, authenticationResultMetadata, null, null, null)
		{
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003AC88 File Offset: 0x00038E88
		internal AuthenticationResult(MsalAccessTokenCacheItem msalAccessTokenCacheItem, MsalIdTokenCacheItem msalIdTokenCacheItem, IAuthenticationScheme authenticationScheme, Guid correlationID, TokenSource tokenSource, ApiEvent apiEvent, Account account, string spaAuthCode, IReadOnlyDictionary<string, string> additionalResponseParameters)
		{
			if (authenticationScheme == null)
			{
				throw new ArgumentNullException("authenticationScheme");
			}
			this._authenticationScheme = authenticationScheme;
			string text = ((msalAccessTokenCacheItem != null) ? msalAccessTokenCacheItem.HomeAccountId : null) ?? ((msalIdTokenCacheItem != null) ? msalIdTokenCacheItem.HomeAccountId : null);
			string text2 = ((msalAccessTokenCacheItem != null) ? msalAccessTokenCacheItem.Environment : null) ?? ((msalIdTokenCacheItem != null) ? msalIdTokenCacheItem.Environment : null);
			this.ClaimsPrincipal = ((msalIdTokenCacheItem != null) ? msalIdTokenCacheItem.IdToken.ClaimsPrincipal : null);
			if (account != null)
			{
				this.Account = account;
			}
			else if (text != null)
			{
				this.Account = new Account(text, (msalIdTokenCacheItem != null) ? msalIdTokenCacheItem.GetUsername() : null, text2, null, null);
			}
			string text3;
			if (msalIdTokenCacheItem == null)
			{
				text3 = null;
			}
			else
			{
				IdToken idToken = msalIdTokenCacheItem.IdToken;
				text3 = ((idToken != null) ? idToken.GetUniqueId() : null);
			}
			this.UniqueId = text3;
			string text4;
			if (msalIdTokenCacheItem == null)
			{
				text4 = null;
			}
			else
			{
				IdToken idToken2 = msalIdTokenCacheItem.IdToken;
				text4 = ((idToken2 != null) ? idToken2.TenantId : null);
			}
			this.TenantId = text4;
			this.IdToken = ((msalIdTokenCacheItem != null) ? msalIdTokenCacheItem.Secret : null);
			this.SpaAuthCode = spaAuthCode;
			this.CorrelationId = correlationID;
			this.ApiEvent = apiEvent;
			this.AuthenticationResultMetadata = new AuthenticationResultMetadata(tokenSource);
			this.AdditionalResponseParameters = additionalResponseParameters;
			if (msalAccessTokenCacheItem != null)
			{
				this.AccessToken = authenticationScheme.FormatAccessToken(msalAccessTokenCacheItem);
				this.ExpiresOn = msalAccessTokenCacheItem.ExpiresOn;
				this.Scopes = msalAccessTokenCacheItem.ScopeSet;
				this.ExtendedExpiresOn = msalAccessTokenCacheItem.ExtendedExpiresOn;
				this.IsExtendedLifeTimeToken = msalAccessTokenCacheItem.IsExtendedLifeTimeToken;
				this.TokenType = msalAccessTokenCacheItem.TokenType;
				if (msalAccessTokenCacheItem.RefreshOn != null)
				{
					this.AuthenticationResultMetadata.RefreshOn = msalAccessTokenCacheItem.RefreshOn;
				}
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003AE16 File Offset: 0x00039016
		internal AuthenticationResult()
		{
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003AE1E File Offset: 0x0003901E
		public string AccessToken { get; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0003AE26 File Offset: 0x00039026
		[Obsolete("This feature has been deprecated", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsExtendedLifeTimeToken { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003AE2E File Offset: 0x0003902E
		public string UniqueId { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0003AE36 File Offset: 0x00039036
		public DateTimeOffset ExpiresOn { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0003AE3E File Offset: 0x0003903E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This feature has been deprecated", false)]
		public DateTimeOffset ExtendedExpiresOn { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0003AE46 File Offset: 0x00039046
		public string TenantId { get; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0003AE4E File Offset: 0x0003904E
		public IAccount Account { get; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0003AE56 File Offset: 0x00039056
		public string IdToken { get; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003AE5E File Offset: 0x0003905E
		public IEnumerable<string> Scopes { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0003AE66 File Offset: 0x00039066
		public Guid CorrelationId { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003AE6E File Offset: 0x0003906E
		public string TokenType { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003AE76 File Offset: 0x00039076
		public string SpaAuthCode { get; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003AE7E File Offset: 0x0003907E
		public IReadOnlyDictionary<string, string> AdditionalResponseParameters { get; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003AE86 File Offset: 0x00039086
		public ClaimsPrincipal ClaimsPrincipal { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003AE8E File Offset: 0x0003908E
		internal ApiEvent ApiEvent { get; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0003AE96 File Offset: 0x00039096
		public AuthenticationResultMetadata AuthenticationResultMetadata { get; }

		// Token: 0x06001040 RID: 4160 RVA: 0x0003AE9E File Offset: 0x0003909E
		public string CreateAuthorizationHeader()
		{
			IAuthenticationScheme authenticationScheme = this._authenticationScheme;
			return (((authenticationScheme != null) ? authenticationScheme.AuthorizationHeaderPrefix : null) ?? this.TokenType) + " " + this.AccessToken;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0003AECC File Offset: 0x000390CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use Account instead (See https://aka.ms/msal-net-2-released)", true)]
		public IUser User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040004E2 RID: 1250
		private readonly IAuthenticationScheme _authenticationScheme;
	}
}
