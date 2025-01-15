using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200006E RID: 110
	internal class ODataValueProviderFactory : ValueProviderFactory, IUriValueProviderFactory
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			return new ODataValueProviderFactory.ODataValueProvider(actionContext.Request.ODataProperties().RoutingConventionsStore);
		}

		// Token: 0x02000208 RID: 520
		private class ODataValueProvider : NameValuePairsValueProvider
		{
			// Token: 0x06001035 RID: 4149 RVA: 0x00040572 File Offset: 0x0003E772
			public ODataValueProvider(IDictionary<string, object> routeData)
				: base(routeData, CultureInfo.InvariantCulture)
			{
			}
		}
	}
}
