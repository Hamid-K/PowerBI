using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x0200002C RID: 44
	internal static class HttpHelper
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00006CFB File Offset: 0x00004EFB
		public static void ApplyHeaderToRequest(string header, string value, HttpWebRequest request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006D08 File Offset: 0x00004F08
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpWebRequest request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006D60 File Offset: 0x00004F60
		public static bool TryGetHeaderValueFromResponse(HttpWebResponse response, string header, out string value)
		{
			if (response != null)
			{
				return HttpHelper.TryGetHeaderValueFromResponseImpl(response, header, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006D72 File Offset: 0x00004F72
		private static void ApplyHeaderToRequestImpl(string header, string value, HttpWebRequest request)
		{
			if (header == "Content-Type")
			{
				request.ContentType = value;
				return;
			}
			if (!(header == "User-Agent"))
			{
				request.Headers.Add(header, value);
				return;
			}
			request.UserAgent = value;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006DB0 File Offset: 0x00004FB0
		private static bool TryGetHeaderValueFromResponseImpl(HttpWebResponse response, string header, out string value)
		{
			if (!(header == "Content-Encoding"))
			{
				if (!(header == "Content-Type"))
				{
					value = response.Headers[header];
				}
				else
				{
					value = response.ContentType;
				}
			}
			else
			{
				value = response.ContentEncoding;
			}
			return !string.IsNullOrEmpty(value);
		}

		// Token: 0x02000071 RID: 113
		public static class Headers
		{
			// Token: 0x0400021E RID: 542
			public const string Authorization = "Authorization";

			// Token: 0x0400021F RID: 543
			public const string ContentEncoding = "Content-Encoding";

			// Token: 0x04000220 RID: 544
			public const string ContentType = "Content-Type";

			// Token: 0x04000221 RID: 545
			public const string KeepAlive = "Keep-Alive";

			// Token: 0x04000222 RID: 546
			public const string Location = "Location";

			// Token: 0x04000223 RID: 547
			public const string UserAgent = "User-Agent";
		}

		// Token: 0x02000072 RID: 114
		public static class HeaderValues
		{
			// Token: 0x04000224 RID: 548
			public const string UserAgent = "AzureClient";

			// Token: 0x04000225 RID: 549
			public const string JsonContentType = "application/json";
		}
	}
}
