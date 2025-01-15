using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x02000033 RID: 51
	public abstract class DataServiceClientRequestMessage : IODataRequestMessage
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00008154 File Offset: 0x00006354
		public DataServiceClientRequestMessage(string actualMethod)
		{
			this.actualHttpMethod = actualMethod;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000194 RID: 404
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000195 RID: 405
		// (set) Token: 0x06000196 RID: 406
		public abstract Uri Url { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000197 RID: 407
		// (set) Token: 0x06000198 RID: 408
		public abstract string Method { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000199 RID: 409
		// (set) Token: 0x0600019A RID: 410
		public abstract ICredentials Credentials { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019B RID: 411
		// (set) Token: 0x0600019C RID: 412
		public abstract int Timeout { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019E RID: 414
		[SuppressMessage("Microsoft.Performance", "CA1811", Justification = "Make code very confusing and cumbersome to write code for various platforms. Hence suppressing the message")]
		public abstract bool SendChunked { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008163 File Offset: 0x00006363
		protected virtual string ActualMethod
		{
			get
			{
				return this.actualHttpMethod;
			}
		}

		// Token: 0x060001A0 RID: 416
		public abstract string GetHeader(string headerName);

		// Token: 0x060001A1 RID: 417
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x060001A2 RID: 418
		public abstract Stream GetStream();

		// Token: 0x060001A3 RID: 419
		public abstract void Abort();

		// Token: 0x060001A4 RID: 420
		public abstract IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state);

		// Token: 0x060001A5 RID: 421
		public abstract Stream EndGetRequestStream(IAsyncResult asyncResult);

		// Token: 0x060001A6 RID: 422
		public abstract IAsyncResult BeginGetResponse(AsyncCallback callback, object state);

		// Token: 0x060001A7 RID: 423
		public abstract IODataResponseMessage EndGetResponse(IAsyncResult asyncResult);

		// Token: 0x060001A8 RID: 424
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is intentionally a method.")]
		public abstract IODataResponseMessage GetResponse();

		// Token: 0x0400008A RID: 138
		private readonly string actualHttpMethod;
	}
}
