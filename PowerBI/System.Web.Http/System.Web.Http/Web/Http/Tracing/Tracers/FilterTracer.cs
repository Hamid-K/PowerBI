using System;
using System.Collections.Generic;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000135 RID: 309
	internal class FilterTracer : IFilter, IDecorator<IFilter>
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00014B76 File Offset: 0x00012D76
		public FilterTracer(IFilter innerFilter, ITraceWriter traceWriter)
		{
			this.InnerFilter = innerFilter;
			this.TraceWriter = traceWriter;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00014B8C File Offset: 0x00012D8C
		public IFilter Inner
		{
			get
			{
				return this.InnerFilter;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00014B94 File Offset: 0x00012D94
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x00014B9C File Offset: 0x00012D9C
		public IFilter InnerFilter { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00014BA5 File Offset: 0x00012DA5
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x00014BAD File Offset: 0x00012DAD
		public ITraceWriter TraceWriter { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00014BB6 File Offset: 0x00012DB6
		public bool AllowMultiple
		{
			get
			{
				return this.InnerFilter.AllowMultiple;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00014BC4 File Offset: 0x00012DC4
		public static IEnumerable<IFilter> CreateFilterTracers(IFilter filter, ITraceWriter traceWriter)
		{
			List<IFilter> list = new List<IFilter>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			ActionFilterAttribute actionFilterAttribute = filter as ActionFilterAttribute;
			if (actionFilterAttribute != null)
			{
				list.Add(new ActionFilterAttributeTracer(actionFilterAttribute, traceWriter));
				flag = true;
			}
			AuthorizationFilterAttribute authorizationFilterAttribute = filter as AuthorizationFilterAttribute;
			if (authorizationFilterAttribute != null)
			{
				list.Add(new AuthorizationFilterAttributeTracer(authorizationFilterAttribute, traceWriter));
				flag2 = true;
			}
			ExceptionFilterAttribute exceptionFilterAttribute = filter as ExceptionFilterAttribute;
			if (exceptionFilterAttribute != null)
			{
				list.Add(new ExceptionFilterAttributeTracer(exceptionFilterAttribute, traceWriter));
				flag3 = true;
			}
			IActionFilter actionFilter = filter as IActionFilter;
			if (actionFilter != null && !flag)
			{
				list.Add(new ActionFilterTracer(actionFilter, traceWriter));
			}
			IAuthorizationFilter authorizationFilter = filter as IAuthorizationFilter;
			if (authorizationFilter != null && !flag2)
			{
				list.Add(new AuthorizationFilterTracer(authorizationFilter, traceWriter));
			}
			IAuthenticationFilter authenticationFilter = filter as IAuthenticationFilter;
			if (authenticationFilter != null)
			{
				list.Add(new AuthenticationFilterTracer(authenticationFilter, traceWriter));
			}
			IExceptionFilter exceptionFilter = filter as IExceptionFilter;
			if (exceptionFilter != null && !flag3)
			{
				list.Add(new ExceptionFilterTracer(exceptionFilter, traceWriter));
			}
			IOverrideFilter overrideFilter = filter as IOverrideFilter;
			if (overrideFilter != null)
			{
				list.Add(new OverrideFilterTracer(overrideFilter, traceWriter));
			}
			if (list.Count == 0)
			{
				list.Add(new FilterTracer(filter, traceWriter));
			}
			return list;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00014CD4 File Offset: 0x00012ED4
		public static IEnumerable<FilterInfo> CreateFilterTracers(FilterInfo filter, ITraceWriter traceWriter)
		{
			IEnumerable<IFilter> enumerable = FilterTracer.CreateFilterTracers(filter.Instance, traceWriter);
			List<FilterInfo> list = new List<FilterInfo>();
			foreach (IFilter filter2 in enumerable)
			{
				list.Add(new FilterInfo(filter2, filter.Scope));
			}
			return list;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00014D3C File Offset: 0x00012F3C
		public static bool IsFilterTracer(IFilter filter)
		{
			return filter is FilterTracer || filter is ActionFilterAttributeTracer || filter is AuthorizationFilterAttributeTracer || filter is ExceptionFilterAttributeTracer;
		}
	}
}
