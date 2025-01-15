using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000044 RID: 68
	public class RouteDataValueProviderFactory : ValueProviderFactory, IUriValueProviderFactory
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00005F88 File Offset: 0x00004188
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IDictionary<string, object> properties = actionContext.Request.Properties;
			RouteDataValueProvider routeDataValueProvider;
			if (!properties.TryGetValue("{C0E50671-A1D4-429E-93C9-2AA63779924F}", out routeDataValueProvider))
			{
				routeDataValueProvider = new RouteDataValueProvider(actionContext, CultureInfo.InvariantCulture);
				properties["{C0E50671-A1D4-429E-93C9-2AA63779924F}"] = routeDataValueProvider;
			}
			return routeDataValueProvider;
		}

		// Token: 0x04000060 RID: 96
		private const string RequestLocalStorageKey = "{C0E50671-A1D4-429E-93C9-2AA63779924F}";
	}
}
