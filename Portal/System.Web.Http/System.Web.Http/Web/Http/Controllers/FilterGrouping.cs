using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F9 RID: 249
	internal class FilterGrouping
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00010390 File Offset: 0x0000E590
		public FilterGrouping(IEnumerable<FilterInfo> filters)
		{
			List<FilterInfo> list = filters.ToList<FilterInfo>();
			List<FilterInfo> list2 = list.Where((FilterInfo f) => f.Instance is IOverrideFilter).ToList<FilterInfo>();
			FilterScope filterScope = FilterGrouping.SelectLastOverrideScope<IActionFilter>(list2);
			FilterScope filterScope2 = FilterGrouping.SelectLastOverrideScope<IAuthorizationFilter>(list2);
			FilterScope filterScope3 = FilterGrouping.SelectLastOverrideScope<IAuthenticationFilter>(list2);
			FilterScope filterScope4 = FilterGrouping.SelectLastOverrideScope<IExceptionFilter>(list2);
			this._actionFilters = FilterGrouping.SelectAvailable<IActionFilter>(list, filterScope);
			this._authorizationFilters = FilterGrouping.SelectAvailable<IAuthorizationFilter>(list, filterScope2);
			this._authenticationFilters = FilterGrouping.SelectAvailable<IAuthenticationFilter>(list, filterScope3);
			this._exceptionFilters = FilterGrouping.SelectAvailable<IExceptionFilter>(list, filterScope4);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00010425 File Offset: 0x0000E625
		public IActionFilter[] ActionFilters
		{
			get
			{
				return this._actionFilters;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001042D File Offset: 0x0000E62D
		public IAuthorizationFilter[] AuthorizationFilters
		{
			get
			{
				return this._authorizationFilters;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00010435 File Offset: 0x0000E635
		public IAuthenticationFilter[] AuthenticationFilters
		{
			get
			{
				return this._authenticationFilters;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001043D File Offset: 0x0000E63D
		public IExceptionFilter[] ExceptionFilters
		{
			get
			{
				return this._exceptionFilters;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00010448 File Offset: 0x0000E648
		private static T[] SelectAvailable<T>(List<FilterInfo> filters, FilterScope overrideFiltersBeforeScope)
		{
			return (from f in filters
				where f.Scope >= overrideFiltersBeforeScope && f.Instance is T
				select (T)((object)f.Instance)).ToArray<T>();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000104A0 File Offset: 0x0000E6A0
		private static FilterScope SelectLastOverrideScope<T>(List<FilterInfo> overrideFilters)
		{
			FilterInfo filterInfo = overrideFilters.Where((FilterInfo f) => ((IOverrideFilter)f.Instance).FiltersToOverride == typeof(T)).LastOrDefault<FilterInfo>();
			if (filterInfo == null)
			{
				return FilterScope.Global;
			}
			return filterInfo.Scope;
		}

		// Token: 0x0400019E RID: 414
		private IActionFilter[] _actionFilters;

		// Token: 0x0400019F RID: 415
		private IAuthorizationFilter[] _authorizationFilters;

		// Token: 0x040001A0 RID: 416
		private IAuthenticationFilter[] _authenticationFilters;

		// Token: 0x040001A1 RID: 417
		private IExceptionFilter[] _exceptionFilters;
	}
}
