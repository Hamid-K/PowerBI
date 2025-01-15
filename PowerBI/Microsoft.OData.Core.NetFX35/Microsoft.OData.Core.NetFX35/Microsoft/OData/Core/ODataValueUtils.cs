using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A6 RID: 422
	internal static class ODataValueUtils
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003640C File Offset: 0x0003460C
		internal static ODataValue ToODataValue(this object objectToConvert)
		{
			if (objectToConvert == null)
			{
				return new ODataNullValue();
			}
			ODataValue odataValue = objectToConvert as ODataValue;
			if (odataValue != null)
			{
				return odataValue;
			}
			return new ODataPrimitiveValue(objectToConvert);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00036434 File Offset: 0x00034634
		internal static object FromODataValue(this ODataValue odataValue)
		{
			if (odataValue is ODataNullValue)
			{
				return null;
			}
			ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				return odataPrimitiveValue.Value;
			}
			return odataValue;
		}
	}
}
