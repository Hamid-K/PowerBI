using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015F RID: 351
	internal static class ODataValueUtils
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
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

		// Token: 0x06000956 RID: 2390 RVA: 0x0001D210 File Offset: 0x0001B410
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
