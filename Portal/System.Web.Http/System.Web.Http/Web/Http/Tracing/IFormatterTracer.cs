using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000117 RID: 279
	internal interface IFormatterTracer
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000766 RID: 1894
		HttpRequestMessage Request { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000767 RID: 1895
		MediaTypeFormatter InnerFormatter { get; }
	}
}
