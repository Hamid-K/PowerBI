using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Internal
{
	// Token: 0x02000180 RID: 384
	internal static class HttpParameterBindingExtensions
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x00019C2C File Offset: 0x00017E2C
		public static bool WillReadUri(this HttpParameterBinding parameterBinding)
		{
			if (parameterBinding == null)
			{
				throw Error.ArgumentNull("parameterBinding");
			}
			IValueProviderParameterBinding valueProviderParameterBinding = parameterBinding as IValueProviderParameterBinding;
			if (valueProviderParameterBinding != null)
			{
				IEnumerable<ValueProviderFactory> valueProviderFactories = valueProviderParameterBinding.ValueProviderFactories;
				if (valueProviderFactories.Any<ValueProviderFactory>())
				{
					if (valueProviderFactories.All((ValueProviderFactory factory) => factory is IUriValueProviderFactory))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
