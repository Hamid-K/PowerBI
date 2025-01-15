using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Formatting;
using System.Text;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000007 RID: 7
	internal static class GetNextPageHelper
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002626 File Offset: 0x00000826
		internal static Uri GetNextPageLink(Uri requestUri, int pageSize, object instance = null, Func<object, string> objectToSkipTokenValue = null)
		{
			return GetNextPageHelper.GetNextPageLink(requestUri, new FormDataCollection(requestUri), pageSize, instance, objectToSkipTokenValue, CompatibilityOptions.None);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002638 File Offset: 0x00000838
		internal static Uri GetNextPageLink(Uri requestUri, IEnumerable<KeyValuePair<string, string>> queryParameters, int pageSize, object instance = null, Func<object, string> objectToSkipTokenValue = null, CompatibilityOptions options = CompatibilityOptions.None)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = pageSize;
			string text = ((objectToSkipTokenValue == null) ? null : objectToSkipTokenValue(instance));
			bool flag = string.IsNullOrWhiteSpace(text);
			foreach (KeyValuePair<string, string> keyValuePair in queryParameters)
			{
				string text2 = keyValuePair.Key.ToLowerInvariant();
				string text3 = keyValuePair.Value;
				if (text2 != null)
				{
					int num3;
					if (!(text2 == "$top"))
					{
						if (!(text2 == "$skip"))
						{
							if (text2 == "$skiptoken")
							{
								continue;
							}
						}
						else
						{
							int num2;
							if (flag && int.TryParse(text3, out num2))
							{
								num += num2;
								continue;
							}
							continue;
						}
					}
					else if (int.TryParse(text3, out num3))
					{
						if ((options & CompatibilityOptions.AllowNextLinkWithNonPositiveTopValue) == CompatibilityOptions.None && num3 <= pageSize)
						{
							return null;
						}
						text3 = (num3 - pageSize).ToString(CultureInfo.InvariantCulture);
					}
				}
				if (text2.Length > 0 && text2[0] == '$')
				{
					text2 = "$" + Uri.EscapeDataString(text2.Substring(1));
				}
				else
				{
					text2 = Uri.EscapeDataString(text2);
				}
				text3 = Uri.EscapeDataString(text3);
				stringBuilder.Append(text2);
				stringBuilder.Append('=');
				stringBuilder.Append(text3);
				stringBuilder.Append('&');
			}
			if (flag)
			{
				stringBuilder.AppendFormat("$skip={0}", num);
			}
			else
			{
				stringBuilder.AppendFormat("$skiptoken={0}", text);
			}
			return new UriBuilder(requestUri)
			{
				Query = stringBuilder.ToString()
			}.Uri;
		}
	}
}
