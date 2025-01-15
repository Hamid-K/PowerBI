using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F4 RID: 244
	public static class ParameterBindingExtensions
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0001011F File Offset: 0x0000E31F
		public static HttpParameterBinding BindAsError(this HttpParameterDescriptor parameter, string message)
		{
			return new ErrorParameterBinding(parameter, message);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00010128 File Offset: 0x0000E328
		public static HttpParameterBinding BindWithAttribute(this HttpParameterDescriptor parameter, ParameterBindingAttribute attribute)
		{
			return attribute.GetBinding(parameter);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00010131 File Offset: 0x0000E331
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter)
		{
			return parameter.BindWithAttribute(new ModelBinderAttribute());
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00010140 File Offset: 0x0000E340
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, IModelBinder binder)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IEnumerable<ValueProviderFactory> valueProviderFactories = new ModelBinderAttribute().GetValueProviderFactories(configuration);
			return parameter.BindWithModelBinding(binder, valueProviderFactories);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00010168 File Offset: 0x0000E368
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, params ValueProviderFactory[] valueProviderFactories)
		{
			return parameter.BindWithModelBinding(valueProviderFactories);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00010174 File Offset: 0x0000E374
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, IEnumerable<ValueProviderFactory> valueProviderFactories)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IModelBinder modelBinder = new ModelBinderAttribute().GetModelBinder(configuration, parameter.ParameterType);
			return new ModelBinderParameterBinding(parameter, modelBinder, valueProviderFactories);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000101A2 File Offset: 0x0000E3A2
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, IModelBinder binder, IEnumerable<ValueProviderFactory> valueProviderFactories)
		{
			return new ModelBinderParameterBinding(parameter, binder, valueProviderFactories);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000101AC File Offset: 0x0000E3AC
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
			IBodyModelValidator bodyModelValidator = configuration.Services.GetBodyModelValidator();
			return new FormatterParameterBinding(parameter, formatters, bodyModelValidator);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000101D9 File Offset: 0x0000E3D9
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter, params MediaTypeFormatter[] formatters)
		{
			return parameter.BindWithFormatter(formatters);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter, IEnumerable<MediaTypeFormatter> formatters)
		{
			IBodyModelValidator bodyModelValidator = parameter.Configuration.Services.GetBodyModelValidator();
			return new FormatterParameterBinding(parameter, formatters, bodyModelValidator);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001020A File Offset: 0x0000E40A
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter, IEnumerable<MediaTypeFormatter> formatters, IBodyModelValidator bodyModelValidator)
		{
			return new FormatterParameterBinding(parameter, formatters, bodyModelValidator);
		}
	}
}
