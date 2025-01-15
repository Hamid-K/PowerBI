using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x02000069 RID: 105
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Returning MemoryStream which doesn't require disposal")]
	public class HttpWebRequestMessage : DataServiceClientRequestMessage
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0000D860 File Offset: 0x0000BA60
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "SetHeader is a safe virtual method to be called from the constructor")]
		public HttpWebRequestMessage(DataServiceClientRequestMessageArgs args)
			: base(args.ActualMethod)
		{
			Util.CheckArgumentNull<DataServiceClientRequestMessageArgs>(args, "args");
			this.effectiveHttpMethod = args.Method;
			this.requestUrl = args.RequestUri;
			this.httpRequest = HttpWebRequestMessage.CreateRequest(this.ActualMethod, this.Url, args);
			foreach (KeyValuePair<string, string> keyValuePair in args.Headers)
			{
				this.SetHeader(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D904 File Offset: 0x0000BB04
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000A08D File Offset: 0x0000828D
		public override Uri Url
		{
			get
			{
				return this.requestUrl;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000D90C File Offset: 0x0000BB0C
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000A08D File Offset: 0x0000828D
		public override string Method
		{
			get
			{
				return this.effectiveHttpMethod;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000D914 File Offset: 0x0000BB14
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(this.httpRequest.Headers.Count);
				foreach (string text in this.httpRequest.Headers.AllKeys)
				{
					string text2 = this.httpRequest.Headers[text];
					list.Add(new KeyValuePair<string, string>(text, text2));
				}
				return list;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public HttpWebRequest HttpWebRequest
		{
			get
			{
				return this.httpRequest;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000D984 File Offset: 0x0000BB84
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000D991 File Offset: 0x0000BB91
		public override ICredentials Credentials
		{
			get
			{
				return this.httpRequest.Credentials;
			}
			set
			{
				this.httpRequest.Credentials = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000D99F File Offset: 0x0000BB9F
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public override int Timeout
		{
			get
			{
				return this.httpRequest.Timeout;
			}
			set
			{
				this.httpRequest.Timeout = (int)Math.Min(2147483647.0, new TimeSpan(0, 0, value).TotalMilliseconds);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000D9E3 File Offset: 0x0000BBE3
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		public override bool SendChunked
		{
			get
			{
				return this.httpRequest.SendChunked;
			}
			set
			{
				this.httpRequest.SendChunked = value;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000D9FE File Offset: 0x0000BBFE
		public override string GetHeader(string headerName)
		{
			Util.CheckArgumentNullAndEmpty(headerName, "headerName");
			return HttpWebRequestMessage.GetHeaderValue(this.httpRequest, headerName);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DA17 File Offset: 0x0000BC17
		public override void SetHeader(string headerName, string headerValue)
		{
			Util.CheckArgumentNullAndEmpty(headerName, "headerName");
			HttpWebRequestMessage.SetHeaderValue(this.httpRequest, headerName, headerValue);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DA31 File Offset: 0x0000BC31
		public override Stream GetStream()
		{
			if (this.inSendingRequest2Event)
			{
				throw new NotSupportedException(Strings.ODataRequestMessage_GetStreamMethodNotSupported);
			}
			return this.httpRequest.GetRequestStream();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DA51 File Offset: 0x0000BC51
		public override void Abort()
		{
			this.httpRequest.Abort();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DA5E File Offset: 0x0000BC5E
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (this.inSendingRequest2Event)
			{
				throw new NotSupportedException(Strings.ODataRequestMessage_GetStreamMethodNotSupported);
			}
			return this.httpRequest.BeginGetRequestStream(callback, state);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.httpRequest.EndGetRequestStream(asyncResult);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000DA8E File Offset: 0x0000BC8E
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.httpRequest.BeginGetResponse(callback, state);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		public override IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
		{
			IODataResponseMessage iodataResponseMessage;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpRequest.EndGetResponse(asyncResult);
				iodataResponseMessage = new HttpWebResponseMessage(httpWebResponse);
			}
			catch (WebException ex)
			{
				throw HttpWebRequestMessage.ConvertToDataServiceWebException(ex);
			}
			return iodataResponseMessage;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		public override IODataResponseMessage GetResponse()
		{
			IODataResponseMessage iodataResponseMessage;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpRequest.GetResponse();
				iodataResponseMessage = new HttpWebResponseMessage(httpWebResponse);
			}
			catch (WebException ex)
			{
				throw HttpWebRequestMessage.ConvertToDataServiceWebException(ex);
			}
			return iodataResponseMessage;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DB24 File Offset: 0x0000BD24
		internal static void SetHttpWebRequestContentLength(HttpWebRequest httpWebRequest, long contentLength)
		{
			httpWebRequest.ContentLength = contentLength;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DB2D File Offset: 0x0000BD2D
		internal static void SetAcceptCharset(HttpWebRequest httpWebRequest, string headerValue)
		{
			httpWebRequest.Headers["Accept-Charset"] = headerValue;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DB40 File Offset: 0x0000BD40
		internal static void SetUserAgentHeader(HttpWebRequest httpWebRequest, string headerValue)
		{
			httpWebRequest.UserAgent = headerValue;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DB49 File Offset: 0x0000BD49
		internal void BeforeSendingRequest2Event()
		{
			this.inSendingRequest2Event = true;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DB52 File Offset: 0x0000BD52
		internal void AfterSendingRequest2Event()
		{
			this.inSendingRequest2Event = false;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "args", Justification = "the parameter is used in the SL version.")]
		private static HttpWebRequest CreateRequest(string method, Uri requestUrl, DataServiceClientRequestMessageArgs args)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
			httpWebRequest.KeepAlive = true;
			httpWebRequest.Method = method;
			return httpWebRequest;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DB84 File Offset: 0x0000BD84
		private static void SetHeaderValue(HttpWebRequest request, string headerName, string headerValue)
		{
			if (string.Equals(headerName, "Accept", StringComparison.OrdinalIgnoreCase))
			{
				request.Accept = headerValue;
				return;
			}
			if (string.Equals(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				request.ContentType = headerValue;
				return;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				HttpWebRequestMessage.SetHttpWebRequestContentLength(request, long.Parse(headerValue, CultureInfo.InvariantCulture));
				return;
			}
			if (string.Equals(headerName, "User-Agent", StringComparison.OrdinalIgnoreCase))
			{
				HttpWebRequestMessage.SetUserAgentHeader(request, headerValue);
				return;
			}
			if (string.Equals(headerName, "Accept-Charset", StringComparison.OrdinalIgnoreCase))
			{
				HttpWebRequestMessage.SetAcceptCharset(request, headerValue);
				return;
			}
			request.Headers[headerName] = headerValue;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DC18 File Offset: 0x0000BE18
		private static string GetHeaderValue(HttpWebRequest request, string headerName)
		{
			if (string.Equals(headerName, "Accept", StringComparison.OrdinalIgnoreCase))
			{
				return request.Accept;
			}
			if (string.Equals(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				return request.ContentType;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				return request.ContentLength.ToString(CultureInfo.InvariantCulture);
			}
			return request.Headers[headerName];
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000DC80 File Offset: 0x0000BE80
		private static DataServiceTransportException ConvertToDataServiceWebException(WebException webException)
		{
			HttpWebResponseMessage httpWebResponseMessage = null;
			if (webException.Response != null)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)webException.Response;
				httpWebResponseMessage = new HttpWebResponseMessage(httpWebResponse);
			}
			return new DataServiceTransportException(httpWebResponseMessage, webException);
		}

		// Token: 0x04000124 RID: 292
		private readonly Uri requestUrl;

		// Token: 0x04000125 RID: 293
		private readonly string effectiveHttpMethod;

		// Token: 0x04000126 RID: 294
		private readonly HttpWebRequest httpRequest;

		// Token: 0x04000127 RID: 295
		private bool inSendingRequest2Event;
	}
}
