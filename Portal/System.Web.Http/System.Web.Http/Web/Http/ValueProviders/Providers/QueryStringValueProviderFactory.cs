using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http.Controllers;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000046 RID: 70
	public class QueryStringValueProviderFactory : ValueProviderFactory, IUriValueProviderFactory
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00005FF8 File Offset: 0x000041F8
		public override IValueProvider GetValueProvider(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			IDictionary<string, object> properties = actionContext.Request.Properties;
			QueryStringValueProvider queryStringValueProvider;
			if (!properties.TryGetValue("{8572540D-3BD9-46DA-B112-A1E6C9086003}", out queryStringValueProvider))
			{
				queryStringValueProvider = new QueryStringValueProvider(actionContext, CultureInfo.InvariantCulture);
				properties["{8572540D-3BD9-46DA-B112-A1E6C9086003}"] = queryStringValueProvider;
			}
			return queryStringValueProvider;
		}

		// Token: 0x04000061 RID: 97
		private const string RequestLocalStorageKey = "{8572540D-3BD9-46DA-B112-A1E6C9086003}";
	}
}
