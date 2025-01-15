using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000128 RID: 296
	internal class ActionFilterTracer : FilterTracer, IActionFilter, IFilter, IDecorator<IActionFilter>
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x00013E45 File Offset: 0x00012045
		public ActionFilterTracer(IActionFilter innerFilter, ITraceWriter traceWriter)
			: base(innerFilter, traceWriter)
		{
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00013E4F File Offset: 0x0001204F
		public new IActionFilter Inner
		{
			get
			{
				return this.InnerActionFilter;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00013E57 File Offset: 0x00012057
		private IActionFilter InnerActionFilter
		{
			get
			{
				return base.InnerFilter as IActionFilter;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00013E64 File Offset: 0x00012064
		Task<HttpResponseMessage> IActionFilter.ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			return base.TraceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this.InnerActionFilter.GetType().Name, "ExecuteActionFilterAsync", null, () => this.InnerActionFilter.ExecuteActionFilterAsync(actionContext, cancellationToken, continuation), delegate(TraceRecord tr, HttpResponseMessage response)
			{
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, null);
		}

		// Token: 0x0400021A RID: 538
		private const string ExecuteActionFilterAsyncMethodName = "ExecuteActionFilterAsync";
	}
}
