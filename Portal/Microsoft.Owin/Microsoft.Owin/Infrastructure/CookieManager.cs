using System;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000033 RID: 51
	public class CookieManager : ICookieManager
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00005864 File Offset: 0x00003A64
		public string GetRequestCookie(IOwinContext context, string key)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			RequestCookieCollection requestCookies = context.Request.Cookies;
			return requestCookies[key];
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005894 File Offset: 0x00003A94
		public void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			context.Response.Cookies.Append(key, value, options);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000058C7 File Offset: 0x00003AC7
		public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			context.Response.Cookies.Delete(key, options);
		}
	}
}
