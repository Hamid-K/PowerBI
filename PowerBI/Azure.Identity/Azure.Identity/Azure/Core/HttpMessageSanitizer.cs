using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azure.Core
{
	// Token: 0x02000016 RID: 22
	internal class HttpMessageSanitizer
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002B54 File Offset: 0x00000D54
		public HttpMessageSanitizer(string[] allowedQueryParameters, string[] allowedHeaders, string redactedPlaceholder = "REDACTED")
		{
			this._logAllHeaders = allowedHeaders.Contains("*");
			this._logFullQueries = allowedQueryParameters.Contains("*");
			this._allowedQueryParameters = allowedQueryParameters;
			this._redactedPlaceholder = redactedPlaceholder;
			this._allowedHeaders = new HashSet<string>(allowedHeaders, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public string SanitizeHeader(string name, string value)
		{
			if (this._logAllHeaders || this._allowedHeaders.Contains(name))
			{
				return value;
			}
			return this._redactedPlaceholder;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public string SanitizeUrl(string url)
		{
			if (this._logFullQueries)
			{
				return url;
			}
			int num = url.IndexOf('?');
			if (num == -1)
			{
				return url;
			}
			StringBuilder stringBuilder = new StringBuilder(url.Length);
			stringBuilder.Append(url, 0, num);
			string text = url.Substring(num);
			int num2 = 1;
			stringBuilder.Append('?');
			do
			{
				int num3 = text.IndexOf('&', num2);
				int num4 = text.IndexOf('=', num2);
				bool flag = false;
				if ((num3 == -1 && num4 == -1) || (num3 != -1 && (num4 == -1 || num4 > num3)))
				{
					num4 = num3;
					flag = true;
				}
				if (num4 == -1)
				{
					num4 = text.Length;
				}
				if (num3 == -1)
				{
					num3 = text.Length;
				}
				else
				{
					num3++;
				}
				ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(text, num2, num4 - num2);
				bool flag2 = false;
				foreach (string text2 in this._allowedQueryParameters)
				{
					if (MemoryExtensions.Equals(readOnlySpan, MemoryExtensions.AsSpan(text2), StringComparison.OrdinalIgnoreCase))
					{
						flag2 = true;
						break;
					}
				}
				int num5 = num3 - num2;
				int num6 = num4 - num2;
				if (flag2)
				{
					stringBuilder.Append(text, num2, num5);
				}
				else if (flag)
				{
					stringBuilder.Append(text, num2, num5);
				}
				else
				{
					stringBuilder.Append(text, num2, num6);
					stringBuilder.Append('=');
					stringBuilder.Append(this._redactedPlaceholder);
					if (text[num3 - 1] == '&')
					{
						stringBuilder.Append('&');
					}
				}
				num2 += num5;
			}
			while (num2 < text.Length);
			return stringBuilder.ToString();
		}

		// Token: 0x04000032 RID: 50
		private const string LogAllValue = "*";

		// Token: 0x04000033 RID: 51
		private readonly bool _logAllHeaders;

		// Token: 0x04000034 RID: 52
		private readonly bool _logFullQueries;

		// Token: 0x04000035 RID: 53
		private readonly string[] _allowedQueryParameters;

		// Token: 0x04000036 RID: 54
		private readonly string _redactedPlaceholder;

		// Token: 0x04000037 RID: 55
		private readonly HashSet<string> _allowedHeaders;

		// Token: 0x04000038 RID: 56
		internal static HttpMessageSanitizer Default = new HttpMessageSanitizer(Array.Empty<string>(), Array.Empty<string>(), "REDACTED");
	}
}
