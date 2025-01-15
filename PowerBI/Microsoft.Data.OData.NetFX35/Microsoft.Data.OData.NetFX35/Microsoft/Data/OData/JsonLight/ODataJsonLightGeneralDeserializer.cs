using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000116 RID: 278
	internal sealed class ODataJsonLightGeneralDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x00019035 File Offset: 0x00017235
		internal ODataJsonLightGeneralDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00019040 File Offset: 0x00017240
		public object ReadValue()
		{
			if (base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				return base.JsonReader.ReadPrimitiveValue();
			}
			if (base.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				return this.ReadAsComplexValue();
			}
			if (base.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				return this.ReadAsCollectionValue();
			}
			return null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00019094 File Offset: 0x00017294
		private ODataComplexValue ReadAsComplexValue()
		{
			base.JsonReader.ReadStartObject();
			List<ODataProperty> list = new List<ODataProperty>();
			while (base.JsonReader.NodeType != JsonNodeType.EndObject)
			{
				string text = base.JsonReader.ReadPropertyName();
				object obj = this.ReadValue();
				list.Add(new ODataProperty
				{
					Name = text,
					Value = obj
				});
			}
			base.JsonReader.ReadEndObject();
			return new ODataComplexValue
			{
				Properties = list,
				TypeName = null
			};
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00019114 File Offset: 0x00017314
		private ODataCollectionValue ReadAsCollectionValue()
		{
			base.JsonReader.ReadStartArray();
			List<object> list = new List<object>();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				object obj = this.ReadValue();
				list.Add(obj);
			}
			base.JsonReader.ReadEndArray();
			return new ODataCollectionValue
			{
				Items = list,
				TypeName = null
			};
		}
	}
}
