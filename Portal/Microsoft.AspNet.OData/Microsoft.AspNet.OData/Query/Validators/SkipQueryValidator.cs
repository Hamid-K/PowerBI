using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000DE RID: 222
	public class SkipQueryValidator
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		public virtual void Validate(SkipQueryOption skipQueryOption, ODataValidationSettings validationSettings)
		{
			if (skipQueryOption == null)
			{
				throw Error.ArgumentNull("skipQueryOption");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			int value = skipQueryOption.Value;
			int? maxSkip = validationSettings.MaxSkip;
			if ((value > maxSkip.GetValueOrDefault()) & (maxSkip != null))
			{
				throw new ODataException(Error.Format(SRResources.SkipTopLimitExceeded, new object[]
				{
					validationSettings.MaxSkip,
					AllowedQueryOptions.Skip,
					skipQueryOption.Value
				}));
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001AB79 File Offset: 0x00018D79
		internal static SkipQueryValidator GetSkipQueryValidator(ODataQueryContext context)
		{
			if (context == null || context.RequestContainer == null)
			{
				return new SkipQueryValidator();
			}
			return ServiceProviderServiceExtensions.GetRequiredService<SkipQueryValidator>(context.RequestContainer);
		}
	}
}
