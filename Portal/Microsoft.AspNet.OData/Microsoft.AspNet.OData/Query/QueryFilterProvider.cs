using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000A6 RID: 166
	public class QueryFilterProvider : IFilterProvider
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00014934 File Offset: 0x00012B34
		public QueryFilterProvider(IActionFilter queryFilter)
		{
			if (queryFilter == null)
			{
				throw Error.ArgumentNull("queryFilter");
			}
			this.QueryFilter = queryFilter;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00014951 File Offset: 0x00012B51
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00014959 File Offset: 0x00012B59
		public IActionFilter QueryFilter { get; private set; }

		// Token: 0x060005CD RID: 1485 RVA: 0x00014964 File Offset: 0x00012B64
		public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor != null && (TypeHelper.IsIQueryable(actionDescriptor.ReturnType) || typeof(SingleResult).IsAssignableFrom(actionDescriptor.ReturnType)))
			{
				if (!actionDescriptor.GetParameters().Any((HttpParameterDescriptor parameter) => typeof(ODataQueryOptions).IsAssignableFrom(parameter.ParameterType)))
				{
					return new FilterInfo[]
					{
						new FilterInfo(this.QueryFilter, 0)
					};
				}
			}
			return Enumerable.Empty<FilterInfo>();
		}
	}
}
