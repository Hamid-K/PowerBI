using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Keys;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002B8 RID: 696
	internal class MsalAppMetadataCacheItem : MsalItemWithAdditionalFields, IEquatable<MsalAppMetadataCacheItem>
	{
		// Token: 0x06001A2D RID: 6701 RVA: 0x00055BBF File Offset: 0x00053DBF
		public MsalAppMetadataCacheItem(string clientId, string preferredCacheEnv, string familyId)
		{
			this.ClientId = clientId;
			this.Environment = preferredCacheEnv;
			this.FamilyId = familyId;
			this.InitCacheKey();
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00055BE4 File Offset: 0x00053DE4
		private void InitCacheKey()
		{
			this.CacheKey = (string.Format("{0}{1}", "appmetadata", '-') + string.Format("{0}{1}{2}", this.Environment, '-', this.ClientId)).ToLowerInvariant();
			this.iOSCacheKeyLazy = new Lazy<IiOSKey>(new Func<IiOSKey>(this.InitiOSKey));
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00055C4C File Offset: 0x00053E4C
		private IiOSKey InitiOSKey()
		{
			string text = string.Format("{0}{1}{2}", "AppMetadata", '-', this.ClientId).ToLowerInvariant();
			string text2 = "1";
			string text3 = (this.Environment ?? "").ToLowerInvariant();
			int num = 3001;
			return new IosKey(text3, text, text2, num);
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00055CA8 File Offset: 0x00053EA8
		public string ClientId { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00055CB0 File Offset: 0x00053EB0
		public string Environment { get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00055CB8 File Offset: 0x00053EB8
		public string FamilyId { get; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00055CC0 File Offset: 0x00053EC0
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x00055CC8 File Offset: 0x00053EC8
		public string CacheKey { get; private set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00055CD1 File Offset: 0x00053ED1
		public IiOSKey iOSCacheKey
		{
			get
			{
				return this.iOSCacheKeyLazy.Value;
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00055CDE File Offset: 0x00053EDE
		internal static MsalAppMetadataCacheItem FromJsonString(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return null;
			}
			return MsalAppMetadataCacheItem.FromJObject(JsonHelper.ParseIntoJsonObject(json));
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00055CF8 File Offset: 0x00053EF8
		internal static MsalAppMetadataCacheItem FromJObject(JObject j)
		{
			string text = JsonHelper.ExtractExistingOrEmptyString(j, "client_id");
			string text2 = JsonHelper.ExtractExistingOrEmptyString(j, "environment");
			string text3 = JsonHelper.ExtractExistingOrEmptyString(j, "family_id");
			MsalAppMetadataCacheItem msalAppMetadataCacheItem = new MsalAppMetadataCacheItem(text, text2, text3);
			msalAppMetadataCacheItem.PopulateFieldsFromJObject(j);
			msalAppMetadataCacheItem.InitCacheKey();
			return msalAppMetadataCacheItem;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00055D3C File Offset: 0x00053F3C
		internal string ToJsonString()
		{
			return this.ToJObject().ToString();
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00055D4C File Offset: 0x00053F4C
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "environment", this.Environment);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "client_id", this.ClientId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "family_id", this.FamilyId);
			return jobject;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00055DA4 File Offset: 0x00053FA4
		public override int GetHashCode()
		{
			return (((-1793347351 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.ClientId)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Environment)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.FamilyId)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(base.AdditionalFieldsJson);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00055E14 File Offset: 0x00054014
		public bool Equals(MsalAppMetadataCacheItem other)
		{
			return this.ClientId == other.ClientId && this.Environment == other.Environment && this.FamilyId == other.FamilyId && base.AdditionalFieldsJson == other.AdditionalFieldsJson;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00055E70 File Offset: 0x00054070
		public override bool Equals(object obj)
		{
			MsalAppMetadataCacheItem msalAppMetadataCacheItem = obj as MsalAppMetadataCacheItem;
			return msalAppMetadataCacheItem != null && this.Equals(msalAppMetadataCacheItem);
		}

		// Token: 0x04000BE2 RID: 3042
		private Lazy<IiOSKey> iOSCacheKeyLazy;
	}
}
