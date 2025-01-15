using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols
{
	// Token: 0x02000006 RID: 6
	public class HttpDocumentRetriever : IDocumentRetriever
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002703 File Offset: 0x00000903
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000270A File Offset: 0x0000090A
		public static bool DefaultSendAdditionalHeaderData { get; set; } = true;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002712 File Offset: 0x00000912
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000271A File Offset: 0x0000091A
		public bool SendAdditionalHeaderData
		{
			get
			{
				return this._sendAdditionalHeaderData;
			}
			set
			{
				this._sendAdditionalHeaderData = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002723 File Offset: 0x00000923
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000272B File Offset: 0x0000092B
		internal IDictionary<string, string> AdditionalHeaderData { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x00002734 File Offset: 0x00000934
		public HttpDocumentRetriever()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000274E File Offset: 0x0000094E
		public HttpDocumentRetriever(HttpClient httpClient)
		{
			if (httpClient == null)
			{
				throw LogHelper.LogArgumentNullException("httpClient");
			}
			this._httpClient = httpClient;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000277D File Offset: 0x0000097D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002785 File Offset: 0x00000985
		public bool RequireHttps { get; set; } = true;

		// Token: 0x0600002F RID: 47 RVA: 0x00002790 File Offset: 0x00000990
		public async Task<string> GetDocumentAsync(string address, CancellationToken cancel)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw LogHelper.LogArgumentNullException("address");
			}
			if (!Utility.IsHttps(address) && this.RequireHttps)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX20108: The address specified '{0}' is not valid as per HTTPS scheme. Please specify an https address for security reasons. If you want to test with http address, set the RequireHttps property  on IDocumentRetriever to false.", new object[] { address }), "address"));
			}
			Exception ex;
			try
			{
				LogHelper.LogVerbose("IDX20805: Obtaining information from metadata endpoint: '{0}'.", new object[] { address });
				HttpClient httpClient = this._httpClient ?? HttpDocumentRetriever._defaultHttpClient;
				Uri uri = new Uri(address, UriKind.RelativeOrAbsolute);
				HttpResponseMessage httpResponseMessage = await this.SendAsyncAndRetryOnNetworkError(httpClient, uri).ConfigureAwait(false);
				HttpResponseMessage response = httpResponseMessage;
				string text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					return text;
				}
				ex = new IOException(LogHelper.FormatInvariant("IDX20807: Unable to retrieve document from: '{0}'. HttpResponseMessage: '{1}', HttpResponseMessage.Content: '{2}'.", new object[] { address, response, text }));
				ex.Data.Add("status_code", response.StatusCode);
				ex.Data.Add("response_content", text);
			}
			catch (Exception ex2)
			{
				throw LogHelper.LogExceptionMessage(new IOException(LogHelper.FormatInvariant("IDX20804: Unable to retrieve document from: '{0}'.", new object[] { address }), ex2));
			}
			throw LogHelper.LogExceptionMessage(ex);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027DC File Offset: 0x000009DC
		private async Task<HttpResponseMessage> SendAsyncAndRetryOnNetworkError(HttpClient httpClient, Uri uri)
		{
			int maxAttempt = 2;
			HttpResponseMessage response = null;
			for (int i = 1; i <= maxAttempt; i++)
			{
				using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri))
				{
					if (this.SendAdditionalHeaderData)
					{
						IdentityModelTelemetryUtil.SetTelemetryData(message, this.AdditionalHeaderData);
					}
					HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(message).ConfigureAwait(false);
					response = httpResponseMessage;
					if (response.IsSuccessStatusCode)
					{
						return response;
					}
					if (!response.StatusCode.Equals(HttpStatusCode.RequestTimeout) && !response.StatusCode.Equals(HttpStatusCode.ServiceUnavailable))
					{
						object obj = message.RequestUri;
						object obj2 = response.StatusCode;
						LogHelper.LogWarning(LogHelper.FormatInvariant("IDX20809: Unable to retrieve document from: '{0}'. Status code: '{1}'. \nResponse content: '{2}'.", new object[]
						{
							obj,
							obj2,
							await response.Content.ReadAsStringAsync().ConfigureAwait(false)
						}), Array.Empty<object>());
						obj = null;
						obj2 = null;
						break;
					}
					if (i < maxAttempt)
					{
						object obj = response.StatusCode;
						LogHelper.LogInformation(LogHelper.FormatInvariant("IDX20808: Network error occurred. Status code: '{0}'. \nResponse content: '{1}'. \nAttempting to retrieve document again from: '{2}'.", new object[]
						{
							obj,
							await response.Content.ReadAsStringAsync().ConfigureAwait(false),
							message.RequestUri
						}), Array.Empty<object>());
						obj = null;
					}
				}
				HttpRequestMessage message = null;
			}
			return response;
		}

		// Token: 0x04000017 RID: 23
		private HttpClient _httpClient;

		// Token: 0x04000018 RID: 24
		private static readonly HttpClient _defaultHttpClient = new HttpClient();

		// Token: 0x04000019 RID: 25
		public const string StatusCode = "status_code";

		// Token: 0x0400001A RID: 26
		public const string ResponseContent = "response_content";

		// Token: 0x0400001C RID: 28
		private bool _sendAdditionalHeaderData = HttpDocumentRetriever.DefaultSendAdditionalHeaderData;
	}
}
