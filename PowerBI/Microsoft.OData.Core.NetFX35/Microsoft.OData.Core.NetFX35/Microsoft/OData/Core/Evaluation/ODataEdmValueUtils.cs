using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000087 RID: 135
	internal static class ODataEdmValueUtils
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00013B00 File Offset: 0x00011D00
		internal static IEdmPropertyValue GetEdmPropertyValue(this ODataProperty property, IEdmStructuredTypeReference declaringType)
		{
			IEdmTypeReference edmTypeReference = null;
			if (declaringType != null)
			{
				IEdmProperty edmProperty = declaringType.FindProperty(property.Name);
				if (edmProperty == null && !declaringType.IsOpen())
				{
					throw new ODataException(Strings.ODataEdmStructuredValue_UndeclaredProperty(property.Name, declaringType.FullName()));
				}
				edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			}
			return new EdmPropertyValue(property.Name, ODataEdmValueUtils.ConvertValue(property.Value, edmTypeReference).Value);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00013B6C File Offset: 0x00011D6C
		internal static IEdmDelayedValue ConvertValue(object value, IEdmTypeReference type)
		{
			if (value == null)
			{
				if (type != null)
				{
					return new ODataEdmNullValue(type);
				}
				return ODataEdmNullValue.UntypedInstance;
			}
			else
			{
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					return new ODataEdmStructuredValue(odataComplexValue);
				}
				ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					return new ODataEdmCollectionValue(odataCollectionValue);
				}
				return EdmValueUtils.ConvertPrimitiveValue(value, (type == null) ? null : type.AsPrimitive());
			}
		}
	}
}
