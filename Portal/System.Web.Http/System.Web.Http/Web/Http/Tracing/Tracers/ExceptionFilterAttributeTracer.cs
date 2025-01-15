using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000133 RID: 307
	internal class ExceptionFilterAttributeTracer : ExceptionFilterAttribute, IDecorator<ExceptionFilterAttribute>
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x000149DF File Offset: 0x00012BDF
		public ExceptionFilterAttributeTracer(ExceptionFilterAttribute innerFilter, ITraceWriter traceWriter)
		{
			this._innerFilter = innerFilter;
			this._traceStore = traceWriter;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000149F5 File Offset: 0x00012BF5
		public ExceptionFilterAttribute Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x000149FD File Offset: 0x00012BFD
		public override bool AllowMultiple
		{
			get
			{
				return this._innerFilter.AllowMultiple;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00014A0A File Offset: 0x00012C0A
		public override object TypeId
		{
			get
			{
				return this._innerFilter.TypeId;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00014A17 File Offset: 0x00012C17
		public override bool Equals(object obj)
		{
			return this._innerFilter.Equals(obj);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00014A25 File Offset: 0x00012C25
		public override int GetHashCode()
		{
			return this._innerFilter.GetHashCode();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00014A32 File Offset: 0x00012C32
		public override bool IsDefaultAttribute()
		{
			return this._innerFilter.IsDefaultAttribute();
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00014A3F File Offset: 0x00012C3F
		public override bool Match(object obj)
		{
			return this._innerFilter.Match(obj);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00005744 File Offset: 0x00003944
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00014A4D File Offset: 0x00012C4D
		public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			return this.OnExceptionAsyncCore(actionExecutedContext, cancellationToken, "OnExceptionAsync");
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00014A5C File Offset: 0x00012C5C
		private Task OnExceptionAsyncCore(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			ExceptionFilterAttributeTracer.<>c__DisplayClass15_0 CS$<>8__locals1 = new ExceptionFilterAttributeTracer.<>c__DisplayClass15_0();
			CS$<>8__locals1.actionExecutedContext = actionExecutedContext;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			return this._traceStore.TraceBeginEndAsync(CS$<>8__locals1.actionExecutedContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = CS$<>8__locals1.actionExecutedContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, delegate
			{
				ExceptionFilterAttributeTracer.<>c__DisplayClass15_0.<<OnExceptionAsyncCore>b__1>d <<OnExceptionAsyncCore>b__1>d;
				<<OnExceptionAsyncCore>b__1>d.<>4__this = CS$<>8__locals1;
				<<OnExceptionAsyncCore>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<OnExceptionAsyncCore>b__1>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<OnExceptionAsyncCore>b__1>d.<>t__builder;
				<>t__builder.Start<ExceptionFilterAttributeTracer.<>c__DisplayClass15_0.<<OnExceptionAsyncCore>b__1>d>(ref <<OnExceptionAsyncCore>b__1>d);
				return <<OnExceptionAsyncCore>b__1>d.<>t__builder.Task;
			}, delegate(TraceRecord tr)
			{
				Exception exception = CS$<>8__locals1.actionExecutedContext.Exception;
				tr.Level = ((exception == null) ? TraceLevel.Info : TraceLevel.Error);
				tr.Exception = exception;
				HttpResponseMessage response2 = CS$<>8__locals1.actionExecutedContext.Response;
				if (response2 != null)
				{
					tr.Status = response2.StatusCode;
				}
			}, delegate(TraceRecord tr)
			{
				HttpResponseMessage response3 = CS$<>8__locals1.actionExecutedContext.Response;
				if (response3 != null)
				{
					tr.Status = response3.StatusCode;
				}
			});
		}

		// Token: 0x04000238 RID: 568
		private readonly ExceptionFilterAttribute _innerFilter;

		// Token: 0x04000239 RID: 569
		private readonly ITraceWriter _traceStore;
	}
}
