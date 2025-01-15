using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000BE RID: 190
	internal sealed class JsonMetadataServicesConverter<T> : JsonResumableConverter<T>
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002DE04 File Offset: 0x0002C004
		internal JsonConverter<T> Converter { get; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0002DE0C File Offset: 0x0002C00C
		internal override Type KeyType
		{
			get
			{
				return this.Converter.KeyType;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002DE19 File Offset: 0x0002C019
		internal override Type ElementType
		{
			get
			{
				return this.Converter.ElementType;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002DE26 File Offset: 0x0002C026
		public override bool HandleNull { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002DE2E File Offset: 0x0002C02E
		internal override bool ConstructorIsParameterized
		{
			get
			{
				return this.Converter.ConstructorIsParameterized;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0002DE3B File Offset: 0x0002C03B
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return this.Converter.SupportsCreateObjectDelegate;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0002DE48 File Offset: 0x0002C048
		internal override bool CanHaveMetadata
		{
			get
			{
				return this.Converter.CanHaveMetadata;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0002DE55 File Offset: 0x0002C055
		internal override bool CanPopulate
		{
			get
			{
				return this.Converter.CanPopulate;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002DE64 File Offset: 0x0002C064
		public JsonMetadataServicesConverter(JsonConverter<T> converter)
		{
			this.Converter = converter;
			base.ConverterStrategy = converter.ConverterStrategy;
			base.IsInternalConverter = converter.IsInternalConverter;
			base.IsInternalConverterForNumberType = converter.IsInternalConverterForNumberType;
			base.CanBePolymorphic = converter.CanBePolymorphic;
			base.HandleNullOnRead = converter.HandleNullOnRead;
			base.HandleNullOnWrite = converter.HandleNullOnWrite;
			this.HandleNull = converter.HandleNullOnWrite;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002DED2 File Offset: 0x0002C0D2
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out T value)
		{
			return this.Converter.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
		internal override bool OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, ref WriteStack state)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			if (!state.SupportContinuation && jsonTypeInfo.CanUseSerializeHandler && !JsonHelpers.RequiresSpecialNumberHandlingOnWrite(state.Current.NumberHandling) && !state.CurrentContainsMetadata)
			{
				((JsonTypeInfo<T>)jsonTypeInfo).SerializeHandler(writer, value);
				return true;
			}
			return this.Converter.OnTryWrite(writer, value, options, ref state);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002DF53 File Offset: 0x0002C153
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			this.Converter.ConfigureJsonTypeInfo(jsonTypeInfo, options);
		}
	}
}
