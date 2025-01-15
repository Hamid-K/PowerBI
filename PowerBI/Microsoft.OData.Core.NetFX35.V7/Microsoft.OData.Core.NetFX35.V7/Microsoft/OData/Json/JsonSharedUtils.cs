using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E7 RID: 487
	internal static class JsonSharedUtils
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x00037229 File Offset: 0x00035429
		internal static bool IsDoubleValueSerializedAsString(double value)
		{
			return double.IsInfinity(value) || double.IsNaN(value);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003723B File Offset: 0x0003543B
		internal static bool ValueTypeMatchesJsonType(ODataPrimitiveValue primitiveValue, IEdmPrimitiveTypeReference valueTypeReference)
		{
			return JsonSharedUtils.ValueTypeMatchesJsonType(primitiveValue, valueTypeReference.PrimitiveKind());
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0003724C File Offset: 0x0003544C
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
