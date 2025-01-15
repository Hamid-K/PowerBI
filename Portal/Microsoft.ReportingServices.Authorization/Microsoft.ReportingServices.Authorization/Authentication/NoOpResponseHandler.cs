using System;

namespace Microsoft.ReportingServices.Authentication
{
	// Token: 0x0200000A RID: 10
	internal sealed class NoOpResponseHandler : IResponseHandler
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000214F File Offset: 0x0000034F
		public bool TrySetCookie(string cookieName, string value, DateTime expiration)
		{
			return true;
		}
	}
}
