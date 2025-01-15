using System;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000796 RID: 1942
	internal static class ODataWrapperHelper
	{
		// Token: 0x060038F9 RID: 14585 RVA: 0x000B7894 File Offset: 0x000B5A94
		public static object WrapValueIfNecessary(object value)
		{
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
