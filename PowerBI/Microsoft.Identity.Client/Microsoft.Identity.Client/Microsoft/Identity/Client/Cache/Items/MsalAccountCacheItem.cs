using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Identity.Client.Cache.Keys;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002B7 RID: 695
	[DebuggerDisplay("{PreferredUsername} {base.Environment}")]
	internal class MsalAccountCacheItem : MsalCacheItemBase
	{
		// Token: 0x06001A0F RID: 6671 RVA: 0x00055760 File Offset: 0x00053960
		internal MsalAccountCacheItem()
		{
			this.AuthorityType = CacheAuthorityType.MSSTS.ToString();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00055788 File Offset: 0x00053988
		internal MsalAccountCacheItem(string preferredCacheEnv, string clientInfo, string homeAccountId, IdToken idToken, string preferredUsername, string tenantId, IDictionary<string, string> wamAccountIds)
			: this()
		{
			this.Init(preferredCacheEnv, (idToken != null) ? idToken.GetUniqueId() : null, clientInfo, homeAccountId, (idToken != null) ? idToken.Name : null, preferredUsername, tenantId, (idToken != null) ? idToken.GivenName : null, (idToken != null) ? idToken.FamilyName : null, wamAccountIds);
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000557E4 File Offset: 0x000539E4
		internal MsalAccountCacheItem(string environment, string localAccountId, string rawClientInfo, string homeAccountId, string name, string preferredUsername, string tenantId, string givenName, string familyName, IDictionary<string, string> wamAccountIds)
			: this()
		{
			this.Init(environment, localAccountId, rawClientInfo, homeAccountId, name, preferredUsername, tenantId, givenName, familyName, wamAccountIds);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0005580E File Offset: 0x00053A0E
		internal MsalAccountCacheItem(string environment, string tenantId, string homeAccountId, string preferredUsername)
			: this()
		{
			base.Environment = environment;
			this.TenantId = tenantId;
			base.HomeAccountId = homeAccountId;
			this.PreferredUsername = preferredUsername;
			this.InitCacheKey();
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00055839 File Offset: 0x00053A39
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x00055841 File Offset: 0x00053A41
		internal string TenantId { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0005584A File Offset: 0x00053A4A
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x00055852 File Offset: 0x00053A52
		internal string PreferredUsername { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0005585B File Offset: 0x00053A5B
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x00055863 File Offset: 0x00053A63
		internal string Name { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0005586C File Offset: 0x00053A6C
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x00055874 File Offset: 0x00053A74
		internal string GivenName { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0005587D File Offset: 0x00053A7D
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x00055885 File Offset: 0x00053A85
		internal string FamilyName { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0005588E File Offset: 0x00053A8E
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x00055896 File Offset: 0x00053A96
		internal string LocalAccountId { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0005589F File Offset: 0x00053A9F
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x000558A7 File Offset: 0x00053AA7
		internal string AuthorityType { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000558B0 File Offset: 0x00053AB0
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x000558B8 File Offset: 0x00053AB8
		internal IDictionary<string, string> WamAccountIds { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x000558C1 File Offset: 0x00053AC1
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x000558C9 File Offset: 0x00053AC9
		public string CacheKey { get; private set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000558D2 File Offset: 0x00053AD2
		public IiOSKey iOSCacheKey
		{
			get
			{
				return this.iOSCacheKeyLazy.Value;
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000558E0 File Offset: 0x00053AE0
		private void Init(string environment, string localAccountId, string rawClientInfo, string homeAccountId, string name, string preferredUsername, string tenantId, string givenName, string familyName, IDictionary<string, string> wamAccountIds)
		{
			base.Environment = environment;
			this.PreferredUsername = preferredUsername;
			this.Name = name;
			this.TenantId = tenantId;
			this.LocalAccountId = localAccountId;
			base.RawClientInfo = rawClientInfo;
			this.GivenName = givenName;
			this.FamilyName = familyName;
			base.HomeAccountId = homeAccountId;
			this.WamAccountIds = wamAccountIds;
			this.InitCacheKey();
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00055940 File Offset: 0x00053B40
		internal void InitCacheKey()
		{
			this.CacheKey = string.Format("{0}{1}{2}{3}{4}", new object[] { base.HomeAccountId, '-', base.Environment, '-', this.TenantId });
			this.iOSCacheKeyLazy = new Lazy<IiOSKey>(new Func<IiOSKey>(this.InitiOSKey));
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000559AC File Offset: 0x00053BAC
		private IiOSKey InitiOSKey()
		{
			string text = MsalCacheKeys.GetiOSAccountKey(base.HomeAccountId, base.Environment);
			string text2 = (this.TenantId ?? "").ToLowerInvariant();
			string preferredUsername = this.PreferredUsername;
			string text3 = ((preferredUsername != null) ? preferredUsername.ToLowerInvariant() : null);
			int num = MsalCacheKeys.iOSAuthorityTypeToAttrType[CacheAuthorityType.MSSTS.ToString()];
			return new IosKey(text, text2, text3, num);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00055A19 File Offset: 0x00053C19
		internal static MsalAccountCacheItem FromJsonString(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return null;
			}
			return MsalAccountCacheItem.FromJObject(JsonHelper.ParseIntoJsonObject(json));
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00055A30 File Offset: 0x00053C30
		internal static MsalAccountCacheItem FromJObject(JObject j)
		{
			MsalAccountCacheItem msalAccountCacheItem = new MsalAccountCacheItem();
			msalAccountCacheItem.PreferredUsername = JsonHelper.ExtractExistingOrEmptyString(j, "username");
			msalAccountCacheItem.Name = JsonHelper.ExtractExistingOrEmptyString(j, "name");
			msalAccountCacheItem.GivenName = JsonHelper.ExtractExistingOrEmptyString(j, "given_name");
			msalAccountCacheItem.FamilyName = JsonHelper.ExtractExistingOrEmptyString(j, "family_name");
			msalAccountCacheItem.LocalAccountId = JsonHelper.ExtractExistingOrEmptyString(j, "local_account_id");
			msalAccountCacheItem.AuthorityType = JsonHelper.ExtractExistingOrEmptyString(j, "authority_type");
			msalAccountCacheItem.TenantId = JsonHelper.ExtractExistingOrEmptyString(j, "realm");
			msalAccountCacheItem.WamAccountIds = JsonHelper.ExtractInnerJsonAsDictionary(j, "wam_account_ids");
			msalAccountCacheItem.PopulateFieldsFromJObject(j);
			msalAccountCacheItem.InitCacheKey();
			return msalAccountCacheItem;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00055AD8 File Offset: 0x00053CD8
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "username", this.PreferredUsername);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "name", this.Name);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "given_name", this.GivenName);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "family_name", this.FamilyName);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "local_account_id", this.LocalAccountId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "authority_type", this.AuthorityType);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "realm", this.TenantId);
			if (this.WamAccountIds != null && this.WamAccountIds.Any<KeyValuePair<string, string>>())
			{
				jobject["wam_account_ids"] = JObject.FromObject(this.WamAccountIds);
			}
			return jobject;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00055BB2 File Offset: 0x00053DB2
		internal string ToJsonString()
		{
			return this.ToJObject().ToString();
		}

		// Token: 0x04000BDD RID: 3037
		private Lazy<IiOSKey> iOSCacheKeyLazy;
	}
}
