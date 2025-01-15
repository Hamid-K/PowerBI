using System;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000101 RID: 257
	public interface IActionHttpMethodProvider
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006D0 RID: 1744
		Collection<HttpMethod> HttpMethods { get; }
	}
}
