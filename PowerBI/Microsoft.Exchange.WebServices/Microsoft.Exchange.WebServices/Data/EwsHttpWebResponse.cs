using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000CC RID: 204
	internal class EwsHttpWebResponse : IEwsHttpWebResponse, IDisposable
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0001DD56 File Offset: 0x0001CD56
		internal EwsHttpWebResponse(HttpWebResponse response)
		{
			this.response = response;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001DD65 File Offset: 0x0001CD65
		void IEwsHttpWebResponse.Close()
		{
			this.response.Close();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001DD72 File Offset: 0x0001CD72
		Stream IEwsHttpWebResponse.GetResponseStream()
		{
			return this.response.GetResponseStream();
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001DD7F File Offset: 0x0001CD7F
		string IEwsHttpWebResponse.ContentEncoding
		{
			get
			{
				return this.response.ContentEncoding;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001DD8C File Offset: 0x0001CD8C
		string IEwsHttpWebResponse.ContentType
		{
			get
			{
				return this.response.ContentType;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001DD99 File Offset: 0x0001CD99
		WebHeaderCollection IEwsHttpWebResponse.Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001DDA6 File Offset: 0x0001CDA6
		Uri IEwsHttpWebResponse.ResponseUri
		{
			get
			{
				return this.response.ResponseUri;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001DDB3 File Offset: 0x0001CDB3
		HttpStatusCode IEwsHttpWebResponse.StatusCode
		{
			get
			{
				return this.response.StatusCode;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001DDC0 File Offset: 0x0001CDC0
		string IEwsHttpWebResponse.StatusDescription
		{
			get
			{
				return this.response.StatusDescription;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001DDCD File Offset: 0x0001CDCD
		Version IEwsHttpWebResponse.ProtocolVersion
		{
			get
			{
				return this.response.ProtocolVersion;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001DDDA File Offset: 0x0001CDDA
		void IDisposable.Dispose()
		{
			this.response.Close();
		}

		// Token: 0x040002C4 RID: 708
		private HttpWebResponse response;
	}
}
