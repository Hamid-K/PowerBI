using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000116 RID: 278
	internal static class JsonSharedUtils
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x000264F1 File Offset: 0x000246F1
		internal static bool IsDoubleValueSerializedAsString(double value)
		{
			return double.IsInfinity(value) || double.IsNaN(value);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00026504 File Offset: 0x00024704
		internal static bool ValueTypeMatchesJsonType(ODataPrimitiveValue primitiveValue, IEdmPrimitiveTypeReference valueTypeReference)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = valueTypeReference.PrimitiveKind();
			if (edmPrimitiveTypeKind <= EdmPrimitiveTypeKind.Double)
			{
				if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Boolean)
				{
					if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Double)
					{
						return false;
					}
					double num = (double)primitiveValue.Value;
					return !JsonSharedUtils.IsDoubleValueSerializedAsString(num);
				}
			}
			else if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Int32 && edmPrimitiveTypeKind != EdmPrimitiveTypeKind.String)
			{
				return false;
			}
			return true;
		}
	}
}
