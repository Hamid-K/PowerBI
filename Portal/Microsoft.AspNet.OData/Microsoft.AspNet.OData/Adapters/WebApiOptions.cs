using System;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001DD RID: 477
	internal class WebApiOptions : IWebApiOptions
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003F853 File Offset: 0x0003DA53
		public WebApiOptions(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this.NullDynamicPropertyIsEnabled = configuration.HasEnabledNullDynamicProperty();
			this.UrlKeyDelimiter = configuration.GetUrlKeyDelimiter();
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0003F881 File Offset: 0x0003DA81
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x0003F889 File Offset: 0x0003DA89
		public ODataUrlKeyDelimiter UrlKeyDelimiter { get; private set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0003F892 File Offset: 0x0003DA92
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x0003F89A File Offset: 0x0003DA9A
		public bool NullDynamicPropertyIsEnabled { get; private set; }
	}
}
