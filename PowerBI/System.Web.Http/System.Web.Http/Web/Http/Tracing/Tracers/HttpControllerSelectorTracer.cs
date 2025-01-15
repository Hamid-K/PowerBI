using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000132 RID: 306
	internal class HttpControllerSelectorTracer : IHttpControllerSelector, IDecorator<IHttpControllerSelector>
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x0001490C File Offset: 0x00012B0C
		public HttpControllerSelectorTracer(IHttpControllerSelector innerSelector, ITraceWriter traceWriter)
		{
			this._innerSelector = innerSelector;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00014922 File Offset: 0x00012B22
		public IHttpControllerSelector Inner
		{
			get
			{
				return this._innerSelector;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001492C File Offset: 0x00012B2C
		HttpControllerDescriptor IHttpControllerSelector.SelectController(HttpRequestMessage request)
		{
			HttpControllerDescriptor controllerDescriptor = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerSelector.GetType().Name, "SelectController", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceRouteMessage, new object[] { FormattingUtilities.RouteToString(request.GetRouteData()) });
			}, delegate
			{
				controllerDescriptor = this._innerSelector.SelectController(request);
			}, delegate(TraceRecord tr)
			{
				tr.Message = ((controllerDescriptor == null) ? SRResources.TraceNoneObjectMessage : controllerDescriptor.ControllerName);
			}, null);
			if (controllerDescriptor != null && !(controllerDescriptor is HttpControllerDescriptorTracer))
			{
				return new HttpControllerDescriptorTracer(controllerDescriptor, this._traceWriter);
			}
			return controllerDescriptor;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000149D2 File Offset: 0x00012BD2
		IDictionary<string, HttpControllerDescriptor> IHttpControllerSelector.GetControllerMapping()
		{
			return this._innerSelector.GetControllerMapping();
		}

		// Token: 0x04000235 RID: 565
		private const string SelectControllerMethodName = "SelectController";

		// Token: 0x04000236 RID: 566
		private readonly IHttpControllerSelector _innerSelector;

		// Token: 0x04000237 RID: 567
		private readonly ITraceWriter _traceWriter;
	}
}
