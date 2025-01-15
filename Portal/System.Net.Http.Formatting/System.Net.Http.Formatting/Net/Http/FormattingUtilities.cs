using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace System.Net.Http
{
	// Token: 0x02000016 RID: 22
	internal static class FormattingUtilities
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003066 File Offset: 0x00001266
		public static bool IsJTokenType(Type type)
		{
			return typeof(JToken).IsAssignableFrom(type);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003078 File Offset: 0x00001278
		public static HttpContentHeaders CreateEmptyContentHeaders()
		{
			HttpContent httpContent = null;
			HttpContentHeaders httpContentHeaders = null;
			try
			{
				httpContent = new StringContent(string.Empty);
				httpContentHeaders = httpContent.Headers;
				httpContentHeaders.Clear();
			}
			finally
			{
				if (httpContent != null)
				{
					httpContent.Dispose();
				}
			}
			return httpContentHeaders;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000030C0 File Offset: 0x000012C0
		public static XmlDictionaryReaderQuotas CreateDefaultReaderQuotas()
		{
			return new XmlDictionaryReaderQuotas
			{
				MaxArrayLength = int.MaxValue,
				MaxBytesPerRead = int.MaxValue,
				MaxDepth = 256,
				MaxNameTableCharCount = int.MaxValue,
				MaxStringContentLength = int.MaxValue
			};
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003100 File Offset: 0x00001300
		public static string UnquoteToken(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
			{
				return token;
			}
			if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
			{
				return token.Substring(1, token.Length - 2);
			}
			return token;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003150 File Offset: 0x00001350
		public static bool ValidateHeaderToken(string token)
		{
			if (token == null)
			{
				return false;
			}
			foreach (char c in token)
			{
				if (c < '!' || c > '~' || "()<>@,;:\\\"/[]?={}".IndexOf(c) != -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003198 File Offset: 0x00001398
		public static string DateToString(DateTimeOffset dateTime)
		{
			return dateTime.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031BE File Offset: 0x000013BE
		public static bool TryParseDate(string input, out DateTimeOffset result)
		{
			return DateTimeOffset.TryParseExact(input, FormattingUtilities.dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeUniversal, out result);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031D3 File Offset: 0x000013D3
		public static bool TryParseInt32(string value, out int result)
		{
			return int.TryParse(value, NumberStyles.None, NumberFormatInfo.InvariantInfo, out result);
		}

		// Token: 0x04000020 RID: 32
		private static readonly string[] dateFormats = new string[]
		{
			"ddd, d MMM yyyy H:m:s 'GMT'", "ddd, d MMM yyyy H:m:s", "d MMM yyyy H:m:s 'GMT'", "d MMM yyyy H:m:s", "ddd, d MMM yy H:m:s 'GMT'", "ddd, d MMM yy H:m:s", "d MMM yy H:m:s 'GMT'", "d MMM yy H:m:s", "dddd, d'-'MMM'-'yy H:m:s 'GMT'", "dddd, d'-'MMM'-'yy H:m:s",
			"ddd, d'-'MMM'-'yyyy H:m:s 'GMT'", "ddd MMM d H:m:s yyyy", "ddd, d MMM yyyy H:m:s zzz", "ddd, d MMM yyyy H:m:s", "d MMM yyyy H:m:s zzz", "d MMM yyyy H:m:s"
		};

		// Token: 0x04000021 RID: 33
		private const string NonTokenChars = "()<>@,;:\\\"/[]?={}";

		// Token: 0x04000022 RID: 34
		public const double Match = 1.0;

		// Token: 0x04000023 RID: 35
		public const double NoMatch = 0.0;

		// Token: 0x04000024 RID: 36
		public const int DefaultMaxDepth = 256;

		// Token: 0x04000025 RID: 37
		public const int DefaultMinDepth = 1;

		// Token: 0x04000026 RID: 38
		public const string HttpRequestedWithHeader = "x-requested-with";

		// Token: 0x04000027 RID: 39
		public const string HttpRequestedWithHeaderValue = "XMLHttpRequest";

		// Token: 0x04000028 RID: 40
		public const string HttpHostHeader = "Host";

		// Token: 0x04000029 RID: 41
		public const string HttpVersionToken = "HTTP";

		// Token: 0x0400002A RID: 42
		public static readonly Type HttpRequestMessageType = typeof(HttpRequestMessage);

		// Token: 0x0400002B RID: 43
		public static readonly Type HttpResponseMessageType = typeof(HttpResponseMessage);

		// Token: 0x0400002C RID: 44
		public static readonly Type HttpContentType = typeof(HttpContent);

		// Token: 0x0400002D RID: 45
		public static readonly Type DelegatingEnumerableGenericType = typeof(DelegatingEnumerable<>);

		// Token: 0x0400002E RID: 46
		public static readonly Type EnumerableInterfaceGenericType = typeof(IEnumerable<>);

		// Token: 0x0400002F RID: 47
		public static readonly Type QueryableInterfaceGenericType = typeof(IQueryable<>);

		// Token: 0x04000030 RID: 48
		public static readonly XsdDataContractExporter XsdDataContractExporter = new XsdDataContractExporter();
	}
}
