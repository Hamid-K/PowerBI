using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000D1 RID: 209
	public interface IFilterProvider
	{
		// Token: 0x0600057C RID: 1404
		IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor);
	}
}
