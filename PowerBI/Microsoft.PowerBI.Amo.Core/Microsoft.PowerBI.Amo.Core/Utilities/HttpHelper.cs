using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200013D RID: 317
	internal static class HttpHelper
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x0003A908 File Offset: 0x00038B08
		public static string GetHttpHeaderValueOrDefault(this HttpHeaders headers, string header, string @default = null)
		{
			string text;
			if (!headers.TryGetHttpHeaderValue(header, out text))
			{
				return @default;
			}
			return text;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003A924 File Offset: 0x00038B24
		public static bool TryGetHttpHeaderValue(this HttpHeaders headers, string header, out string value)
		{
			IEnumerable<string> enumerable;
			if (headers.TryGetValues(header, out enumerable))
			{
				using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						value = text;
						return true;
					}
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003A980 File Offset: 0x00038B80
		public static Stream GetResponseContent(this HttpResponseMessage response)
		{
			return response.Content.ReadAsStreamAsync().WaitForTaskCompletionAndGetResult<Stream>();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003A994 File Offset: 0x00038B94
		public static bool TryGetResponseContent(this HttpResponseMessage response, out Stream content)
		{
			bool flag;
			try
			{
				Task<Stream> task = response.Content.ReadAsStreamAsync();
				task.Wait();
				content = task.Result;
				flag = true;
			}
			catch
			{
				content = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		public static void ApplyHeaderToRequest(string header, string value, HttpWebRequest request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003A9E4 File Offset: 0x00038BE4
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpWebRequest request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003AA3C File Offset: 0x00038C3C
		public static bool TryGetHeaderValueFromResponse(HttpWebResponse response, string header, out string value)
		{
			if (response != null)
			{
				return HttpHelper.TryGetHeaderValueFromResponseImpl(response, header, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003AA4E File Offset: 0x00038C4E
		public static void ApplyHeaderToRequest(string header, string value, HttpRequestMessage request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003AA58 File Offset: 0x00038C58
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpRequestMessage request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		public static bool TryGetHeaderValueFromResponse(HttpResponseMessage response, string header, out string value)
		{
			if (response != null)
			{
				if (!(header == "Content-Encoding"))
				{
					if (!(header == "Content-Type"))
					{
						return response.Headers.TryGetHttpHeaderValue(header, out value);
					}
				}
				else
				{
					ICollection<string> contentEncoding = response.Content.Headers.ContentEncoding;
					if (contentEncoding == null)
					{
						goto IL_008E;
					}
					using (IEnumerator<string> enumerator = contentEncoding.GetEnumerator())
					{
						if (!enumerator.MoveNext())
						{
							goto IL_008E;
						}
						string text = enumerator.Current;
						value = text;
						return true;
					}
				}
				MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
				if (contentType != null)
				{
					value = contentType.MediaType;
					return true;
				}
			}
			IL_008E:
			value = null;
			return false;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003AB64 File Offset: 0x00038D64
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

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003ABA0 File Offset: 0x00038DA0
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

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		private static void ApplyHeaderToRequestImpl(string header, string value, HttpRequestMessage request)
		{
			if (!(header == "Content-Type"))
			{
				if (!(header == "User-Agent"))
				{
					request.Headers.Add(header, value);
				}
				else
				{
					request.Headers.UserAgent.Clear();
					if (!string.IsNullOrEmpty(value))
					{
						request.Headers.UserAgent.Add(new ProductInfoHeaderValue(HttpHelper.CreateUserAgentHeader(value)));
						return;
					}
				}
				return;
			}
			request.Content.Headers.ContentType = new MediaTypeHeaderValue(value);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003AC7C File Offset: 0x00038E7C
		private static ProductHeaderValue CreateUserAgentHeader(string userAgent)
		{
			int num = userAgent.IndexOf('/');
			if (num <= 0 || num >= userAgent.Length - 1)
			{
				return new ProductHeaderValue(userAgent);
			}
			return new ProductHeaderValue(userAgent.Substring(0, num), userAgent.Substring(num + 1));
		}

		// Token: 0x020001DC RID: 476
		public static class Headers
		{
			// Token: 0x040011AC RID: 4524
			public const string Authorization = "Authorization";

			// Token: 0x040011AD RID: 4525
			public const string ContentEncoding = "Content-Encoding";

			// Token: 0x040011AE RID: 4526
			public const string ContentType = "Content-Type";

			// Token: 0x040011AF RID: 4527
			public const string KeepAlive = "Keep-Alive";

			// Token: 0x040011B0 RID: 4528
			public const string Location = "Location";

			// Token: 0x040011B1 RID: 4529
			public const string UserAgent = "User-Agent";
		}

		// Token: 0x020001DD RID: 477
		public static class HeaderValues
		{
			// Token: 0x040011B2 RID: 4530
			public const string UserAgent = "AMO";

			// Token: 0x040011B3 RID: 4531
			public const string JsonContentType = "application/json";
		}
	}
}
