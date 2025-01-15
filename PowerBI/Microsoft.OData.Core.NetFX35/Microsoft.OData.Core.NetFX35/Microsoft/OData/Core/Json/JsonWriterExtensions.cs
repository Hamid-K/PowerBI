using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200011B RID: 283
	internal static class JsonWriterExtensions
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00026E50 File Offset: 0x00025050
		internal static void WriteJsonObjectValue(this IJsonWriter jsonWriter, IDictionary<string, object> jsonObjectValue, Action<IJsonWriter> injectPropertyAction)
		{
			jsonWriter.StartObjectScope();
			if (injectPropertyAction != null)
			{
				injectPropertyAction.Invoke(jsonWriter);
			}
			foreach (KeyValuePair<string, object> keyValuePair in jsonObjectValue)
			{
				jsonWriter.WriteName(keyValuePair.Key);
				jsonWriter.WriteJsonValue(keyValuePair.Value);
			}
			jsonWriter.EndObjectScope();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00026EC4 File Offset: 0x000250C4
		internal static void WritePrimitiveValue(this IJsonWriter jsonWriter, object value)
		{
			switch (PlatformHelper.GetTypeCode(value.GetType()))
			{
			case 3:
				jsonWriter.WriteValue((bool)value);
				return;
			case 5:
				jsonWriter.WriteValue((sbyte)value);
				return;
			case 6:
				jsonWriter.WriteValue((byte)value);
				return;
			case 7:
				jsonWriter.WriteValue((short)value);
				return;
			case 9:
				jsonWriter.WriteValue((int)value);
				return;
			case 11:
				jsonWriter.WriteValue((long)value);
				return;
			case 13:
				jsonWriter.WriteValue((float)value);
				return;
			case 14:
				jsonWriter.WriteValue((double)value);
				return;
			case 15:
				jsonWriter.WriteValue((decimal)value);
				return;
			case 18:
				jsonWriter.WriteValue((string)value);
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

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00027040 File Offset: 0x00025240
		private static void WriteJsonArrayValue(this IJsonWriter jsonWriter, IEnumerable arrayValue)
		{
			jsonWriter.StartArrayScope();
			foreach (object obj in arrayValue)
			{
				jsonWriter.WriteJsonValue(obj);
			}
			jsonWriter.EndArrayScope();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002709C File Offset: 0x0002529C
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
