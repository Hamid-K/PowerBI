using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000131 RID: 305
	internal class HttpControllerActivatorTracer : IHttpControllerActivator, IDecorator<IHttpControllerActivator>
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00014837 File Offset: 0x00012A37
		public HttpControllerActivatorTracer(IHttpControllerActivator innerActivator, ITraceWriter traceWriter)
		{
			this._innerActivator = innerActivator;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001484D File Offset: 0x00012A4D
		public IHttpControllerActivator Inner
		{
			get
			{
				return this._innerActivator;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00014858 File Offset: 0x00012A58
		IHttpController IHttpControllerActivator.Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			IHttpController controller = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerActivator.GetType().Name, "Create", null, delegate
			{
				controller = this._innerActivator.Create(request, controllerDescriptor, controllerType);
			}, delegate(TraceRecord tr)
			{
				tr.Message = ((controller == null) ? SRResources.TraceNoneObjectMessage : controller.GetType().FullName);
			}, null);
			if (controller != null && !(controller is HttpControllerTracer))
			{
				controller = new HttpControllerTracer(request, controller, this._traceWriter);
			}
			return controller;
		}

		// Token: 0x04000232 RID: 562
		private const string CreateMethodName = "Create";

		// Token: 0x04000233 RID: 563
		private readonly IHttpControllerActivator _innerActivator;

		// Token: 0x04000234 RID: 564
		private readonly ITraceWriter _traceWriter;
	}
}
