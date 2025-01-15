using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x0200012B RID: 299
	internal class ActionValueBinderTracer : IActionValueBinder, IDecorator<IActionValueBinder>
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x0001408E File Offset: 0x0001228E
		public ActionValueBinderTracer(IActionValueBinder innerBinder, ITraceWriter traceWriter)
		{
			this._innerBinder = innerBinder;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000140A4 File Offset: 0x000122A4
		public IActionValueBinder Inner
		{
			get
			{
				return this._innerBinder;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000140AC File Offset: 0x000122AC
		HttpActionBinding IActionValueBinder.GetBinding(HttpActionDescriptor actionDescriptor)
		{
			HttpActionBinding binding = this._innerBinder.GetBinding(actionDescriptor);
			if (binding == null)
			{
				return null;
			}
			HttpParameterBinding[] parameterBindings = binding.ParameterBindings;
			HttpParameterBinding[] array = new HttpParameterBinding[parameterBindings.Length];
			for (int i = 0; i < array.Length; i++)
			{
				HttpParameterBinding httpParameterBinding = parameterBindings[i];
				FormatterParameterBinding formatterParameterBinding = httpParameterBinding as FormatterParameterBinding;
				array[i] = ((formatterParameterBinding != null) ? new FormatterParameterBindingTracer(formatterParameterBinding, this._traceWriter) : new HttpParameterBindingTracer(httpParameterBinding, this._traceWriter));
			}
			binding.ParameterBindings = array;
			if (!(binding is HttpActionBindingTracer))
			{
				return new HttpActionBindingTracer(binding, this._traceWriter);
			}
			return binding;
		}

		// Token: 0x04000221 RID: 545
		private readonly IActionValueBinder _innerBinder;

		// Token: 0x04000222 RID: 546
		private readonly ITraceWriter _traceWriter;
	}
}
