using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000DC RID: 220
	public class ODataQueryValidator
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001A6D8 File Offset: 0x000188D8
		public virtual void Validate(ODataQueryOptions options, ODataValidationSettings validationSettings)
		{
			if (options == null)
			{
				throw Error.ArgumentNull("options");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			if (options.Apply != null && options.Apply.ApplyClause != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Apply, validationSettings.AllowedQueryOptions);
			}
			if (options.Skip != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Skip, validationSettings.AllowedQueryOptions);
				options.Skip.Validate(validationSettings);
			}
			if (options.Top != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Top, validationSettings.AllowedQueryOptions);
				options.Top.Validate(validationSettings);
			}
			if (options.OrderBy != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.OrderBy, validationSettings.AllowedQueryOptions);
				options.OrderBy.Validate(validationSettings);
			}
			if (options.Filter != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Filter, validationSettings.AllowedQueryOptions);
				options.Filter.Validate(validationSettings);
			}
			if (options.Count != null || options.InternalRequest.IsCountRequest())
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Count, validationSettings.AllowedQueryOptions);
				if (options.Count != null)
				{
					options.Count.Validate(validationSettings);
				}
			}
			if (options.SkipToken != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.SkipToken, validationSettings.AllowedQueryOptions);
				options.SkipToken.Validate(validationSettings);
			}
			if (options.RawValues.Expand != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Expand, validationSettings.AllowedQueryOptions);
			}
			if (options.RawValues.Select != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Select, validationSettings.AllowedQueryOptions);
			}
			if (options.SelectExpand != null)
			{
				options.SelectExpand.Validate(validationSettings);
			}
			if (options.RawValues.Format != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.Format, validationSettings.AllowedQueryOptions);
			}
			if (options.RawValues.SkipToken != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.SkipToken, validationSettings.AllowedQueryOptions);
			}
			if (options.RawValues.DeltaToken != null)
			{
				ODataQueryValidator.ValidateQueryOptionAllowed(AllowedQueryOptions.DeltaToken, validationSettings.AllowedQueryOptions);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001A89F File Offset: 0x00018A9F
		internal static ODataQueryValidator GetODataQueryValidator(ODataQueryContext context)
		{
			if (context == null || context.RequestContainer == null)
			{
				return new ODataQueryValidator();
			}
			return ServiceProviderServiceExtensions.GetRequiredService<ODataQueryValidator>(context.RequestContainer);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001A8BD File Offset: 0x00018ABD
		private static void ValidateQueryOptionAllowed(AllowedQueryOptions queryOption, AllowedQueryOptions allowed)
		{
			if ((queryOption & allowed) == AllowedQueryOptions.None)
			{
				throw new ODataException(Error.Format(SRResources.NotAllowedQueryOption, new object[] { queryOption, "AllowedQueryOptions" }));
			}
		}
	}
}
