using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class FromODataUriAttribute : ModelBinderAttribute
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000317C File Offset: 0x0000137C
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			if (parameter == null)
			{
				throw Error.ArgumentNull("parameter");
			}
			IModelBinder binder = FromODataUriAttribute._provider.GetBinder(parameter.Configuration, parameter.ParameterType);
			IEnumerable<ValueProviderFactory> valueProviderFactories = this.GetValueProviderFactories(parameter.Configuration);
			return new ModelBinderParameterBinding(parameter, binder, valueProviderFactories);
		}

		// Token: 0x04000015 RID: 21
		private static readonly ModelBinderProvider _provider = new ODataModelBinderProvider();
	}
}
