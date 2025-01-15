using System;
using System.Net;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000CA RID: 202
	internal class EwsHttpWebRequestFactory : IEwsHttpWebRequestFactory
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0001DD16 File Offset: 0x0001CD16
		IEwsHttpWebRequest IEwsHttpWebRequestFactory.CreateRequest(Uri uri)
		{
			return new EwsHttpWebRequest(uri);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001DD1E File Offset: 0x0001CD1E
		IEwsHttpWebResponse IEwsHttpWebRequestFactory.CreateExceptionResponse(WebException exception)
		{
			EwsUtilities.ValidateParam(exception, "exception");
			if (exception.Response == null)
			{
				throw new InvalidOperationException("The exception does not contain response.");
			}
			return new EwsHttpWebResponse(exception.Response as HttpWebResponse);
		}
	}
}
