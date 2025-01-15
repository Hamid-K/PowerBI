using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000D8 RID: 216
	public class CountQueryValidator
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x000189EA File Offset: 0x00016BEA
		public CountQueryValidator(DefaultQuerySettings defaultQuerySettings)
		{
			this._defaultQuerySettings = defaultQuerySettings;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000189FC File Offset: 0x00016BFC
		public virtual void Validate(CountQueryOption countQueryOption, ODataValidationSettings validationSettings)
		{
			if (countQueryOption == null)
			{
				throw Error.ArgumentNull("countQueryOption");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			ODataPath path = countQueryOption.Context.Path;
			if (path != null && path.Segments.Count > 0)
			{
				IEdmProperty targetProperty = countQueryOption.Context.TargetProperty;
				IEdmStructuredType targetStructuredType = countQueryOption.Context.TargetStructuredType;
				string targetName = countQueryOption.Context.TargetName;
				if (EdmLibHelpers.IsNotCountable(targetProperty, targetStructuredType, countQueryOption.Context.Model, this._defaultQuerySettings.EnableCount))
				{
					if (targetProperty == null)
					{
						throw new InvalidOperationException(Error.Format(SRResources.NotCountableEntitySetUsedForCount, new object[] { targetName }));
					}
					throw new InvalidOperationException(Error.Format(SRResources.NotCountablePropertyUsedForCount, new object[] { targetName }));
				}
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00018ABF File Offset: 0x00016CBF
		internal static CountQueryValidator GetCountQueryValidator(ODataQueryContext context)
		{
			if (context == null)
			{
				return new CountQueryValidator(new DefaultQuerySettings());
			}
			if (context.RequestContainer != null)
			{
				return ServiceProviderServiceExtensions.GetRequiredService<CountQueryValidator>(context.RequestContainer);
			}
			return new CountQueryValidator(context.DefaultQuerySettings);
		}

		// Token: 0x0400022A RID: 554
		private readonly DefaultQuerySettings _defaultQuerySettings;
	}
}
