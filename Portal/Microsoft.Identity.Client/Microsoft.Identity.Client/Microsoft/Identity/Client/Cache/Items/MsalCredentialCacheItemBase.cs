using System;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002BA RID: 698
	internal class MsalCredentialCacheItemBase : MsalCacheItemBase
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00055F5D File Offset: 0x0005415D
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x00055F65 File Offset: 0x00054165
		internal string CredentialType { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00055F6E File Offset: 0x0005416E
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x00055F76 File Offset: 0x00054176
		public string ClientId { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00055F7F File Offset: 0x0005417F
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00055F87 File Offset: 0x00054187
		public string Secret { get; set; }

		// Token: 0x06001A4C RID: 6732 RVA: 0x00055F90 File Offset: 0x00054190
		internal override void PopulateFieldsFromJObject(JObject j)
		{
			this.CredentialType = JsonHelper.ExtractExistingOrEmptyString(j, "credential_type");
			this.ClientId = JsonHelper.ExtractExistingOrEmptyString(j, "client_id");
			this.Secret = JsonHelper.ExtractExistingOrEmptyString(j, "secret");
			base.PopulateFieldsFromJObject(j);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00055FCC File Offset: 0x000541CC
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "client_id", this.ClientId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "secret", this.Secret);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "credential_type", this.CredentialType);
			return jobject;
		}
	}
}
