using System;
using System.Collections.Generic;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000041 RID: 65
	public interface IContentNegotiator
	{
		// Token: 0x06000273 RID: 627
		ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters);
	}
}
