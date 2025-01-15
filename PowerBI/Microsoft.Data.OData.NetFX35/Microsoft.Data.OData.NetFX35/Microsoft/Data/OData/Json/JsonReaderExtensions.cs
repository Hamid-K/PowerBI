using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x0200024A RID: 586
	internal static class JsonReaderExtensions
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x00043BDA File Offset: 0x00041DDA
		internal static void ReadStartObject(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartObject);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00043BE3 File Offset: 0x00041DE3
		internal static void ReadEndObject(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndObject);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00043BEC File Offset: 0x00041DEC
		internal static void ReadStartArray(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartArray);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00043BF5 File Offset: 0x00041DF5
		internal static void ReadEndArray(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndArray);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00043BFE File Offset: 0x00041DFE
		internal static string GetPropertyName(this JsonReader jsonReader)
		{
			return (string)jsonReader.Value;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00043C0C File Offset: 0x00041E0C
		internal static string ReadPropertyName(this JsonReader jsonReader)
		{
			jsonReader.ValidateNodeType(JsonNodeType.Property);
			string propertyName = jsonReader.GetPropertyName();
			jsonReader.ReadNext();
			return propertyName;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00043C30 File Offset: 0x00041E30
		internal static object ReadPrimitiveValue(this JsonReader jsonReader)
		{
			object value = jsonReader.Value;
			jsonReader.ReadNext(JsonNodeType.PrimitiveValue);
			return value;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00043C4C File Offset: 0x00041E4C
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

		// Token: 0x060011E3 RID: 4579 RVA: 0x00043C7C File Offset: 0x00041E7C
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

		// Token: 0x060011E4 RID: 4580 RVA: 0x00043CAC File Offset: 0x00041EAC
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
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsDouble(obj));
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00043D08 File Offset: 0x00041F08
		internal static void SkipValue(this JsonReader jsonReader)
		{
			jsonReader.SkipValue(null);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00043D14 File Offset: 0x00041F14
		internal static void SkipValue(this JsonReader jsonReader, StringBuilder jsonRawValueStringBuilder)
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
				if (jsonRawValueStringBuilder != null)
				{
					jsonRawValueStringBuilder.Append(jsonReader.RawValue);
				}
				jsonReader.ReadNext();
			}
			while (num > 0);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00043D71 File Offset: 0x00041F71
		internal static JsonNodeType ReadNext(this JsonReader jsonReader)
		{
			jsonReader.Read();
			return jsonReader.NodeType;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00043D80 File Offset: 0x00041F80
		internal static bool IsOnValueNode(this JsonReader jsonReader)
		{
			JsonNodeType nodeType = jsonReader.NodeType;
			return nodeType == JsonNodeType.PrimitiveValue || nodeType == JsonNodeType.StartObject || nodeType == JsonNodeType.StartArray;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00043DA2 File Offset: 0x00041FA2
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00043DA4 File Offset: 0x00041FA4
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00043DA6 File Offset: 0x00041FA6
		internal static ODataException CreateException(string exceptionMessage)
		{
			return new ODataException(exceptionMessage);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00043DAE File Offset: 0x00041FAE
		private static void ReadNext(this JsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			jsonReader.ValidateNodeType(expectedNodeType);
			jsonReader.Read();
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00043DBE File Offset: 0x00041FBE
		private static void ValidateNodeType(this JsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			if (jsonReader.NodeType != expectedNodeType)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(expectedNodeType, jsonReader.NodeType));
			}
		}
	}
}
