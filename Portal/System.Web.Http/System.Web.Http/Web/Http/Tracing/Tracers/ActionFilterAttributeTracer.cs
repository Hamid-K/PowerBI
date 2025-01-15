using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000127 RID: 295
	internal class ActionFilterAttributeTracer : ActionFilterAttribute, IDecorator<ActionFilterAttribute>
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x00013CAD File Offset: 0x00011EAD
		public ActionFilterAttributeTracer(ActionFilterAttribute innerFilter, ITraceWriter traceWriter)
		{
			this._innerFilter = innerFilter;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00013CC3 File Offset: 0x00011EC3
		public ActionFilterAttribute Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00013CCB File Offset: 0x00011ECB
		public override bool AllowMultiple
		{
			get
			{
				return this._innerFilter.AllowMultiple;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public override object TypeId
		{
			get
			{
				return this._innerFilter.TypeId;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00013CE5 File Offset: 0x00011EE5
		public override bool Equals(object obj)
		{
			return this._innerFilter.Equals(obj);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00013CF3 File Offset: 0x00011EF3
		public override int GetHashCode()
		{
			return this._innerFilter.GetHashCode();
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00013D00 File Offset: 0x00011F00
		public override bool IsDefaultAttribute()
		{
			return this._innerFilter.IsDefaultAttribute();
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00013D0D File Offset: 0x00011F0D
		public override bool Match(object obj)
		{
			return this._innerFilter.Match(obj);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00005744 File Offset: 0x00003944
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00013D1B File Offset: 0x00011F1B
		public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			return this.OnActionExecutedAsyncCore(actionExecutedContext, cancellationToken, "OnActionExecutedAsync");
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00013D2C File Offset: 0x00011F2C
		private Task OnActionExecutedAsyncCore(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			ActionFilterAttributeTracer.<>c__DisplayClass15_0 CS$<>8__locals1 = new ActionFilterAttributeTracer.<>c__DisplayClass15_0();
			CS$<>8__locals1.actionExecutedContext = actionExecutedContext;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			return this._traceWriter.TraceBeginEndAsync(CS$<>8__locals1.actionExecutedContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionFilterMessage, new object[] { FormattingUtilities.ActionDescriptorToString(CS$<>8__locals1.actionExecutedContext.ActionContext.ActionDescriptor) });
				tr.Exception = CS$<>8__locals1.actionExecutedContext.Exception;
				HttpResponseMessage response = CS$<>8__locals1.actionExecutedContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, delegate
			{
				ActionFilterAttributeTracer.<>c__DisplayClass15_0.<<OnActionExecutedAsyncCore>b__1>d <<OnActionExecutedAsyncCore>b__1>d;
				<<OnActionExecutedAsyncCore>b__1>d.<>4__this = CS$<>8__locals1;
				<<OnActionExecutedAsyncCore>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<OnActionExecutedAsyncCore>b__1>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<OnActionExecutedAsyncCore>b__1>d.<>t__builder;
				<>t__builder.Start<ActionFilterAttributeTracer.<>c__DisplayClass15_0.<<OnActionExecutedAsyncCore>b__1>d>(ref <<OnActionExecutedAsyncCore>b__1>d);
				return <<OnActionExecutedAsyncCore>b__1>d.<>t__builder.Task;
			}, delegate(TraceRecord tr)
			{
				tr.Exception = CS$<>8__locals1.actionExecutedContext.Exception;
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

		// Token: 0x060007E0 RID: 2016 RVA: 0x00005744 File Offset: 0x00003944
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00013DB1 File Offset: 0x00011FB1
		public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this.OnActionExecutingAsyncCore(actionContext, cancellationToken, "OnActionExecutingAsync");
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00013DC0 File Offset: 0x00011FC0
		private Task OnActionExecutingAsyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			ActionFilterAttributeTracer.<>c__DisplayClass18_0 CS$<>8__locals1 = new ActionFilterAttributeTracer.<>c__DisplayClass18_0();
			CS$<>8__locals1.actionContext = actionContext;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			return this._traceWriter.TraceBeginEndAsync(CS$<>8__locals1.actionContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionFilterMessage, new object[] { FormattingUtilities.ActionDescriptorToString(CS$<>8__locals1.actionContext.ActionDescriptor) });
				HttpResponseMessage response = CS$<>8__locals1.actionContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, delegate
			{
				ActionFilterAttributeTracer.<>c__DisplayClass18_0.<<OnActionExecutingAsyncCore>b__1>d <<OnActionExecutingAsyncCore>b__1>d;
				<<OnActionExecutingAsyncCore>b__1>d.<>4__this = CS$<>8__locals1;
				<<OnActionExecutingAsyncCore>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<OnActionExecutingAsyncCore>b__1>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<OnActionExecutingAsyncCore>b__1>d.<>t__builder;
				<>t__builder.Start<ActionFilterAttributeTracer.<>c__DisplayClass18_0.<<OnActionExecutingAsyncCore>b__1>d>(ref <<OnActionExecutingAsyncCore>b__1>d);
				return <<OnActionExecutingAsyncCore>b__1>d.<>t__builder.Task;
			}, delegate(TraceRecord tr)
			{
				HttpResponseMessage response2 = CS$<>8__locals1.actionContext.Response;
				if (response2 != null)
				{
					tr.Status = response2.StatusCode;
				}
			}, delegate(TraceRecord tr)
			{
				HttpResponseMessage response3 = CS$<>8__locals1.actionContext.Response;
				if (response3 != null)
				{
					tr.Status = response3.StatusCode;
				}
			});
		}

		// Token: 0x04000218 RID: 536
		private readonly ActionFilterAttribute _innerFilter;

		// Token: 0x04000219 RID: 537
		private readonly ITraceWriter _traceWriter;
	}
}
