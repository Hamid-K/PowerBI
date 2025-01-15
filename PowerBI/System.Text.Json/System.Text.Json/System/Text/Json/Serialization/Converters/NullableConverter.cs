using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000FF RID: 255
	internal sealed class NullableConverter<T> : JsonConverter<T?> where T : struct
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00032ACB File Offset: 0x00030CCB
		internal override Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00032AD7 File Offset: 0x00030CD7
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00032ADA File Offset: 0x00030CDA
		internal override bool CanPopulate
		{
			get
			{
				return this._elementConverter.CanPopulate;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00032AE7 File Offset: 0x00030CE7
		internal override bool ConstructorIsParameterized
		{
			get
			{
				return this._elementConverter.ConstructorIsParameterized;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00032AF4 File Offset: 0x00030CF4
		public NullableConverter(JsonConverter<T> elementConverter)
		{
			this._elementConverter = elementConverter;
			base.IsInternalConverterForNumberType = elementConverter.IsInternalConverterForNumberType;
			base.ConverterStrategy = elementConverter.ConverterStrategy;
			base.ConstructorInfo = elementConverter.ConstructorInfo;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00032B28 File Offset: 0x00030D28
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out T? value)
		{
			if (!state.IsContinuation && reader.TokenType == JsonTokenType.Null)
			{
				value = null;
				return true;
			}
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			state.Current.JsonTypeInfo = state.Current.JsonTypeInfo.ElementTypeInfo;
			T t;
			if (this._elementConverter.OnTryRead(ref reader, typeof(T), options, ref state, out t))
			{
				value = new T?(t);
				state.Current.JsonTypeInfo = jsonTypeInfo;
				return true;
			}
			state.Current.JsonTypeInfo = jsonTypeInfo;
			value = null;
			return false;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00032BCC File Offset: 0x00030DCC
		internal override bool OnTryWrite(Utf8JsonWriter writer, T? value, JsonSerializerOptions options, ref WriteStack state)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return true;
			}
			state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
			JsonConverter<T> elementConverter = this._elementConverter;
			T value2 = value.Value;
			return elementConverter.TryWrite(writer, in value2, options, ref state);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00032C28 File Offset: 0x00030E28
		public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}
			T t = this._elementConverter.Read(ref reader, typeof(T), options);
			return new T?(t);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00032C67 File Offset: 0x00030E67
		public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			this._elementConverter.Write(writer, value.Value, options);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00032C90 File Offset: 0x00030E90
		internal override T? ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling numberHandling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}
			T t = this._elementConverter.ReadNumberWithCustomHandling(ref reader, numberHandling, options);
			return new T?(t);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00032CC6 File Offset: 0x00030EC6
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, T? value, JsonNumberHandling handling)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			this._elementConverter.WriteNumberWithCustomHandling(writer, value.Value, handling);
		}

		// Token: 0x04000416 RID: 1046
		private readonly JsonConverter<T> _elementConverter;
	}
}
