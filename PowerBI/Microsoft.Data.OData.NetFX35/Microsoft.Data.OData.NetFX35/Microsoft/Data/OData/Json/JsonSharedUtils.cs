using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000147 RID: 327
	internal static class JsonSharedUtils
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		internal static bool IsDoubleValueSerializedAsString(double value)
		{
			return double.IsInfinity(value) || double.IsNaN(value);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
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
