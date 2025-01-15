using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Nodes
{
	// Token: 0x02000061 RID: 97
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonValue : JsonNode
	{
		// Token: 0x06000788 RID: 1928 RVA: 0x000229C0 File Offset: 0x00020BC0
		[NullableContext(1)]
		public static JsonValue Create(bool value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<bool>(value, JsonMetadataServices.BooleanConverter, null);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000229E4 File Offset: 0x00020BE4
		public static JsonValue Create(bool? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<bool>(value.Value, JsonMetadataServices.BooleanConverter, null);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00022A18 File Offset: 0x00020C18
		[NullableContext(1)]
		public static JsonValue Create(byte value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<byte>(value, JsonMetadataServices.ByteConverter, null);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00022A3C File Offset: 0x00020C3C
		public static JsonValue Create(byte? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<byte>(value.Value, JsonMetadataServices.ByteConverter, null);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00022A70 File Offset: 0x00020C70
		[NullableContext(1)]
		public static JsonValue Create(char value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<char>(value, JsonMetadataServices.CharConverter, null);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00022A94 File Offset: 0x00020C94
		public static JsonValue Create(char? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<char>(value.Value, JsonMetadataServices.CharConverter, null);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00022AC8 File Offset: 0x00020CC8
		[NullableContext(1)]
		public static JsonValue Create(DateTime value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<DateTime>(value, JsonMetadataServices.DateTimeConverter, null);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00022AEC File Offset: 0x00020CEC
		public static JsonValue Create(DateTime? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<DateTime>(value.Value, JsonMetadataServices.DateTimeConverter, null);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00022B20 File Offset: 0x00020D20
		[NullableContext(1)]
		public static JsonValue Create(DateTimeOffset value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<DateTimeOffset>(value, JsonMetadataServices.DateTimeOffsetConverter, null);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00022B44 File Offset: 0x00020D44
		public static JsonValue Create(DateTimeOffset? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<DateTimeOffset>(value.Value, JsonMetadataServices.DateTimeOffsetConverter, null);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00022B78 File Offset: 0x00020D78
		[NullableContext(1)]
		public static JsonValue Create(decimal value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<decimal>(value, JsonMetadataServices.DecimalConverter, null);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00022B9C File Offset: 0x00020D9C
		public static JsonValue Create(decimal? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<decimal>(value.Value, JsonMetadataServices.DecimalConverter, null);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00022BD0 File Offset: 0x00020DD0
		[NullableContext(1)]
		public static JsonValue Create(double value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<double>(value, JsonMetadataServices.DoubleConverter, null);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00022BF4 File Offset: 0x00020DF4
		public static JsonValue Create(double? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<double>(value.Value, JsonMetadataServices.DoubleConverter, null);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00022C28 File Offset: 0x00020E28
		[NullableContext(1)]
		public static JsonValue Create(Guid value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<Guid>(value, JsonMetadataServices.GuidConverter, null);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00022C4C File Offset: 0x00020E4C
		public static JsonValue Create(Guid? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<Guid>(value.Value, JsonMetadataServices.GuidConverter, null);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00022C80 File Offset: 0x00020E80
		[NullableContext(1)]
		public static JsonValue Create(short value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<short>(value, JsonMetadataServices.Int16Converter, null);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00022CA4 File Offset: 0x00020EA4
		public static JsonValue Create(short? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<short>(value.Value, JsonMetadataServices.Int16Converter, null);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00022CD8 File Offset: 0x00020ED8
		[NullableContext(1)]
		public static JsonValue Create(int value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<int>(value, JsonMetadataServices.Int32Converter, null);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00022CFC File Offset: 0x00020EFC
		public static JsonValue Create(int? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<int>(value.Value, JsonMetadataServices.Int32Converter, null);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00022D30 File Offset: 0x00020F30
		[NullableContext(1)]
		public static JsonValue Create(long value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<long>(value, JsonMetadataServices.Int64Converter, null);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00022D54 File Offset: 0x00020F54
		public static JsonValue Create(long? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<long>(value.Value, JsonMetadataServices.Int64Converter, null);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00022D88 File Offset: 0x00020F88
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static JsonValue Create(sbyte value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<sbyte>(value, JsonMetadataServices.SByteConverter, null);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00022DAC File Offset: 0x00020FAC
		[CLSCompliant(false)]
		public static JsonValue Create(sbyte? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<sbyte>(value.Value, JsonMetadataServices.SByteConverter, null);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00022DE0 File Offset: 0x00020FE0
		[NullableContext(1)]
		public static JsonValue Create(float value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<float>(value, JsonMetadataServices.SingleConverter, null);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00022E04 File Offset: 0x00021004
		public static JsonValue Create(float? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<float>(value.Value, JsonMetadataServices.SingleConverter, null);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00022E38 File Offset: 0x00021038
		[return: NotNullIfNotNull("value")]
		public static JsonValue Create(string value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<string>(value, JsonMetadataServices.StringConverter, null);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00022E60 File Offset: 0x00021060
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static JsonValue Create(ushort value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<ushort>(value, JsonMetadataServices.UInt16Converter, null);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00022E84 File Offset: 0x00021084
		[CLSCompliant(false)]
		public static JsonValue Create(ushort? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<ushort>(value.Value, JsonMetadataServices.UInt16Converter, null);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00022EB8 File Offset: 0x000210B8
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static JsonValue Create(uint value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<uint>(value, JsonMetadataServices.UInt32Converter, null);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00022EDC File Offset: 0x000210DC
		[CLSCompliant(false)]
		public static JsonValue Create(uint? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<uint>(value.Value, JsonMetadataServices.UInt32Converter, null);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00022F10 File Offset: 0x00021110
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static JsonValue Create(ulong value, JsonNodeOptions? options = null)
		{
			return new JsonValuePrimitive<ulong>(value, JsonMetadataServices.UInt64Converter, null);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00022F34 File Offset: 0x00021134
		[CLSCompliant(false)]
		public static JsonValue Create(ulong? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			return new JsonValuePrimitive<ulong>(value.Value, JsonMetadataServices.UInt64Converter, null);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00022F68 File Offset: 0x00021168
		public static JsonValue Create(JsonElement value, JsonNodeOptions? options = null)
		{
			if (value.ValueKind == JsonValueKind.Null)
			{
				return null;
			}
			JsonValue.VerifyJsonElementIsNotArrayOrObject(ref value);
			return new JsonValuePrimitive<JsonElement>(value, JsonMetadataServices.JsonElementConverter, null);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00022F9C File Offset: 0x0002119C
		public static JsonValue Create(JsonElement? value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			JsonElement value2 = value.Value;
			if (value2.ValueKind == JsonValueKind.Null)
			{
				return null;
			}
			JsonValue.VerifyJsonElementIsNotArrayOrObject(ref value2);
			return new JsonValuePrimitive<JsonElement>(value2, JsonMetadataServices.JsonElementConverter, null);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00022FE3 File Offset: 0x000211E3
		private protected JsonValue(JsonNodeOptions? options = null)
			: base(options)
		{
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00022FEC File Offset: 0x000211EC
		[RequiresUnreferencedCode("Creating JsonValue instances with non-primitive types is not compatible with trimming. It can result in non-primitive types being serialized, which may have their members trimmed. Use the overload that takes a JsonTypeInfo, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("Creating JsonValue instances with non-primitive types requires generating code at runtime.")]
		public static JsonValue Create<T>(T value, JsonNodeOptions? options = null)
		{
			if (value == null)
			{
				return null;
			}
			if (!(value is JsonElement))
			{
				JsonTypeInfo<T> jsonTypeInfo = (JsonTypeInfo<T>)JsonSerializerOptions.Default.GetTypeInfo(typeof(T));
				return new JsonValueCustomized<T>(value, jsonTypeInfo, options);
			}
			JsonElement jsonElement = value as JsonElement;
			if (jsonElement.ValueKind == JsonValueKind.Null)
			{
				return null;
			}
			JsonValue.VerifyJsonElementIsNotArrayOrObject(ref jsonElement);
			return new JsonValuePrimitive<JsonElement>(jsonElement, JsonMetadataServices.JsonElementConverter, options);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00023064 File Offset: 0x00021264
		public static JsonValue Create<T>(T value, [Nullable(1)] JsonTypeInfo<T> jsonTypeInfo, JsonNodeOptions? options = null)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			if (value == null)
			{
				return null;
			}
			if (value is JsonElement)
			{
				JsonElement jsonElement = value as JsonElement;
				if (jsonElement.ValueKind == JsonValueKind.Null)
				{
					return null;
				}
				JsonValue.VerifyJsonElementIsNotArrayOrObject(ref jsonElement);
			}
			jsonTypeInfo.EnsureConfigured();
			return new JsonValueCustomized<T>(value, jsonTypeInfo, options);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000230C7 File Offset: 0x000212C7
		internal override void GetPath(List<string> path, JsonNode child)
		{
			JsonNode parent = base.Parent;
			if (parent == null)
			{
				return;
			}
			parent.GetPath(path, this);
		}

		// Token: 0x060007AF RID: 1967
		public abstract bool TryGetValue<T>([NotNullWhen(true)] out T value);

		// Token: 0x060007B0 RID: 1968 RVA: 0x000230DC File Offset: 0x000212DC
		private static void VerifyJsonElementIsNotArrayOrObject(ref JsonElement element)
		{
			JsonValueKind valueKind = element.ValueKind;
			bool flag = valueKind - JsonValueKind.Object <= 1;
			if (flag)
			{
				ThrowHelper.ThrowInvalidOperationException_NodeElementCannotBeObjectOrArray();
			}
		}

		// Token: 0x04000287 RID: 647
		internal const string CreateUnreferencedCodeMessage = "Creating JsonValue instances with non-primitive types is not compatible with trimming. It can result in non-primitive types being serialized, which may have their members trimmed.";

		// Token: 0x04000288 RID: 648
		internal const string CreateDynamicCodeMessage = "Creating JsonValue instances with non-primitive types requires generating code at runtime.";
	}
}
