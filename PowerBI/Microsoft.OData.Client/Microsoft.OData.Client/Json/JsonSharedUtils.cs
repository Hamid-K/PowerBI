using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x02000006 RID: 6
	internal static class JsonSharedUtils
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000287E File Offset: 0x00000A7E
		internal static bool IsDoubleValueSerializedAsString(double value)
		{
			return double.IsInfinity(value) || double.IsNaN(value);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002890 File Offset: 0x00000A90
		internal static bool ValueTypeMatchesJsonType(ODataPrimitiveValue primitiveValue, IEdmPrimitiveTypeReference valueTypeReference)
		{
			return JsonSharedUtils.ValueTypeMatchesJsonType(primitiveValue, valueTypeReference.PrimitiveKind());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028A0 File Offset: 0x00000AA0
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
