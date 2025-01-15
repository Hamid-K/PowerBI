using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x02000219 RID: 537
	internal static class JsonSharedUtils
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x00043065 File Offset: 0x00041265
		internal static bool IsDoubleValueSerializedAsString(double value)
		{
			return double.IsInfinity(value) || double.IsNaN(value);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00043077 File Offset: 0x00041277
		internal static bool ValueTypeMatchesJsonType(ODataPrimitiveValue primitiveValue, IEdmPrimitiveTypeReference valueTypeReference)
		{
			return JsonSharedUtils.ValueTypeMatchesJsonType(primitiveValue, valueTypeReference.PrimitiveKind());
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00043088 File Offset: 0x00041288
		internal static bool ValueTypeMatchesJsonType(ODataPrimitiveValue primitiveValue, EdmPrimitiveTypeKind primitiveTypeKind)
		{
			if (primitiveTypeKind <= EdmPrimitiveTypeKind.Double)
			{
				if (primitiveTypeKind != EdmPrimitiveTypeKind.Boolean)
				{
					if (primitiveTypeKind != EdmPrimitiveTypeKind.Double)
					{
						return false;
					}
					double num = (double)primitiveValue.Value;
					return !JsonSharedUtils.IsDoubleValueSerializedAsString(num);
				}
			}
			else if (primitiveTypeKind != EdmPrimitiveTypeKind.Int32 && primitiveTypeKind != EdmPrimitiveTypeKind.String)
			{
				return false;
			}
			return true;
		}
	}
}
