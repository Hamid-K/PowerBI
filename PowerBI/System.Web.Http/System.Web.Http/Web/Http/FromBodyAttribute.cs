using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;

namespace System.Web.Http
{
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class FromBodyAttribute : ParameterBindingAttribute
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00004E30 File Offset: 0x00003030
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			if (parameter == null)
			{
				throw Error.ArgumentNull("parameter");
			}
			IEnumerable<MediaTypeFormatter> formatters = parameter.Configuration.Formatters;
			IBodyModelValidator bodyModelValidator = parameter.Configuration.Services.GetBodyModelValidator();
			return parameter.BindWithFormatter(formatters, bodyModelValidator);
		}
	}
}
