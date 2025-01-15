using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200012F RID: 303
	internal class AuthorizationFilterTracer : FilterTracer, IAuthorizationFilter, IFilter, IDecorator<IAuthorizationFilter>
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x00013E45 File Offset: 0x00012045
		public AuthorizationFilterTracer(IAuthorizationFilter innerFilter, ITraceWriter traceWriter)
			: base(innerFilter, traceWriter)
		{
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000144E6 File Offset: 0x000126E6
		public new IAuthorizationFilter Inner
		{
			get
			{
				return this.InnerAuthorizationFilter;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x000144EE File Offset: 0x000126EE
		private IAuthorizationFilter InnerAuthorizationFilter
		{
			get
			{
				return base.InnerFilter as IAuthorizationFilter;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000144FC File Offset: 0x000126FC
		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			return base.TraceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this.InnerAuthorizationFilter.GetType().Name, "ExecuteAuthorizationFilterAsync", null, () => this.InnerAuthorizationFilter.ExecuteAuthorizationFilterAsync(actionContext, cancellationToken, continuation), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x0400022D RID: 557
		private const string ExecuteAuthorizationFilterAsyncMethodName = "ExecuteAuthorizationFilterAsync";
	}
}
