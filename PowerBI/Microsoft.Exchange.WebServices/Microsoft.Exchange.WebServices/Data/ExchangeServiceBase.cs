using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200000F RID: 15
	public abstract class ExchangeServiceBase
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000040 RID: 64 RVA: 0x00002930 File Offset: 0x00001930
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x00002949 File Offset: 0x00001949
		public event ResponseHeadersCapturedHandler OnResponseHeadersCaptured;

		// Token: 0x06000042 RID: 66 RVA: 0x00002962 File Offset: 0x00001962
		internal void DoOnSerializeCustomSoapHeaders(XmlWriter writer)
		{
			EwsUtilities.Assert(writer != null, "ExchangeService.DoOnSerializeCustomSoapHeaders", "writer is null");
			if (this.OnSerializeCustomSoapHeaders != null)
			{
				this.OnSerializeCustomSoapHeaders(writer);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029B8 File Offset: 0x000019B8
		internal IEwsHttpWebRequest PrepareHttpWebRequestForUrl(Uri url, bool acceptGzipEncoding, bool allowAutoRedirect)
		{
			if (url.Scheme != Uri.UriSchemeHttp && url.Scheme != Uri.UriSchemeHttps)
			{
				throw new ServiceLocalException(string.Format(Strings.UnsupportedWebProtocol, url.Scheme));
			}
			IEwsHttpWebRequest request = this.HttpWebRequestFactory.CreateRequest(url);
			request.PreAuthenticate = this.PreAuthenticate;
			request.Timeout = this.Timeout;
			this.SetContentType(request);
			request.Method = "POST";
			request.UserAgent = this.UserAgent;
			request.AllowAutoRedirect = allowAutoRedirect;
			request.CookieContainer = this.CookieContainer;
			request.KeepAlive = this.keepAlive;
			request.ConnectionGroupName = this.connectionGroupName;
			if (acceptGzipEncoding)
			{
				request.Headers.Add(22, "gzip,deflate");
			}
			if (!string.IsNullOrEmpty(this.clientRequestId))
			{
				request.Headers.Add("client-request-id", this.clientRequestId);
				if (this.returnClientRequestId)
				{
					request.Headers.Add("return-client-request-id", "true");
				}
			}
			if (this.webProxy != null)
			{
				request.Proxy = this.webProxy;
			}
			if (this.HttpHeaders.Count > 0)
			{
				this.HttpHeaders.ForEach(delegate(KeyValuePair<string, string> kv)
				{
					request.Headers.Add(kv.Key, kv.Value);
				});
			}
			request.UseDefaultCredentials = this.UseDefaultCredentials;
			if (!request.UseDefaultCredentials)
			{
				ExchangeCredentials exchangeCredentials = this.Credentials;
				if (exchangeCredentials == null)
				{
					throw new ServiceLocalException(Strings.CredentialsRequired);
				}
				exchangeCredentials.PreAuthenticate();
				exchangeCredentials.PrepareWebRequest(request);
			}
			this.httpResponseHeaders.Clear();
			return request;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BAE File Offset: 0x00001BAE
		internal virtual void SetContentType(IEwsHttpWebRequest request)
		{
			request.ContentType = "text/xml; charset=utf-8";
			request.Accept = "text/xml";
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BC8 File Offset: 0x00001BC8
		internal void InternalProcessHttpErrorResponse(IEwsHttpWebResponse httpWebResponse, WebException webException, TraceFlags responseHeadersTraceFlag, TraceFlags responseTraceFlag)
		{
			EwsUtilities.Assert(httpWebResponse.StatusCode != 500, "ExchangeServiceBase.InternalProcessHttpErrorResponse", "InternalProcessHttpErrorResponse does not handle 500 ISE errors, the caller is supposed to handle this.");
			this.ProcessHttpResponseHeaders(responseHeadersTraceFlag, httpWebResponse);
			if (httpWebResponse.StatusCode == 456)
			{
				string statusDescription = httpWebResponse.StatusDescription;
				Uri uri = null;
				if (Uri.IsWellFormedUriString(statusDescription, 1))
				{
					uri = new Uri(statusDescription);
				}
				this.TraceMessage(responseTraceFlag, string.Format("Account is locked. Unlock URL is {0}", uri));
				throw new AccountIsLockedException(string.Format(Strings.AccountIsLocked, uri), uri, webException);
			}
		}

		// Token: 0x06000046 RID: 70
		internal abstract void ProcessHttpErrorResponse(IEwsHttpWebResponse httpWebResponse, WebException webException);

		// Token: 0x06000047 RID: 71 RVA: 0x00002C4D File Offset: 0x00001C4D
		internal bool IsTraceEnabledFor(TraceFlags traceFlags)
		{
			return this.TraceEnabled && (this.TraceFlags & traceFlags) != TraceFlags.None;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C68 File Offset: 0x00001C68
		internal void TraceMessage(TraceFlags traceType, string logEntry)
		{
			if (this.IsTraceEnabledFor(traceType))
			{
				string text = traceType.ToString();
				string text2 = EwsUtilities.FormatLogMessage(text, logEntry);
				this.TraceListener.Trace(text, text2);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CA0 File Offset: 0x00001CA0
		internal void TraceXml(TraceFlags traceType, MemoryStream stream)
		{
			if (this.IsTraceEnabledFor(traceType))
			{
				string text = traceType.ToString();
				string text2 = EwsUtilities.FormatLogMessageWithXmlContent(text, stream);
				this.TraceListener.Trace(text, text2);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002CD8 File Offset: 0x00001CD8
		internal void TraceHttpRequestHeaders(TraceFlags traceType, IEwsHttpWebRequest request)
		{
			if (this.IsTraceEnabledFor(traceType))
			{
				string text = traceType.ToString();
				string text2 = EwsUtilities.FormatHttpRequestHeaders(request);
				string text3 = EwsUtilities.FormatLogMessage(text, text2);
				this.TraceListener.Trace(text, text3);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D16 File Offset: 0x00001D16
		internal void ProcessHttpResponseHeaders(TraceFlags traceType, IEwsHttpWebResponse response)
		{
			this.TraceHttpResponseHeaders(traceType, response);
			this.SaveHttpResponseHeaders(response.Headers);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D2C File Offset: 0x00001D2C
		private void TraceHttpResponseHeaders(TraceFlags traceType, IEwsHttpWebResponse response)
		{
			if (this.IsTraceEnabledFor(traceType))
			{
				string text = traceType.ToString();
				string text2 = EwsUtilities.FormatHttpResponseHeaders(response);
				string text3 = EwsUtilities.FormatLogMessage(text, text2);
				this.TraceListener.Trace(text, text3);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D6C File Offset: 0x00001D6C
		private void SaveHttpResponseHeaders(WebHeaderCollection headers)
		{
			EwsUtilities.Assert(this.httpResponseHeaders.Count == 0, "ExchangeServiceBase.SaveHttpResponseHeaders", "expect no headers in the dictionary yet.");
			this.httpResponseHeaders.Clear();
			foreach (string text in headers.AllKeys)
			{
				this.httpResponseHeaders.Add(text, headers[text]);
			}
			if (this.OnResponseHeadersCaptured != null)
			{
				this.OnResponseHeadersCaptured(headers);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DE4 File Offset: 0x00001DE4
		internal DateTime? ConvertUniversalDateTimeStringToLocalDateTime(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(DateTime?);
			}
			DateTime dateTime = DateTime.Parse(value, CultureInfo.InvariantCulture, 80);
			if (this.TimeZone == TimeZoneInfo.Utc)
			{
				return new DateTime?(dateTime);
			}
			DateTime dateTime2 = EwsUtilities.ConvertTime(dateTime, TimeZoneInfo.Utc, this.TimeZone);
			if (EwsUtilities.IsLocalTimeZone(this.TimeZone))
			{
				return new DateTime?(new DateTime(dateTime2.Ticks, 2));
			}
			return new DateTime?(dateTime2);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E60 File Offset: 0x00001E60
		internal DateTime? ConvertStartDateToUnspecifiedDateTime(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return default(DateTime?);
			}
			return new DateTime?(DateTimeOffset.Parse(value, CultureInfo.InvariantCulture).Date);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E98 File Offset: 0x00001E98
		internal string ConvertDateTimeToUniversalDateTimeString(DateTime value)
		{
			DateTime dateTime;
			switch (value.Kind)
			{
			case 0:
				dateTime = EwsUtilities.ConvertTime(value, this.TimeZone, TimeZoneInfo.Utc);
				goto IL_0045;
			case 2:
				dateTime = EwsUtilities.ConvertTime(value, TimeZoneInfo.Local, TimeZoneInfo.Utc);
				goto IL_0045;
			}
			dateTime = value;
			IL_0045:
			return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EFB File Offset: 0x00001EFB
		internal void SetCustomUserAgent(string userAgent)
		{
			this.userAgent = userAgent;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F04 File Offset: 0x00001F04
		internal ExchangeServiceBase()
			: this(TimeZoneInfo.Local)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F14 File Offset: 0x00001F14
		internal ExchangeServiceBase(TimeZoneInfo timeZone)
		{
			this.timeZone = timeZone;
			this.UseDefaultCredentials = true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FAD File Offset: 0x00001FAD
		internal ExchangeServiceBase(ExchangeVersion requestedServerVersion)
			: this(requestedServerVersion, TimeZoneInfo.Local)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FBB File Offset: 0x00001FBB
		internal ExchangeServiceBase(ExchangeVersion requestedServerVersion, TimeZoneInfo timeZone)
			: this(timeZone)
		{
			this.requestedServerVersion = requestedServerVersion;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FCC File Offset: 0x00001FCC
		internal ExchangeServiceBase(ExchangeServiceBase service, ExchangeVersion requestedServerVersion)
			: this(requestedServerVersion)
		{
			this.useDefaultCredentials = service.useDefaultCredentials;
			this.credentials = service.credentials;
			this.traceEnabled = service.traceEnabled;
			this.traceListener = service.traceListener;
			this.traceFlags = service.traceFlags;
			this.timeout = service.timeout;
			this.preAuthenticate = service.preAuthenticate;
			this.userAgent = service.userAgent;
			this.acceptGzipEncoding = service.acceptGzipEncoding;
			this.keepAlive = service.keepAlive;
			this.connectionGroupName = service.connectionGroupName;
			this.timeZone = service.timeZone;
			this.httpHeaders = service.httpHeaders;
			this.ewsHttpWebRequestFactory = service.ewsHttpWebRequestFactory;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003088 File Offset: 0x00002088
		internal ExchangeServiceBase(ExchangeServiceBase service)
			: this(service, service.RequestedServerVersion)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003097 File Offset: 0x00002097
		internal virtual void Validate()
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003099 File Offset: 0x00002099
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000030A1 File Offset: 0x000020A1
		public CookieContainer CookieContainer
		{
			get
			{
				return this.cookieContainer;
			}
			set
			{
				this.cookieContainer = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030AA File Offset: 0x000020AA
		internal TimeZoneInfo TimeZone
		{
			get
			{
				return this.timeZone;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000030B2 File Offset: 0x000020B2
		internal TimeZoneDefinition TimeZoneDefinition
		{
			get
			{
				if (this.timeZoneDefinition == null)
				{
					this.timeZoneDefinition = new TimeZoneDefinition(this.TimeZone);
				}
				return this.timeZoneDefinition;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030D3 File Offset: 0x000020D3
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000030DB File Offset: 0x000020DB
		public bool SendClientLatencies
		{
			get
			{
				return this.sendClientLatencies;
			}
			set
			{
				this.sendClientLatencies = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030E4 File Offset: 0x000020E4
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000030EC File Offset: 0x000020EC
		public bool TraceEnabled
		{
			get
			{
				return this.traceEnabled;
			}
			set
			{
				this.traceEnabled = value;
				if (this.traceEnabled && this.traceListener == null)
				{
					this.traceListener = new EwsTraceListener();
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003110 File Offset: 0x00002110
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003118 File Offset: 0x00002118
		public TraceFlags TraceFlags
		{
			get
			{
				return this.traceFlags;
			}
			set
			{
				this.traceFlags = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003121 File Offset: 0x00002121
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003129 File Offset: 0x00002129
		public ITraceListener TraceListener
		{
			get
			{
				return this.traceListener;
			}
			set
			{
				this.traceListener = value;
				this.traceEnabled = value != null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000313F File Offset: 0x0000213F
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003147 File Offset: 0x00002147
		public ExchangeCredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
				this.useDefaultCredentials = false;
				this.cookieContainer = new CookieContainer();
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003162 File Offset: 0x00002162
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000316A File Offset: 0x0000216A
		public bool UseDefaultCredentials
		{
			get
			{
				return this.useDefaultCredentials;
			}
			set
			{
				this.useDefaultCredentials = value;
				if (value)
				{
					this.credentials = null;
					this.cookieContainer = new CookieContainer();
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003188 File Offset: 0x00002188
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003190 File Offset: 0x00002190
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Strings.TimeoutMustBeGreaterThanZero);
				}
				this.timeout = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000031AD File Offset: 0x000021AD
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000031B5 File Offset: 0x000021B5
		public bool PreAuthenticate
		{
			get
			{
				return this.preAuthenticate;
			}
			set
			{
				this.preAuthenticate = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000031BE File Offset: 0x000021BE
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000031C6 File Offset: 0x000021C6
		public bool AcceptGzipEncoding
		{
			get
			{
				return this.acceptGzipEncoding;
			}
			set
			{
				this.acceptGzipEncoding = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000031CF File Offset: 0x000021CF
		public ExchangeVersion RequestedServerVersion
		{
			get
			{
				return this.requestedServerVersion;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000031D7 File Offset: 0x000021D7
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000031DF File Offset: 0x000021DF
		public string UserAgent
		{
			get
			{
				return this.userAgent;
			}
			set
			{
				this.userAgent = value + " (" + ExchangeServiceBase.defaultUserAgent + ")";
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000031FC File Offset: 0x000021FC
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003204 File Offset: 0x00002204
		public ExchangeServerInfo ServerInfo
		{
			get
			{
				return this.serverInfo;
			}
			internal set
			{
				this.serverInfo = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000320D File Offset: 0x0000220D
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003215 File Offset: 0x00002215
		public IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
			set
			{
				this.webProxy = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000321E File Offset: 0x0000221E
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003226 File Offset: 0x00002226
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000322F File Offset: 0x0000222F
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003237 File Offset: 0x00002237
		public string ConnectionGroupName
		{
			get
			{
				return this.connectionGroupName;
			}
			set
			{
				this.connectionGroupName = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003240 File Offset: 0x00002240
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003248 File Offset: 0x00002248
		public string ClientRequestId
		{
			get
			{
				return this.clientRequestId;
			}
			set
			{
				this.clientRequestId = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003251 File Offset: 0x00002251
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003259 File Offset: 0x00002259
		public bool ReturnClientRequestId
		{
			get
			{
				return this.returnClientRequestId;
			}
			set
			{
				this.returnClientRequestId = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003262 File Offset: 0x00002262
		public IDictionary<string, string> HttpHeaders
		{
			get
			{
				return this.httpHeaders;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000326A File Offset: 0x0000226A
		public IDictionary<string, string> HttpResponseHeaders
		{
			get
			{
				return this.httpResponseHeaders;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003274 File Offset: 0x00002274
		internal static byte[] SessionKey
		{
			get
			{
				byte[] array;
				lock (ExchangeServiceBase.lockObj)
				{
					if (ExchangeServiceBase.binarySecret == null)
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						ExchangeServiceBase.binarySecret = new byte[32];
						randomNumberGenerator.GetNonZeroBytes(ExchangeServiceBase.binarySecret);
					}
					array = ExchangeServiceBase.binarySecret;
				}
				return array;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000032D4 File Offset: 0x000022D4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000032DC File Offset: 0x000022DC
		internal IEwsHttpWebRequestFactory HttpWebRequestFactory
		{
			get
			{
				return this.ewsHttpWebRequestFactory;
			}
			set
			{
				this.ewsHttpWebRequestFactory = ((value == null) ? new EwsHttpWebRequestFactory() : value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000032EF File Offset: 0x000022EF
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000032F7 File Offset: 0x000022F7
		internal bool SuppressXmlVersionHeader { get; set; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000085 RID: 133 RVA: 0x00003300 File Offset: 0x00002300
		// (remove) Token: 0x06000086 RID: 134 RVA: 0x00003319 File Offset: 0x00002319
		public event CustomXmlSerializationDelegate OnSerializeCustomSoapHeaders;

		// Token: 0x04000019 RID: 25
		internal const HttpStatusCode AccountIsLocked = 456;

		// Token: 0x0400001A RID: 26
		private static readonly object lockObj = new object();

		// Token: 0x0400001B RID: 27
		private readonly ExchangeVersion requestedServerVersion = ExchangeVersion.Exchange2013_SP1;

		// Token: 0x0400001C RID: 28
		private static byte[] binarySecret;

		// Token: 0x0400001D RID: 29
		private static string defaultUserAgent = "ExchangeServicesClient/" + EwsUtilities.BuildVersion;

		// Token: 0x0400001F RID: 31
		private ExchangeCredentials credentials;

		// Token: 0x04000020 RID: 32
		private bool useDefaultCredentials;

		// Token: 0x04000021 RID: 33
		private int timeout = 100000;

		// Token: 0x04000022 RID: 34
		private bool traceEnabled;

		// Token: 0x04000023 RID: 35
		private bool sendClientLatencies = true;

		// Token: 0x04000024 RID: 36
		private TraceFlags traceFlags = TraceFlags.All;

		// Token: 0x04000025 RID: 37
		private ITraceListener traceListener = new EwsTraceListener();

		// Token: 0x04000026 RID: 38
		private bool preAuthenticate;

		// Token: 0x04000027 RID: 39
		private string userAgent = ExchangeServiceBase.defaultUserAgent;

		// Token: 0x04000028 RID: 40
		private bool acceptGzipEncoding = true;

		// Token: 0x04000029 RID: 41
		private bool keepAlive = true;

		// Token: 0x0400002A RID: 42
		private string connectionGroupName;

		// Token: 0x0400002B RID: 43
		private string clientRequestId;

		// Token: 0x0400002C RID: 44
		private bool returnClientRequestId;

		// Token: 0x0400002D RID: 45
		private CookieContainer cookieContainer = new CookieContainer();

		// Token: 0x0400002E RID: 46
		private TimeZoneInfo timeZone;

		// Token: 0x0400002F RID: 47
		private TimeZoneDefinition timeZoneDefinition;

		// Token: 0x04000030 RID: 48
		private ExchangeServerInfo serverInfo;

		// Token: 0x04000031 RID: 49
		private IWebProxy webProxy;

		// Token: 0x04000032 RID: 50
		private IDictionary<string, string> httpHeaders = new Dictionary<string, string>();

		// Token: 0x04000033 RID: 51
		private IDictionary<string, string> httpResponseHeaders = new Dictionary<string, string>();

		// Token: 0x04000034 RID: 52
		private IEwsHttpWebRequestFactory ewsHttpWebRequestFactory = new EwsHttpWebRequestFactory();
	}
}
