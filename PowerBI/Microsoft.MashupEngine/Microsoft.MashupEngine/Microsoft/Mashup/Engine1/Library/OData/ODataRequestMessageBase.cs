using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Http;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200074C RID: 1868
	internal abstract class ODataRequestMessageBase
	{
		// Token: 0x0600373F RID: 14143 RVA: 0x000B0B8E File Offset: 0x000AED8E
		public ODataRequestMessageBase(MashupHttpWebRequest httpWebRequest)
		{
			this.httpWebRequest = httpWebRequest;
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000B0BA0 File Offset: 0x000AEDA0
		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[this.httpWebRequest.Headers.Count];
				string[] allKeys = this.httpWebRequest.Headers.AllKeys;
				for (int i = 0; i < allKeys.Length; i++)
				{
					string text = allKeys[i];
					string text2 = this.httpWebRequest.Headers[text];
					array[i] = new KeyValuePair<string, string>(text, text2);
				}
				return array;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000B0C09 File Offset: 0x000AEE09
		// (set) Token: 0x06003742 RID: 14146 RVA: 0x000B0C11 File Offset: 0x000AEE11
		public string Method { get; set; }

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000B0C1A File Offset: 0x000AEE1A
		// (set) Token: 0x06003744 RID: 14148 RVA: 0x000B0C22 File Offset: 0x000AEE22
		public Uri Url { get; set; }

		// Token: 0x06003745 RID: 14149 RVA: 0x000B0C2C File Offset: 0x000AEE2C
		public string GetHeader(string headerName)
		{
			if (string.Equals(headerName, "Accept", StringComparison.OrdinalIgnoreCase))
			{
				return this.httpWebRequest.Accept;
			}
			if (string.Equals(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				return this.httpWebRequest.ContentType;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				return this.httpWebRequest.ContentLength.ToString(CultureInfo.InvariantCulture);
			}
			return this.httpWebRequest.Headers[headerName];
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000B0CA5 File Offset: 0x000AEEA5
		public Stream GetStream()
		{
			return this.httpWebRequest.GetRequestStream();
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000B0CB4 File Offset: 0x000AEEB4
		public void SetHeader(string headerName, string headerValue)
		{
			if (string.Equals(headerName, "Accept", StringComparison.OrdinalIgnoreCase))
			{
				this.httpWebRequest.Accept = headerValue;
				return;
			}
			if (string.Equals(headerName, "Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				this.httpWebRequest.ContentType = headerValue;
				return;
			}
			if (string.Equals(headerName, "Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				this.httpWebRequest.ContentLength = long.Parse(headerValue, CultureInfo.InvariantCulture);
				return;
			}
			if (string.Equals(headerName, "User-Agent", StringComparison.OrdinalIgnoreCase))
			{
				this.httpWebRequest.UserAgent = headerValue;
				return;
			}
			this.httpWebRequest.Headers[headerName] = headerValue;
		}

		// Token: 0x04001C82 RID: 7298
		private readonly MashupHttpWebRequest httpWebRequest;
	}
}
