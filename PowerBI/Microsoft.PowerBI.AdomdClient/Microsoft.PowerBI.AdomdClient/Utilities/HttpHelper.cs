using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000148 RID: 328
	internal static class HttpHelper
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x00037CD4 File Offset: 0x00035ED4
		public static string GetHttpHeaderValueOrDefault(this HttpHeaders headers, string header, string @default = null)
		{
			string text;
			if (!headers.TryGetHttpHeaderValue(header, out text))
			{
				return @default;
			}
			return text;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00037CF0 File Offset: 0x00035EF0
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

		// Token: 0x06001041 RID: 4161 RVA: 0x00037D4C File Offset: 0x00035F4C
		public static Stream GetResponseContent(this HttpResponseMessage response)
		{
			return response.Content.ReadAsStreamAsync().WaitForTaskCompletionAndGetResult<Stream>();
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00037D60 File Offset: 0x00035F60
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

		// Token: 0x06001043 RID: 4163 RVA: 0x00037DA4 File Offset: 0x00035FA4
		public static void ApplyHeaderToRequest(string header, string value, HttpWebRequest request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00037DB0 File Offset: 0x00035FB0
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpWebRequest request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00037E08 File Offset: 0x00036008
		public static bool TryGetHeaderValueFromResponse(HttpWebResponse response, string header, out string value)
		{
			if (response != null)
			{
				return HttpHelper.TryGetHeaderValueFromResponseImpl(response, header, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00037E1A File Offset: 0x0003601A
		public static void ApplyHeaderToRequest(string header, string value, HttpRequestMessage request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00037E24 File Offset: 0x00036024
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpRequestMessage request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00037E7C File Offset: 0x0003607C
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

		// Token: 0x06001049 RID: 4169 RVA: 0x00037F30 File Offset: 0x00036130
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

		// Token: 0x0600104A RID: 4170 RVA: 0x00037F6C File Offset: 0x0003616C
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

		// Token: 0x0600104B RID: 4171 RVA: 0x00037FC4 File Offset: 0x000361C4
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

		// Token: 0x0600104C RID: 4172 RVA: 0x00038048 File Offset: 0x00036248
		private static ProductHeaderValue CreateUserAgentHeader(string userAgent)
		{
			int num = userAgent.IndexOf('/');
			if (num <= 0 || num >= userAgent.Length - 1)
			{
				return new ProductHeaderValue(userAgent);
			}
			return new ProductHeaderValue(userAgent.Substring(0, num), userAgent.Substring(num + 1));
		}

		// Token: 0x020001FF RID: 511
		public static class Headers
		{
			// Token: 0x04000EE0 RID: 3808
			public const string Authorization = "Authorization";

			// Token: 0x04000EE1 RID: 3809
			public const string ContentEncoding = "Content-Encoding";

			// Token: 0x04000EE2 RID: 3810
			public const string ContentType = "Content-Type";

			// Token: 0x04000EE3 RID: 3811
			public const string KeepAlive = "Keep-Alive";

			// Token: 0x04000EE4 RID: 3812
			public const string Location = "Location";

			// Token: 0x04000EE5 RID: 3813
			public const string UserAgent = "User-Agent";
		}

		// Token: 0x02000200 RID: 512
		public static class HeaderValues
		{
			// Token: 0x04000EE6 RID: 3814
			public const string UserAgent = "ADOMD.NET";

			// Token: 0x04000EE7 RID: 3815
			public const string JsonContentType = "application/json";
		}
	}
}
