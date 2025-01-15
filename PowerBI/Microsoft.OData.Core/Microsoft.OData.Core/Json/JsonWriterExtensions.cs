using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Json
{
	// Token: 0x0200021C RID: 540
	internal static class JsonWriterExtensions
	{
		// Token: 0x060017D4 RID: 6100 RVA: 0x00043D60 File Offset: 0x00041F60
		internal static void WriteJsonObjectValue(this IJsonWriter jsonWriter, IDictionary<string, object> jsonObjectValue, Action<IJsonWriter> injectPropertyAction)
		{
			jsonWriter.StartObjectScope();
			if (injectPropertyAction != null)
			{
				injectPropertyAction(jsonWriter);
			}
			foreach (KeyValuePair<string, object> keyValuePair in jsonObjectValue)
			{
				jsonWriter.WriteName(keyValuePair.Key);
				jsonWriter.WriteJsonValue(keyValuePair.Value);
			}
			jsonWriter.EndObjectScope();
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00043DD4 File Offset: 0x00041FD4
		internal static void WritePrimitiveValue(this IJsonWriter jsonWriter, object value)
		{
			if (value is bool)
			{
				jsonWriter.WriteValue((bool)value);
				return;
			}
			if (value is byte)
			{
				jsonWriter.WriteValue((byte)value);
				return;
			}
			if (value is decimal)
			{
				jsonWriter.WriteValue((decimal)value);
				return;
			}
			if (value is double)
			{
				jsonWriter.WriteValue((double)value);
				return;
			}
			if (value is short)
			{
				jsonWriter.WriteValue((short)value);
				return;
			}
			if (value is int)
			{
				jsonWriter.WriteValue((int)value);
				return;
			}
			if (value is long)
			{
				jsonWriter.WriteValue((long)value);
				return;
			}
			if (value is sbyte)
			{
				jsonWriter.WriteValue((sbyte)value);
				return;
			}
			if (value is float)
			{
				jsonWriter.WriteValue((float)value);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				jsonWriter.WriteValue(text);
				return;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				jsonWriter.WriteValue(array);
				return;
			}
			if (value is DateTimeOffset)
			{
				jsonWriter.WriteValue((DateTimeOffset)value);
				return;
			}
			if (value is Guid)
			{
				jsonWriter.WriteValue((Guid)value);
				return;
			}
			if (value is TimeSpan)
			{
				jsonWriter.WriteValue((TimeSpan)value);
				return;
			}
			if (value is Date)
			{
				jsonWriter.WriteValue((Date)value);
				return;
			}
			if (value is TimeOfDay)
			{
				jsonWriter.WriteValue((TimeOfDay)value);
				return;
			}
			throw new ODataException(Strings.ODataJsonWriter_UnsupportedValueType(value.GetType().FullName));
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00043F40 File Offset: 0x00042140
		internal static void WriteODataValue(this IJsonWriter jsonWriter, ODataValue odataValue)
		{
			if (odataValue == null || odataValue is ODataNullValue)
			{
				jsonWriter.WriteValue(null);
				return;
			}
			object obj = odataValue.FromODataValue();
			if (EdmLibraryExtensions.IsPrimitiveType(obj.GetType()))
			{
				jsonWriter.WritePrimitiveValue(obj);
				return;
			}
			ODataResourceValue odataResourceValue = odataValue as ODataResourceValue;
			if (odataResourceValue != null)
			{
				jsonWriter.StartObjectScope();
				foreach (ODataProperty odataProperty in odataResourceValue.Properties)
				{
					jsonWriter.WriteName(odataProperty.Name);
					jsonWriter.WriteODataValue(odataProperty.ODataValue);
				}
				jsonWriter.EndObjectScope();
				return;
			}
			ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				jsonWriter.StartArrayScope();
				foreach (object obj2 in odataCollectionValue.Items)
				{
					ODataValue odataValue2 = obj2 as ODataValue;
					if (obj2 == null)
					{
						throw new ODataException(Strings.ODataJsonWriter_UnsupportedValueInCollection);
					}
					jsonWriter.WriteODataValue(odataValue2);
				}
				jsonWriter.EndArrayScope();
				return;
			}
			throw new ODataException(Strings.ODataJsonWriter_UnsupportedValueType(odataValue.GetType().FullName));
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00044070 File Offset: 0x00042270
		private static void WriteJsonArrayValue(this IJsonWriter jsonWriter, IEnumerable arrayValue)
		{
			jsonWriter.StartArrayScope();
			foreach (object obj in arrayValue)
			{
				jsonWriter.WriteJsonValue(obj);
			}
			jsonWriter.EndArrayScope();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000440CC File Offset: 0x000422CC
		private static void WriteJsonValue(this IJsonWriter jsonWriter, object propertyValue)
		{
			if (propertyValue == null)
			{
				jsonWriter.WriteValue(null);
				return;
			}
			if (EdmLibraryExtensions.IsPrimitiveType(propertyValue.GetType()))
			{
				jsonWriter.WritePrimitiveValue(propertyValue);
				return;
			}
			IDictionary<string, object> dictionary = propertyValue as IDictionary<string, object>;
			if (dictionary != null)
			{
				jsonWriter.WriteJsonObjectValue(dictionary, null);
				return;
			}
			IEnumerable enumerable = propertyValue as IEnumerable;
			jsonWriter.WriteJsonArrayValue(enumerable);
		}
	}
}
