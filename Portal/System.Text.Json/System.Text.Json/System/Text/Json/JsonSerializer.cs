using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Converters;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace System.Text.Json
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	[Nullable(0)]
	public static class JsonSerializer
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00011500 File Offset: 0x0000F700
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>([Nullable(1)] this JsonDocument document, JsonSerializerOptions options = null)
		{
			if (document == null)
			{
				ThrowHelper.ThrowArgumentNullException("document");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			ReadOnlySpan<byte> span = document.GetRootRawValue().Span;
			return JsonSerializer.ReadFromSpan<TValue>(span, typeInfo, null);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00011540 File Offset: 0x0000F740
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[return: Nullable(2)]
		public static object Deserialize(this JsonDocument document, Type returnType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (document == null)
			{
				ThrowHelper.ThrowArgumentNullException("document");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			ReadOnlySpan<byte> span = document.GetRootRawValue().Span;
			return JsonSerializer.ReadFromSpanAsObject(span, typeInfo, null);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011590 File Offset: 0x0000F790
		[return: Nullable(2)]
		public static TValue Deserialize<[Nullable(2)] TValue>(this JsonDocument document, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (document == null)
			{
				ThrowHelper.ThrowArgumentNullException("document");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			ReadOnlySpan<byte> span = document.GetRootRawValue().Span;
			return JsonSerializer.ReadFromSpan<TValue>(span, jsonTypeInfo, null);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000115DC File Offset: 0x0000F7DC
		[return: Nullable(2)]
		public static object Deserialize(this JsonDocument document, JsonTypeInfo jsonTypeInfo)
		{
			if (document == null)
			{
				ThrowHelper.ThrowArgumentNullException("document");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			ReadOnlySpan<byte> span = document.GetRootRawValue().Span;
			return JsonSerializer.ReadFromSpanAsObject(span, jsonTypeInfo, null);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011628 File Offset: 0x0000F828
		[return: Nullable(2)]
		public static object Deserialize(this JsonDocument document, Type returnType, JsonSerializerContext context)
		{
			if (document == null)
			{
				ThrowHelper.ThrowArgumentNullException("document");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			ReadOnlySpan<byte> span = document.GetRootRawValue().Span;
			return JsonSerializer.ReadFromSpanAsObject(span, typeInfo, null);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011684 File Offset: 0x0000F884
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>(this JsonElement element, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			ReadOnlySpan<byte> span = element.GetRawValue().Span;
			return JsonSerializer.ReadFromSpan<TValue>(span, typeInfo, null);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000116B8 File Offset: 0x0000F8B8
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static object Deserialize(this JsonElement element, [Nullable(1)] Type returnType, JsonSerializerOptions options = null)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			ReadOnlySpan<byte> span = element.GetRawValue().Span;
			return JsonSerializer.ReadFromSpanAsObject(span, typeInfo, null);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000116FC File Offset: 0x0000F8FC
		[NullableContext(2)]
		public static TValue Deserialize<TValue>(this JsonElement element, [Nullable(1)] JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			ReadOnlySpan<byte> span = element.GetRawValue().Span;
			return JsonSerializer.ReadFromSpan<TValue>(span, jsonTypeInfo, null);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001173C File Offset: 0x0000F93C
		[return: Nullable(2)]
		public static object Deserialize(this JsonElement element, JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			ReadOnlySpan<byte> span = element.GetRawValue().Span;
			return JsonSerializer.ReadFromSpanAsObject(span, jsonTypeInfo, null);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001177C File Offset: 0x0000F97C
		[return: Nullable(2)]
		public static object Deserialize(this JsonElement element, Type returnType, JsonSerializerContext context)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			ReadOnlySpan<byte> span = element.GetRawValue().Span;
			return JsonSerializer.ReadFromSpanAsObject(span, typeInfo, null);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000117CC File Offset: 0x0000F9CC
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>(this JsonNode node, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.ReadFromNode<TValue>(node, typeInfo);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000117E8 File Offset: 0x0000F9E8
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static object Deserialize(this JsonNode node, [Nullable(1)] Type returnType, JsonSerializerOptions options = null)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return JsonSerializer.ReadFromNodeAsObject(node, typeInfo);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00011811 File Offset: 0x0000FA11
		[NullableContext(2)]
		public static TValue Deserialize<TValue>(this JsonNode node, [Nullable(1)] JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromNode<TValue>(node, jsonTypeInfo);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001182D File Offset: 0x0000FA2D
		[NullableContext(2)]
		public static object Deserialize(this JsonNode node, [Nullable(1)] JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromNodeAsObject(node, jsonTypeInfo);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001184C File Offset: 0x0000FA4C
		[return: Nullable(2)]
		public static object Deserialize([Nullable(2)] this JsonNode node, Type returnType, JsonSerializerContext context)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			return JsonSerializer.ReadFromNodeAsObject(node, typeInfo);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011884 File Offset: 0x0000FA84
		private static TValue ReadFromNode<TValue>(JsonNode node, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			TValue tvalue;
			using (PooledByteBufferWriter pooledByteBufferWriter = new PooledByteBufferWriter(options.DefaultBufferSize))
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(pooledByteBufferWriter, options.GetWriterOptions()))
				{
					if (node == null)
					{
						utf8JsonWriter.WriteNullValue();
					}
					else
					{
						node.WriteTo(utf8JsonWriter, options);
					}
				}
				tvalue = JsonSerializer.ReadFromSpan<TValue>(pooledByteBufferWriter.WrittenMemory.Span, jsonTypeInfo, null);
			}
			return tvalue;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001191C File Offset: 0x0000FB1C
		private static object ReadFromNodeAsObject(JsonNode node, JsonTypeInfo jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			object obj;
			using (PooledByteBufferWriter pooledByteBufferWriter = new PooledByteBufferWriter(options.DefaultBufferSize))
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(pooledByteBufferWriter, options.GetWriterOptions()))
				{
					if (node == null)
					{
						utf8JsonWriter.WriteNullValue();
					}
					else
					{
						node.WriteTo(utf8JsonWriter, options);
					}
				}
				obj = JsonSerializer.ReadFromSpanAsObject(pooledByteBufferWriter.WrittenMemory.Span, jsonTypeInfo, null);
			}
			return obj;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000119B4 File Offset: 0x0000FBB4
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonDocument SerializeToDocument<[Nullable(2)] TValue>(TValue value, [Nullable(2)] JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.WriteDocument<TValue>(in value, typeInfo);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000119D0 File Offset: 0x0000FBD0
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonDocument SerializeToDocument([Nullable(2)] object value, Type inputType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			return JsonSerializer.WriteDocumentAsObject(value, typeInfo);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000119F3 File Offset: 0x0000FBF3
		public static JsonDocument SerializeToDocument<[Nullable(2)] TValue>(TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteDocument<TValue>(in value, jsonTypeInfo);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011A10 File Offset: 0x0000FC10
		public static JsonDocument SerializeToDocument([Nullable(2)] object value, JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteDocumentAsObject(value, jsonTypeInfo);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public static JsonDocument SerializeToDocument([Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			return JsonSerializer.WriteDocumentAsObject(value, JsonSerializer.GetTypeInfo(context, inputType));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00011A50 File Offset: 0x0000FC50
		private static JsonDocument WriteDocument<TValue>(in TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			PooledByteBufferWriter pooledByteBufferWriter = new PooledByteBufferWriter(options.DefaultBufferSize);
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriter(options, pooledByteBufferWriter);
			JsonDocument jsonDocument;
			try
			{
				jsonTypeInfo.Serialize(utf8JsonWriter, in value, null);
				jsonDocument = JsonDocument.ParseRented(pooledByteBufferWriter, options.GetDocumentOptions());
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriter(utf8JsonWriter);
			}
			return jsonDocument;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		private static JsonDocument WriteDocumentAsObject(object value, JsonTypeInfo jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			PooledByteBufferWriter pooledByteBufferWriter = new PooledByteBufferWriter(options.DefaultBufferSize);
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriter(options, pooledByteBufferWriter);
			JsonDocument jsonDocument;
			try
			{
				jsonTypeInfo.SerializeAsObject(utf8JsonWriter, value);
				jsonDocument = JsonDocument.ParseRented(pooledByteBufferWriter, options.GetDocumentOptions());
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriter(utf8JsonWriter);
			}
			return jsonDocument;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00011B00 File Offset: 0x0000FD00
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonElement SerializeToElement<TValue>([Nullable(1)] TValue value, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.WriteElement<TValue>(in value, typeInfo);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00011B1C File Offset: 0x0000FD1C
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonElement SerializeToElement(object value, [Nullable(1)] Type inputType, JsonSerializerOptions options = null)
		{
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			return JsonSerializer.WriteElementAsObject(value, typeInfo);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011B3F File Offset: 0x0000FD3F
		public static JsonElement SerializeToElement<[Nullable(2)] TValue>(TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteElement<TValue>(in value, jsonTypeInfo);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011B5C File Offset: 0x0000FD5C
		public static JsonElement SerializeToElement([Nullable(2)] object value, JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteElementAsObject(value, jsonTypeInfo);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00011B78 File Offset: 0x0000FD78
		public static JsonElement SerializeToElement([Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			return JsonSerializer.WriteElementAsObject(value, typeInfo);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		private static JsonElement WriteElement<TValue>(in TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			JsonElement jsonElement;
			try
			{
				jsonTypeInfo.Serialize(utf8JsonWriter, in value, null);
				jsonElement = JsonElement.ParseValue(pooledByteBufferWriter.WrittenMemory.Span, options.GetDocumentOptions());
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return jsonElement;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011C0C File Offset: 0x0000FE0C
		private static JsonElement WriteElementAsObject(object value, JsonTypeInfo jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			JsonElement jsonElement;
			try
			{
				jsonTypeInfo.SerializeAsObject(utf8JsonWriter, value);
				jsonElement = JsonElement.ParseValue(pooledByteBufferWriter.WrittenMemory.Span, options.GetDocumentOptions());
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return jsonElement;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011C70 File Offset: 0x0000FE70
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonNode SerializeToNode<TValue>([Nullable(1)] TValue value, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.WriteNode<TValue>(in value, typeInfo);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011C8C File Offset: 0x0000FE8C
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static JsonNode SerializeToNode(object value, [Nullable(1)] Type inputType, JsonSerializerOptions options = null)
		{
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			return JsonSerializer.WriteNodeAsObject(value, typeInfo);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011CAF File Offset: 0x0000FEAF
		[return: Nullable(2)]
		public static JsonNode SerializeToNode<[Nullable(2)] TValue>(TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteNode<TValue>(in value, jsonTypeInfo);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011CCC File Offset: 0x0000FECC
		[NullableContext(2)]
		public static JsonNode SerializeToNode(object value, [Nullable(1)] JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteNodeAsObject(value, jsonTypeInfo);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		[return: Nullable(2)]
		public static JsonNode SerializeToNode([Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			return JsonSerializer.WriteNodeAsObject(value, typeInfo);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011D18 File Offset: 0x0000FF18
		private static JsonNode WriteNode<TValue>(in TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			JsonNode jsonNode;
			try
			{
				jsonTypeInfo.Serialize(utf8JsonWriter, in value, null);
				jsonNode = JsonNode.Parse(pooledByteBufferWriter.WrittenMemory.Span, new JsonNodeOptions?(options.GetNodeOptions()), options.GetDocumentOptions());
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return jsonNode;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011D88 File Offset: 0x0000FF88
		private static JsonNode WriteNodeAsObject(object value, JsonTypeInfo jsonTypeInfo)
		{
			JsonSerializerOptions options = jsonTypeInfo.Options;
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			JsonNode jsonNode;
			try
			{
				jsonTypeInfo.SerializeAsObject(utf8JsonWriter, value);
				jsonNode = JsonNode.Parse(pooledByteBufferWriter.WrittenMemory.Span, new JsonNodeOptions?(options.GetNodeOptions()), options.GetDocumentOptions());
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return jsonNode;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		public static bool IsReflectionEnabledByDefault { get; }

		// Token: 0x0600044A RID: 1098 RVA: 0x00011DFB File Offset: 0x0000FFFB
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonTypeInfo GetTypeInfo(JsonSerializerOptions options, Type inputType)
		{
			if (options == null)
			{
				options = JsonSerializerOptions.Default;
			}
			options.MakeReadOnly(true);
			if (!(inputType == JsonTypeInfo.ObjectType))
			{
				return options.GetTypeInfoForRootType(inputType, false);
			}
			return options.ObjectTypeInfo;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00011E2A File Offset: 0x0001002A
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		private static JsonTypeInfo<T> GetTypeInfo<T>(JsonSerializerOptions options)
		{
			return (JsonTypeInfo<T>)JsonSerializer.GetTypeInfo(options, typeof(T));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00011E44 File Offset: 0x00010044
		private static JsonTypeInfo GetTypeInfo(JsonSerializerContext context, Type inputType)
		{
			JsonTypeInfo typeInfo = context.GetTypeInfo(inputType);
			if (typeInfo == null)
			{
				ThrowHelper.ThrowInvalidOperationException_NoMetadataForType(inputType, context);
			}
			typeInfo.EnsureConfigured();
			return typeInfo;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00011E6C File Offset: 0x0001006C
		private static void ValidateInputType(object value, Type inputType)
		{
			if (inputType == null)
			{
				ThrowHelper.ThrowArgumentNullException("inputType");
			}
			if (value != null)
			{
				Type type = value.GetType();
				if (!inputType.IsAssignableFrom(type))
				{
					ThrowHelper.ThrowArgumentException_DeserializeWrongType(inputType, value);
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00011EA0 File Offset: 0x000100A0
		internal static bool IsValidNumberHandlingValue(JsonNumberHandling handling)
		{
			return JsonHelpers.IsInRangeInclusive((int)handling, 0, 7);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00011EAC File Offset: 0x000100AC
		internal static bool IsValidCreationHandlingValue(JsonObjectCreationHandling handling)
		{
			return handling <= JsonObjectCreationHandling.Populate;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011EC4 File Offset: 0x000100C4
		internal static bool IsValidUnmappedMemberHandlingValue(JsonUnmappedMemberHandling handling)
		{
			return handling <= JsonUnmappedMemberHandling.Disallow;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00011EDC File Offset: 0x000100DC
		[return: NotNullIfNotNull("value")]
		internal static T UnboxOnRead<T>(object value)
		{
			if (value == null)
			{
				if (default(T) != null)
				{
					JsonSerializer.<UnboxOnRead>g__ThrowUnableToCastValue|50_0<T>(value);
				}
				return default(T);
			}
			if (value is T)
			{
				return (T)((object)value);
			}
			JsonSerializer.<UnboxOnRead>g__ThrowUnableToCastValue|50_0<T>(value);
			return default(T);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00011F2C File Offset: 0x0001012C
		[return: NotNullIfNotNull("value")]
		internal static T UnboxOnWrite<T>(object value)
		{
			if (default(T) != null && value == null)
			{
				ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(typeof(T));
			}
			return (T)((object)value);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011F64 File Offset: 0x00010164
		internal static bool TryReadMetadata(JsonConverter converter, JsonTypeInfo jsonTypeInfo, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			for (;;)
			{
				if (state.Current.PropertyState == StackFramePropertyState.None)
				{
					state.Current.PropertyState = StackFramePropertyState.ReadName;
					if (!reader.Read())
					{
						break;
					}
				}
				if (state.Current.PropertyState < StackFramePropertyState.Name)
				{
					if (reader.TokenType == JsonTokenType.EndObject)
					{
						return true;
					}
					if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Ref) != MetadataPropertyName.None)
					{
						ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties((ref reader).GetSpan(), ref state);
					}
					ReadOnlySpan<byte> span = (ref reader).GetSpan();
					MetadataPropertyName metadataPropertyName = (state.Current.LatestMetadataPropertyName = JsonSerializer.GetMetadataPropertyName(span, jsonTypeInfo.PolymorphicTypeResolver));
					switch (metadataPropertyName)
					{
					case MetadataPropertyName.Values:
						state.Current.JsonPropertyName = JsonSerializer.s_valuesPropertyName;
						if (state.Current.MetadataPropertyNames == MetadataPropertyName.None)
						{
							ThrowHelper.ThrowJsonException_MetadataStandaloneValuesProperty(ref state, span);
						}
						break;
					case MetadataPropertyName.Id:
						state.Current.JsonPropertyName = JsonSerializer.s_idPropertyName;
						if (state.ReferenceResolver == null)
						{
							ThrowHelper.ThrowJsonException_MetadataUnexpectedProperty(span, ref state);
						}
						if ((state.Current.MetadataPropertyNames & (MetadataPropertyName.Id | MetadataPropertyName.Ref)) != MetadataPropertyName.None)
						{
							ThrowHelper.ThrowJsonException_MetadataIdIsNotFirstProperty(span, ref state);
						}
						if (!converter.CanHaveMetadata)
						{
							ThrowHelper.ThrowJsonException_MetadataCannotParsePreservedObjectIntoImmutable(converter.Type);
						}
						break;
					case MetadataPropertyName.Values | MetadataPropertyName.Id:
						return true;
					case MetadataPropertyName.Ref:
						state.Current.JsonPropertyName = JsonSerializer.s_refPropertyName;
						if (state.ReferenceResolver == null)
						{
							ThrowHelper.ThrowJsonException_MetadataUnexpectedProperty(span, ref state);
						}
						if (converter.IsValueType)
						{
							ThrowHelper.ThrowJsonException_MetadataInvalidReferenceToValueType(converter.Type);
						}
						if (state.Current.MetadataPropertyNames != MetadataPropertyName.None)
						{
							ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties((ref reader).GetSpan(), ref state);
						}
						break;
					default:
					{
						if (metadataPropertyName != MetadataPropertyName.Type)
						{
							return true;
						}
						PolymorphicTypeResolver polymorphicTypeResolver = jsonTypeInfo.PolymorphicTypeResolver;
						state.Current.JsonPropertyName = ((polymorphicTypeResolver != null) ? polymorphicTypeResolver.TypeDiscriminatorPropertyNameUtf8 : null) ?? JsonSerializer.s_typePropertyName;
						if (jsonTypeInfo.PolymorphicTypeResolver == null)
						{
							ThrowHelper.ThrowJsonException_MetadataUnexpectedProperty(span, ref state);
						}
						if (state.PolymorphicTypeDiscriminator != null)
						{
							ThrowHelper.ThrowJsonException_MetadataDuplicateTypeProperty();
						}
						break;
					}
					}
					state.Current.PropertyState = StackFramePropertyState.Name;
				}
				if (state.Current.PropertyState < StackFramePropertyState.ReadValue)
				{
					state.Current.PropertyState = StackFramePropertyState.ReadValue;
					if (!reader.Read())
					{
						return false;
					}
				}
				MetadataPropertyName latestMetadataPropertyName = state.Current.LatestMetadataPropertyName;
				switch (latestMetadataPropertyName)
				{
				case MetadataPropertyName.Values:
					goto IL_02C1;
				case MetadataPropertyName.Id:
					if (reader.TokenType != JsonTokenType.String)
					{
						ThrowHelper.ThrowJsonException_MetadataValueWasNotString(reader.TokenType);
					}
					if (state.ReferenceId != null)
					{
						ThrowHelper.ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(JsonSerializer.s_refPropertyName, ref reader, ref state);
					}
					state.ReferenceId = reader.GetString();
					break;
				case MetadataPropertyName.Values | MetadataPropertyName.Id:
					break;
				case MetadataPropertyName.Ref:
					if (reader.TokenType != JsonTokenType.String)
					{
						ThrowHelper.ThrowJsonException_MetadataValueWasNotString(reader.TokenType);
					}
					if (state.ReferenceId != null)
					{
						ThrowHelper.ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(JsonSerializer.s_refPropertyName, ref reader, ref state);
					}
					state.ReferenceId = reader.GetString();
					break;
				default:
					if (latestMetadataPropertyName == MetadataPropertyName.Type)
					{
						JsonTokenType tokenType = reader.TokenType;
						if (tokenType != JsonTokenType.String)
						{
							if (tokenType != JsonTokenType.Number)
							{
								ThrowHelper.ThrowJsonException_MetadataValueWasNotString(reader.TokenType);
							}
							else
							{
								state.PolymorphicTypeDiscriminator = reader.GetInt32();
							}
						}
						else
						{
							state.PolymorphicTypeDiscriminator = reader.GetString();
						}
					}
					break;
				}
				state.Current.MetadataPropertyNames = state.Current.MetadataPropertyNames | state.Current.LatestMetadataPropertyName;
				state.Current.PropertyState = StackFramePropertyState.None;
				state.Current.JsonPropertyName = null;
			}
			return false;
			IL_02C1:
			if (reader.TokenType != JsonTokenType.StartArray)
			{
				ThrowHelper.ThrowJsonException_MetadataValuesInvalidToken(reader.TokenType);
			}
			state.Current.PropertyState = StackFramePropertyState.None;
			state.Current.MetadataPropertyNames = state.Current.MetadataPropertyNames | state.Current.LatestMetadataPropertyName;
			return true;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000122A4 File Offset: 0x000104A4
		internal unsafe static bool IsMetadataPropertyName(ReadOnlySpan<byte> propertyName, PolymorphicTypeResolver resolver)
		{
			if (propertyName.Length > 0 && *propertyName[0] == 36)
			{
				return true;
			}
			if (resolver == null)
			{
				return false;
			}
			byte[] typeDiscriminatorPropertyNameUtf = resolver.TypeDiscriminatorPropertyNameUtf8;
			bool? flag = ((typeDiscriminatorPropertyNameUtf != null) ? new bool?(typeDiscriminatorPropertyNameUtf.AsSpan<byte>().SequenceEqual(propertyName)) : null);
			bool flag2 = true;
			return (flag.GetValueOrDefault() == flag2) & (flag != null);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001230C File Offset: 0x0001050C
		internal unsafe static MetadataPropertyName GetMetadataPropertyName(ReadOnlySpan<byte> propertyName, PolymorphicTypeResolver resolver)
		{
			if (propertyName.Length > 0 && *propertyName[0] == 36)
			{
				switch (propertyName.Length)
				{
				case 3:
					if (*propertyName[1] == 105 && *propertyName[2] == 100)
					{
						return MetadataPropertyName.Id;
					}
					break;
				case 4:
					if (*propertyName[1] == 114 && *propertyName[2] == 101 && *propertyName[3] == 102)
					{
						return MetadataPropertyName.Ref;
					}
					break;
				case 5:
					if (((resolver != null) ? resolver.TypeDiscriminatorPropertyNameUtf8 : null) == null && *propertyName[1] == 116 && *propertyName[2] == 121 && *propertyName[3] == 112 && *propertyName[4] == 101)
					{
						return MetadataPropertyName.Type;
					}
					break;
				case 7:
					if (*propertyName[1] == 118 && *propertyName[2] == 97 && *propertyName[3] == 108 && *propertyName[4] == 117 && *propertyName[5] == 101 && *propertyName[6] == 115)
					{
						return MetadataPropertyName.Values;
					}
					break;
				}
			}
			byte[] array = ((resolver != null) ? resolver.TypeDiscriminatorPropertyNameUtf8 : null);
			if (array != null && propertyName.SequenceEqual(array))
			{
				return MetadataPropertyName.Type;
			}
			return MetadataPropertyName.None;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001246C File Offset: 0x0001066C
		internal static bool TryHandleReferenceFromJsonElement(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonElement element, [NotNullWhen(true)] out object referenceValue)
		{
			bool flag = false;
			referenceValue = null;
			if (element.ValueKind == JsonValueKind.Object)
			{
				int num = 0;
				foreach (JsonProperty jsonProperty in element.EnumerateObject())
				{
					num++;
					if (flag)
					{
						ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties();
					}
					else
					{
						if (jsonProperty.EscapedNameEquals(JsonSerializer.s_idPropertyName))
						{
							if (state.ReferenceId != null)
							{
								ThrowHelper.ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(JsonSerializer.s_refPropertyName, ref reader, ref state);
							}
							if (jsonProperty.Value.ValueKind != JsonValueKind.String)
							{
								ThrowHelper.ThrowJsonException_MetadataValueWasNotString(jsonProperty.Value.ValueKind);
							}
							object obj = element;
							state.ReferenceResolver.AddReference(jsonProperty.Value.GetString(), obj);
							referenceValue = obj;
							return true;
						}
						if (jsonProperty.EscapedNameEquals(JsonSerializer.s_refPropertyName))
						{
							if (state.ReferenceId != null)
							{
								ThrowHelper.ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(JsonSerializer.s_refPropertyName, ref reader, ref state);
							}
							if (num > 1)
							{
								ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties();
							}
							if (jsonProperty.Value.ValueKind != JsonValueKind.String)
							{
								ThrowHelper.ThrowJsonException_MetadataValueWasNotString(jsonProperty.Value.ValueKind);
							}
							referenceValue = state.ReferenceResolver.ResolveReference(jsonProperty.Value.GetString());
							flag = true;
						}
					}
				}
				return flag;
			}
			return flag;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000125FC File Offset: 0x000107FC
		internal static bool TryHandleReferenceFromJsonNode(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonNode jsonNode, [NotNullWhen(true)] out object referenceValue)
		{
			bool flag = false;
			referenceValue = null;
			JsonObject jsonObject = jsonNode as JsonObject;
			if (jsonObject != null)
			{
				int num = 0;
				foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject)
				{
					num++;
					if (flag)
					{
						ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties();
					}
					else
					{
						if (keyValuePair.Key == "$id")
						{
							if (state.ReferenceId != null)
							{
								ThrowHelper.ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(JsonSerializer.s_refPropertyName, ref reader, ref state);
							}
							string text = JsonSerializer.<TryHandleReferenceFromJsonNode>g__ReadAsStringMetadataValue|64_0(keyValuePair.Value);
							state.ReferenceResolver.AddReference(text, jsonNode);
							referenceValue = jsonNode;
							return true;
						}
						if (keyValuePair.Key == "$ref")
						{
							if (state.ReferenceId != null)
							{
								ThrowHelper.ThrowNotSupportedException_ObjectWithParameterizedCtorRefMetadataNotSupported(JsonSerializer.s_refPropertyName, ref reader, ref state);
							}
							if (num > 1)
							{
								ThrowHelper.ThrowJsonException_MetadataReferenceObjectCannotContainOtherProperties();
							}
							string text2 = JsonSerializer.<TryHandleReferenceFromJsonNode>g__ReadAsStringMetadataValue|64_0(keyValuePair.Value);
							referenceValue = state.ReferenceResolver.ResolveReference(text2);
							flag = true;
						}
					}
				}
				return flag;
			}
			return flag;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00012714 File Offset: 0x00010914
		internal static void ValidateMetadataForObjectConverter(ref ReadStack state)
		{
			if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Values) != MetadataPropertyName.None)
			{
				ThrowHelper.ThrowJsonException_MetadataUnexpectedProperty(JsonSerializer.s_valuesPropertyName, ref state);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00012738 File Offset: 0x00010938
		internal static void ValidateMetadataForArrayConverter(JsonConverter converter, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			JsonTokenType tokenType = reader.TokenType;
			if (tokenType != JsonTokenType.EndObject)
			{
				if (tokenType != JsonTokenType.StartArray)
				{
					ThrowHelper.ThrowJsonException_MetadataInvalidPropertyInArrayMetadata(ref state, converter.Type, in reader);
				}
			}
			else if (state.Current.MetadataPropertyNames != MetadataPropertyName.Ref)
			{
				ThrowHelper.ThrowJsonException_MetadataPreservedArrayValuesNotFound(ref state, converter.Type);
				return;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00012780 File Offset: 0x00010980
		internal static T ResolveReferenceId<T>(ref ReadStack state)
		{
			string referenceId = state.ReferenceId;
			object obj = state.ReferenceResolver.ResolveReference(referenceId);
			state.ReferenceId = null;
			T t;
			try
			{
				t = (T)((object)obj);
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowInvalidOperationException_MetadataReferenceOfTypeCannotBeAssignedToType(referenceId, obj.GetType(), typeof(T));
				t = default(T);
			}
			return t;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000127E8 File Offset: 0x000109E8
		internal static JsonPropertyInfo LookupProperty(object obj, ReadOnlySpan<byte> unescapedPropertyName, ref ReadStack state, JsonSerializerOptions options, out bool useExtensionProperty, bool createExtensionProperty = true)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			useExtensionProperty = false;
			byte[] array;
			JsonPropertyInfo jsonPropertyInfo = jsonTypeInfo.GetProperty(unescapedPropertyName, ref state.Current, out array);
			state.Current.PropertyIndex = state.Current.PropertyIndex + 1;
			state.Current.JsonPropertyName = array;
			if (jsonPropertyInfo == JsonPropertyInfo.s_missingProperty)
			{
				if (jsonTypeInfo.EffectiveUnmappedMemberHandling == JsonUnmappedMemberHandling.Disallow)
				{
					string text = JsonHelpers.Utf8GetString(unescapedPropertyName);
					ThrowHelper.ThrowJsonException_UnmappedJsonProperty(jsonTypeInfo.Type, text);
				}
				JsonPropertyInfo extensionDataProperty = jsonTypeInfo.ExtensionDataProperty;
				if (extensionDataProperty != null && extensionDataProperty.HasGetter && extensionDataProperty.HasSetter)
				{
					state.Current.JsonPropertyNameAsString = JsonHelpers.Utf8GetString(unescapedPropertyName);
					if (createExtensionProperty)
					{
						JsonSerializer.CreateExtensionDataProperty(obj, extensionDataProperty, options);
					}
					jsonPropertyInfo = extensionDataProperty;
					useExtensionProperty = true;
				}
			}
			state.Current.JsonPropertyInfo = jsonPropertyInfo;
			state.Current.NumberHandling = jsonPropertyInfo.EffectiveNumberHandling;
			return jsonPropertyInfo;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000128B4 File Offset: 0x00010AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ReadOnlySpan<byte> GetPropertyName([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader)
		{
			ReadOnlySpan<byte> span = (ref reader).GetSpan();
			ReadOnlySpan<byte> readOnlySpan;
			if (reader.ValueIsEscaped)
			{
				readOnlySpan = JsonReaderHelper.GetUnescapedSpan(span);
			}
			else
			{
				readOnlySpan = span;
			}
			if (state.Current.CanContainMetadata && JsonSerializer.IsMetadataPropertyName(span, state.Current.BaseJsonTypeInfo.PolymorphicTypeResolver))
			{
				ThrowHelper.ThrowUnexpectedMetadataException(span, ref reader, ref state);
			}
			return readOnlySpan;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001290C File Offset: 0x00010B0C
		internal static void CreateExtensionDataProperty(object obj, JsonPropertyInfo jsonPropertyInfo, JsonSerializerOptions options)
		{
			if (jsonPropertyInfo.GetValueAsObject(obj) == null)
			{
				Func<object> func = jsonPropertyInfo.JsonTypeInfo.CreateObject ?? jsonPropertyInfo.JsonTypeInfo.CreateObjectForExtensionDataProperty;
				if (func == null)
				{
					if (jsonPropertyInfo.PropertyType.FullName == "System.Text.Json.Nodes.JsonObject")
					{
						ThrowHelper.ThrowInvalidOperationException_NodeJsonObjectCustomConverterNotAllowedOnExtensionProperty();
					}
					else
					{
						ThrowHelper.ThrowNotSupportedException_SerializationNotSupported(jsonPropertyInfo.PropertyType);
					}
				}
				object obj2 = func();
				jsonPropertyInfo.Set(obj, obj2);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00012980 File Offset: 0x00010B80
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>([Nullable(0)] ReadOnlySpan<byte> utf8Json, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.ReadFromSpan<TValue>(utf8Json, typeInfo, null);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000129A4 File Offset: 0x00010BA4
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static object Deserialize([Nullable(0)] ReadOnlySpan<byte> utf8Json, [Nullable(1)] Type returnType, JsonSerializerOptions options = null)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return JsonSerializer.ReadFromSpanAsObject(utf8Json, typeInfo, null);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000129D8 File Offset: 0x00010BD8
		[NullableContext(2)]
		public static TValue Deserialize<TValue>([Nullable(0)] ReadOnlySpan<byte> utf8Json, [Nullable(1)] JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromSpan<TValue>(utf8Json, jsonTypeInfo, null);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00012A08 File Offset: 0x00010C08
		[NullableContext(0)]
		[return: Nullable(2)]
		public static object Deserialize(ReadOnlySpan<byte> utf8Json, [Nullable(1)] JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromSpanAsObject(utf8Json, jsonTypeInfo, null);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00012A38 File Offset: 0x00010C38
		[return: Nullable(2)]
		public static object Deserialize([Nullable(0)] ReadOnlySpan<byte> utf8Json, Type returnType, JsonSerializerContext context)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			return JsonSerializer.ReadFromSpanAsObject(utf8Json, JsonSerializer.GetTypeInfo(context, returnType), null);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00012A78 File Offset: 0x00010C78
		private static TValue ReadFromSpan<TValue>(ReadOnlySpan<byte> utf8Json, JsonTypeInfo<TValue> jsonTypeInfo, int? actualByteCount = null)
		{
			JsonReaderState jsonReaderState = new JsonReaderState(jsonTypeInfo.Options.GetReaderOptions());
			Utf8JsonReader utf8JsonReader = new Utf8JsonReader(utf8Json, true, jsonReaderState);
			ReadStack readStack = default(ReadStack);
			readStack.Initialize(jsonTypeInfo, false);
			return jsonTypeInfo.Deserialize(ref utf8JsonReader, ref readStack);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00012AC0 File Offset: 0x00010CC0
		private static object ReadFromSpanAsObject(ReadOnlySpan<byte> utf8Json, JsonTypeInfo jsonTypeInfo, int? actualByteCount = null)
		{
			JsonReaderState jsonReaderState = new JsonReaderState(jsonTypeInfo.Options.GetReaderOptions());
			Utf8JsonReader utf8JsonReader = new Utf8JsonReader(utf8Json, true, jsonReaderState);
			ReadStack readStack = default(ReadStack);
			readStack.Initialize(jsonTypeInfo, false);
			return jsonTypeInfo.DeserializeAsObject(ref utf8JsonReader, ref readStack);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012B08 File Offset: 0x00010D08
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[return: Nullable(new byte[] { 0, 2 })]
		public static ValueTask<TValue> DeserializeAsync<TValue>([Nullable(1)] Stream utf8Json, JsonSerializerOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return typeInfo.DeserializeAsync(utf8Json, cancellationToken);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00012B34 File Offset: 0x00010D34
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>([Nullable(1)] Stream utf8Json, JsonSerializerOptions options = null)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return typeInfo.Deserialize(utf8Json);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012B5C File Offset: 0x00010D5C
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[return: Nullable(new byte[] { 0, 2 })]
		public static ValueTask<object> DeserializeAsync(Stream utf8Json, Type returnType, [Nullable(2)] JsonSerializerOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return typeInfo.DeserializeAsObjectAsync(utf8Json, cancellationToken);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012B94 File Offset: 0x00010D94
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[return: Nullable(2)]
		public static object Deserialize(Stream utf8Json, Type returnType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return typeInfo.DeserializeAsObject(utf8Json);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012BCA File Offset: 0x00010DCA
		[return: Nullable(new byte[] { 0, 2 })]
		public static ValueTask<TValue> DeserializeAsync<[Nullable(2)] TValue>(Stream utf8Json, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return jsonTypeInfo.DeserializeAsync(utf8Json, cancellationToken);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00012BF4 File Offset: 0x00010DF4
		[return: Nullable(new byte[] { 0, 2 })]
		public static ValueTask<object> DeserializeAsync(Stream utf8Json, JsonTypeInfo jsonTypeInfo, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return jsonTypeInfo.DeserializeAsObjectAsync(utf8Json, cancellationToken);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00012C1E File Offset: 0x00010E1E
		[return: Nullable(2)]
		public static TValue Deserialize<[Nullable(2)] TValue>(Stream utf8Json, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return jsonTypeInfo.Deserialize(utf8Json);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00012C47 File Offset: 0x00010E47
		[return: Nullable(2)]
		public static object Deserialize(Stream utf8Json, JsonTypeInfo jsonTypeInfo)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return jsonTypeInfo.DeserializeAsObject(utf8Json);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012C70 File Offset: 0x00010E70
		[return: Nullable(new byte[] { 0, 2 })]
		public static ValueTask<object> DeserializeAsync(Stream utf8Json, Type returnType, JsonSerializerContext context, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			return typeInfo.DeserializeAsObjectAsync(utf8Json, cancellationToken);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012CB4 File Offset: 0x00010EB4
		[return: Nullable(2)]
		public static object Deserialize(Stream utf8Json, Type returnType, JsonSerializerContext context)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			return typeInfo.DeserializeAsObject(utf8Json);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012CF8 File Offset: 0x00010EF8
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[return: Nullable(new byte[] { 1, 2 })]
		public static IAsyncEnumerable<TValue> DeserializeAsyncEnumerable<TValue>([Nullable(1)] Stream utf8Json, JsonSerializerOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.DeserializeAsyncEnumerableCore<TValue>(utf8Json, typeInfo, cancellationToken);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00012D21 File Offset: 0x00010F21
		[return: Nullable(new byte[] { 1, 2 })]
		public static IAsyncEnumerable<TValue> DeserializeAsyncEnumerable<[Nullable(2)] TValue>(Stream utf8Json, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.DeserializeAsyncEnumerableCore<TValue>(utf8Json, jsonTypeInfo, cancellationToken);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00012D4C File Offset: 0x00010F4C
		private static IAsyncEnumerable<T> DeserializeAsyncEnumerableCore<T>(Stream utf8Json, JsonTypeInfo<T> jsonTypeInfo, CancellationToken cancellationToken)
		{
			JsonTypeInfo asyncEnumerableQueueTypeInfo = jsonTypeInfo._asyncEnumerableQueueTypeInfo;
			JsonTypeInfo<Queue<T>> jsonTypeInfo2 = ((asyncEnumerableQueueTypeInfo != null) ? ((JsonTypeInfo<Queue<T>>)asyncEnumerableQueueTypeInfo) : JsonSerializer.<DeserializeAsyncEnumerableCore>g__CreateQueueTypeInfo|90_1<T>(jsonTypeInfo));
			return JsonSerializer.<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0<T>(utf8Json, jsonTypeInfo2, cancellationToken);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00012D7C File Offset: 0x00010F7C
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>([Nullable(1)] [StringSyntax("Json")] string json, JsonSerializerOptions options = null)
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.ReadFromSpan<TValue>(json.AsSpan(), typeInfo);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00012DAC File Offset: 0x00010FAC
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>([Nullable(0)] [StringSyntax("Json")] ReadOnlySpan<char> json, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.ReadFromSpan<TValue>(json, typeInfo);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00012DC8 File Offset: 0x00010FC8
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		[return: Nullable(2)]
		public static object Deserialize([StringSyntax("Json")] string json, Type returnType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return JsonSerializer.ReadFromSpanAsObject(json.AsSpan(), typeInfo);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00012E04 File Offset: 0x00011004
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static object Deserialize([Nullable(0)] [StringSyntax("Json")] ReadOnlySpan<char> json, [Nullable(1)] Type returnType, JsonSerializerOptions options = null)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return JsonSerializer.ReadFromSpanAsObject(json, typeInfo);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00012E2D File Offset: 0x0001102D
		[return: Nullable(2)]
		public static TValue Deserialize<[Nullable(2)] TValue>([StringSyntax("Json")] string json, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromSpan<TValue>(json.AsSpan(), jsonTypeInfo);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00012E5B File Offset: 0x0001105B
		[NullableContext(2)]
		public static TValue Deserialize<TValue>([Nullable(0)] [StringSyntax("Json")] ReadOnlySpan<char> json, [Nullable(1)] JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromSpan<TValue>(json, jsonTypeInfo);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00012E77 File Offset: 0x00011077
		[return: Nullable(2)]
		public static object Deserialize([StringSyntax("Json")] string json, JsonTypeInfo jsonTypeInfo)
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromSpanAsObject(json.AsSpan(), jsonTypeInfo);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00012EA5 File Offset: 0x000110A5
		[NullableContext(0)]
		[return: Nullable(2)]
		public static object Deserialize([StringSyntax("Json")] ReadOnlySpan<char> json, [Nullable(1)] JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadFromSpanAsObject(json, jsonTypeInfo);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00012EC4 File Offset: 0x000110C4
		[return: Nullable(2)]
		public static object Deserialize([StringSyntax("Json")] string json, Type returnType, JsonSerializerContext context)
		{
			if (json == null)
			{
				ThrowHelper.ThrowArgumentNullException("json");
			}
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			return JsonSerializer.ReadFromSpanAsObject(json.AsSpan(), typeInfo);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00012F0C File Offset: 0x0001110C
		[return: Nullable(2)]
		public static object Deserialize([Nullable(0)] [StringSyntax("Json")] ReadOnlySpan<char> json, Type returnType, JsonSerializerContext context)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, returnType);
			return JsonSerializer.ReadFromSpanAsObject(json, typeInfo);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00012F44 File Offset: 0x00011144
		private static TValue ReadFromSpan<TValue>(ReadOnlySpan<char> json, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			byte[] array = null;
			Span<byte> span = (((long)json.Length <= 349525L) ? (array = ArrayPool<byte>.Shared.Rent(json.Length * 3)) : new byte[JsonReaderHelper.GetUtf8ByteCount(json)]);
			TValue tvalue;
			try
			{
				int utf8FromText = JsonReaderHelper.GetUtf8FromText(json, span);
				span = span.Slice(0, utf8FromText);
				tvalue = JsonSerializer.ReadFromSpan<TValue>(span, jsonTypeInfo, new int?(utf8FromText));
			}
			finally
			{
				if (array != null)
				{
					span.Clear();
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return tvalue;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012FDC File Offset: 0x000111DC
		private static object ReadFromSpanAsObject(ReadOnlySpan<char> json, JsonTypeInfo jsonTypeInfo)
		{
			byte[] array = null;
			Span<byte> span = (((long)json.Length <= 349525L) ? (array = ArrayPool<byte>.Shared.Rent(json.Length * 3)) : new byte[JsonReaderHelper.GetUtf8ByteCount(json)]);
			object obj;
			try
			{
				int utf8FromText = JsonReaderHelper.GetUtf8FromText(json, span);
				span = span.Slice(0, utf8FromText);
				obj = JsonSerializer.ReadFromSpanAsObject(span, jsonTypeInfo, new int?(utf8FromText));
			}
			finally
			{
				if (array != null)
				{
					span.Clear();
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return obj;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00013074 File Offset: 0x00011274
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static TValue Deserialize<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.Read<TValue>(ref reader, typeInfo);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00013090 File Offset: 0x00011290
		[NullableContext(2)]
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static object Deserialize(ref Utf8JsonReader reader, [Nullable(1)] Type returnType, JsonSerializerOptions options = null)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, returnType);
			return JsonSerializer.ReadAsObject(ref reader, typeInfo);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000130B9 File Offset: 0x000112B9
		[NullableContext(2)]
		public static TValue Deserialize<TValue>(ref Utf8JsonReader reader, [Nullable(1)] JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.Read<TValue>(ref reader, jsonTypeInfo);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000130D5 File Offset: 0x000112D5
		[return: Nullable(2)]
		public static object Deserialize(ref Utf8JsonReader reader, JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.ReadAsObject(ref reader, jsonTypeInfo);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000130F1 File Offset: 0x000112F1
		[return: Nullable(2)]
		public static object Deserialize(ref Utf8JsonReader reader, Type returnType, JsonSerializerContext context)
		{
			if (returnType == null)
			{
				ThrowHelper.ThrowArgumentNullException("returnType");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			return JsonSerializer.ReadAsObject(ref reader, JsonSerializer.GetTypeInfo(context, returnType));
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001311C File Offset: 0x0001131C
		private static TValue Read<TValue>(ref Utf8JsonReader reader, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (reader.CurrentState.Options.CommentHandling == JsonCommentHandling.Allow)
			{
				ThrowHelper.ThrowArgumentException_SerializerDoesNotSupportComments("reader");
			}
			ReadStack readStack = default(ReadStack);
			readStack.Initialize(jsonTypeInfo, false);
			Utf8JsonReader utf8JsonReader = reader;
			TValue tvalue;
			try
			{
				Utf8JsonReader readerScopedToNextValue = JsonSerializer.GetReaderScopedToNextValue(ref reader, ref readStack);
				tvalue = jsonTypeInfo.Deserialize(ref readerScopedToNextValue, ref readStack);
			}
			catch (JsonException)
			{
				reader = utf8JsonReader;
				throw;
			}
			return tvalue;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00013198 File Offset: 0x00011398
		private static object ReadAsObject(ref Utf8JsonReader reader, JsonTypeInfo jsonTypeInfo)
		{
			if (reader.CurrentState.Options.CommentHandling == JsonCommentHandling.Allow)
			{
				ThrowHelper.ThrowArgumentException_SerializerDoesNotSupportComments("reader");
			}
			ReadStack readStack = default(ReadStack);
			readStack.Initialize(jsonTypeInfo, false);
			Utf8JsonReader utf8JsonReader = reader;
			object obj;
			try
			{
				Utf8JsonReader readerScopedToNextValue = JsonSerializer.GetReaderScopedToNextValue(ref reader, ref readStack);
				obj = jsonTypeInfo.DeserializeAsObject(ref readerScopedToNextValue, ref readStack);
			}
			catch (JsonException)
			{
				reader = utf8JsonReader;
				throw;
			}
			return obj;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00013214 File Offset: 0x00011414
		private unsafe static Utf8JsonReader GetReaderScopedToNextValue(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			ReadOnlySpan<byte> readOnlySpan = default(ReadOnlySpan<byte>);
			ReadOnlySequence<byte> readOnlySequence = default(ReadOnlySequence<byte>);
			try
			{
				JsonTokenType tokenType = reader.TokenType;
				if ((tokenType == JsonTokenType.None || tokenType == JsonTokenType.PropertyName) && !reader.Read())
				{
					ThrowHelper.ThrowJsonReaderException(ref reader, ExceptionResource.ExpectedOneCompleteToken, 0, default(ReadOnlySpan<byte>));
				}
				switch (reader.TokenType)
				{
				case JsonTokenType.StartObject:
				case JsonTokenType.StartArray:
				{
					long tokenStartIndex = reader.TokenStartIndex;
					if (!reader.TrySkip())
					{
						ThrowHelper.ThrowJsonReaderException(ref reader, ExceptionResource.NotEnoughData, 0, default(ReadOnlySpan<byte>));
					}
					long num = reader.BytesConsumed - tokenStartIndex;
					ReadOnlySequence<byte> originalSequence = reader.OriginalSequence;
					if (originalSequence.IsEmpty)
					{
						readOnlySpan = checked(reader.OriginalSpan.Slice((int)tokenStartIndex, (int)num));
						goto IL_01D6;
					}
					readOnlySequence = originalSequence.Slice(tokenStartIndex, num);
					goto IL_01D6;
				}
				case JsonTokenType.String:
				{
					ReadOnlySequence<byte> originalSequence2 = reader.OriginalSequence;
					if (originalSequence2.IsEmpty)
					{
						int num2 = reader.ValueSpan.Length + 2;
						readOnlySpan = reader.OriginalSpan.Slice((int)reader.TokenStartIndex, num2);
						goto IL_01D6;
					}
					long num3 = (reader.HasValueSequence ? (reader.ValueSequence.Length + 2L) : ((long)(reader.ValueSpan.Length + 2)));
					readOnlySequence = originalSequence2.Slice(reader.TokenStartIndex, num3);
					goto IL_01D6;
				}
				case JsonTokenType.Number:
				case JsonTokenType.True:
				case JsonTokenType.False:
				case JsonTokenType.Null:
					if (reader.HasValueSequence)
					{
						readOnlySequence = reader.ValueSequence;
						goto IL_01D6;
					}
					readOnlySpan = reader.ValueSpan;
					goto IL_01D6;
				}
				byte b = (reader.HasValueSequence ? (*reader.ValueSequence.First.Span[0]) : (*reader.ValueSpan[0]));
				ThrowHelper.ThrowJsonReaderException(ref reader, ExceptionResource.ExpectedStartOfValueNotFound, b, default(ReadOnlySpan<byte>));
				IL_01D6:;
			}
			catch (JsonReaderException ex)
			{
				ThrowHelper.ReThrowWithPath(ref state, ex);
			}
			if (!readOnlySpan.IsEmpty)
			{
				return new Utf8JsonReader(readOnlySpan, reader.CurrentState.Options);
			}
			return new Utf8JsonReader(readOnlySequence, reader.CurrentState.Options);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013458 File Offset: 0x00011658
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static byte[] SerializeToUtf8Bytes<[Nullable(2)] TValue>(TValue value, [Nullable(2)] JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.WriteBytes<TValue>(in value, typeInfo);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013474 File Offset: 0x00011674
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static byte[] SerializeToUtf8Bytes([Nullable(2)] object value, Type inputType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			return JsonSerializer.WriteBytesAsObject(value, typeInfo);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00013497 File Offset: 0x00011697
		public static byte[] SerializeToUtf8Bytes<[Nullable(2)] TValue>(TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteBytes<TValue>(in value, jsonTypeInfo);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000134B4 File Offset: 0x000116B4
		public static byte[] SerializeToUtf8Bytes([Nullable(2)] object value, JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteBytesAsObject(value, jsonTypeInfo);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000134D0 File Offset: 0x000116D0
		public static byte[] SerializeToUtf8Bytes([Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			return JsonSerializer.WriteBytesAsObject(value, typeInfo);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00013500 File Offset: 0x00011700
		private static byte[] WriteBytes<TValue>(in TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			byte[] array;
			try
			{
				jsonTypeInfo.Serialize(utf8JsonWriter, in value, null);
				array = pooledByteBufferWriter.WrittenMemory.ToArray();
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return array;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013550 File Offset: 0x00011750
		private static byte[] WriteBytesAsObject(object value, JsonTypeInfo jsonTypeInfo)
		{
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			byte[] array;
			try
			{
				jsonTypeInfo.SerializeAsObject(utf8JsonWriter, value);
				array = pooledByteBufferWriter.WrittenMemory.ToArray();
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return array;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000135A0 File Offset: 0x000117A0
		internal static MetadataPropertyName WriteMetadataForObject(JsonConverter jsonConverter, ref WriteStack state, Utf8JsonWriter writer)
		{
			MetadataPropertyName metadataPropertyName = MetadataPropertyName.None;
			if (state.NewReferenceId != null)
			{
				writer.WriteString(JsonSerializer.s_metadataId, state.NewReferenceId);
				metadataPropertyName |= MetadataPropertyName.Id;
				state.NewReferenceId = null;
			}
			object polymorphicTypeDiscriminator = state.PolymorphicTypeDiscriminator;
			if (polymorphicTypeDiscriminator != null)
			{
				JsonEncodedText? customTypeDiscriminatorPropertyNameJsonEncoded = state.PolymorphicTypeResolver.CustomTypeDiscriminatorPropertyNameJsonEncoded;
				JsonEncodedText jsonEncodedText;
				if (customTypeDiscriminatorPropertyNameJsonEncoded != null)
				{
					JsonEncodedText valueOrDefault = customTypeDiscriminatorPropertyNameJsonEncoded.GetValueOrDefault();
					jsonEncodedText = valueOrDefault;
				}
				else
				{
					jsonEncodedText = JsonSerializer.s_metadataType;
				}
				JsonEncodedText jsonEncodedText2 = jsonEncodedText;
				string text = polymorphicTypeDiscriminator as string;
				if (text != null)
				{
					writer.WriteString(jsonEncodedText2, text);
				}
				else
				{
					writer.WriteNumber(jsonEncodedText2, (int)polymorphicTypeDiscriminator);
				}
				metadataPropertyName |= MetadataPropertyName.Type;
				state.PolymorphicTypeDiscriminator = null;
			}
			return metadataPropertyName;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00013638 File Offset: 0x00011838
		internal static MetadataPropertyName WriteMetadataForCollection(JsonConverter jsonConverter, ref WriteStack state, Utf8JsonWriter writer)
		{
			writer.WriteStartObject();
			MetadataPropertyName metadataPropertyName = JsonSerializer.WriteMetadataForObject(jsonConverter, ref state, writer);
			writer.WritePropertyName(JsonSerializer.s_metadataValues);
			return metadataPropertyName;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013660 File Offset: 0x00011860
		internal static bool TryGetReferenceForValue(object currentValue, ref WriteStack state, Utf8JsonWriter writer)
		{
			bool flag;
			string reference = state.ReferenceResolver.GetReference(currentValue, out flag);
			if (flag)
			{
				writer.WriteStartObject();
				writer.WriteString(JsonSerializer.s_metadataRef, reference);
				writer.WriteEndObject();
				state.PolymorphicTypeDiscriminator = null;
				state.PolymorphicTypeResolver = null;
			}
			else
			{
				state.NewReferenceId = reference;
			}
			return flag;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000136B0 File Offset: 0x000118B0
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static Task SerializeAsync<[Nullable(2)] TValue>(Stream utf8Json, TValue value, [Nullable(2)] JsonSerializerOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return typeInfo.SerializeAsync(utf8Json, value, cancellationToken, null);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000136DC File Offset: 0x000118DC
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static void Serialize<[Nullable(2)] TValue>(Stream utf8Json, TValue value, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			typeInfo.Serialize(utf8Json, in value, null);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00013708 File Offset: 0x00011908
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static Task SerializeAsync(Stream utf8Json, [Nullable(2)] object value, Type inputType, [Nullable(2)] JsonSerializerOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			return typeInfo.SerializeAsObjectAsync(utf8Json, value, cancellationToken);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001373C File Offset: 0x0001193C
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static void Serialize(Stream utf8Json, [Nullable(2)] object value, Type inputType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			typeInfo.SerializeAsObject(utf8Json, value);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001376D File Offset: 0x0001196D
		public static Task SerializeAsync<[Nullable(2)] TValue>(Stream utf8Json, TValue value, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return jsonTypeInfo.SerializeAsync(utf8Json, value, cancellationToken, null);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00013799 File Offset: 0x00011999
		public static void Serialize<[Nullable(2)] TValue>(Stream utf8Json, TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			jsonTypeInfo.Serialize(utf8Json, in value, null);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000137C5 File Offset: 0x000119C5
		public static Task SerializeAsync(Stream utf8Json, [Nullable(2)] object value, JsonTypeInfo jsonTypeInfo, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return jsonTypeInfo.SerializeAsObjectAsync(utf8Json, value, cancellationToken);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000137F0 File Offset: 0x000119F0
		public static void Serialize(Stream utf8Json, [Nullable(2)] object value, JsonTypeInfo jsonTypeInfo)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			jsonTypeInfo.SerializeAsObject(utf8Json, value);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001381C File Offset: 0x00011A1C
		public static Task SerializeAsync(Stream utf8Json, [Nullable(2)] object value, Type inputType, JsonSerializerContext context, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			return typeInfo.SerializeAsObjectAsync(utf8Json, value, cancellationToken);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001385C File Offset: 0x00011A5C
		public static void Serialize(Stream utf8Json, [Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (utf8Json == null)
			{
				ThrowHelper.ThrowArgumentNullException("utf8Json");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			typeInfo.SerializeAsObject(utf8Json, value);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001389C File Offset: 0x00011A9C
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static string Serialize<[Nullable(2)] TValue>(TValue value, [Nullable(2)] JsonSerializerOptions options = null)
		{
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			return JsonSerializer.WriteString<TValue>(in value, typeInfo);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000138B8 File Offset: 0x00011AB8
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static string Serialize([Nullable(2)] object value, Type inputType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			return JsonSerializer.WriteStringAsObject(value, typeInfo);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000138DB File Offset: 0x00011ADB
		public static string Serialize<[Nullable(2)] TValue>(TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteString<TValue>(in value, jsonTypeInfo);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000138F8 File Offset: 0x00011AF8
		public static string Serialize([Nullable(2)] object value, JsonTypeInfo jsonTypeInfo)
		{
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			return JsonSerializer.WriteStringAsObject(value, jsonTypeInfo);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00013914 File Offset: 0x00011B14
		public static string Serialize([Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			return JsonSerializer.WriteStringAsObject(value, typeInfo);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00013944 File Offset: 0x00011B44
		private static string WriteString<TValue>(in TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			string text;
			try
			{
				jsonTypeInfo.Serialize(utf8JsonWriter, in value, null);
				text = JsonReaderHelper.TranscodeHelper(pooledByteBufferWriter.WrittenMemory.Span);
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return text;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00013998 File Offset: 0x00011B98
		private static string WriteStringAsObject(object value, JsonTypeInfo jsonTypeInfo)
		{
			PooledByteBufferWriter pooledByteBufferWriter;
			Utf8JsonWriter utf8JsonWriter = Utf8JsonWriterCache.RentWriterAndBuffer(jsonTypeInfo.Options, out pooledByteBufferWriter);
			string text;
			try
			{
				jsonTypeInfo.SerializeAsObject(utf8JsonWriter, value);
				text = JsonReaderHelper.TranscodeHelper(pooledByteBufferWriter.WrittenMemory.Span);
			}
			finally
			{
				Utf8JsonWriterCache.ReturnWriterAndBuffer(utf8JsonWriter, pooledByteBufferWriter);
			}
			return text;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000139EC File Offset: 0x00011BEC
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static void Serialize<[Nullable(2)] TValue>(Utf8JsonWriter writer, TValue value, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			JsonTypeInfo<TValue> typeInfo = JsonSerializer.GetTypeInfo<TValue>(options);
			typeInfo.Serialize(writer, in value, null);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00013A18 File Offset: 0x00011C18
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public static void Serialize(Utf8JsonWriter writer, [Nullable(2)] object value, Type inputType, [Nullable(2)] JsonSerializerOptions options = null)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(options, inputType);
			typeInfo.SerializeAsObject(writer, value);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00013A49 File Offset: 0x00011C49
		public static void Serialize<[Nullable(2)] TValue>(Utf8JsonWriter writer, TValue value, JsonTypeInfo<TValue> jsonTypeInfo)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			jsonTypeInfo.Serialize(writer, in value, null);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00013A75 File Offset: 0x00011C75
		public static void Serialize(Utf8JsonWriter writer, [Nullable(2)] object value, JsonTypeInfo jsonTypeInfo)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			if (jsonTypeInfo == null)
			{
				ThrowHelper.ThrowArgumentNullException("jsonTypeInfo");
			}
			jsonTypeInfo.EnsureConfigured();
			jsonTypeInfo.SerializeAsObject(writer, value);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public static void Serialize(Utf8JsonWriter writer, [Nullable(2)] object value, Type inputType, JsonSerializerContext context)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			if (context == null)
			{
				ThrowHelper.ThrowArgumentNullException("context");
			}
			JsonSerializer.ValidateInputType(value, inputType);
			JsonTypeInfo typeInfo = JsonSerializer.GetTypeInfo(context, inputType);
			typeInfo.SerializeAsObject(writer, value);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00013AE0 File Offset: 0x00011CE0
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static JsonSerializer()
		{
			bool flag;
			JsonSerializer.IsReflectionEnabledByDefault = !AppContext.TryGetSwitch("System.Text.Json.JsonSerializer.IsReflectionEnabledByDefault", out flag) || flag;
			JsonSerializer.s_idPropertyName = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F630228BD37E08E6923D5691940E6DFEEDB75FB8BC0780EE1ABA98C0C1A49778), 3).ToArray();
			JsonSerializer.s_refPropertyName = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.84499CE395D80E3A7694D419919F5383E3C8B46E4B78F0FA291576406ECE3FA2), 4).ToArray();
			JsonSerializer.s_typePropertyName = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AB50C59D4F64B05ABA854F60FD7DA6C6EB8F4715BCCE00520E5ACA886CB5B92C), 5).ToArray();
			JsonSerializer.s_valuesPropertyName = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F2CBFF984BEDEC6EBB2619964766BBE0A2EF28F9A3E5FF81ED9B35C90A9E1C70), 7).ToArray();
			JsonSerializer.s_metadataId = JsonEncodedText.Encode("$id", null);
			JsonSerializer.s_metadataRef = JsonEncodedText.Encode("$ref", null);
			JsonSerializer.s_metadataType = JsonEncodedText.Encode("$type", null);
			JsonSerializer.s_metadataValues = JsonEncodedText.Encode("$values", null);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00013BA4 File Offset: 0x00011DA4
		[CompilerGenerated]
		internal static void <UnboxOnRead>g__ThrowUnableToCastValue|50_0<T>(object value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowInvalidOperationException_DeserializeUnableToAssignNull(typeof(T));
				return;
			}
			ThrowHelper.ThrowInvalidCastException_DeserializeUnableToAssignValue(value.GetType(), typeof(T));
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00013BD0 File Offset: 0x00011DD0
		[CompilerGenerated]
		internal static string <TryHandleReferenceFromJsonNode>g__ReadAsStringMetadataValue|64_0(JsonNode jsonNode)
		{
			JsonValue jsonValue = jsonNode as JsonValue;
			string text;
			if (jsonValue != null && jsonValue.TryGetValue<string>(out text) && text != null)
			{
				return text;
			}
			JsonValueKind jsonValueKind;
			if (jsonNode != null)
			{
				if (!(jsonNode is JsonObject))
				{
					if (!(jsonNode is JsonArray))
					{
						JsonValue<JsonElement> jsonValue2 = jsonNode as JsonValue<JsonElement>;
						if (jsonValue2 == null)
						{
							jsonValueKind = JsonValueKind.Undefined;
						}
						else
						{
							jsonValueKind = jsonValue2.Value.ValueKind;
						}
					}
					else
					{
						jsonValueKind = JsonValueKind.Array;
					}
				}
				else
				{
					jsonValueKind = JsonValueKind.Object;
				}
			}
			else
			{
				jsonValueKind = JsonValueKind.Null;
			}
			JsonValueKind jsonValueKind2 = jsonValueKind;
			ThrowHelper.ThrowJsonException_MetadataValueWasNotString(jsonValueKind2);
			return null;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00013C40 File Offset: 0x00011E40
		[AsyncIteratorStateMachine(typeof(JsonSerializer.<<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d<>))]
		[CompilerGenerated]
		internal static IAsyncEnumerable<T> <DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0<T>(Stream utf8Json, JsonTypeInfo<Queue<T>> queueTypeInfo, [EnumeratorCancellation] CancellationToken cancellationToken)
		{
			JsonSerializer.<<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d<T> <<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d = new JsonSerializer.<<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d<T>(-2);
			<<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d.<>3__utf8Json = utf8Json;
			<<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d.<>3__queueTypeInfo = queueTypeInfo;
			<<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d.<>3__cancellationToken = cancellationToken;
			return <<DeserializeAsyncEnumerableCore>g__CreateAsyncEnumerable|90_0>d;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00013C60 File Offset: 0x00011E60
		[CompilerGenerated]
		internal static JsonTypeInfo<Queue<T>> <DeserializeAsyncEnumerableCore>g__CreateQueueTypeInfo|90_1<T>(JsonTypeInfo<T> jsonTypeInfo)
		{
			QueueOfTConverter<Queue<T>, T> queueOfTConverter = new QueueOfTConverter<Queue<T>, T>();
			JsonTypeInfo<Queue<T>> jsonTypeInfo2 = new JsonTypeInfo<Queue<T>>(queueOfTConverter, jsonTypeInfo.Options);
			jsonTypeInfo2.CreateObject = () => new Queue<T>();
			jsonTypeInfo2.ElementTypeInfo = jsonTypeInfo;
			jsonTypeInfo2.NumberHandling = new JsonNumberHandling?(jsonTypeInfo.Options.NumberHandling);
			JsonTypeInfo<Queue<T>> jsonTypeInfo3 = jsonTypeInfo2;
			jsonTypeInfo3.EnsureConfigured();
			jsonTypeInfo._asyncEnumerableQueueTypeInfo = jsonTypeInfo3;
			return jsonTypeInfo3;
		}

		// Token: 0x0400018E RID: 398
		internal const string SerializationUnreferencedCodeMessage = "JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.";

		// Token: 0x0400018F RID: 399
		internal const string SerializationRequiresDynamicCodeMessage = "JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.";

		// Token: 0x04000191 RID: 401
		internal const string IdPropertyName = "$id";

		// Token: 0x04000192 RID: 402
		internal const string RefPropertyName = "$ref";

		// Token: 0x04000193 RID: 403
		internal const string TypePropertyName = "$type";

		// Token: 0x04000194 RID: 404
		internal const string ValuesPropertyName = "$values";

		// Token: 0x04000195 RID: 405
		private static readonly byte[] s_idPropertyName;

		// Token: 0x04000196 RID: 406
		private static readonly byte[] s_refPropertyName;

		// Token: 0x04000197 RID: 407
		private static readonly byte[] s_typePropertyName;

		// Token: 0x04000198 RID: 408
		private static readonly byte[] s_valuesPropertyName;

		// Token: 0x04000199 RID: 409
		internal static readonly JsonEncodedText s_metadataId;

		// Token: 0x0400019A RID: 410
		internal static readonly JsonEncodedText s_metadataRef;

		// Token: 0x0400019B RID: 411
		internal static readonly JsonEncodedText s_metadataType;

		// Token: 0x0400019C RID: 412
		internal static readonly JsonEncodedText s_metadataValues;

		// Token: 0x0400019D RID: 413
		internal const float FlushThreshold = 0.9f;
	}
}
