using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Json.Nodes
{
	// Token: 0x02000063 RID: 99
	[DebuggerDisplay("{ToJsonString(),nq}")]
	[DebuggerTypeProxy(typeof(JsonValue<>.DebugView))]
	internal abstract class JsonValue<TValue> : JsonValue
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x0002318B File Offset: 0x0002138B
		protected JsonValue(TValue value, JsonNodeOptions? options = null)
			: base(options)
		{
			if (value is JsonNode)
			{
				ThrowHelper.ThrowArgumentException_NodeValueNotAllowed("value");
			}
			this.Value = value;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000231B4 File Offset: 0x000213B4
		public override T GetValue<T>()
		{
			TValue tvalue = this.Value;
			if (tvalue is T)
			{
				return tvalue as T;
			}
			if (this.Value is JsonElement)
			{
				return this.ConvertJsonElement<T>();
			}
			string nodeUnableToConvert = SR.NodeUnableToConvert;
			tvalue = this.Value;
			throw new InvalidOperationException(SR.Format(nodeUnableToConvert, tvalue.GetType(), typeof(T)));
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00023230 File Offset: 0x00021430
		public override bool TryGetValue<T>([NotNullWhen(true)] out T value)
		{
			TValue value2 = this.Value;
			if (value2 is T)
			{
				T t = value2 as T;
				value = t;
				return true;
			}
			if (this.Value is JsonElement)
			{
				return this.TryConvertJsonElement<T>(out value);
			}
			value = default(T);
			return false;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00023290 File Offset: 0x00021490
		internal sealed override JsonValueKind GetValueKindCore()
		{
			TValue value = this.Value;
			if (value is JsonElement)
			{
				return (value as JsonElement).ValueKind;
			}
			JsonValueKind valueKind;
			using (PooledByteBufferWriter pooledByteBufferWriter = base.WriteToPooledBuffer(null, default(JsonWriterOptions), 16384))
			{
				valueKind = JsonElement.ParseValue(pooledByteBufferWriter.WrittenMemory.Span, default(JsonDocumentOptions)).ValueKind;
			}
			return valueKind;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00023328 File Offset: 0x00021528
		internal sealed override bool DeepEqualsCore(JsonNode otherNode)
		{
			if (otherNode == null)
			{
				return false;
			}
			TValue value = this.Value;
			if (value is JsonElement)
			{
				JsonElement jsonElement = value as JsonElement;
				JsonValue<JsonElement> jsonValue = otherNode as JsonValue<JsonElement>;
				if (jsonValue != null)
				{
					JsonElement value2 = jsonValue.Value;
					if (jsonElement.ValueKind != value2.ValueKind)
					{
						return false;
					}
					switch (jsonElement.ValueKind)
					{
					case JsonValueKind.String:
						return jsonElement.ValueEquals(value2.GetString());
					case JsonValueKind.Number:
						return jsonElement.GetRawValue().Span.SequenceEqual(value2.GetRawValue().Span);
					case JsonValueKind.True:
					case JsonValueKind.False:
					case JsonValueKind.Null:
						return true;
					default:
						return false;
					}
				}
			}
			bool flag;
			using (PooledByteBufferWriter pooledByteBufferWriter = base.WriteToPooledBuffer(null, default(JsonWriterOptions), 16384))
			{
				using (PooledByteBufferWriter pooledByteBufferWriter2 = otherNode.WriteToPooledBuffer(null, default(JsonWriterOptions), 16384))
				{
					flag = pooledByteBufferWriter.WrittenMemory.Span.SequenceEqual(pooledByteBufferWriter2.WrittenMemory.Span);
				}
			}
			return flag;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00023474 File Offset: 0x00021674
		internal TypeToConvert ConvertJsonElement<TypeToConvert>()
		{
			JsonElement jsonElement = (JsonElement)((object)this.Value);
			switch (jsonElement.ValueKind)
			{
			case JsonValueKind.String:
				if (typeof(TypeToConvert) == typeof(string))
				{
					return (TypeToConvert)((object)jsonElement.GetString());
				}
				if (typeof(TypeToConvert) == typeof(DateTime) || typeof(TypeToConvert) == typeof(DateTime?))
				{
					return (TypeToConvert)((object)jsonElement.GetDateTime());
				}
				if (typeof(TypeToConvert) == typeof(DateTimeOffset) || typeof(TypeToConvert) == typeof(DateTimeOffset?))
				{
					return (TypeToConvert)((object)jsonElement.GetDateTimeOffset());
				}
				if (typeof(TypeToConvert) == typeof(Guid) || typeof(TypeToConvert) == typeof(Guid?))
				{
					return (TypeToConvert)((object)jsonElement.GetGuid());
				}
				if (typeof(TypeToConvert) == typeof(char) || typeof(TypeToConvert) == typeof(char?))
				{
					string @string = jsonElement.GetString();
					if (@string.Length == 1)
					{
						return (TypeToConvert)((object)@string[0]);
					}
				}
				break;
			case JsonValueKind.Number:
				if (typeof(TypeToConvert) == typeof(int) || typeof(TypeToConvert) == typeof(int?))
				{
					return (TypeToConvert)((object)jsonElement.GetInt32());
				}
				if (typeof(TypeToConvert) == typeof(long) || typeof(TypeToConvert) == typeof(long?))
				{
					return (TypeToConvert)((object)jsonElement.GetInt64());
				}
				if (typeof(TypeToConvert) == typeof(double) || typeof(TypeToConvert) == typeof(double?))
				{
					return (TypeToConvert)((object)jsonElement.GetDouble());
				}
				if (typeof(TypeToConvert) == typeof(short) || typeof(TypeToConvert) == typeof(short?))
				{
					return (TypeToConvert)((object)jsonElement.GetInt16());
				}
				if (typeof(TypeToConvert) == typeof(decimal) || typeof(TypeToConvert) == typeof(decimal?))
				{
					return (TypeToConvert)((object)jsonElement.GetDecimal());
				}
				if (typeof(TypeToConvert) == typeof(byte) || typeof(TypeToConvert) == typeof(byte?))
				{
					return (TypeToConvert)((object)jsonElement.GetByte());
				}
				if (typeof(TypeToConvert) == typeof(float) || typeof(TypeToConvert) == typeof(float?))
				{
					return (TypeToConvert)((object)jsonElement.GetSingle());
				}
				if (typeof(TypeToConvert) == typeof(uint) || typeof(TypeToConvert) == typeof(uint?))
				{
					return (TypeToConvert)((object)jsonElement.GetUInt32());
				}
				if (typeof(TypeToConvert) == typeof(ushort) || typeof(TypeToConvert) == typeof(ushort?))
				{
					return (TypeToConvert)((object)jsonElement.GetUInt16());
				}
				if (typeof(TypeToConvert) == typeof(ulong) || typeof(TypeToConvert) == typeof(ulong?))
				{
					return (TypeToConvert)((object)jsonElement.GetUInt64());
				}
				if (typeof(TypeToConvert) == typeof(sbyte) || typeof(TypeToConvert) == typeof(sbyte?))
				{
					return (TypeToConvert)((object)jsonElement.GetSByte());
				}
				break;
			case JsonValueKind.True:
			case JsonValueKind.False:
				if (typeof(TypeToConvert) == typeof(bool) || typeof(TypeToConvert) == typeof(bool?))
				{
					return (TypeToConvert)((object)jsonElement.GetBoolean());
				}
				break;
			}
			throw new InvalidOperationException(SR.Format(SR.NodeUnableToConvertElement, jsonElement.ValueKind, typeof(TypeToConvert)));
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00023998 File Offset: 0x00021B98
		internal bool TryConvertJsonElement<TypeToConvert>([NotNullWhen(true)] out TypeToConvert result)
		{
			JsonElement jsonElement = (JsonElement)((object)this.Value);
			switch (jsonElement.ValueKind)
			{
			case JsonValueKind.String:
				if (typeof(TypeToConvert) == typeof(string))
				{
					string @string = jsonElement.GetString();
					result = (TypeToConvert)((object)@string);
					return true;
				}
				if (typeof(TypeToConvert) == typeof(DateTime) || typeof(TypeToConvert) == typeof(DateTime?))
				{
					DateTime dateTime;
					bool flag = jsonElement.TryGetDateTime(out dateTime);
					result = (TypeToConvert)((object)dateTime);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(DateTimeOffset) || typeof(TypeToConvert) == typeof(DateTimeOffset?))
				{
					DateTimeOffset dateTimeOffset;
					bool flag = jsonElement.TryGetDateTimeOffset(out dateTimeOffset);
					result = (TypeToConvert)((object)dateTimeOffset);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(Guid) || typeof(TypeToConvert) == typeof(Guid?))
				{
					Guid guid;
					bool flag = jsonElement.TryGetGuid(out guid);
					result = (TypeToConvert)((object)guid);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(char) || typeof(TypeToConvert) == typeof(char?))
				{
					string string2 = jsonElement.GetString();
					if (string2.Length == 1)
					{
						result = (TypeToConvert)((object)string2[0]);
						return true;
					}
				}
				break;
			case JsonValueKind.Number:
				if (typeof(TypeToConvert) == typeof(int) || typeof(TypeToConvert) == typeof(int?))
				{
					int num;
					bool flag = jsonElement.TryGetInt32(out num);
					result = (TypeToConvert)((object)num);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(long) || typeof(TypeToConvert) == typeof(long?))
				{
					long num2;
					bool flag = jsonElement.TryGetInt64(out num2);
					result = (TypeToConvert)((object)num2);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(double) || typeof(TypeToConvert) == typeof(double?))
				{
					double num3;
					bool flag = jsonElement.TryGetDouble(out num3);
					result = (TypeToConvert)((object)num3);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(short) || typeof(TypeToConvert) == typeof(short?))
				{
					short num4;
					bool flag = jsonElement.TryGetInt16(out num4);
					result = (TypeToConvert)((object)num4);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(decimal) || typeof(TypeToConvert) == typeof(decimal?))
				{
					decimal num5;
					bool flag = jsonElement.TryGetDecimal(out num5);
					result = (TypeToConvert)((object)num5);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(byte) || typeof(TypeToConvert) == typeof(byte?))
				{
					byte b;
					bool flag = jsonElement.TryGetByte(out b);
					result = (TypeToConvert)((object)b);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(float) || typeof(TypeToConvert) == typeof(float?))
				{
					float num6;
					bool flag = jsonElement.TryGetSingle(out num6);
					result = (TypeToConvert)((object)num6);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(uint) || typeof(TypeToConvert) == typeof(uint?))
				{
					uint num7;
					bool flag = jsonElement.TryGetUInt32(out num7);
					result = (TypeToConvert)((object)num7);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(ushort) || typeof(TypeToConvert) == typeof(ushort?))
				{
					ushort num8;
					bool flag = jsonElement.TryGetUInt16(out num8);
					result = (TypeToConvert)((object)num8);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(ulong) || typeof(TypeToConvert) == typeof(ulong?))
				{
					ulong num9;
					bool flag = jsonElement.TryGetUInt64(out num9);
					result = (TypeToConvert)((object)num9);
					return flag;
				}
				if (typeof(TypeToConvert) == typeof(sbyte) || typeof(TypeToConvert) == typeof(sbyte?))
				{
					sbyte b2;
					bool flag = jsonElement.TryGetSByte(out b2);
					result = (TypeToConvert)((object)b2);
					return flag;
				}
				break;
			case JsonValueKind.True:
			case JsonValueKind.False:
				if (typeof(TypeToConvert) == typeof(bool) || typeof(TypeToConvert) == typeof(bool?))
				{
					result = (TypeToConvert)((object)jsonElement.GetBoolean());
					return true;
				}
				break;
			}
			result = default(TypeToConvert);
			return false;
		}

		// Token: 0x0400028A RID: 650
		internal readonly TValue Value;

		// Token: 0x02000130 RID: 304
		[ExcludeFromCodeCoverage]
		[DebuggerDisplay("{Json,nq}")]
		private sealed class DebugView
		{
			// Token: 0x06000DD0 RID: 3536 RVA: 0x00035C90 File Offset: 0x00033E90
			public DebugView(JsonValue<TValue> node)
			{
				this._node = node;
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00035C9F File Offset: 0x00033E9F
			public string Json
			{
				get
				{
					return this._node.ToJsonString(null);
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00035CAD File Offset: 0x00033EAD
			public string Path
			{
				get
				{
					return this._node.GetPath();
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00035CBA File Offset: 0x00033EBA
			public TValue Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x040004BF RID: 1215
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public JsonValue<TValue> _node;
		}
	}
}
