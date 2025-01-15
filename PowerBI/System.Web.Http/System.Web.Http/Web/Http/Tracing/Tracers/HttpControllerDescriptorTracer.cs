using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200012C RID: 300
	internal class HttpControllerDescriptorTracer : HttpControllerDescriptor, IDecorator<HttpControllerDescriptor>
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00014137 File Offset: 0x00012337
		public HttpControllerDescriptorTracer(HttpControllerDescriptor innerDescriptor, ITraceWriter traceWriter)
		{
			base.Configuration = innerDescriptor.Configuration;
			base.ControllerName = innerDescriptor.ControllerName;
			base.ControllerType = innerDescriptor.ControllerType;
			this._innerDescriptor = innerDescriptor;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00014171 File Offset: 0x00012371
		public HttpControllerDescriptor Inner
		{
			get
			{
				return this._innerDescriptor;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00014179 File Offset: 0x00012379
		public override ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._innerDescriptor.Properties;
			}
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00014186 File Offset: 0x00012386
		public override Collection<T> GetCustomAttributes<T>()
		{
			return this._innerDescriptor.GetCustomAttributes<T>();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00014193 File Offset: 0x00012393
		public override Collection<T> GetCustomAttributes<T>(bool inherit)
		{
			return this._innerDescriptor.GetCustomAttributes<T>(inherit);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000141A1 File Offset: 0x000123A1
		public override Collection<IFilter> GetFilters()
		{
			return this._innerDescriptor.GetFilters();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000141B0 File Offset: 0x000123B0
		public override IHttpController CreateController(HttpRequestMessage request)
		{
			IHttpController controller = null;
			this._traceWriter.TraceBeginEnd(request, TraceCategories.ControllersCategory, TraceLevel.Info, this._innerDescriptor.GetType().Name, "CreateController", null, delegate
			{
				controller = this._innerDescriptor.CreateController(request);
			}, delegate(TraceRecord tr)
			{
				tr.Message = ((controller == null) ? SRResources.TraceNoneObjectMessage : HttpControllerTracer.ActualControllerType(controller).FullName);
			}, null);
			if (controller != null && !(controller is HttpControllerTracer))
			{
				return new HttpControllerTracer(request, controller, this._traceWriter);
			}
			return controller;
		}

		// Token: 0x04000223 RID: 547
		private const string CreateControllerMethodName = "CreateController";

		// Token: 0x04000224 RID: 548
		private readonly HttpControllerDescriptor _innerDescriptor;

		// Token: 0x04000225 RID: 549
		private readonly ITraceWriter _traceWriter;
	}
}
