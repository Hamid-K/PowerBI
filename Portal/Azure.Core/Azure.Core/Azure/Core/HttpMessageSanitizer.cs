using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core
{
	// Token: 0x02000073 RID: 115
	[NullableContext(1)]
	[Nullable(0)]
	internal class HttpMessageSanitizer
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public HttpMessageSanitizer(string[] allowedQueryParameters, string[] allowedHeaders, string redactedPlaceholder = "REDACTED")
		{
			this._logAllHeaders = allowedHeaders.Contains("*");
			this._logFullQueries = allowedQueryParameters.Contains("*");
			this._allowedQueryParameters = allowedQueryParameters;
			this._redactedPlaceholder = redactedPlaceholder;
			this._allowedHeaders = new HashSet<string>(allowedHeaders, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B900 File Offset: 0x00009B00
		public string SanitizeHeader(string name, string value)
		{
			if (this._logAllHeaders || this._allowedHeaders.Contains(name))
			{
				return value;
			}
			return this._redactedPlaceholder;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B920 File Offset: 0x00009B20
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

		// Token: 0x04000195 RID: 405
		private const string LogAllValue = "*";

		// Token: 0x04000196 RID: 406
		private readonly bool _logAllHeaders;

		// Token: 0x04000197 RID: 407
		private readonly bool _logFullQueries;

		// Token: 0x04000198 RID: 408
		private readonly string[] _allowedQueryParameters;

		// Token: 0x04000199 RID: 409
		private readonly string _redactedPlaceholder;

		// Token: 0x0400019A RID: 410
		private readonly HashSet<string> _allowedHeaders;

		// Token: 0x0400019B RID: 411
		internal static HttpMessageSanitizer Default = new HttpMessageSanitizer(Array.Empty<string>(), Array.Empty<string>(), "REDACTED");
	}
}
