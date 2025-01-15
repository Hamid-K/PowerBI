using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000DF RID: 223
	public class TopQueryValidator
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0001AB98 File Offset: 0x00018D98
		public virtual void Validate(TopQueryOption topQueryOption, ODataValidationSettings validationSettings)
		{
			if (topQueryOption == null)
			{
				throw Error.ArgumentNull("topQueryOption");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			int value = topQueryOption.Value;
			int? maxTop = validationSettings.MaxTop;
			if ((value > maxTop.GetValueOrDefault()) & (maxTop != null))
			{
				throw new ODataException(Error.Format(SRResources.SkipTopLimitExceeded, new object[]
				{
					validationSettings.MaxTop,
					AllowedQueryOptions.Top,
					topQueryOption.Value
				}));
			}
			IEdmProperty targetProperty = topQueryOption.Context.TargetProperty;
			IEdmStructuredType targetStructuredType = topQueryOption.Context.TargetStructuredType;
			int num;
			if (EdmLibHelpers.IsTopLimitExceeded(targetProperty, targetStructuredType, topQueryOption.Context.Model, topQueryOption.Value, topQueryOption.Context.DefaultQuerySettings, out num))
			{
				throw new ODataException(Error.Format(SRResources.SkipTopLimitExceeded, new object[]
				{
					num,
					AllowedQueryOptions.Top,
					topQueryOption.Value
				}));
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001AC91 File Offset: 0x00018E91
		internal static TopQueryValidator GetTopQueryValidator(ODataQueryContext context)
		{
			if (context == null || context.RequestContainer == null)
			{
				return new TopQueryValidator();
			}
			return ServiceProviderServiceExtensions.GetRequiredService<TopQueryValidator>(context.RequestContainer);
		}
	}
}
