using System;
using System.Diagnostics;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002B9 RID: 697
	[DebuggerDisplay("env: {Environment} accountId: {HomeAccountId}")]
	internal abstract class MsalCacheItemBase : MsalItemWithAdditionalFields
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00055E90 File Offset: 0x00054090
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x00055E98 File Offset: 0x00054098
		internal string HomeAccountId { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00055EA1 File Offset: 0x000540A1
		// (set) Token: 0x06001A40 RID: 6720 RVA: 0x00055EA9 File Offset: 0x000540A9
		internal string Environment { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00055EB2 File Offset: 0x000540B2
		// (set) Token: 0x06001A42 RID: 6722 RVA: 0x00055EBA File Offset: 0x000540BA
		internal string RawClientInfo { get; set; }

		// Token: 0x06001A43 RID: 6723 RVA: 0x00055EC3 File Offset: 0x000540C3
		internal override void PopulateFieldsFromJObject(JObject j)
		{
			this.HomeAccountId = JsonHelper.ExtractExistingOrEmptyString(j, "home_account_id");
			this.Environment = JsonHelper.ExtractExistingOrEmptyString(j, "environment");
			this.RawClientInfo = JsonHelper.ExtractExistingOrEmptyString(j, "client_info");
			base.PopulateFieldsFromJObject(j);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00055F00 File Offset: 0x00054100
		internal override JObject ToJObject()
		{
			JObject jobject = base.ToJObject();
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "home_account_id", this.HomeAccountId);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "environment", this.Environment);
			MsalItemWithAdditionalFields.SetItemIfValueNotNull(jobject, "client_info", this.RawClientInfo);
			return jobject;
		}
	}
}
