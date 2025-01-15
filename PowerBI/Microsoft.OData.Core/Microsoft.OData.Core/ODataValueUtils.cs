using System;

namespace Microsoft.OData
{
	// Token: 0x020000EB RID: 235
	internal static class ODataValueUtils
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001CCD8 File Offset: 0x0001AED8
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

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001CD28 File Offset: 0x0001AF28
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
