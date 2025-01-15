using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000013 RID: 19
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestHeadersExtensions
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002C9C File Offset: 0x00000E9C
		public static Collection<CookieHeaderValue> GetCookies(this HttpRequestHeaders headers)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			Collection<CookieHeaderValue> collection = new Collection<CookieHeaderValue>();
			IEnumerable<string> enumerable;
			if (headers.TryGetValues("Cookie", out enumerable))
			{
				using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CookieHeaderValue cookieHeaderValue;
						if (CookieHeaderValue.TryParse(enumerator.Current, out cookieHeaderValue))
						{
							collection.Add(cookieHeaderValue);
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D10 File Offset: 0x00000F10
		public static Collection<CookieHeaderValue> GetCookies(this HttpRequestHeaders headers, string name)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			Func<CookieState, bool> <>9__1;
			return new Collection<CookieHeaderValue>(headers.GetCookies().Where(delegate(CookieHeaderValue header)
			{
				IEnumerable<CookieState> cookies = header.Cookies;
				Func<CookieState, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (CookieState state) => string.Equals(state.Name, name, StringComparison.OrdinalIgnoreCase));
				}
				return cookies.Any(func);
			}).ToArray<CookieHeaderValue>());
		}

		// Token: 0x0400001F RID: 31
		private const string Cookie = "Cookie";
	}
}
