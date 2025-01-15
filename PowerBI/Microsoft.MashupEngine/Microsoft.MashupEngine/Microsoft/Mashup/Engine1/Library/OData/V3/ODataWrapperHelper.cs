using System;
using Microsoft.Data.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008C8 RID: 2248
	internal static class ODataWrapperHelper
	{
		// Token: 0x06004054 RID: 16468 RVA: 0x000D6D70 File Offset: 0x000D4F70
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
			return value;
		}
	}
}
