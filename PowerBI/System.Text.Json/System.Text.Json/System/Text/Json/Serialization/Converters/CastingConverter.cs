using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000B8 RID: 184
	internal sealed class CastingConverter<T> : JsonConverter<T>
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002D9FE File Offset: 0x0002BBFE
		internal override Type KeyType
		{
			get
			{
				return this._sourceConverter.KeyType;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0002DA0B File Offset: 0x0002BC0B
		internal override Type ElementType
		{
			get
			{
				return this._sourceConverter.ElementType;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002DA18 File Offset: 0x0002BC18
		public override bool HandleNull { get; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0002DA20 File Offset: 0x0002BC20
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return this._sourceConverter.SupportsCreateObjectDelegate;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002DA30 File Offset: 0x0002BC30
		internal CastingConverter(JsonConverter sourceConverter)
		{
			this._sourceConverter = sourceConverter;
			base.IsInternalConverter = sourceConverter.IsInternalConverter;
			base.IsInternalConverterForNumberType = sourceConverter.IsInternalConverterForNumberType;
			base.ConverterStrategy = sourceConverter.ConverterStrategy;
			base.CanBePolymorphic = sourceConverter.CanBePolymorphic;
			base.HandleNullOnRead = sourceConverter.HandleNullOnRead;
			base.HandleNullOnWrite = sourceConverter.HandleNullOnWrite;
			this.HandleNull = sourceConverter.HandleNullOnWrite;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0002DA9E File Offset: 0x0002BC9E
		internal override JsonConverter SourceConverterForCastingConverter
		{
			get
			{
				return this._sourceConverter;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002DAA6 File Offset: 0x0002BCA6
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonSerializer.UnboxOnRead<T>(this._sourceConverter.ReadAsObject(ref reader, typeToConvert, options));
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002DABB File Offset: 0x0002BCBB
		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			this._sourceConverter.WriteAsObject(writer, value, options);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out T value)
		{
			object obj;
			bool flag = this._sourceConverter.OnTryReadAsObject(ref reader, typeToConvert, options, ref state, out obj);
			value = JsonSerializer.UnboxOnRead<T>(obj);
			return flag;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002DAFE File Offset: 0x0002BCFE
		internal override bool OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, ref WriteStack state)
		{
			return this._sourceConverter.OnTryWriteAsObject(writer, value, options, ref state);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002DB15 File Offset: 0x0002BD15
		public override T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonSerializer.UnboxOnRead<T>(this._sourceConverter.ReadAsPropertyNameAsObject(ref reader, typeToConvert, options));
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002DB2A File Offset: 0x0002BD2A
		internal override T ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonSerializer.UnboxOnRead<T>(this._sourceConverter.ReadAsPropertyNameCoreAsObject(ref reader, typeToConvert, options));
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002DB3F File Offset: 0x0002BD3F
		public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] T value, JsonSerializerOptions options)
		{
			this._sourceConverter.WriteAsPropertyNameAsObject(writer, value, options);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002DB54 File Offset: 0x0002BD54
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, T value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			this._sourceConverter.WriteAsPropertyNameCoreAsObject(writer, value, options, isWritingExtensionDataProperty);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002DB6B File Offset: 0x0002BD6B
		internal override T ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			return JsonSerializer.UnboxOnRead<T>(this._sourceConverter.ReadNumberWithCustomHandlingAsObject(ref reader, handling, options));
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002DB80 File Offset: 0x0002BD80
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, T value, JsonNumberHandling handling)
		{
			this._sourceConverter.WriteNumberWithCustomHandlingAsObject(writer, value, handling);
		}

		// Token: 0x040003F0 RID: 1008
		private readonly JsonConverter _sourceConverter;
	}
}
