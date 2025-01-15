using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000AA7 RID: 2727
	internal class WrappingHttpWebRequest : MashupHttpWebRequest
	{
		// Token: 0x06004C34 RID: 19508 RVA: 0x000FC034 File Offset: 0x000FA234
		public WrappingHttpWebRequest(HttpWebRequest request)
		{
			this.request = request;
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x000FC043 File Offset: 0x000FA243
		// (set) Token: 0x06004C36 RID: 19510 RVA: 0x000FC050 File Offset: 0x000FA250
		public override string Accept
		{
			get
			{
				return this.request.Accept;
			}
			set
			{
				this.request.Accept = value;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x000FC05E File Offset: 0x000FA25E
		public override Uri Address
		{
			get
			{
				return this.request.Address;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x000FC06B File Offset: 0x000FA26B
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x000FC078 File Offset: 0x000FA278
		public override bool AllowAutoRedirect
		{
			get
			{
				return this.request.AllowAutoRedirect;
			}
			set
			{
				this.request.AllowAutoRedirect = value;
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x000FC086 File Offset: 0x000FA286
		// (set) Token: 0x06004C3B RID: 19515 RVA: 0x000FC093 File Offset: 0x000FA293
		public override bool AllowWriteStreamBuffering
		{
			get
			{
				return this.request.AllowWriteStreamBuffering;
			}
			set
			{
				this.request.AllowWriteStreamBuffering = value;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x000FC0A1 File Offset: 0x000FA2A1
		// (set) Token: 0x06004C3D RID: 19517 RVA: 0x000FC0AE File Offset: 0x000FA2AE
		public override DecompressionMethods AutomaticDecompression
		{
			get
			{
				return this.request.AutomaticDecompression;
			}
			set
			{
				this.request.AutomaticDecompression = value;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x000FC0BC File Offset: 0x000FA2BC
		// (set) Token: 0x06004C3F RID: 19519 RVA: 0x000FC0C9 File Offset: 0x000FA2C9
		public override X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.request.ClientCertificates;
			}
			set
			{
				this.request.ClientCertificates = value;
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x000FC0D7 File Offset: 0x000FA2D7
		// (set) Token: 0x06004C41 RID: 19521 RVA: 0x000FC0E4 File Offset: 0x000FA2E4
		public override string Connection
		{
			get
			{
				return this.request.Connection;
			}
			set
			{
				this.request.Connection = value;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06004C42 RID: 19522 RVA: 0x000FC0F2 File Offset: 0x000FA2F2
		// (set) Token: 0x06004C43 RID: 19523 RVA: 0x000FC0FF File Offset: 0x000FA2FF
		public override string ConnectionGroupName
		{
			get
			{
				return this.request.ConnectionGroupName;
			}
			set
			{
				this.request.ConnectionGroupName = value;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06004C44 RID: 19524 RVA: 0x000FC10D File Offset: 0x000FA30D
		// (set) Token: 0x06004C45 RID: 19525 RVA: 0x000FC11A File Offset: 0x000FA31A
		public override long ContentLength
		{
			get
			{
				return this.request.ContentLength;
			}
			set
			{
				this.request.ContentLength = value;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06004C46 RID: 19526 RVA: 0x000FC128 File Offset: 0x000FA328
		// (set) Token: 0x06004C47 RID: 19527 RVA: 0x000FC135 File Offset: 0x000FA335
		public override string ContentType
		{
			get
			{
				return this.request.ContentType;
			}
			set
			{
				this.request.ContentType = value;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x000FC143 File Offset: 0x000FA343
		// (set) Token: 0x06004C49 RID: 19529 RVA: 0x000FC150 File Offset: 0x000FA350
		public override HttpContinueDelegate ContinueDelegate
		{
			get
			{
				return this.request.ContinueDelegate;
			}
			set
			{
				this.request.ContinueDelegate = value;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06004C4A RID: 19530 RVA: 0x000FC15E File Offset: 0x000FA35E
		// (set) Token: 0x06004C4B RID: 19531 RVA: 0x000FC16B File Offset: 0x000FA36B
		public override CookieContainer CookieContainer
		{
			get
			{
				return this.request.CookieContainer;
			}
			set
			{
				this.request.CookieContainer = value;
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x06004C4C RID: 19532 RVA: 0x000FC179 File Offset: 0x000FA379
		// (set) Token: 0x06004C4D RID: 19533 RVA: 0x000FC186 File Offset: 0x000FA386
		public override ICredentials Credentials
		{
			get
			{
				return this.request.Credentials;
			}
			set
			{
				this.request.Credentials = value;
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x06004C4E RID: 19534 RVA: 0x000FC194 File Offset: 0x000FA394
		// (set) Token: 0x06004C4F RID: 19535 RVA: 0x000FC1A1 File Offset: 0x000FA3A1
		public override string Expect
		{
			get
			{
				return this.request.Expect;
			}
			set
			{
				this.request.Expect = value;
			}
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06004C50 RID: 19536 RVA: 0x000FC1AF File Offset: 0x000FA3AF
		public override bool HaveResponse
		{
			get
			{
				return this.request.HaveResponse;
			}
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06004C51 RID: 19537 RVA: 0x000FC1BC File Offset: 0x000FA3BC
		// (set) Token: 0x06004C52 RID: 19538 RVA: 0x000FC1C9 File Offset: 0x000FA3C9
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.request.Headers;
			}
			set
			{
				this.request.Headers = value;
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06004C53 RID: 19539 RVA: 0x000FC1D7 File Offset: 0x000FA3D7
		// (set) Token: 0x06004C54 RID: 19540 RVA: 0x000FC1E4 File Offset: 0x000FA3E4
		public override DateTime IfModifiedSince
		{
			get
			{
				return this.request.IfModifiedSince;
			}
			set
			{
				this.request.IfModifiedSince = value;
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x000FC1F2 File Offset: 0x000FA3F2
		// (set) Token: 0x06004C56 RID: 19542 RVA: 0x000FC1FF File Offset: 0x000FA3FF
		public override bool KeepAlive
		{
			get
			{
				return this.request.KeepAlive;
			}
			set
			{
				this.request.KeepAlive = value;
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x000FC20D File Offset: 0x000FA40D
		// (set) Token: 0x06004C58 RID: 19544 RVA: 0x000FC21A File Offset: 0x000FA41A
		public override int MaximumAutomaticRedirections
		{
			get
			{
				return this.request.MaximumAutomaticRedirections;
			}
			set
			{
				this.request.MaximumAutomaticRedirections = value;
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06004C59 RID: 19545 RVA: 0x000FC228 File Offset: 0x000FA428
		// (set) Token: 0x06004C5A RID: 19546 RVA: 0x000FC235 File Offset: 0x000FA435
		public override int MaximumResponseHeadersLength
		{
			get
			{
				return this.request.MaximumResponseHeadersLength;
			}
			set
			{
				this.request.MaximumResponseHeadersLength = value;
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06004C5B RID: 19547 RVA: 0x000FC243 File Offset: 0x000FA443
		// (set) Token: 0x06004C5C RID: 19548 RVA: 0x000FC250 File Offset: 0x000FA450
		public override string MediaType
		{
			get
			{
				return this.request.MediaType;
			}
			set
			{
				this.request.MediaType = value;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06004C5D RID: 19549 RVA: 0x000FC25E File Offset: 0x000FA45E
		// (set) Token: 0x06004C5E RID: 19550 RVA: 0x000FC26B File Offset: 0x000FA46B
		public override string Method
		{
			get
			{
				return this.request.Method;
			}
			set
			{
				this.request.Method = value;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06004C5F RID: 19551 RVA: 0x000FC279 File Offset: 0x000FA479
		// (set) Token: 0x06004C60 RID: 19552 RVA: 0x000FC286 File Offset: 0x000FA486
		public override bool Pipelined
		{
			get
			{
				return this.request.Pipelined;
			}
			set
			{
				this.request.Pipelined = value;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06004C61 RID: 19553 RVA: 0x000FC294 File Offset: 0x000FA494
		// (set) Token: 0x06004C62 RID: 19554 RVA: 0x000FC2A1 File Offset: 0x000FA4A1
		public override bool PreAuthenticate
		{
			get
			{
				return this.request.PreAuthenticate;
			}
			set
			{
				this.request.PreAuthenticate = value;
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06004C63 RID: 19555 RVA: 0x000FC2AF File Offset: 0x000FA4AF
		// (set) Token: 0x06004C64 RID: 19556 RVA: 0x000FC2BC File Offset: 0x000FA4BC
		public override Version ProtocolVersion
		{
			get
			{
				return this.request.ProtocolVersion;
			}
			set
			{
				this.request.ProtocolVersion = value;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06004C65 RID: 19557 RVA: 0x000FC2CA File Offset: 0x000FA4CA
		// (set) Token: 0x06004C66 RID: 19558 RVA: 0x000FC2D7 File Offset: 0x000FA4D7
		public override IWebProxy Proxy
		{
			get
			{
				return this.request.Proxy;
			}
			set
			{
				this.request.Proxy = value;
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06004C67 RID: 19559 RVA: 0x000FC2E5 File Offset: 0x000FA4E5
		// (set) Token: 0x06004C68 RID: 19560 RVA: 0x000FC2F2 File Offset: 0x000FA4F2
		public override int ReadWriteTimeout
		{
			get
			{
				return this.request.ReadWriteTimeout;
			}
			set
			{
				this.request.ReadWriteTimeout = value;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06004C69 RID: 19561 RVA: 0x000FC300 File Offset: 0x000FA500
		// (set) Token: 0x06004C6A RID: 19562 RVA: 0x000FC30D File Offset: 0x000FA50D
		public override string Referer
		{
			get
			{
				return this.request.Referer;
			}
			set
			{
				this.request.Referer = value;
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06004C6B RID: 19563 RVA: 0x000FC31B File Offset: 0x000FA51B
		public override Uri RequestUri
		{
			get
			{
				return this.request.RequestUri;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x000FC328 File Offset: 0x000FA528
		// (set) Token: 0x06004C6D RID: 19565 RVA: 0x000FC335 File Offset: 0x000FA535
		public override bool SendChunked
		{
			get
			{
				return this.request.SendChunked;
			}
			set
			{
				this.request.SendChunked = value;
			}
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06004C6E RID: 19566 RVA: 0x000FC343 File Offset: 0x000FA543
		public override ServicePoint ServicePoint
		{
			get
			{
				return this.request.ServicePoint;
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06004C6F RID: 19567 RVA: 0x000FC350 File Offset: 0x000FA550
		// (set) Token: 0x06004C70 RID: 19568 RVA: 0x000FC35D File Offset: 0x000FA55D
		public override int Timeout
		{
			get
			{
				return this.request.Timeout;
			}
			set
			{
				this.request.Timeout = value;
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06004C71 RID: 19569 RVA: 0x000FC36B File Offset: 0x000FA56B
		// (set) Token: 0x06004C72 RID: 19570 RVA: 0x000FC378 File Offset: 0x000FA578
		public override string TransferEncoding
		{
			get
			{
				return this.request.TransferEncoding;
			}
			set
			{
				this.request.TransferEncoding = value;
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06004C73 RID: 19571 RVA: 0x000FC386 File Offset: 0x000FA586
		// (set) Token: 0x06004C74 RID: 19572 RVA: 0x000FC393 File Offset: 0x000FA593
		public override bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.request.UnsafeAuthenticatedConnectionSharing;
			}
			set
			{
				this.request.UnsafeAuthenticatedConnectionSharing = value;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x000FC3A1 File Offset: 0x000FA5A1
		// (set) Token: 0x06004C76 RID: 19574 RVA: 0x000FC3AE File Offset: 0x000FA5AE
		public override bool UseDefaultCredentials
		{
			get
			{
				return this.request.UseDefaultCredentials;
			}
			set
			{
				this.request.UseDefaultCredentials = value;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x000FC3BC File Offset: 0x000FA5BC
		// (set) Token: 0x06004C78 RID: 19576 RVA: 0x000FC3C9 File Offset: 0x000FA5C9
		public override string UserAgent
		{
			get
			{
				return this.request.UserAgent;
			}
			set
			{
				this.request.UserAgent = value;
			}
		}

		// Token: 0x06004C79 RID: 19577 RVA: 0x000FC3D7 File Offset: 0x000FA5D7
		public override void Abort()
		{
			this.request.Abort();
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x000FC3E4 File Offset: 0x000FA5E4
		public override void AddRange(int range)
		{
			this.request.AddRange(range);
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x000FC3F2 File Offset: 0x000FA5F2
		public override void AddRange(int from, int to)
		{
			this.request.AddRange(from, to);
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x000FC401 File Offset: 0x000FA601
		public override void AddRange(string rangeSpecifier, int range)
		{
			this.request.AddRange(rangeSpecifier, range);
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x000FC410 File Offset: 0x000FA610
		public override void AddRange(string rangeSpecifier, int from, int to)
		{
			this.request.AddRange(rangeSpecifier, from, to);
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x000FC420 File Offset: 0x000FA620
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.request.BeginGetRequestStream(callback, state);
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x000FC42F File Offset: 0x000FA62F
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.request.BeginGetResponse(callback, state);
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x000FC440 File Offset: 0x000FA640
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.WrapExceptionResponse<Stream>(() => this.request.EndGetRequestStream(asyncResult));
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x000FC474 File Offset: 0x000FA674
		public override Stream EndGetRequestStream(IAsyncResult asyncResult, out TransportContext context)
		{
			TransportContext innerContext = null;
			Stream stream = this.WrapExceptionResponse<Stream>(() => this.request.EndGetRequestStream(asyncResult, out innerContext));
			context = innerContext;
			return stream;
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x000FC4B8 File Offset: 0x000FA6B8
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			return this.WrapResponse(() => this.request.EndGetResponse(asyncResult));
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x000FC4EB File Offset: 0x000FA6EB
		public override Stream GetRequestStream()
		{
			return this.WrapExceptionResponse<Stream>(() => this.request.GetRequestStream());
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x000FC500 File Offset: 0x000FA700
		public override Stream GetRequestStream(out TransportContext context)
		{
			TransportContext innerContext = null;
			Stream stream = this.WrapExceptionResponse<Stream>(() => this.request.GetRequestStream(out innerContext));
			context = innerContext;
			return stream;
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x000FC53B File Offset: 0x000FA73B
		public override WebResponse GetResponse()
		{
			return this.WrapResponse(new Func<WebResponse>(this.request.GetResponse));
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x000FC558 File Offset: 0x000FA758
		private WebResponse WrapResponse(Func<WebResponse> getResponse)
		{
			return this.WrapExceptionResponse<WebResponse>(delegate
			{
				WebResponse webResponse = getResponse();
				HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
				if (httpWebResponse != null)
				{
					webResponse = new WrappingHttpWebResponse(httpWebResponse);
				}
				return webResponse;
			});
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x000FC584 File Offset: 0x000FA784
		private T WrapExceptionResponse<T>(Func<T> getValue)
		{
			T t;
			try
			{
				t = getValue();
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					MashupHttpWebResponse mashupHttpWebResponse = new WrappingHttpWebResponse(httpWebResponse);
					WebException ex2 = new WebException(ex.Message, ex.InnerException, ex.Status, mashupHttpWebResponse);
					foreach (object obj in ex.Data.Keys)
					{
						string text = (string)obj;
						ex2.Data[text] = ex.Data[text];
					}
					ex2.HelpLink = ex.HelpLink;
					ex2.Source = ex.Source;
					throw ex2;
				}
				throw;
			}
			return t;
		}

		// Token: 0x0400286F RID: 10351
		private readonly HttpWebRequest request;
	}
}
