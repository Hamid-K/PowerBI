using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000084 RID: 132
	public interface IHttpControllerSelector
	{
		// Token: 0x0600034E RID: 846
		HttpControllerDescriptor SelectController(HttpRequestMessage request);

		// Token: 0x0600034F RID: 847
		IDictionary<string, HttpControllerDescriptor> GetControllerMapping();
	}
}
