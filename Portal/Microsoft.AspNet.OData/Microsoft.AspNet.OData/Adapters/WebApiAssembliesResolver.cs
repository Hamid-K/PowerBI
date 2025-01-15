using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001DA RID: 474
	internal class WebApiAssembliesResolver : IWebApiAssembliesResolver
	{
		// Token: 0x06000F84 RID: 3972 RVA: 0x0003F673 File Offset: 0x0003D873
		public WebApiAssembliesResolver()
		{
			this.innerResolver = new DefaultAssembliesResolver();
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003F686 File Offset: 0x0003D886
		public WebApiAssembliesResolver(IAssembliesResolver resolver)
		{
			if (resolver == null)
			{
				throw Error.ArgumentNull("resolver");
			}
			this.innerResolver = resolver;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0003F6A3 File Offset: 0x0003D8A3
		public IEnumerable<Assembly> Assemblies
		{
			get
			{
				return this.innerResolver.GetAssemblies();
			}
		}

		// Token: 0x0400044A RID: 1098
		private IAssembliesResolver innerResolver;

		// Token: 0x0400044B RID: 1099
		public static IWebApiAssembliesResolver Default = new WebApiAssembliesResolver();
	}
}
