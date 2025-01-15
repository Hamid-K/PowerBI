using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200012E RID: 302
	internal class AuthorizationFilterAttributeTracer : AuthorizationFilterAttribute, IDecorator<AuthorizationFilterAttribute>
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x000143DC File Offset: 0x000125DC
		public AuthorizationFilterAttributeTracer(AuthorizationFilterAttribute innerFilter, ITraceWriter traceWriter)
		{
			this._innerFilter = innerFilter;
			this._traceStore = traceWriter;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x000143F2 File Offset: 0x000125F2
		public AuthorizationFilterAttribute Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000143FA File Offset: 0x000125FA
		public override bool AllowMultiple
		{
			get
			{
				return this._innerFilter.AllowMultiple;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00014407 File Offset: 0x00012607
		public override object TypeId
		{
			get
			{
				return this._innerFilter.TypeId;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00014414 File Offset: 0x00012614
		public override bool Equals(object obj)
		{
			return this._innerFilter.Equals(obj);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00014422 File Offset: 0x00012622
		public override int GetHashCode()
		{
			return this._innerFilter.GetHashCode();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001442F File Offset: 0x0001262F
		public override bool IsDefaultAttribute()
		{
			return this._innerFilter.IsDefaultAttribute();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001443C File Offset: 0x0001263C
		public override bool Match(object obj)
		{
			return this._innerFilter.Match(obj);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00005744 File Offset: 0x00003944
		public override void OnAuthorization(HttpActionContext actionContext)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001444A File Offset: 0x0001264A
		public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this.OnAuthorizationSyncCore(actionContext, cancellationToken, "OnAuthorizationAsync");
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001445C File Offset: 0x0001265C
		private Task OnAuthorizationSyncCore(HttpActionContext actionContext, CancellationToken cancellationToken, [CallerMemberName] string methodName = null)
		{
			AuthorizationFilterAttributeTracer.<>c__DisplayClass15_0 CS$<>8__locals1 = new AuthorizationFilterAttributeTracer.<>c__DisplayClass15_0();
			CS$<>8__locals1.actionContext = actionContext;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			return this._traceStore.TraceBeginEndAsync(CS$<>8__locals1.actionContext.ControllerContext.Request, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, methodName, delegate(TraceRecord tr)
			{
				HttpResponseMessage response = CS$<>8__locals1.actionContext.Response;
				if (response != null)
				{
					tr.Status = response.StatusCode;
				}
			}, delegate
			{
				AuthorizationFilterAttributeTracer.<>c__DisplayClass15_0.<<OnAuthorizationSyncCore>b__1>d <<OnAuthorizationSyncCore>b__1>d;
				<<OnAuthorizationSyncCore>b__1>d.<>4__this = CS$<>8__locals1;
				<<OnAuthorizationSyncCore>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<OnAuthorizationSyncCore>b__1>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<OnAuthorizationSyncCore>b__1>d.<>t__builder;
				<>t__builder.Start<AuthorizationFilterAttributeTracer.<>c__DisplayClass15_0.<<OnAuthorizationSyncCore>b__1>d>(ref <<OnAuthorizationSyncCore>b__1>d);
				return <<OnAuthorizationSyncCore>b__1>d.<>t__builder.Task;
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

		// Token: 0x0400022B RID: 555
		private readonly AuthorizationFilterAttribute _innerFilter;

		// Token: 0x0400022C RID: 556
		private readonly ITraceWriter _traceStore;
	}
}
