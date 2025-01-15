using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001D9 RID: 473
	internal class WebApiActionMap : IWebApiActionMap
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003F648 File Offset: 0x0003D848
		public WebApiActionMap(ILookup<string, HttpActionDescriptor> actionMap)
		{
			if (actionMap == null)
			{
				throw Error.ArgumentNull("actionMap");
			}
			this.innerMap = actionMap;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003F665 File Offset: 0x0003D865
		public bool Contains(string name)
		{
			return this.innerMap.Contains(name);
		}

		// Token: 0x04000449 RID: 1097
		private ILookup<string, HttpActionDescriptor> innerMap;
	}
}
