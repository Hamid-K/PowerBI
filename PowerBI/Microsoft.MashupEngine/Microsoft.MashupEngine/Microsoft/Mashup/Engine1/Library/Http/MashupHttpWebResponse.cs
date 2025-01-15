using System;
using System.Net;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A6A RID: 2666
	internal abstract class MashupHttpWebResponse : WebResponse
	{
		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x06004A9C RID: 19100
		public abstract string CharacterSet { get; }

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06004A9D RID: 19101
		public abstract string ContentEncoding { get; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06004A9E RID: 19102
		// (set) Token: 0x06004A9F RID: 19103
		public abstract CookieCollection Cookies { get; set; }

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06004AA0 RID: 19104
		public abstract DateTime LastModified { get; }

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06004AA1 RID: 19105
		public abstract string Method { get; }

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06004AA2 RID: 19106
		public abstract Version ProtocolVersion { get; }

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06004AA3 RID: 19107
		public abstract string Server { get; }

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06004AA4 RID: 19108
		public abstract HttpStatusCode StatusCode { get; }

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06004AA5 RID: 19109
		public abstract string StatusDescription { get; }

		// Token: 0x06004AA6 RID: 19110
		public abstract string GetResponseHeader(string headerName);

		// Token: 0x06004AA7 RID: 19111
		public abstract void Buffer();
	}
}
