using System;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x020000DE RID: 222
	public class SendingRequestEventArgs : EventArgs
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001E99E File Offset: 0x0001CB9E
		internal SendingRequestEventArgs(WebRequest request, WebHeaderCollection requestHeaders)
		{
			this.request = request;
			this.requestHeaders = requestHeaders;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		// (set) Token: 0x06000774 RID: 1908 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		public WebRequest Request
		{
			get
			{
				return this.request;
			}
			set
			{
				Util.CheckArgumentNull<WebRequest>(value, "value");
				if (!(value is HttpWebRequest))
				{
					throw Error.Argument(Strings.Context_SendingRequestEventArgsNotHttp, "value");
				}
				this.request = value;
				this.requestHeaders = value.Headers;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001E9F5 File Offset: 0x0001CBF5
		public WebHeaderCollection RequestHeaders
		{
			get
			{
				return this.requestHeaders;
			}
		}

		// Token: 0x04000358 RID: 856
		private WebRequest request;

		// Token: 0x04000359 RID: 857
		private WebHeaderCollection requestHeaders;
	}
}
