using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.Spatial;

namespace Microsoft.OData.Json
{
	// Token: 0x020001EC RID: 492
	internal static class ODataJsonReaderCoreUtils
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x00037D78 File Offset: 0x00035F78
		internal static ISpatial ReadSpatialValue(IJsonReader jsonReader, bool insideJsonObjectValue, ODataInputContext inputContext, IEdmPrimitiveTypeReference expectedValueTypeReference, bool validateNullValue, int recursionDepth, string propertyName)
		{
			if (!insideJsonObjectValue && ODataJsonReaderCoreUtils.TryReadNullValue(jsonReader, inputContext, expectedValueTypeReference, validateNullValue, propertyName, default(bool?)))
			{
				return null;
			}
			ISpatial spatial = null;
			if (insideJsonObjectValue || jsonReader.NodeType == JsonNodeType.StartObject)
			{
				IDictionary<string, object> dictionary = ODataJsonReaderCoreUtils.ReadObjectValue(jsonReader, insideJsonObjectValue, inputContext, recursionDepth);
				GeoJsonObjectFormatter geoJsonObjectFormatter = SpatialImplementation.CurrentImplementation.CreateGeoJsonObjectFormatter();
				if (expectedValueTypeReference.IsGeography())
				{
					spatial = geoJsonObjectFormatter.Read<Geography>(dictionary);
				}
				else
				{
					spatial = geoJsonObjectFormatter.Read<Geometry>(dictionary);
				}
			}
			if (spatial == null)
			{
				throw new ODataException(Strings.ODataJsonReaderCoreUtils_CannotReadSpatialPropertyValue);
			}
			return spatial;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00037DEE File Offset: 0x00035FEE
		internal static bool TryReadNullValue(IJsonReader jsonReader, ODataInputContext inputContext, IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty = null)
		{
			if (jsonReader.NodeType == JsonNodeType.PrimitiveValue && jsonReader.Value == null)
			{
				jsonReader.ReadNext();
				inputContext.MessageReaderSettings.Validator.ValidateNullValue(expectedTypeReference, validateNullValue, propertyName, isDynamicProperty);
				return true;
			}
			return false;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00037E24 File Offset: 0x00036024
		private static IDictionary<string, object> ReadObjectValue(IJsonReader jsonReader, bool insideJsonObjectValue, ODataInputContext inputContext, int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			IDictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			if (!insideJsonObjectValue)
			{
				jsonReader.ReadNext();
			}
			while (jsonReader.NodeType != JsonNodeType.EndObject)
			{
				string text = jsonReader.ReadPropertyName();
				JsonNodeType nodeType = jsonReader.NodeType;
				object obj;
				if (nodeType != JsonNodeType.StartObject)
				{
					if (nodeType != JsonNodeType.StartArray)
					{
						if (nodeType != JsonNodeType.PrimitiveValue)
						{
							return null;
						}
						obj = jsonReader.ReadPrimitiveValue();
					}
					else
					{
						obj = ODataJsonReaderCoreUtils.ReadArrayValue(jsonReader, inputContext, recursionDepth);
					}
				}
				else
				{
					obj = ODataJsonReaderCoreUtils.ReadObjectValue(jsonReader, false, inputContext, recursionDepth);
				}
				dictionary.Add(ODataAnnotationNames.RemoveAnnotationPrefix(text), obj);
			}
			jsonReader.ReadEndObject();
			return dictionary;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00037EBC File Offset: 0x000360BC
		private static IEnumerable<object> ReadArrayValue(IJsonReader jsonReader, ODataInputContext inputContext, int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			List<object> list = new List<object>();
			jsonReader.ReadNext();
			while (jsonReader.NodeType != JsonNodeType.EndArray)
			{
				JsonNodeType nodeType = jsonReader.NodeType;
				if (nodeType != JsonNodeType.StartObject)
				{
					if (nodeType != JsonNodeType.StartArray)
					{
						if (nodeType != JsonNodeType.PrimitiveValue)
						{
							return null;
						}
						list.Add(jsonReader.ReadPrimitiveValue());
					}
					else
					{
						list.Add(ODataJsonReaderCoreUtils.ReadArrayValue(jsonReader, inputContext, recursionDepth));
					}
				}
				else
				{
					list.Add(ODataJsonReaderCoreUtils.ReadObjectValue(jsonReader, false, inputContext, recursionDepth));
				}
			}
			jsonReader.ReadEndArray();
			return list;
		}
	}
}
