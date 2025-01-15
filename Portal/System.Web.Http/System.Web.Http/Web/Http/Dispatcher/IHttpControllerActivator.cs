using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000080 RID: 128
	public interface IHttpControllerActivator
	{
		// Token: 0x06000336 RID: 822
		IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType);
	}
}
