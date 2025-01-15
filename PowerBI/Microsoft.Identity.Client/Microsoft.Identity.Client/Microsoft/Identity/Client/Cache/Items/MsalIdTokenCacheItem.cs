using System;
using Microsoft.Identity.Client.Cache.Keys;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002BB RID: 699
	internal class MsalIdTokenCacheItem : MsalCredentialCacheItemBase
	{
		// Token: 0x06001A4F RID: 6735 RVA: 0x00056029 File Offset: 0x00054229
		internal MsalIdTokenCacheItem()
		{
			base.CredentialType = "IdToken";
			this.idTokenLazy = new Lazy<IdToken>(() => IdToken.Parse(base.Secret));
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00056053 File Offset: 0x00054253
		internal MsalIdTokenCacheItem(string preferredCacheEnv, string clientId, MsalTokenResponse response, string tenantId, string homeAccountId)
			: this(preferredCacheEnv, clientId, response.IdToken, response.ClientInfo, homeAccountId, tenantId)
		{
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0005606D File Offset: 0x0005426D
		internal MsalIdTokenCacheItem(string preferredCacheEnv, string clientId, string secret, string rawClientInfo, string homeAccountId, string tenantId)
			: this()
		{
			base.Environment = preferredCacheEnv;
			this.TenantId = tenantId;
			base.ClientId = clientId;
			base.Secret = secret;
			base.RawClientInfo = rawClientInfo;
			base.HomeAccountId = homeAccountId;
			this.InitCacheKey();
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000560A8 File Offset: 0x000542A8
		internal void InitCacheKey()
		{
			this.CacheKey = MsalCacheKeys.GetCredentialKey(base.HomeAccountId, base.Environment, "IdToken", base.ClientId, this.TenantId, null, Array.Empty<string>());
			this.iOSCacheKeyLazy = new Lazy<IiOSKey>(new Func<IiOSKey>(this.InitiOSKey));
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000560FC File Offset: 0x000542FC
		private IiOSKey InitiOSKey()
		{
			string text = MsalCacheKeys.GetiOSAccountKey(base.HomeAccountId, base.Environment);
			string text2 = MsalCacheKeys.GetiOSGenericKey("IdToken", base.ClientId, this.TenantId);
			string text3 = MsalCacheKeys.GetiOSServiceKey("IdToken", base.ClientId, this.TenantId, null, Array.Empty<string>());
			int num = 2003;
			return new IosKey(text, text3, text2, num);
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00056161 File Offset: 0x00054361
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00056169 File Offset: 0x00054369
		internal string TenantId { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00056172 File Offset: 0x00054372
		internal IdToken IdToken
		{
			get
			{
				return this.idTokenLazy.Value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0005617F File Offset: 0x0005437F
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00056187 File Offset: 0x00054387
		public string CacheKey { get; private set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00056190 File Offset: 0x00054390
		public IiOSKey iOSCacheKey
		{
			get
			{
				return this.iOSCacheKeyLazy.Value;
			}
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0005619D File Offset: 0x0005439D
		internal static MsalIdTokenCacheItem FromJsonString(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return null;
			}
			return MsalIdTokenCacheItem.FromJObject(JsonHelper.ParseIntoJsonObject(json));
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000561B4 File Offset: 0x000543B4
		internal static MsalIdTokenCacheItem FromJObject(JObject j)
		{
			MsalIdTokenCacheItem msalIdTokenCacheItem = new MsalIdTokenCacheItem();
			msalIdTokenCacheItem.TenantId = JsonHelper.ExtractExistingOrEmptyString(j, "realm");
			msalIdTokenCacheItem.PopulateFieldsFromJObject(j);
			msalIdTokenCacheItem.InitCacheKey();
			return msalIdTokenCacheItem;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000561D9 File Offset: 0x000543D9
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "realm", this.TenantId);
			return jobject;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000561F7 File Offset: 0x000543F7
		internal string ToJsonString()
		{
			return this.ToJObject().ToString();
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00056204 File Offset: 0x00054404
		internal string GetUsername()
		{
			IdToken idToken = this.IdToken;
			string text;
			if ((text = ((idToken != null) ? idToken.PreferredUsername : null)) == null)
			{
				IdToken idToken2 = this.IdToken;
				if (idToken2 == null)
				{
					return null;
				}
				text = idToken2.Upn;
			}
			return text;
		}

		// Token: 0x04000BEA RID: 3050
		private readonly Lazy<IdToken> idTokenLazy;

		// Token: 0x04000BEC RID: 3052
		private Lazy<IiOSKey> iOSCacheKeyLazy;
	}
}
