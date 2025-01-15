using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F8 RID: 248
	public interface IControllerConfiguration
	{
		// Token: 0x06000674 RID: 1652
		void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor);
	}
}
