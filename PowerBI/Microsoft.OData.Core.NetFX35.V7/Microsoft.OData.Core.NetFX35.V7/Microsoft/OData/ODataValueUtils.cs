using System;

namespace Microsoft.OData
{
	// Token: 0x020000A1 RID: 161
	internal static class ODataValueUtils
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x000105C4 File Offset: 0x0000E7C4
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
			if (objectToConvert.GetType().IsEnum())
			{
				return new ODataEnumValue(objectToConvert.ToString().Replace(", ", ","));
			}
			return new ODataPrimitiveValue(objectToConvert);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00010614 File Offset: 0x0000E814
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
