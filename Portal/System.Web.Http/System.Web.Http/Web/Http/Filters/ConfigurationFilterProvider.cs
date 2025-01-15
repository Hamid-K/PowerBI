using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000CD RID: 205
	public class ConfigurationFilterProvider : IFilterProvider
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0000E0AB File Offset: 0x0000C2AB
		public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			return configuration.Filters;
		}
	}
}
