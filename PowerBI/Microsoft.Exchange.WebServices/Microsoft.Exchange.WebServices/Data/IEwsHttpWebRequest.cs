using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C7 RID: 199
	internal interface IEwsHttpWebRequest
	{
		// Token: 0x060008C6 RID: 2246
		void Abort();

		// Token: 0x060008C7 RID: 2247
		IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);

		// Token: 0x060008C8 RID: 2248
		IAsyncResult BeginGetResponse(AsyncCallback callback, object state);

		// Token: 0x060008C9 RID: 2249
		Stream EndGetRequestStream(IAsyncResult asyncResult);

		// Token: 0x060008CA RID: 2250
		IEwsHttpWebResponse EndGetResponse(IAsyncResult asyncResult);

		// Token: 0x060008CB RID: 2251
		Stream GetRequestStream();

		// Token: 0x060008CC RID: 2252
		IEwsHttpWebResponse GetResponse();

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008CD RID: 2253
		// (set) Token: 0x060008CE RID: 2254
		string Accept { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008CF RID: 2255
		// (set) Token: 0x060008D0 RID: 2256
		bool AllowAutoRedirect { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008D1 RID: 2257
		// (set) Token: 0x060008D2 RID: 2258
		X509CertificateCollection ClientCertificates { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008D3 RID: 2259
		// (set) Token: 0x060008D4 RID: 2260
		string ContentType { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008D5 RID: 2261
		// (set) Token: 0x060008D6 RID: 2262
		CookieContainer CookieContainer { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008D7 RID: 2263
		// (set) Token: 0x060008D8 RID: 2264
		ICredentials Credentials { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008D9 RID: 2265
		// (set) Token: 0x060008DA RID: 2266
		WebHeaderCollection Headers { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008DB RID: 2267
		// (set) Token: 0x060008DC RID: 2268
		string Method { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008DD RID: 2269
		// (set) Token: 0x060008DE RID: 2270
		bool PreAuthenticate { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008DF RID: 2271
		// (set) Token: 0x060008E0 RID: 2272
		IWebProxy Proxy { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008E1 RID: 2273
		Uri RequestUri { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008E2 RID: 2274
		// (set) Token: 0x060008E3 RID: 2275
		int Timeout { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008E4 RID: 2276
		// (set) Token: 0x060008E5 RID: 2277
		bool UseDefaultCredentials { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008E6 RID: 2278
		// (set) Token: 0x060008E7 RID: 2279
		string UserAgent { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008E8 RID: 2280
		// (set) Token: 0x060008E9 RID: 2281
		bool KeepAlive { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008EA RID: 2282
		// (set) Token: 0x060008EB RID: 2283
		string ConnectionGroupName { get; set; }
	}
}
