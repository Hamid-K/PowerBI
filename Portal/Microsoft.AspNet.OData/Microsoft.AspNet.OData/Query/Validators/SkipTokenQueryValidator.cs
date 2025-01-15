using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000D7 RID: 215
	public class SkipTokenQueryValidator
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0001897C File Offset: 0x00016B7C
		public virtual void Validate(SkipTokenQueryOption skipToken, ODataValidationSettings validationSettings)
		{
			if (skipToken == null)
			{
				throw Error.ArgumentNull("skipQueryOption");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			if (skipToken.Context != null && !skipToken.Context.DefaultQuerySettings.EnableSkipToken)
			{
				throw new ODataException(Error.Format(SRResources.NotAllowedQueryOption, new object[]
				{
					AllowedQueryOptions.SkipToken,
					"AllowedQueryOptions"
				}));
			}
		}
	}
}
