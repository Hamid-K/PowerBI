using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000105 RID: 261
	public static class PrimitivePropertyConfigurationExtensions
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x00025E24 File Offset: 0x00024024
		public static PrimitivePropertyConfiguration AsDate(this PrimitivePropertyConfiguration property)
		{
			if (property == null)
			{
				throw Error.ArgumentNull("property");
			}
			if (!TypeHelper.IsDateTime(property.RelatedClrType))
			{
				throw Error.Argument("property", SRResources.MustBeDateTimeProperty, new object[]
				{
					property.PropertyInfo.Name,
					property.DeclaringType.FullName
				});
			}
			property.TargetEdmTypeKind = new EdmPrimitiveTypeKind?(EdmPrimitiveTypeKind.Date);
			return property;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00025E8C File Offset: 0x0002408C
		public static PrimitivePropertyConfiguration AsTimeOfDay(this PrimitivePropertyConfiguration property)
		{
			if (property == null)
			{
				throw Error.ArgumentNull("property");
			}
			if (!TypeHelper.IsTimeSpan(property.RelatedClrType))
			{
				throw Error.Argument("property", SRResources.MustBeTimeSpanProperty, new object[]
				{
					property.PropertyInfo.Name,
					property.DeclaringType.FullName
				});
			}
			property.TargetEdmTypeKind = new EdmPrimitiveTypeKind?(EdmPrimitiveTypeKind.TimeOfDay);
			return property;
		}
	}
}
