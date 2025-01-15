using System;
using System.Net;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000C9 RID: 201
	internal interface IEwsHttpWebRequestFactory
	{
		// Token: 0x06000913 RID: 2323
		IEwsHttpWebRequest CreateRequest(Uri uri);

		// Token: 0x06000914 RID: 2324
		IEwsHttpWebResponse CreateExceptionResponse(WebException exception);
	}
}
