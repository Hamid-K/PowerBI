using System;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Cache.Items
{
	// Token: 0x020002BC RID: 700
	internal abstract class MsalItemWithAdditionalFields
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0005623A File Offset: 0x0005443A
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00056242 File Offset: 0x00054442
		internal string AdditionalFieldsJson { get; set; } = "{}";

		// Token: 0x06001A62 RID: 6754 RVA: 0x0005624B File Offset: 0x0005444B
		internal virtual void PopulateFieldsFromJObject(JObject j)
		{
			this.AdditionalFieldsJson = j.ToString();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00056259 File Offset: 0x00054459
		internal virtual JObject ToJObject()
		{
			if (!string.IsNullOrWhiteSpace(this.AdditionalFieldsJson))
			{
				return JsonHelper.ParseIntoJsonObject(this.AdditionalFieldsJson);
			}
			return new JObject();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00056279 File Offset: 0x00054479
		internal static void SetItemIfValueNotNull(JObject json, string key, JToken value)
		{
			MsalItemWithAdditionalFields.SetValueIfFilterMatches(json, key, value, (string strVal) => !string.IsNullOrEmpty(strVal));
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000562A4 File Offset: 0x000544A4
		internal static void SetItemIfValueNotNullOrDefault(JObject json, string key, JToken value, string defaultValue)
		{
			MsalItemWithAdditionalFields.SetValueIfFilterMatches(json, key, value, (string strVal) => !string.IsNullOrEmpty(strVal) && !strVal.Equals(defaultValue, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000562D4 File Offset: 0x000544D4
		private static void SetValueIfFilterMatches(JObject json, string key, JToken value, Func<string, bool> filter)
		{
			bool flag = true;
			object obj = value.ToObject<object>();
			if (obj == null)
			{
				flag = false;
			}
			else
			{
				string text = obj as string;
				if (text != null)
				{
					flag = filter(text);
				}
			}
			if (flag)
			{
				json[key] = value;
			}
		}
	}
}
