using System;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000034 RID: 52
	public interface ICookieManager
	{
		// Token: 0x06000201 RID: 513
		string GetRequestCookie(IOwinContext context, string key);

		// Token: 0x06000202 RID: 514
		void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options);

		// Token: 0x06000203 RID: 515
		void DeleteCookie(IOwinContext context, string key, CookieOptions options);
	}
}
