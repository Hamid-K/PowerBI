using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000191 RID: 401
	internal static class ODataValueExtensions
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x00034210 File Offset: 0x00032410
		public static object GetInnerValue(this ODataValue odataValue)
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
