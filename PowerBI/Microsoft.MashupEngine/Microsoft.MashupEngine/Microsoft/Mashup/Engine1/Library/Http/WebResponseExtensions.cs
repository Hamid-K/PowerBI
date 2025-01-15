using System;
using System.Net;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000AA6 RID: 2726
	internal static class WebResponseExtensions
	{
		// Token: 0x06004C33 RID: 19507 RVA: 0x000FC014 File Offset: 0x000FA214
		public static WebResponse Wrap(this WebResponse webResponse)
		{
			HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
			if (httpWebResponse != null)
			{
				webResponse = new WrappingHttpWebResponse(httpWebResponse);
			}
			return webResponse;
		}
	}
}
