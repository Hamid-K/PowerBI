using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000012 RID: 18
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpResponseHeadersExtensions
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002C14 File Offset: 0x00000E14
		public static void AddCookies(this HttpResponseHeaders headers, IEnumerable<CookieHeaderValue> cookies)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (cookies == null)
			{
				throw Error.ArgumentNull("cookies");
			}
			foreach (CookieHeaderValue cookieHeaderValue in cookies)
			{
				if (cookieHeaderValue == null)
				{
					throw Error.Argument("cookies", Resources.CookieNull, new object[0]);
				}
				headers.TryAddWithoutValidation("Set-Cookie", cookieHeaderValue.ToString());
			}
		}

		// Token: 0x0400001E RID: 30
		private const string SetCookie = "Set-Cookie";
	}
}
