using System;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x02000009 RID: 9
	public interface IResponseHandler
	{
		// Token: 0x06000013 RID: 19
		bool TrySetCookie(string cookieName, string value, DateTime expiration);
	}
}
