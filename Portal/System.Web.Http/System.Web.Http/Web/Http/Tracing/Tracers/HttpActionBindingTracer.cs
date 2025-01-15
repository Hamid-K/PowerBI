using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000125 RID: 293
	internal class HttpActionBindingTracer : HttpActionBinding, IDecorator<HttpActionBinding>
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0001392C File Offset: 0x00011B2C
		public HttpActionBindingTracer(HttpActionBinding innerBinding, ITraceWriter traceWriter)
		{
			this._innerBinding = innerBinding;
			this._traceWriter = traceWriter;
			if (this._innerBinding.ParameterBindings != null)
			{
				base.ParameterBindings = this._innerBinding.ParameterBindings;
			}
			if (this._innerBinding.ActionDescriptor != null)
			{
				base.ActionDescriptor = this._innerBinding.ActionDescriptor;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00013989 File Offset: 0x00011B89
		public HttpActionBinding Inner
		{
			get
			{
				return this._innerBinding;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00013994 File Offset: 0x00011B94
		public override Task ExecuteBindingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(actionContext.ControllerContext.Request, TraceCategories.ModelBindingCategory, TraceLevel.Info, this._innerBinding.GetType().Name, "ExecuteBindingAsync", null, () => this._innerBinding.ExecuteBindingAsync(actionContext, cancellationToken), delegate(TraceRecord tr)
			{
				if (!actionContext.ModelState.IsValid)
				{
					tr.Message = Error.Format(SRResources.TraceModelStateInvalidMessage, new object[] { FormattingUtilities.ModelStateToString(actionContext.ModelState) });
					return;
				}
				if (actionContext.ActionDescriptor.GetParameters().Count > 0)
				{
					tr.Message = Error.Format(SRResources.TraceValidModelState, new object[] { FormattingUtilities.ActionArgumentsToString(actionContext.ActionArguments) });
				}
			}, null);
		}

		// Token: 0x04000212 RID: 530
		private const string ExecuteBindingAsyncMethodName = "ExecuteBindingAsync";

		// Token: 0x04000213 RID: 531
		private readonly HttpActionBinding _innerBinding;

		// Token: 0x04000214 RID: 532
		private readonly ITraceWriter _traceWriter;
	}
}
