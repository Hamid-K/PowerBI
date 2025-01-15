using System;
using System.Collections.Generic;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200011D RID: 285
	internal static class ODataJsonReaderCoreUtils
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x00027120 File Offset: 0x00025320
		internal static ISpatial ReadSpatialValue(BufferingJsonReader jsonReader, bool insideJsonObjectValue, ODataInputContext inputContext, IEdmPrimitiveTypeReference expectedValueTypeReference, bool validateNullValue, int recursionDepth, string propertyName)
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
				if (expectedValueTypeReference.IsGeographyType())
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

		// Token: 0x06000ABD RID: 2749 RVA: 0x00027196 File Offset: 0x00025396
		internal static bool TryReadNullValue(BufferingJsonReader jsonReader, ODataInputContext inputContext, IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName, bool? isDynamicProperty = null)
		{
			if (jsonReader.NodeType == JsonNodeType.PrimitiveValue && jsonReader.Value == null)
			{
				jsonReader.ReadNext();
				ReaderValidationUtils.ValidateNullValue(inputContext.Model, expectedTypeReference, inputContext.MessageReaderSettings, validateNullValue, propertyName, isDynamicProperty);
				return true;
			}
			return false;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000271CC File Offset: 0x000253CC
		private static IDictionary<string, object> ReadObjectValue(JsonReader jsonReader, bool insideJsonObjectValue, ODataInputContext inputContext, int recursionDepth)
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
				switch (nodeType)
				{
				case JsonNodeType.StartObject:
					obj = ODataJsonReaderCoreUtils.ReadObjectValue(jsonReader, false, inputContext, recursionDepth);
					break;
				case JsonNodeType.EndObject:
					goto IL_0076;
				case JsonNodeType.StartArray:
					obj = ODataJsonReaderCoreUtils.ReadArrayValue(jsonReader, inputContext, recursionDepth);
					break;
				default:
					if (nodeType != JsonNodeType.PrimitiveValue)
					{
						goto IL_0076;
					}
					obj = jsonReader.ReadPrimitiveValue();
					break;
				}
				dictionary.Add(ODataAnnotationNames.RemoveAnnotationPrefix(text), obj);
				continue;
				IL_0076:
				return null;
			}
			jsonReader.ReadEndObject();
			return dictionary;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00027270 File Offset: 0x00025470
		private static IEnumerable<object> ReadArrayValue(JsonReader jsonReader, ODataInputContext inputContext, int recursionDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, inputContext.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
			List<object> list = new List<object>();
			jsonReader.ReadNext();
			while (jsonReader.NodeType != JsonNodeType.EndArray)
			{
				JsonNodeType nodeType = jsonReader.NodeType;
				switch (nodeType)
				{
				case JsonNodeType.StartObject:
					list.Add(ODataJsonReaderCoreUtils.ReadObjectValue(jsonReader, false, inputContext, recursionDepth));
					continue;
				case JsonNodeType.EndObject:
					break;
				case JsonNodeType.StartArray:
					list.Add(ODataJsonReaderCoreUtils.ReadArrayValue(jsonReader, inputContext, recursionDepth));
					continue;
				default:
					if (nodeType == JsonNodeType.PrimitiveValue)
					{
						list.Add(jsonReader.ReadPrimitiveValue());
						continue;
					}
					break;
				}
				return null;
			}
			jsonReader.ReadEndArray();
			return list;
		}
	}
}
