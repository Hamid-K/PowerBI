using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x0200006A RID: 106
	public class HttpWebResponseMessage : IODataResponseMessage, IDisposable
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		public HttpWebResponseMessage(IDictionary<string, string> headers, int statusCode, Func<Stream> getResponseStream)
		{
			this.headers = new HeaderCollection(headers);
			this.statusCode = statusCode;
			this.getResponseStream = getResponseStream;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public HttpWebResponseMessage(HttpWebResponse httpResponse)
		{
			Util.CheckArgumentNull<HttpWebResponse>(httpResponse, "httpResponse");
			this.headers = new HeaderCollection(httpResponse.Headers);
			this.statusCode = (int)httpResponse.StatusCode;
			this.getResponseStream = new Func<Stream>(httpResponse.GetResponseStream);
			this.httpWebResponse = httpResponse;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000DD2A File Offset: 0x0000BF2A
		internal HttpWebResponseMessage(HeaderCollection headers, int statusCode, Func<Stream> getResponseStream)
		{
			this.headers = headers;
			this.statusCode = statusCode;
			this.getResponseStream = getResponseStream;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000DD47 File Offset: 0x0000BF47
		public virtual IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.headers.AsEnumerable();
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public HttpWebResponse Response
		{
			get
			{
				return this.httpWebResponse;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000DD5C File Offset: 0x0000BF5C
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000A08D File Offset: 0x0000828D
		public virtual int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public virtual string GetHeader(string headerName)
		{
			Util.CheckArgumentNullAndEmpty(headerName, "headerName");
			string text;
			if (this.headers.TryGetHeader(headerName, out text))
			{
				return text;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.Ordinal))
			{
				return "-1";
			}
			return null;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000A08D File Offset: 0x0000828D
		public virtual void SetHeader(string headerName, string headerValue)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		public virtual Stream GetStream()
		{
			return this.getResponseStream();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		protected virtual void Dispose(bool disposing)
		{
			HttpWebResponse httpWebResponse = this.httpWebResponse;
			this.httpWebResponse = null;
			if (httpWebResponse != null)
			{
				((IDisposable)httpWebResponse).Dispose();
			}
		}

		// Token: 0x04000128 RID: 296
		private readonly HeaderCollection headers;

		// Token: 0x04000129 RID: 297
		private readonly Func<Stream> getResponseStream;

		// Token: 0x0400012A RID: 298
		private readonly int statusCode;

		// Token: 0x0400012B RID: 299
		private HttpWebResponse httpWebResponse;
	}
}
