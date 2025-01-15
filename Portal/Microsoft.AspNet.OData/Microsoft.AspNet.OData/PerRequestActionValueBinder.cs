using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000012 RID: 18
	internal class PerRequestActionValueBinder : IActionValueBinder
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003200 File Offset: 0x00001400
		public PerRequestActionValueBinder(IActionValueBinder innerActionValueBinder)
		{
			if (innerActionValueBinder == null)
			{
				throw Error.ArgumentNull("innerActionValueBinder");
			}
			this._innerActionValueBinder = innerActionValueBinder;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003220 File Offset: 0x00001420
		public HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			HttpActionBinding binding = this._innerActionValueBinder.GetBinding(actionDescriptor);
			if (binding == null)
			{
				return null;
			}
			if (binding.ParameterBindings != null)
			{
				for (int i = 0; i < binding.ParameterBindings.Length; i++)
				{
					HttpParameterBinding httpParameterBinding = binding.ParameterBindings[i];
					if (httpParameterBinding != null && httpParameterBinding is FormatterParameterBinding)
					{
						binding.ParameterBindings[i] = new PerRequestParameterBinding(httpParameterBinding.Descriptor, actionDescriptor.Configuration.Formatters);
					}
				}
			}
			return binding;
		}

		// Token: 0x04000016 RID: 22
		private IActionValueBinder _innerActionValueBinder;
	}
}
