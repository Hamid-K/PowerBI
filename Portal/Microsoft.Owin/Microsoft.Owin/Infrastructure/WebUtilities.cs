using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x0200003E RID: 62
	public static class WebUtilities
	{
		// Token: 0x06000247 RID: 583 RVA: 0x000064E4 File Offset: 0x000046E4
		public static string AddQueryString(string uri, string queryString)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (string.IsNullOrEmpty(queryString))
			{
				return uri;
			}
			return uri + ((uri.IndexOf('?') != -1) ? "&" : "?") + queryString;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006530 File Offset: 0x00004730
		public static string AddQueryString(string uri, string name, string value)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			bool hasQuery = uri.IndexOf('?') != -1;
			return string.Concat(new string[]
			{
				uri,
				hasQuery ? "&" : "?",
				Uri.EscapeDataString(name),
				"=",
				Uri.EscapeDataString(value)
			});
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000065B4 File Offset: 0x000047B4
		public static string AddQueryString(string uri, IDictionary<string, string> queryString)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (queryString == null)
			{
				throw new ArgumentNullException("queryString");
			}
			StringBuilder sb = new StringBuilder();
			sb.Append(uri);
			bool hasQuery = uri.IndexOf('?') != -1;
			foreach (KeyValuePair<string, string> parameter in queryString)
			{
				sb.Append(hasQuery ? '&' : '?');
				sb.Append(Uri.EscapeDataString(parameter.Key));
				sb.Append('=');
				sb.Append(Uri.EscapeDataString(parameter.Value));
				hasQuery = true;
			}
			return sb.ToString();
		}
	}
}
