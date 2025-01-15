using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C8 RID: 200
	internal class EwsHttpWebRequest : IEwsHttpWebRequest
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0001DAE6 File Offset: 0x0001CAE6
		internal EwsHttpWebRequest(Uri uri)
		{
			this.request = (HttpWebRequest)WebRequest.Create(uri);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001DAFF File Offset: 0x0001CAFF
		void IEwsHttpWebRequest.Abort()
		{
			this.request.Abort();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001DB0C File Offset: 0x0001CB0C
		IAsyncResult IEwsHttpWebRequest.BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.request.BeginGetRequestStream(callback, state);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001DB1B File Offset: 0x0001CB1B
		IAsyncResult IEwsHttpWebRequest.BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.request.BeginGetResponse(callback, state);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001DB2A File Offset: 0x0001CB2A
		Stream IEwsHttpWebRequest.EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.request.EndGetRequestStream(asyncResult);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001DB38 File Offset: 0x0001CB38
		IEwsHttpWebResponse IEwsHttpWebRequest.EndGetResponse(IAsyncResult asyncResult)
		{
			return new EwsHttpWebResponse((HttpWebResponse)this.request.EndGetResponse(asyncResult));
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001DB50 File Offset: 0x0001CB50
		Stream IEwsHttpWebRequest.GetRequestStream()
		{
			return this.request.GetRequestStream();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001DB5D File Offset: 0x0001CB5D
		IEwsHttpWebResponse IEwsHttpWebRequest.GetResponse()
		{
			return new EwsHttpWebResponse(this.request.GetResponse() as HttpWebResponse);
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0001DB74 File Offset: 0x0001CB74
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0001DB81 File Offset: 0x0001CB81
		string IEwsHttpWebRequest.Accept
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0001DB8F File Offset: 0x0001CB8F
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0001DB9C File Offset: 0x0001CB9C
		bool IEwsHttpWebRequest.AllowAutoRedirect
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

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0001DBAA File Offset: 0x0001CBAA
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0001DBB7 File Offset: 0x0001CBB7
		X509CertificateCollection IEwsHttpWebRequest.ClientCertificates
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0001DBC5 File Offset: 0x0001CBC5
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x0001DBD2 File Offset: 0x0001CBD2
		string IEwsHttpWebRequest.ContentType
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0001DBE0 File Offset: 0x0001CBE0
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0001DBED File Offset: 0x0001CBED
		CookieContainer IEwsHttpWebRequest.CookieContainer
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001DBFB File Offset: 0x0001CBFB
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0001DC08 File Offset: 0x0001CC08
		ICredentials IEwsHttpWebRequest.Credentials
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

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001DC16 File Offset: 0x0001CC16
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0001DC23 File Offset: 0x0001CC23
		WebHeaderCollection IEwsHttpWebRequest.Headers
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001DC31 File Offset: 0x0001CC31
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0001DC3E File Offset: 0x0001CC3E
		string IEwsHttpWebRequest.Method
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001DC4C File Offset: 0x0001CC4C
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0001DC59 File Offset: 0x0001CC59
		IWebProxy IEwsHttpWebRequest.Proxy
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001DC67 File Offset: 0x0001CC67
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0001DC74 File Offset: 0x0001CC74
		bool IEwsHttpWebRequest.PreAuthenticate
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

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001DC82 File Offset: 0x0001CC82
		Uri IEwsHttpWebRequest.RequestUri
		{
			get
			{
				return this.request.RequestUri;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001DC8F File Offset: 0x0001CC8F
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0001DC9C File Offset: 0x0001CC9C
		int IEwsHttpWebRequest.Timeout
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001DCAA File Offset: 0x0001CCAA
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0001DCB7 File Offset: 0x0001CCB7
		bool IEwsHttpWebRequest.UseDefaultCredentials
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

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001DCC5 File Offset: 0x0001CCC5
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001DCD2 File Offset: 0x0001CCD2
		string IEwsHttpWebRequest.UserAgent
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001DCE0 File Offset: 0x0001CCE0
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001DCED File Offset: 0x0001CCED
		public bool KeepAlive
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

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001DCFB File Offset: 0x0001CCFB
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0001DD08 File Offset: 0x0001CD08
		public string ConnectionGroupName
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

		// Token: 0x040002C3 RID: 707
		private HttpWebRequest request;
	}
}
