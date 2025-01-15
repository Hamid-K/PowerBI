using System;
using Microsoft.Identity.Client.Cache.Keys;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002BD RID: 701
	internal class MsalRefreshTokenCacheItem : MsalCredentialCacheItemBase
	{
		// Token: 0x06001A68 RID: 6760 RVA: 0x00056321 File Offset: 0x00054521
		internal MsalRefreshTokenCacheItem()
		{
			base.CredentialType = "RefreshToken";
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00056334 File Offset: 0x00054534
		internal MsalRefreshTokenCacheItem(string preferredCacheEnv, string clientId, MsalTokenResponse response, string homeAccountId)
			: this(preferredCacheEnv, clientId, response.RefreshToken, response.ClientInfo, response.FamilyId, homeAccountId)
		{
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00056352 File Offset: 0x00054552
		internal MsalRefreshTokenCacheItem(string preferredCacheEnv, string clientId, string secret, string rawClientInfo, string familyId, string homeAccountId)
			: this()
		{
			base.ClientId = clientId;
			base.Environment = preferredCacheEnv;
			base.Secret = secret;
			base.RawClientInfo = rawClientInfo;
			this.FamilyId = familyId;
			base.HomeAccountId = homeAccountId;
			this.InitCacheKey();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00056390 File Offset: 0x00054590
		internal void InitCacheKey()
		{
			string text;
			if (!string.IsNullOrWhiteSpace(this.FamilyId))
			{
				char c = '-';
				text = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", new object[] { base.HomeAccountId, c, base.Environment, c, "RefreshToken", c, this.FamilyId, c, c }).ToLowerInvariant();
			}
			else
			{
				text = MsalCacheKeys.GetCredentialKey(base.HomeAccountId, base.Environment, "RefreshToken", base.ClientId, null, null, Array.Empty<string>());
			}
			this.CacheKey = text;
			this.iOSCacheKeyLazy = new Lazy<IiOSKey>(() => this.InitiOSKey());
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00056458 File Offset: 0x00054658
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
			return MsalCacheKeys.GetCredentialKey(text, base.Environment, "RefreshToken", base.ClientId, null, null, Array.Empty<string>());
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000564A8 File Offset: 0x000546A8
		private IiOSKey InitiOSKey()
		{
			string text = this.GetiOSService();
			string text2 = this.GetiOSGeneric();
			string text3 = MsalCacheKeys.GetiOSAccountKey(base.HomeAccountId, base.Environment);
			int num = 2002;
			return new IosKey(text3, text, text2, num);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000564E8 File Offset: 0x000546E8
		private string GetiOSGeneric()
		{
			if (!string.IsNullOrWhiteSpace(this.FamilyId))
			{
				return string.Format("{0}{1}{2}{3}", new object[] { "RefreshToken", '-', this.FamilyId, '-' }).ToLowerInvariant();
			}
			return MsalCacheKeys.GetiOSGenericKey("RefreshToken", base.ClientId, null);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00056550 File Offset: 0x00054750
		public string GetiOSService()
		{
			if (!string.IsNullOrWhiteSpace(this.FamilyId))
			{
				return string.Format("{0}{1}{2}{3}{4}", new object[] { "RefreshToken", '-', this.FamilyId, '-', '-' }).ToLowerInvariant();
			}
			return MsalCacheKeys.GetiOSServiceKey("RefreshToken", base.ClientId, null, null, Array.Empty<string>());
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x000565C6 File Offset: 0x000547C6
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x000565CE File Offset: 0x000547CE
		public string FamilyId { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000565D7 File Offset: 0x000547D7
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x000565DF File Offset: 0x000547DF
		internal string OboCacheKey { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x000565E8 File Offset: 0x000547E8
		public bool IsFRT
		{
			get
			{
				return !string.IsNullOrEmpty(this.FamilyId);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000565F8 File Offset: 0x000547F8
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00056600 File Offset: 0x00054800
		public string CacheKey { get; private set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00056609 File Offset: 0x00054809
		public IiOSKey iOSCacheKey
		{
			get
			{
				return this.iOSCacheKeyLazy.Value;
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00056616 File Offset: 0x00054816
		internal static MsalRefreshTokenCacheItem FromJsonString(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return null;
			}
			return MsalRefreshTokenCacheItem.FromJObject(JsonHelper.ParseIntoJsonObject(json));
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0005662D File Offset: 0x0005482D
		internal static MsalRefreshTokenCacheItem FromJObject(JObject j)
		{
			MsalRefreshTokenCacheItem msalRefreshTokenCacheItem = new MsalRefreshTokenCacheItem();
			msalRefreshTokenCacheItem.FamilyId = JsonHelper.ExtractExistingOrEmptyString(j, "family_id");
			msalRefreshTokenCacheItem.OboCacheKey = JsonHelper.ExtractExistingOrEmptyString(j, "user_assertion_hash");
			msalRefreshTokenCacheItem.PopulateFieldsFromJObject(j);
			msalRefreshTokenCacheItem.InitCacheKey();
			return msalRefreshTokenCacheItem;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00056663 File Offset: 0x00054863
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "family_id", this.FamilyId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "user_assertion_hash", this.OboCacheKey);
			return jobject;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00056697 File Offset: 0x00054897
		internal string ToJsonString()
		{
			return this.ToJObject().ToString();
		}

		// Token: 0x04000BF1 RID: 3057
		private Lazy<IiOSKey> iOSCacheKeyLazy;
	}
}
