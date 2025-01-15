using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x000261F8 File Offset: 0x000243F8
		internal T ReadCore(ref Utf8JsonReader reader, JsonSerializerOptions options, ref ReadStack state)
		{
			T t;
			try
			{
				if (!state.IsContinuation)
				{
					if (!JsonConverter.SingleValueReadWithReadAhead(base.RequiresReadAhead, ref reader, ref state))
					{
						if (!state.SupportContinuation)
						{
							state.BytesConsumed += reader.BytesConsumed;
							return default(T);
						}
						state.BytesConsumed += reader.BytesConsumed;
						if (state.Current.ReturnValue == null)
						{
							t = default(T);
							return t;
						}
						return (T)((object)state.Current.ReturnValue);
					}
				}
				else if (!JsonConverter.SingleValueReadWithReadAhead(true, ref reader, ref state))
				{
					state.BytesConsumed += reader.BytesConsumed;
					return default(T);
				}
				T t2;
				bool flag2;
				bool flag = this.TryRead(ref reader, state.Current.JsonTypeInfo.Type, options, ref state, out t2, out flag2);
				if (flag && !reader.Read() && !reader.IsFinalBlock)
				{
					state.Current.ReturnValue = t2;
				}
				state.BytesConsumed += reader.BytesConsumed;
				t = t2;
			}
			catch (JsonReaderException ex)
			{
				ThrowHelper.ReThrowWithPath(ref state, ex);
				t = default(T);
			}
			catch (FormatException ex2) when (ex2.Source == "System.Text.Json.Rethrowable")
			{
				ThrowHelper.ReThrowWithPath(ref state, in reader, ex2);
				t = default(T);
			}
			catch (InvalidOperationException ex3) when (ex3.Source == "System.Text.Json.Rethrowable")
			{
				ThrowHelper.ReThrowWithPath(ref state, in reader, ex3);
				t = default(T);
			}
			catch (JsonException ex4) when (ex4.Path == null)
			{
				ThrowHelper.AddJsonExceptionInformation(ref state, in reader, ex4);
				throw;
			}
			catch (NotSupportedException ex5)
			{
				if (ex5.Message.Contains(" Path: "))
				{
					throw;
				}
				ThrowHelper.ThrowNotSupportedException(ref state, in reader, ex5);
				t = default(T);
			}
			return t;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002646C File Offset: 0x0002466C
		internal bool WriteCore(Utf8JsonWriter writer, in T value, JsonSerializerOptions options, ref WriteStack state)
		{
			bool flag;
			try
			{
				flag = this.TryWrite(writer, in value, options, ref state);
			}
			catch (InvalidOperationException ex) when (ex.Source == "System.Text.Json.Rethrowable")
			{
				ThrowHelper.ReThrowWithPath(ref state, ex);
				throw;
			}
			catch (JsonException ex2) when (ex2.Path == null)
			{
				ThrowHelper.AddJsonExceptionInformation(ref state, ex2);
				throw;
			}
			catch (NotSupportedException ex3)
			{
				if (ex3.Message.Contains(" Path: "))
				{
					throw;
				}
				ThrowHelper.ThrowNotSupportedException(ref state, ex3);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00026524 File Offset: 0x00024724
		protected internal JsonConverter()
		{
			base.IsValueType = typeof(T).IsValueType;
			if (this.HandleNull)
			{
				base.HandleNullOnRead = true;
				base.HandleNullOnWrite = true;
				return;
			}
			if (base.UsesDefaultHandleNull)
			{
				base.HandleNullOnRead = default(T) != null;
				base.HandleNullOnWrite = false;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002659C File Offset: 0x0002479C
		public override bool CanConvert(Type typeToConvert)
		{
			return typeToConvert == typeof(T);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000265AE File Offset: 0x000247AE
		private protected override ConverterStrategy GetDefaultConverterStrategy()
		{
			return ConverterStrategy.Value;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000265B1 File Offset: 0x000247B1
		internal sealed override JsonTypeInfo CreateJsonTypeInfo(JsonSerializerOptions options)
		{
			return new JsonTypeInfo<T>(this, options);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000265BA File Offset: 0x000247BA
		[Nullable(2)]
		internal override Type KeyType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000265BD File Offset: 0x000247BD
		[Nullable(2)]
		internal override Type ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000265C0 File Offset: 0x000247C0
		public virtual bool HandleNull
		{
			get
			{
				base.UsesDefaultHandleNull = true;
				return false;
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000265CC File Offset: 0x000247CC
		internal sealed override void WriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(value);
			this.Write(writer, t, options);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000265EC File Offset: 0x000247EC
		internal sealed override bool OnTryWriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, ref WriteStack state)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(value);
			return this.OnTryWrite(writer, t, options, ref state);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002660C File Offset: 0x0002480C
		internal sealed override void WriteAsPropertyNameAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(value);
			this.WriteAsPropertyName(writer, t, options);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002662C File Offset: 0x0002482C
		internal sealed override void WriteAsPropertyNameCoreAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(value);
			this.WriteAsPropertyNameCore(writer, t, options, isWritingExtensionDataProperty);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002664C File Offset: 0x0002484C
		internal sealed override void WriteNumberWithCustomHandlingAsObject(Utf8JsonWriter writer, object value, JsonNumberHandling handling)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(value);
			this.WriteNumberWithCustomHandling(writer, t, handling);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002666C File Offset: 0x0002486C
		internal sealed override bool TryWriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, ref WriteStack state)
		{
			T t = JsonSerializer.UnboxOnWrite<T>(value);
			return this.TryWrite(writer, in t, options, ref state);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002668C File Offset: 0x0002488C
		internal virtual bool OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, ref WriteStack state)
		{
			this.Write(writer, value, options);
			return true;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00026698 File Offset: 0x00024898
		internal virtual bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out T value)
		{
			value = this.Read(ref reader, typeToConvert, options);
			return true;
		}

		// Token: 0x060008D7 RID: 2263
		[return: Nullable(2)]
		public abstract T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);

		// Token: 0x060008D8 RID: 2264 RVA: 0x000266AC File Offset: 0x000248AC
		internal bool TryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out T value, out bool isPopulatedValue)
		{
			if (reader.TokenType == JsonTokenType.Null && !base.HandleNullOnRead && !state.IsContinuation)
			{
				if (default(T) != null)
				{
					ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
				}
				value = default(T);
				isPopulatedValue = false;
				return true;
			}
			if (base.ConverterStrategy == ConverterStrategy.Value)
			{
				if (base.IsInternalConverter)
				{
					if (state.Current.NumberHandling != null && base.IsInternalConverterForNumberType)
					{
						value = this.ReadNumberWithCustomHandling(ref reader, state.Current.NumberHandling.Value, options);
					}
					else
					{
						value = this.Read(ref reader, typeToConvert, options);
					}
				}
				else
				{
					JsonTokenType tokenType = reader.TokenType;
					int currentDepth = reader.CurrentDepth;
					long bytesConsumed = reader.BytesConsumed;
					if (state.Current.NumberHandling != null && base.IsInternalConverterForNumberType)
					{
						value = this.ReadNumberWithCustomHandling(ref reader, state.Current.NumberHandling.Value, options);
					}
					else
					{
						value = this.Read(ref reader, typeToConvert, options);
					}
					this.VerifyRead(tokenType, currentDepth, bytesConsumed, true, ref reader);
				}
				isPopulatedValue = false;
				return true;
			}
			bool isContinuation = state.IsContinuation;
			bool flag;
			if (base.CanBePolymorphic)
			{
				flag = this.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
				isPopulatedValue = false;
				return true;
			}
			JsonPropertyInfo jsonPropertyInfo = state.Current.JsonPropertyInfo;
			object returnValue = state.Current.ReturnValue;
			state.Push();
			if (returnValue != null && jsonPropertyInfo != null && !jsonPropertyInfo.IsForTypeInfo)
			{
				state.Current.HasParentObject = true;
			}
			flag = this.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
			isPopulatedValue = state.Current.IsPopulating;
			state.Pop(flag);
			return flag;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00026864 File Offset: 0x00024A64
		internal sealed override bool OnTryReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value)
		{
			T t;
			bool flag = this.OnTryRead(ref reader, typeToConvert, options, ref state, out t);
			value = t;
			return flag;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002688C File Offset: 0x00024A8C
		internal sealed override bool TryReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value)
		{
			T t;
			bool flag2;
			bool flag = this.TryRead(ref reader, typeToConvert, options, ref state, out t, out flag2);
			value = t;
			return flag;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000268B4 File Offset: 0x00024AB4
		internal sealed override object ReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			T t = this.Read(ref reader, typeToConvert, options);
			return t;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000268D4 File Offset: 0x00024AD4
		internal sealed override object ReadAsPropertyNameAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			T t = this.ReadAsPropertyName(ref reader, typeToConvert, options);
			return t;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000268F4 File Offset: 0x00024AF4
		internal sealed override object ReadAsPropertyNameCoreAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			T t = this.ReadAsPropertyNameCore(ref reader, typeToConvert, options);
			return t;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00026914 File Offset: 0x00024B14
		internal sealed override object ReadNumberWithCustomHandlingAsObject(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			T t = this.ReadNumberWithCustomHandling(ref reader, handling, options);
			return t;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00026931 File Offset: 0x00024B31
		private static bool IsNull(T value)
		{
			return value == null;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002693C File Offset: 0x00024B3C
		internal bool TryWrite(Utf8JsonWriter writer, in T value, JsonSerializerOptions options, ref WriteStack state)
		{
			if (writer.CurrentDepth >= options.EffectiveMaxDepth)
			{
				ThrowHelper.ThrowJsonException_SerializerCycleDetected(options.EffectiveMaxDepth);
			}
			if (default(T) == null && !base.HandleNullOnWrite && JsonConverter<T>.IsNull(value))
			{
				writer.WriteNullValue();
				return true;
			}
			if (base.ConverterStrategy == ConverterStrategy.Value)
			{
				int currentDepth = writer.CurrentDepth;
				if (state.Current.NumberHandling != null && base.IsInternalConverterForNumberType)
				{
					this.WriteNumberWithCustomHandling(writer, value, state.Current.NumberHandling.Value);
				}
				else
				{
					this.Write(writer, value, options);
				}
				this.VerifyWrite(currentDepth, writer);
				return true;
			}
			bool isContinuation = state.IsContinuation;
			bool flag;
			if (!base.IsValueType && value != null && state.Current.PolymorphicSerializationState != PolymorphicSerializationState.PolymorphicReEntryStarted)
			{
				JsonTypeInfo jsonTypeInfo = state.PeekNestedJsonTypeInfo();
				JsonConverter jsonConverter = ((base.CanBePolymorphic || jsonTypeInfo.PolymorphicTypeResolver != null) ? base.ResolvePolymorphicConverter(value, jsonTypeInfo, options, ref state) : null);
				if (!isContinuation && options.ReferenceHandlingStrategy != ReferenceHandlingStrategy.None && base.TryHandleSerializedObjectReference(writer, value, options, jsonConverter, ref state))
				{
					return true;
				}
				if (jsonConverter != null)
				{
					flag = jsonConverter.TryWriteAsObject(writer, value, options, ref state);
					state.Current.ExitPolymorphicConverter(flag);
					if (flag && state.Current.IsPushedReferenceForCycleDetection)
					{
						state.ReferenceResolver.PopReferenceForCycleDetection();
						state.Current.IsPushedReferenceForCycleDetection = false;
					}
					return flag;
				}
			}
			state.Push();
			flag = this.OnTryWrite(writer, value, options, ref state);
			state.Pop(flag);
			if (flag && state.Current.IsPushedReferenceForCycleDetection)
			{
				state.ReferenceResolver.PopReferenceForCycleDetection();
				state.Current.IsPushedReferenceForCycleDetection = false;
			}
			return flag;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00026B30 File Offset: 0x00024D30
		internal bool TryWriteDataExtensionProperty(Utf8JsonWriter writer, T value, JsonSerializerOptions options, ref WriteStack state)
		{
			if (!base.IsInternalConverter)
			{
				return this.TryWrite(writer, in value, options, ref state);
			}
			JsonDictionaryConverter<T> jsonDictionaryConverter;
			if ((jsonDictionaryConverter = this as JsonDictionaryConverter<T>) == null)
			{
				JsonMetadataServicesConverter<T> jsonMetadataServicesConverter = this as JsonMetadataServicesConverter<T>;
				jsonDictionaryConverter = ((jsonMetadataServicesConverter != null) ? jsonMetadataServicesConverter.Converter : null) as JsonDictionaryConverter<T>;
			}
			JsonDictionaryConverter<T> jsonDictionaryConverter2 = jsonDictionaryConverter;
			if (jsonDictionaryConverter2 == null)
			{
				return this.TryWrite(writer, in value, options, ref state);
			}
			if (writer.CurrentDepth >= options.EffectiveMaxDepth)
			{
				ThrowHelper.ThrowJsonException_SerializerCycleDetected(options.EffectiveMaxDepth);
			}
			bool isContinuation = state.IsContinuation;
			state.Push();
			if (!isContinuation)
			{
				state.Current.OriginalDepth = writer.CurrentDepth;
			}
			state.Current.IsWritingExtensionDataProperty = true;
			state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
			bool flag = jsonDictionaryConverter2.OnWriteResume(writer, value, options, ref state);
			if (flag)
			{
				this.VerifyWrite(state.Current.OriginalDepth, writer);
			}
			state.Pop(flag);
			return flag;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00026C1B File Offset: 0x00024E1B
		public sealed override Type Type { get; } = typeof(T);

		// Token: 0x060008E3 RID: 2275 RVA: 0x00026C24 File Offset: 0x00024E24
		internal void VerifyRead(JsonTokenType tokenType, int depth, long bytesConsumed, bool isValueConverter, ref Utf8JsonReader reader)
		{
			if (tokenType != JsonTokenType.StartObject)
			{
				if (tokenType == JsonTokenType.StartArray)
				{
					if (reader.TokenType != JsonTokenType.EndArray)
					{
						ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
						return;
					}
					if (depth != reader.CurrentDepth)
					{
						ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
						return;
					}
				}
				else if (isValueConverter)
				{
					if (reader.BytesConsumed != bytesConsumed)
					{
						ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
						return;
					}
				}
				else if (!base.CanBePolymorphic && (!base.HandleNullOnRead || tokenType != JsonTokenType.Null))
				{
					ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
				}
			}
			else
			{
				if (reader.TokenType != JsonTokenType.EndObject)
				{
					ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
					return;
				}
				if (depth != reader.CurrentDepth)
				{
					ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
					return;
				}
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00026CAD File Offset: 0x00024EAD
		internal void VerifyWrite(int originalDepth, Utf8JsonWriter writer)
		{
			if (originalDepth != writer.CurrentDepth)
			{
				ThrowHelper.ThrowJsonException_SerializationConverterWrite(this);
			}
		}

		// Token: 0x060008E5 RID: 2277
		public abstract void Write(Utf8JsonWriter writer, [Nullable(0)] T value, JsonSerializerOptions options);

		// Token: 0x060008E6 RID: 2278 RVA: 0x00026CC0 File Offset: 0x00024EC0
		public virtual T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			JsonConverter<T> fallbackConverterForPropertyNameSerialization = this.GetFallbackConverterForPropertyNameSerialization(options);
			if (fallbackConverterForPropertyNameSerialization == null)
			{
				ThrowHelper.ThrowNotSupportedException_DictionaryKeyTypeNotSupported(this.Type, this);
			}
			return fallbackConverterForPropertyNameSerialization.ReadAsPropertyNameCore(ref reader, typeToConvert, options);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00026CF0 File Offset: 0x00024EF0
		internal virtual T ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			long bytesConsumed = reader.BytesConsumed;
			T t = this.ReadAsPropertyName(ref reader, typeToConvert, options);
			if (reader.BytesConsumed != bytesConsumed)
			{
				ThrowHelper.ThrowJsonException_SerializationConverterRead(this);
			}
			return t;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00026D20 File Offset: 0x00024F20
		public virtual void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] T value, JsonSerializerOptions options)
		{
			JsonConverter<T> fallbackConverterForPropertyNameSerialization = this.GetFallbackConverterForPropertyNameSerialization(options);
			if (fallbackConverterForPropertyNameSerialization == null)
			{
				ThrowHelper.ThrowNotSupportedException_DictionaryKeyTypeNotSupported(this.Type, this);
			}
			fallbackConverterForPropertyNameSerialization.WriteAsPropertyNameCore(writer, value, options, false);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00026D50 File Offset: 0x00024F50
		internal virtual void WriteAsPropertyNameCore(Utf8JsonWriter writer, [DisallowNull] T value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			if (isWritingExtensionDataProperty)
			{
				writer.WritePropertyName((string)((object)value));
				return;
			}
			int currentDepth = writer.CurrentDepth;
			this.WriteAsPropertyName(writer, value, options);
			if (currentDepth != writer.CurrentDepth || writer.TokenType != JsonTokenType.PropertyName)
			{
				ThrowHelper.ThrowJsonException_SerializationConverterWrite(this);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00026DB0 File Offset: 0x00024FB0
		private JsonConverter<T> GetFallbackConverterForPropertyNameSerialization(JsonSerializerOptions options)
		{
			JsonConverter<T> jsonConverter = null;
			if (!base.IsInternalConverter && !(options.TypeInfoResolver is JsonSerializerContext))
			{
				jsonConverter = this._fallbackConverterForPropertyNameSerialization;
				JsonConverter jsonConverter2;
				if (jsonConverter == null && DefaultJsonTypeInfoResolver.TryGetDefaultSimpleConverter(this.Type, out jsonConverter2))
				{
					jsonConverter = (this._fallbackConverterForPropertyNameSerialization = (JsonConverter<T>)jsonConverter2);
				}
			}
			return jsonConverter;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00026DFC File Offset: 0x00024FFC
		internal virtual T ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00026E0E File Offset: 0x0002500E
		internal virtual void WriteNumberWithCustomHandling(Utf8JsonWriter writer, T value, JsonNumberHandling handling)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x040002FB RID: 763
		private JsonConverter<T> _fallbackConverterForPropertyNameSerialization;
	}
}
