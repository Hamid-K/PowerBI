using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache.Keys;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002B6 RID: 694
	internal class MsalAccessTokenCacheItem : MsalCredentialCacheItemBase
	{
		// Token: 0x060019ED RID: 6637 RVA: 0x0005510C File Offset: 0x0005330C
		internal MsalAccessTokenCacheItem(string preferredCacheEnv, string clientId, MsalTokenResponse response, string tenantId, string homeAccountId, string keyId = null, string oboCacheKey = null)
			: this(ScopeHelper.OrderScopesAlphabetically(response.Scope), DateTimeOffset.UtcNow, DateTimeHelpers.DateTimeOffsetFromDuration(response.ExpiresIn), DateTimeHelpers.DateTimeOffsetFromDuration(response.ExtendedExpiresIn), DateTimeHelpers.DateTimeOffsetFromDuration(response.RefreshIn), tenantId, keyId, response.TokenType)
		{
			base.Environment = preferredCacheEnv;
			base.ClientId = clientId;
			base.Secret = response.AccessToken;
			base.RawClientInfo = response.ClientInfo;
			base.HomeAccountId = homeAccountId;
			this.OboCacheKey = oboCacheKey;
			this.InitCacheKey();
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00055198 File Offset: 0x00053398
		internal MsalAccessTokenCacheItem(string preferredCacheEnv, string clientId, string scopes, string tenantId, string secret, DateTimeOffset cachedAt, DateTimeOffset expiresOn, DateTimeOffset extendedExpiresOn, string rawClientInfo, string homeAccountId, string keyId = null, DateTimeOffset? refreshOn = null, string tokenType = "Bearer", string oboCacheKey = null)
			: this(scopes, cachedAt, expiresOn, extendedExpiresOn, refreshOn, tenantId, keyId, tokenType)
		{
			base.Environment = preferredCacheEnv;
			base.ClientId = clientId;
			base.Secret = secret;
			base.RawClientInfo = rawClientInfo;
			base.HomeAccountId = homeAccountId;
			this.OboCacheKey = oboCacheKey;
			this.InitCacheKey();
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000551F0 File Offset: 0x000533F0
		private MsalAccessTokenCacheItem(string scopes, DateTimeOffset cachedAt, DateTimeOffset expiresOn, DateTimeOffset extendedExpiresOn, DateTimeOffset? refreshOn, string tenantId, string keyId, string tokenType)
		{
			base.CredentialType = "AccessToken";
			this.ScopeString = scopes;
			this.ScopeSet = ScopeHelper.ConvertStringToScopeSet(this.ScopeString);
			this.ExpiresOn = expiresOn;
			this.ExtendedExpiresOn = extendedExpiresOn;
			this.RefreshOn = refreshOn;
			this.TenantId = tenantId ?? "";
			this.KeyId = keyId;
			this.TokenType = tokenType;
			this.CachedAt = cachedAt;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00055268 File Offset: 0x00053468
		internal MsalAccessTokenCacheItem WithExpiresOn(DateTimeOffset expiresOn)
		{
			return new MsalAccessTokenCacheItem(base.Environment, base.ClientId, this.ScopeString, this.TenantId, base.Secret, this.CachedAt, expiresOn, this.ExtendedExpiresOn, base.RawClientInfo, base.HomeAccountId, this.KeyId, this.RefreshOn, this.TokenType, this.OboCacheKey);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000552CC File Offset: 0x000534CC
		internal void InitCacheKey()
		{
			this._extraKeyParts = null;
			this._credentialDescriptor = "AccessToken";
			if (AuthSchemeHelper.StoreTokenTypeInCacheKey(this.TokenType))
			{
				this._extraKeyParts = new string[] { this.TokenType };
				this._credentialDescriptor = "AccessToken_With_AuthScheme";
			}
			this.CacheKey = MsalCacheKeys.GetCredentialKey(base.HomeAccountId, base.Environment, this._credentialDescriptor, base.ClientId, this.TenantId, this.ScopeString, this._extraKeyParts);
			this.iOSCacheKeyLazy = new Lazy<IiOSKey>(new Func<IiOSKey>(this.InitiOSKey));
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00055364 File Offset: 0x00053564
		internal string ToLogString(bool piiEnabled = false)
		{
			string text;
			if (!piiEnabled)
			{
				string homeAccountId = base.HomeAccountId;
				text = ((homeAccountId != null) ? homeAccountId.GetHashCode().ToString() : null);
			}
			else
			{
				text = base.HomeAccountId;
			}
			return MsalCacheKeys.GetCredentialKey(text, base.Environment, this._credentialDescriptor, base.ClientId, this.TenantId, this.ScopeString, this._extraKeyParts);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000553C0 File Offset: 0x000535C0
		private IiOSKey InitiOSKey()
		{
			string text = MsalCacheKeys.GetiOSAccountKey(base.HomeAccountId, base.Environment);
			string text2 = MsalCacheKeys.GetiOSServiceKey(this._credentialDescriptor, base.ClientId, this.TenantId, this.ScopeString, this._extraKeyParts);
			string text3 = MsalCacheKeys.GetiOSGenericKey(this._credentialDescriptor, base.ClientId, this.TenantId);
			int num = 2001;
			return new IosKey(text, text2, text3, num);
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0005542D File Offset: 0x0005362D
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x00055435 File Offset: 0x00053635
		internal string TenantId { get; private set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0005543E File Offset: 0x0005363E
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x00055446 File Offset: 0x00053646
		internal string OboCacheKey { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0005544F File Offset: 0x0005364F
		internal string KeyId { get; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00055457 File Offset: 0x00053657
		internal string TokenType { get; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0005545F File Offset: 0x0005365F
		internal HashSet<string> ScopeSet { get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00055467 File Offset: 0x00053667
		internal string ScopeString { get; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0005546F File Offset: 0x0005366F
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x00055477 File Offset: 0x00053677
		internal DateTimeOffset ExpiresOn { get; private set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00055480 File Offset: 0x00053680
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x00055488 File Offset: 0x00053688
		internal DateTimeOffset ExtendedExpiresOn { get; private set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00055491 File Offset: 0x00053691
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00055499 File Offset: 0x00053699
		internal DateTimeOffset? RefreshOn { get; private set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000554A2 File Offset: 0x000536A2
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x000554AA File Offset: 0x000536AA
		internal DateTimeOffset CachedAt { get; private set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x000554B3 File Offset: 0x000536B3
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x000554BB File Offset: 0x000536BB
		public bool IsExtendedLifeTimeToken { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000554C4 File Offset: 0x000536C4
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x000554CC File Offset: 0x000536CC
		internal string CacheKey { get; private set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000554D5 File Offset: 0x000536D5
		public IiOSKey iOSCacheKey
		{
			get
			{
				return this.iOSCacheKeyLazy.Value;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000554E2 File Offset: 0x000536E2
		internal static MsalAccessTokenCacheItem FromJsonString(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return null;
			}
			return MsalAccessTokenCacheItem.FromJObject(JsonHelper.ParseIntoJsonObject(json));
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000554FC File Offset: 0x000536FC
		internal static MsalAccessTokenCacheItem FromJObject(JObject j)
		{
			long num = JsonHelper.ExtractParsedIntOrZero(j, "cached_at");
			long num2 = JsonHelper.ExtractParsedIntOrZero(j, "expires_on");
			long num3 = JsonHelper.ExtractParsedIntOrZero(j, "refresh_on");
			long num4 = JsonHelper.ExtractParsedIntOrZero(j, "ext_expires_on");
			long num5 = JsonHelper.ExtractParsedIntOrZero(j, "extended_expires_on");
			if (num5 == 0L && num4 > 0L)
			{
				num5 = num4;
			}
			string text = JsonHelper.ExtractExistingOrEmptyString(j, "realm");
			string text2 = JsonHelper.ExtractExistingOrDefault<string>(j, "user_assertion_hash");
			string text3 = JsonHelper.ExtractExistingOrDefault<string>(j, "kid");
			string text4 = JsonHelper.ExtractExistingOrDefault<string>(j, "token_type") ?? "Bearer";
			string text5 = JsonHelper.ExtractExistingOrEmptyString(j, "target");
			DateTimeOffset dateTimeOffset = DateTimeHelpers.UnixTimestampToDateTime((double)num2);
			DateTimeOffset dateTimeOffset2 = DateTimeHelpers.UnixTimestampToDateTime((double)num5);
			DateTimeOffset? dateTimeOffset3 = DateTimeHelpers.UnixTimestampToDateTimeOrNull((double)num3);
			MsalAccessTokenCacheItem msalAccessTokenCacheItem = new MsalAccessTokenCacheItem(text5, DateTimeHelpers.UnixTimestampToDateTime((double)num), dateTimeOffset, dateTimeOffset2, dateTimeOffset3, text, text3, text4);
			msalAccessTokenCacheItem.OboCacheKey = text2;
			msalAccessTokenCacheItem.PopulateFieldsFromJObject(j);
			msalAccessTokenCacheItem.InitCacheKey();
			return msalAccessTokenCacheItem;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000555E4 File Offset: 0x000537E4
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			string text = DateTimeHelpers.DateTimeToUnixTimestamp(this.ExtendedExpiresOn);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "realm", this.TenantId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "target", this.ScopeString);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "user_assertion_hash", this.OboCacheKey);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "cached_at", DateTimeHelpers.DateTimeToUnixTimestamp(this.CachedAt));
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "expires_on", DateTimeHelpers.DateTimeToUnixTimestamp(this.ExpiresOn));
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "extended_expires_on", text);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "kid", this.KeyId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNullOrDefault(jobject, "token_type", this.TokenType, "Bearer");
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "refresh_on", (this.RefreshOn != null) ? DateTimeHelpers.DateTimeToUnixTimestamp(this.RefreshOn.Value) : null);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "ext_expires_on", text);
			return jobject;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00055704 File Offset: 0x00053904
		internal string ToJsonString()
		{
			return this.ToJObject().ToString();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00055711 File Offset: 0x00053911
		internal MsalIdTokenCacheItem GetIdTokenItem()
		{
			return new MsalIdTokenCacheItem(base.Environment, base.ClientId, base.Secret, base.RawClientInfo, base.HomeAccountId, this.TenantId);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0005573C File Offset: 0x0005393C
		internal bool IsExpiredWithBuffer()
		{
			return this.ExpiresOn < DateTime.UtcNow + Constants.AccessTokenExpirationBuffer;
		}

		// Token: 0x04000BC5 RID: 3013
		private string[] _extraKeyParts;

		// Token: 0x04000BC6 RID: 3014
		private string _credentialDescriptor;

		// Token: 0x04000BD3 RID: 3027
		private Lazy<IiOSKey> iOSCacheKeyLazy;
	}
}
