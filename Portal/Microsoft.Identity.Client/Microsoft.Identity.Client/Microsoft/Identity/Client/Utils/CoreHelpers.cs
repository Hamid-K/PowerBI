using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C4 RID: 452
	internal static class CoreHelpers
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0004470F File Offset: 0x0004290F
		internal static string ByteArrayToString(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				return null;
			}
			return Encoding.UTF8.GetString(input, 0, input.Length);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00044729 File Offset: 0x00042929
		public static string UrlEncode(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return message;
			}
			message = Uri.EscapeDataString(message);
			message = message.Replace("%20", "+");
			return message;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00044750 File Offset: 0x00042950
		public static string UrlDecode(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return message;
			}
			message = message.Replace("+", "%20");
			message = Uri.UnescapeDataString(message);
			return message;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00044777 File Offset: 0x00042977
		public static void AddKeyValueString(StringBuilder messageBuilder, string key, string value)
		{
			CoreHelpers.AddKeyValueString(messageBuilder, key, value.ToCharArray());
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00044788 File Offset: 0x00042988
		public static string ToQueryParameter(this IDictionary<string, string> input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (input.Count > 0)
			{
				foreach (string text in input.Keys)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}&", text, CoreHelpers.UrlEncode(input[text]));
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004481C File Offset: 0x00042A1C
		public static Dictionary<string, string> ParseKeyValueList(string input, char delimiter, bool urlDecode, bool lowercaseKeys, RequestContext requestContext)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in CoreHelpers.SplitWithQuotes(input, delimiter))
			{
				IReadOnlyList<string> readOnlyList = CoreHelpers.SplitWithQuotes(text, '=');
				if (readOnlyList.Count == 2 && !string.IsNullOrWhiteSpace(readOnlyList[0]) && !string.IsNullOrWhiteSpace(readOnlyList[1]))
				{
					string text2 = readOnlyList[0];
					string text3 = readOnlyList[1];
					if (urlDecode)
					{
						text2 = CoreHelpers.UrlDecode(text2);
						text3 = CoreHelpers.UrlDecode(text3);
					}
					if (lowercaseKeys)
					{
						text2 = text2.Trim().ToLowerInvariant();
					}
					text3 = text3.Trim().Trim(new char[] { '"' }).Trim();
					if (dictionary.ContainsKey(text2) && requestContext != null)
					{
						requestContext.Logger.Warning(string.Format(CultureInfo.InvariantCulture, "Key/value pair list contains redundant key '{0}'.", text2));
					}
					dictionary[text2] = text3;
				}
			}
			return dictionary;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00044928 File Offset: 0x00042B28
		public static Dictionary<string, string> ParseKeyValueList(string input, char delimiter, bool urlDecode, RequestContext requestContext)
		{
			return CoreHelpers.ParseKeyValueList(input, delimiter, urlDecode, true, requestContext);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00044934 File Offset: 0x00042B34
		internal static IReadOnlyList<string> SplitWithQuotes(string input, char delimiter)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return Array.Empty<string>();
			}
			List<string> list = new List<string>();
			int num = 0;
			bool flag = false;
			string text;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == delimiter && !flag)
				{
					text = input.Substring(num, i - num);
					if (!string.IsNullOrWhiteSpace(text.Trim()))
					{
						list.Add(text);
					}
					num = i + 1;
				}
				else if (input[i] == '"')
				{
					flag = !flag;
				}
			}
			text = input.Substring(num);
			if (!string.IsNullOrWhiteSpace(text.Trim()))
			{
				list.Add(text);
			}
			return list;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x000449D4 File Offset: 0x00042BD4
		private static void AddKeyValueString(StringBuilder messageBuilder, string key, char[] value)
		{
			string text = ((messageBuilder.Length == 0) ? string.Empty : "&");
			messageBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}=", text, key);
			messageBuilder.Append(value);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00044A11 File Offset: 0x00042C11
		internal static string GetCcsClientInfoHint(string userObjectId, string userTenantID)
		{
			if (!string.IsNullOrEmpty(userObjectId) && !string.IsNullOrEmpty(userTenantID))
			{
				return "oid:" + userObjectId + "@" + userTenantID;
			}
			return string.Empty;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00044A3A File Offset: 0x00042C3A
		internal static string GetCcsUpnHint(string upn)
		{
			if (!string.IsNullOrEmpty(upn))
			{
				return "upn:" + upn;
			}
			return string.Empty;
		}
	}
}
