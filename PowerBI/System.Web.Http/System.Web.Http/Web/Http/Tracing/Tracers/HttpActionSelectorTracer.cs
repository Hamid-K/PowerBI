using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200012A RID: 298
	internal class HttpActionSelectorTracer : IHttpActionSelector, IDecorator<IHttpActionSelector>
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x00013FB9 File Offset: 0x000121B9
		public HttpActionSelectorTracer(IHttpActionSelector innerSelector, ITraceWriter traceWriter)
		{
			this._innerSelector = innerSelector;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00013FCF File Offset: 0x000121CF
		public IHttpActionSelector Inner
		{
			get
			{
				return this._innerSelector;
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00013FD7 File Offset: 0x000121D7
		public ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor)
		{
			return this._innerSelector.GetActionMapping(controllerDescriptor);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00013FE8 File Offset: 0x000121E8
		HttpActionDescriptor IHttpActionSelector.SelectAction(HttpControllerContext controllerContext)
		{
			HttpActionDescriptor actionDescriptor = null;
			this._traceWriter.TraceBeginEnd(controllerContext.Request, TraceCategories.ActionCategory, TraceLevel.Info, this._innerSelector.GetType().Name, "SelectAction", null, delegate
			{
				actionDescriptor = this._innerSelector.SelectAction(controllerContext);
			}, delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceActionSelectedMessage, new object[] { FormattingUtilities.ActionDescriptorToString(actionDescriptor) });
			}, null);
			if (actionDescriptor != null && !(actionDescriptor is HttpActionDescriptorTracer))
			{
				return new HttpActionDescriptorTracer(controllerContext, actionDescriptor, this._traceWriter);
			}
			return actionDescriptor;
		}

		// Token: 0x0400021E RID: 542
		private const string SelectActionMethodName = "SelectAction";

		// Token: 0x0400021F RID: 543
		private readonly IHttpActionSelector _innerSelector;

		// Token: 0x04000220 RID: 544
		private readonly ITraceWriter _traceWriter;
	}
}
