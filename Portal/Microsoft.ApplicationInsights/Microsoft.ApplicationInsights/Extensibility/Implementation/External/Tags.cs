using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.External
{
	// Token: 0x020000C3 RID: 195
	internal static class Tags
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x000176D8 File Offset: 0x000158D8
		internal static void SetStringValueOrRemove(this IDictionary<string, string> tags, string tagKey, string tagValue)
		{
			tags.SetTagValueOrRemove(tagKey, tagValue);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000176E2 File Offset: 0x000158E2
		internal static void SetTagValueOrRemove<T>(this IDictionary<string, string> tags, string tagKey, T tagValue)
		{
			tags.SetTagValueOrRemove(tagKey, Convert.ToString(tagValue, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000176FB File Offset: 0x000158FB
		internal static void CopyTagValue(bool? sourceValue, ref bool? targetValue)
		{
			if (sourceValue != null && targetValue == null)
			{
				targetValue = sourceValue;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00017718 File Offset: 0x00015918
		internal static string GetTagValueOrNull(this IDictionary<string, string> tags, string tagKey)
		{
			string text;
			if (tags.TryGetValue(tagKey, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00017734 File Offset: 0x00015934
		internal static void UpdateTagValue(this IDictionary<string, string> tags, string tagKey, string tagValue)
		{
			if (!string.IsNullOrEmpty(tagValue))
			{
				int num;
				if (Property.TagSizeLimits.TryGetValue(tagKey, out num) && tagValue.Length > num)
				{
					tagValue = Property.TrimAndTruncate(tagValue, num);
				}
				tags.Add(tagKey, tagValue);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00017772 File Offset: 0x00015972
		internal static void CopyTagValue(string sourceValue, ref string targetValue)
		{
			if (!string.IsNullOrEmpty(sourceValue) && string.IsNullOrEmpty(targetValue))
			{
				targetValue = sourceValue;
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00017788 File Offset: 0x00015988
		internal static void UpdateTagValue(this IDictionary<string, string> tags, string tagKey, bool? tagValue)
		{
			if (tagValue != null)
			{
				string text = tagValue.Value.ToString(CultureInfo.InvariantCulture);
				tags.Add(tagKey, text);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000177BB File Offset: 0x000159BB
		private static void SetTagValueOrRemove(this IDictionary<string, string> tags, string tagKey, string tagValue)
		{
			if (string.IsNullOrEmpty(tagValue))
			{
				tags.Remove(tagKey);
				return;
			}
			tags[tagKey] = tagValue;
		}
	}
}
