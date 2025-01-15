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
		// Token: 0x0600104C RID: 4172 RVA: 0x00038004 File Offset: 0x00036204
		public static string GetHttpHeaderValueOrDefault(this HttpHeaders headers, string header, string @default = null)
		{
			string text;
			if (!headers.TryGetHttpHeaderValue(header, out text))
			{
				return @default;
			}
			return text;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00038020 File Offset: 0x00036220
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

		// Token: 0x0600104E RID: 4174 RVA: 0x0003807C File Offset: 0x0003627C
		public static Stream GetResponseContent(this HttpResponseMessage response)
		{
			return response.Content.ReadAsStreamAsync().WaitForTaskCompletionAndGetResult<Stream>();
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00038090 File Offset: 0x00036290
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

		// Token: 0x06001050 RID: 4176 RVA: 0x000380D4 File Offset: 0x000362D4
		public static void ApplyHeaderToRequest(string header, string value, HttpWebRequest request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000380E0 File Offset: 0x000362E0
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpWebRequest request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00038138 File Offset: 0x00036338
		public static bool TryGetHeaderValueFromResponse(HttpWebResponse response, string header, out string value)
		{
			if (response != null)
			{
				return HttpHelper.TryGetHeaderValueFromResponseImpl(response, header, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0003814A File Offset: 0x0003634A
		public static void ApplyHeaderToRequest(string header, string value, HttpRequestMessage request)
		{
			HttpHelper.ApplyHeaderToRequestImpl(header, value, request);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00038154 File Offset: 0x00036354
		public static void ApplyHeadersToRequest(IEnumerable<KeyValuePair<string, string>> headers, HttpRequestMessage request)
		{
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				HttpHelper.ApplyHeaderToRequestImpl(keyValuePair.Key, keyValuePair.Value, request);
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000381AC File Offset: 0x000363AC
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

		// Token: 0x06001056 RID: 4182 RVA: 0x00038260 File Offset: 0x00036460
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

		// Token: 0x06001057 RID: 4183 RVA: 0x0003829C File Offset: 0x0003649C
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

		// Token: 0x06001058 RID: 4184 RVA: 0x000382F4 File Offset: 0x000364F4
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

		// Token: 0x06001059 RID: 4185 RVA: 0x00038378 File Offset: 0x00036578
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
			// Token: 0x04000EF6 RID: 3830
			public const string Authorization = "Authorization";

			// Token: 0x04000EF7 RID: 3831
			public const string ContentEncoding = "Content-Encoding";

			// Token: 0x04000EF8 RID: 3832
			public const string ContentType = "Content-Type";

			// Token: 0x04000EF9 RID: 3833
			public const string KeepAlive = "Keep-Alive";

			// Token: 0x04000EFA RID: 3834
			public const string Location = "Location";

			// Token: 0x04000EFB RID: 3835
			public const string UserAgent = "User-Agent";
		}

		// Token: 0x02000200 RID: 512
		public static class HeaderValues
		{
			// Token: 0x04000EFC RID: 3836
			public const string UserAgent = "ADOMD.NET";

			// Token: 0x04000EFD RID: 3837
			public const string JsonContentType = "application/json";
		}
	}
}
