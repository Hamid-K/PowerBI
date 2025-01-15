using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000C9 RID: 201
	public class ActionDescriptorFilterProvider : IFilterProvider
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			IEnumerable<FilterInfo> enumerable = from instance in actionDescriptor.ControllerDescriptor.GetFilters()
				select new FilterInfo(instance, FilterScope.Controller);
			IEnumerable<FilterInfo> enumerable2 = from instance in actionDescriptor.GetFilters()
				select new FilterInfo(instance, FilterScope.Action);
			return enumerable.Concat(enumerable2);
		}
	}
}
