using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing.Conventions;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001DC RID: 476
	internal class WebApiControllerContext : IWebApiControllerContext
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x0003F7CC File Offset: 0x0003D9CC
		public WebApiControllerContext(HttpControllerContext controllerContext, SelectControllerResult controllerResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (controllerResult == null)
			{
				throw Error.ArgumentNull("controllerResult");
			}
			this.innerContext = controllerContext;
			this.ControllerResult = controllerResult;
			HttpRequestMessage request = controllerContext.Request;
			if (request != null)
			{
				this.Request = new WebApiRequestMessage(request);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003F81F File Offset: 0x0003DA1F
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0003F827 File Offset: 0x0003DA27
		public SelectControllerResult ControllerResult { get; private set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003F830 File Offset: 0x0003DA30
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0003F838 File Offset: 0x0003DA38
		public IWebApiRequestMessage Request { get; private set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003F841 File Offset: 0x0003DA41
		public IDictionary<string, object> RouteData
		{
			get
			{
				return this.innerContext.RouteData.Values;
			}
		}

		// Token: 0x0400044D RID: 1101
		private HttpControllerContext innerContext;
	}
}
