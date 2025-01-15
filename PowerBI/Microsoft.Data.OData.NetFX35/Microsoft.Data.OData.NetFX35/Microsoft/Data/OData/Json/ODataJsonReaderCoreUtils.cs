using System;
using System.Collections.Generic;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000178 RID: 376
	internal static class ODataJsonReaderCoreUtils
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00022CF4 File Offset: 0x00020EF4
		internal static ISpatial ReadSpatialValue(BufferingJsonReader jsonReader, bool insideJsonObjectValue, ODataInputContext inputContext, IEdmPrimitiveTypeReference expectedValueTypeReference, bool validateNullValue, int recursionDepth, string propertyName)
		{
			ODataVersionChecker.CheckSpatialValue(inputContext.Version);
			if (!insideJsonObjectValue && ODataJsonReaderCoreUtils.TryReadNullValue(jsonReader, inputContext, expectedValueTypeReference, validateNullValue, propertyName))
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

		// Token: 0x06000A65 RID: 2661 RVA: 0x00022D6C File Offset: 0x00020F6C
		internal static bool TryReadNullValue(BufferingJsonReader jsonReader, ODataInputContext inputContext, IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName)
		{
			if (jsonReader.NodeType == JsonNodeType.PrimitiveValue && jsonReader.Value == null)
			{
				jsonReader.ReadNext();
				ReaderValidationUtils.ValidateNullValue(inputContext.Model, expectedTypeReference, inputContext.MessageReaderSettings, validateNullValue, inputContext.Version, propertyName);
				return true;
			}
			return false;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00022DA4 File Offset: 0x00020FA4
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
				dictionary.Add(text, obj);
				continue;
				IL_0076:
				return null;
			}
			jsonReader.ReadEndObject();
			return dictionary;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00022E44 File Offset: 0x00021044
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
