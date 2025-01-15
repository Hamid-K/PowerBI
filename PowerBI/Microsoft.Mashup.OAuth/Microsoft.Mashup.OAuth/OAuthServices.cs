using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200001D RID: 29
	public struct OAuthServices
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004DB7 File Offset: 0x00002FB7
		private OAuthServices(IOAuthTracingService tracingService, IOAuthHttpClient httpClient, string[] interestingHeaders, IOAuthConfigService configService)
		{
			this.tracingService = tracingService;
			this.httpClient = httpClient;
			this.interestingHeaders = interestingHeaders;
			this.configService = configService;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public static OAuthServices From(IOAuthTracingService tracingService)
		{
			return OAuthServices.From(tracingService, OAuthServices.defaultHttpClient, null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public static OAuthServices From(IOAuthTracingService tracingService, IOAuthHttpClient httpClient, IOAuthConfigService configService)
		{
			if (httpClient == null)
			{
				throw new ArgumentNullException("httpClient");
			}
			return new OAuthServices(tracingService, httpClient, OAuthServices.emptyStringArray, configService);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E01 File Offset: 0x00003001
		public void WriteTrace(string traceName, TraceEventType severityLevel, Dictionary<string, object> traceValues, bool isPii)
		{
			IOAuthTracingService ioauthTracingService = this.tracingService;
			if (ioauthTracingService == null)
			{
				return;
			}
			ioauthTracingService.WriteTrace(traceName, severityLevel, traceValues, isPii);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004E18 File Offset: 0x00003018
		public WebRequest CreateRequest(Uri requestUri)
		{
			return this.httpClient.CreateRequest(requestUri);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004E26 File Offset: 0x00003026
		public HttpStatusCode GetResponseStatus(WebResponse response)
		{
			return this.httpClient.GetResponseStatus(response);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004E34 File Offset: 0x00003034
		public bool TryLookupConfigValue(string key, out object value)
		{
			if (this.configService != null)
			{
				return this.configService.TryLookupConfigValue(key, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004E50 File Offset: 0x00003050
		internal OAuthServices AddInterestingHeader(string header)
		{
			return new OAuthServices(this.tracingService, this.httpClient, OAuthServices.Add<string>(this.interestingHeaders, header), this.configService);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004E75 File Offset: 0x00003075
		internal void Write(string traceName, TraceEventType severityLevel, params object[] values)
		{
			if (this.tracingService == null)
			{
				return;
			}
			this.tracingService.WriteTrace(traceName, severityLevel, this.NewErrorDictionary(values), true);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004E95 File Offset: 0x00003095
		internal void WritePiiSafe(string traceName, TraceEventType severityLevel, params object[] values)
		{
			if (this.tracingService == null)
			{
				return;
			}
			this.tracingService.WriteTrace(traceName, severityLevel, this.NewErrorDictionary(values), false);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004EB8 File Offset: 0x000030B8
		internal void Write(string traceName, WebException exception)
		{
			if (this.tracingService == null)
			{
				return;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(3 + this.interestingHeaders.Length);
			dictionary["Exception"] = exception;
			WebResponse response = exception.Response;
			if (response != null)
			{
				dictionary["Status"] = (int)this.GetResponseStatus(response);
				dictionary["Response"] = Utilities.Contents(Utilities.GetResponseBody(response));
				foreach (string text in this.interestingHeaders)
				{
					dictionary[text] = response.Headers[text];
				}
			}
			this.tracingService.WriteTrace(traceName, TraceEventType.Error, dictionary, true);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004F60 File Offset: 0x00003160
		internal string GetInterestingHeaders(WebHeaderCollection headers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.interestingHeaders)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}", text, headers[text]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004FC4 File Offset: 0x000031C4
		private Dictionary<string, object> NewErrorDictionary(object[] values)
		{
			int num = values.Length / 2;
			Dictionary<string, object> dictionary = new Dictionary<string, object>(num + this.interestingHeaders.Length);
			for (int i = 0; i < num; i++)
			{
				string text = values[2 * i] as string;
				object obj = values[2 * i + 1];
				if (obj is WebHeaderCollection)
				{
					WebHeaderCollection webHeaderCollection = (WebHeaderCollection)obj;
					foreach (string text2 in this.interestingHeaders)
					{
						dictionary[text2] = webHeaderCollection[text2];
					}
				}
				else if (text != null)
				{
					dictionary[text] = obj;
				}
			}
			return dictionary;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000505C File Offset: 0x0000325C
		private static T[] Add<T>(T[] array, T value)
		{
			T[] array2 = new T[array.Length + 1];
			Array.Copy(array, array2, array.Length);
			array2[array.Length] = value;
			return array2;
		}

		// Token: 0x040000BC RID: 188
		private static readonly string[] emptyStringArray = new string[0];

		// Token: 0x040000BD RID: 189
		private static readonly IOAuthHttpClient defaultHttpClient = new OAuthServices.DefaultHttpClient();

		// Token: 0x040000BE RID: 190
		public static readonly OAuthServices Empty = new OAuthServices(null, OAuthServices.defaultHttpClient, OAuthServices.emptyStringArray, null);

		// Token: 0x040000BF RID: 191
		private readonly IOAuthTracingService tracingService;

		// Token: 0x040000C0 RID: 192
		private readonly IOAuthHttpClient httpClient;

		// Token: 0x040000C1 RID: 193
		private readonly string[] interestingHeaders;

		// Token: 0x040000C2 RID: 194
		private readonly IOAuthConfigService configService;

		// Token: 0x02000030 RID: 48
		private class DefaultHttpClient : IOAuthHttpClient
		{
			// Token: 0x0600016E RID: 366 RVA: 0x00007210 File Offset: 0x00005410
			public WebRequest CreateRequest(Uri requestUri)
			{
				WebRequest webRequest = ProxyWebRequest.CreateRequest(requestUri);
				HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
				if (httpWebRequest != null)
				{
					httpWebRequest.AllowAutoRedirect = false;
					httpWebRequest.UserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";
				}
				return webRequest;
			}

			// Token: 0x0600016F RID: 367 RVA: 0x00007240 File Offset: 0x00005440
			public HttpStatusCode GetResponseStatus(WebResponse response)
			{
				HttpWebResponse httpWebResponse = response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					return httpWebResponse.StatusCode;
				}
				return (HttpStatusCode)0;
			}
		}
	}
}
