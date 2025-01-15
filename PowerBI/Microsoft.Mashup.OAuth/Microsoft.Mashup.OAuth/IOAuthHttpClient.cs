using System;
using System.Net;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000013 RID: 19
	public interface IOAuthHttpClient
	{
		// Token: 0x06000083 RID: 131
		WebRequest CreateRequest(Uri requestUri);

		// Token: 0x06000084 RID: 132
		HttpStatusCode GetResponseStatus(WebResponse response);
	}
}
