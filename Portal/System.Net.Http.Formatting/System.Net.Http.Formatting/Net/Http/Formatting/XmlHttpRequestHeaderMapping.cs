using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004F RID: 79
	public class XmlHttpRequestHeaderMapping : RequestHeaderMapping
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000A19C File Offset: 0x0000839C
		public XmlHttpRequestHeaderMapping()
			: base("x-requested-with", "XMLHttpRequest", StringComparison.OrdinalIgnoreCase, true, MediaTypeConstants.ApplicationJsonMediaType)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000A1B8 File Offset: 0x000083B8
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (request.Headers.Accept.Count == 0 || (request.Headers.Accept.Count == 1 && request.Headers.Accept.First<MediaTypeWithQualityHeaderValue>().MediaType.Equals("*/*", StringComparison.Ordinal)))
			{
				return base.TryMatchMediaType(request);
			}
			return 0.0;
		}
	}
}
