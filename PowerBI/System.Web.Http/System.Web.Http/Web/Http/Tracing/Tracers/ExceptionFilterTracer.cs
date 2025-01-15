using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000134 RID: 308
	internal class ExceptionFilterTracer : FilterTracer, IExceptionFilter, IFilter, IDecorator<IExceptionFilter>
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x00013E45 File Offset: 0x00012045
		public ExceptionFilterTracer(IExceptionFilter innerFilter, ITraceWriter traceWriter)
			: base(innerFilter, traceWriter)
		{
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00014AE1 File Offset: 0x00012CE1
		public new IExceptionFilter Inner
		{
			get
			{
				return this.InnerExceptionFilter;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00014AE9 File Offset: 0x00012CE9
		public IExceptionFilter InnerExceptionFilter
		{
			get
			{
				return base.InnerFilter as IExceptionFilter;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			return base.TraceWriter.TraceBeginEndAsync(actionExecutedContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this.InnerExceptionFilter.GetType().Name, "ExecuteExceptionFilterAsync", delegate(TraceRecord tr)
			{
				tr.Exception = actionExecutedContext.Exception;
			}, () => this.InnerExceptionFilter.ExecuteExceptionFilterAsync(actionExecutedContext, cancellationToken), delegate(TraceRecord tr)
			{
				tr.Exception = actionExecutedContext.Exception;
			}, null);
		}

		// Token: 0x0400023A RID: 570
		private const string ExecuteExceptionFilterAsyncMethodName = "ExecuteExceptionFilterAsync";
	}
}
