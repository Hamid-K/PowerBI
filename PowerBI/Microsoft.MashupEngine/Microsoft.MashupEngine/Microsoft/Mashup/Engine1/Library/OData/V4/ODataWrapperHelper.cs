using System;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200086F RID: 2159
	internal static class ODataWrapperHelper
	{
		// Token: 0x06003E35 RID: 15925 RVA: 0x000CB648 File Offset: 0x000C9848
		public static object WrapValueIfNecessary(object value)
		{
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return new ODataComplexValueWrapper(odataComplexValue);
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return new ODataCollectionValueWrapper(odataCollectionValue);
			}
			ODataStreamReferenceValue odataStreamReferenceValue = value as ODataStreamReferenceValue;
			if (odataStreamReferenceValue != null)
			{
				return new ODataStreamReferenceValueWrapper(odataStreamReferenceValue);
			}
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return new ODataEnumValueWrapper(odataEnumValue);
			}
			return value;
		}
	}
}
