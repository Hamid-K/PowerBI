using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	internal sealed class NonValidatingParameterBindingAttribute : ParameterBindingAttribute
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000031D8 File Offset: 0x000013D8
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			IEnumerable<MediaTypeFormatter> formatters = parameter.Configuration.Formatters;
			return new NonValidatingParameterBindingAttribute.NonValidatingParameterBinding(parameter, formatters);
		}

		// Token: 0x020001E8 RID: 488
		private sealed class NonValidatingParameterBinding : PerRequestParameterBinding
		{
			// Token: 0x06000FD4 RID: 4052 RVA: 0x0003FEFC File Offset: 0x0003E0FC
			public NonValidatingParameterBinding(HttpParameterDescriptor descriptor, IEnumerable<MediaTypeFormatter> formatters)
				: base(descriptor, formatters)
			{
			}

			// Token: 0x06000FD5 RID: 4053 RVA: 0x0003FF06 File Offset: 0x0003E106
			protected override HttpParameterBinding CreateInnerBinding(IEnumerable<MediaTypeFormatter> perRequestFormatters)
			{
				return ParameterBindingExtensions.BindWithFormatter(base.Descriptor, perRequestFormatters, null);
			}
		}
	}
}
