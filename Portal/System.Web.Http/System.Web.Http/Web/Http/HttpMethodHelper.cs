using System;
using System.Net.Http;

namespace System.Web.Http
{
	// Token: 0x02000010 RID: 16
	internal static class HttpMethodHelper
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003674 File Offset: 0x00001874
		internal static HttpMethod GetHttpMethod(string method)
		{
			if (string.IsNullOrEmpty(method))
			{
				return null;
			}
			if (string.Equals("GET", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Get;
			}
			if (string.Equals("POST", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Post;
			}
			if (string.Equals("PUT", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Put;
			}
			if (string.Equals("DELETE", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Delete;
			}
			if (string.Equals("HEAD", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Head;
			}
			if (string.Equals("OPTIONS", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Options;
			}
			if (string.Equals("TRACE", method, StringComparison.OrdinalIgnoreCase))
			{
				return HttpMethod.Trace;
			}
			return new HttpMethod(method);
		}
	}
}
