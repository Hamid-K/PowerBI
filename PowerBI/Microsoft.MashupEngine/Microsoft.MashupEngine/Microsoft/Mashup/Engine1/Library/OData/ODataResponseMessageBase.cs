using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200074D RID: 1869
	internal abstract class ODataResponseMessageBase : IDisposable
	{
		// Token: 0x06003748 RID: 14152 RVA: 0x000B0D49 File Offset: 0x000AEF49
		protected ODataResponseMessageBase(HttpResponseData httpResponseData)
		{
			this.httpResponseData = httpResponseData;
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000B0D58 File Offset: 0x000AEF58
		public long ContentLength
		{
			get
			{
				return this.httpResponseData.ContentLength;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000B0D65 File Offset: 0x000AEF65
		public HashSet<string> ContentTypes
		{
			get
			{
				return this.httpResponseData.ContentTypes;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000B0D72 File Offset: 0x000AEF72
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.httpResponseData.Headers;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000B0D7F File Offset: 0x000AEF7F
		// (set) Token: 0x0600374D RID: 14157 RVA: 0x000B0D8C File Offset: 0x000AEF8C
		public int StatusCode
		{
			get
			{
				return this.httpResponseData.StatusCode;
			}
			set
			{
				this.httpResponseData.StatusCode = value;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000B0D9A File Offset: 0x000AEF9A
		public Uri ResponseUri
		{
			get
			{
				return this.httpResponseData.ResponseUri;
			}
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000B0DA8 File Offset: 0x000AEFA8
		public string GetHeader(string headerName)
		{
			string text;
			if (this.httpResponseData.Headers.TryGetValue(headerName, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000B0DCD File Offset: 0x000AEFCD
		public Stream GetStream()
		{
			return this.httpResponseData.Stream;
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000B0DDA File Offset: 0x000AEFDA
		public void SetHeader(string headerName, string headerValue)
		{
			this.httpResponseData.Headers[headerName] = headerValue;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000B0DEE File Offset: 0x000AEFEE
		public void Dispose()
		{
			this.httpResponseData.Dispose();
		}

		// Token: 0x04001C85 RID: 7301
		private HttpResponseData httpResponseData;
	}
}
