using System;
using System.Diagnostics;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000115 RID: 277
	internal static class JsonReaderExtensions
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x000262C9 File Offset: 0x000244C9
		internal static void ReadStartObject(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartObject);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000262D2 File Offset: 0x000244D2
		internal static void ReadEndObject(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndObject);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000262DB File Offset: 0x000244DB
		internal static void ReadStartArray(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartArray);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000262E4 File Offset: 0x000244E4
		internal static void ReadEndArray(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndArray);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000262F0 File Offset: 0x000244F0
		internal static string GetPropertyName(this JsonReader jsonReader)
		{
			return (string)jsonReader.Value;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002630C File Offset: 0x0002450C
		internal static string ReadPropertyName(this JsonReader jsonReader)
		{
			jsonReader.ValidateNodeType(JsonNodeType.Property);
			string propertyName = jsonReader.GetPropertyName();
			jsonReader.ReadNext();
			return propertyName;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00026330 File Offset: 0x00024530
		internal static object ReadPrimitiveValue(this JsonReader jsonReader)
		{
			object value = jsonReader.Value;
			jsonReader.ReadNext(JsonNodeType.PrimitiveValue);
			return value;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002634C File Offset: 0x0002454C
		internal static string ReadStringValue(this JsonReader jsonReader)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			string text = obj as string;
			if (obj == null || text != null)
			{
				return text;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsString(obj));
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002637C File Offset: 0x0002457C
		internal static string ReadStringValue(this JsonReader jsonReader, string propertyName)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			string text = obj as string;
			if (obj == null || text != null)
			{
				return text;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadPropertyValueAsString(obj, propertyName));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000263AC File Offset: 0x000245AC
		internal static double? ReadDoubleValue(this JsonReader jsonReader)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			double? num = obj as double?;
			if (obj == null || num != null)
			{
				return num;
			}
			int? num2 = obj as int?;
			if (num2 != null)
			{
				return new double?((double)num2.Value);
			}
			decimal? num3 = obj as decimal?;
			if (num3 != null)
			{
				return new double?((double)num3.Value);
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsDouble(obj));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00026430 File Offset: 0x00024630
		internal static void SkipValue(this JsonReader jsonReader)
		{
			int num = 0;
			do
			{
				switch (jsonReader.NodeType)
				{
				case JsonNodeType.StartObject:
				case JsonNodeType.StartArray:
					num++;
					break;
				case JsonNodeType.EndObject:
				case JsonNodeType.EndArray:
					num--;
					break;
				}
				jsonReader.ReadNext();
			}
			while (num > 0);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002647D File Offset: 0x0002467D
		internal static JsonNodeType ReadNext(this JsonReader jsonReader)
		{
			jsonReader.Read();
			return jsonReader.NodeType;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002648C File Offset: 0x0002468C
		internal static bool IsOnValueNode(this JsonReader jsonReader)
		{
			JsonNodeType nodeType = jsonReader.NodeType;
			return nodeType == JsonNodeType.PrimitiveValue || nodeType == JsonNodeType.StartObject || nodeType == JsonNodeType.StartArray;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000264AE File Offset: 0x000246AE
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000264B0 File Offset: 0x000246B0
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000264B2 File Offset: 0x000246B2
		internal static ODataException CreateException(string exceptionMessage)
		{
			return new ODataException(exceptionMessage);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000264BA File Offset: 0x000246BA
		private static void ReadNext(this JsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			jsonReader.ValidateNodeType(expectedNodeType);
			jsonReader.Read();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000264CA File Offset: 0x000246CA
		private static void ValidateNodeType(this JsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			if (jsonReader.NodeType != expectedNodeType)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(expectedNodeType, jsonReader.NodeType));
			}
		}
	}
}
