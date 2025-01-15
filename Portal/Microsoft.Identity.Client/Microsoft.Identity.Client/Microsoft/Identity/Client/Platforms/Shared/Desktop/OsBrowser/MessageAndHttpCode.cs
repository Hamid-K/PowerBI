using System;
using System.Net;

namespace Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser
{
	// Token: 0x02000185 RID: 389
	internal class MessageAndHttpCode
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x0003F9C8 File Offset: 0x0003DBC8
		public MessageAndHttpCode(HttpStatusCode httpCode, string message)
		{
			this.HttpCode = httpCode;
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this.Message = message;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0003F9ED File Offset: 0x0003DBED
		public HttpStatusCode HttpCode { get; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0003F9F5 File Offset: 0x0003DBF5
		public string Message { get; }
	}
}
