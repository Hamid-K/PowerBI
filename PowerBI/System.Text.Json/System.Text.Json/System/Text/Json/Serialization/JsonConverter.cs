using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200008E RID: 142
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonConverter
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00025D5F File Offset: 0x00023F5F
		internal JsonConverter()
		{
			this.IsInternalConverter = base.GetType().Assembly == typeof(JsonConverter).Assembly;
			this.ConverterStrategy = this.GetDefaultConverterStrategy();
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600087C RID: 2172
		public abstract Type Type { get; }

		// Token: 0x0600087D RID: 2173
		[NullableContext(1)]
		public abstract bool CanConvert(Type typeToConvert);

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00025D98 File Offset: 0x00023F98
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x00025DA0 File Offset: 0x00023FA0
		internal ConverterStrategy ConverterStrategy
		{
			get
			{
				return this._converterStrategy;
			}
			set
			{
				this.CanUseDirectReadOrWrite = value == ConverterStrategy.Value && this.IsInternalConverter;
				this.RequiresReadAhead = value == ConverterStrategy.Value;
				this._converterStrategy = value;
			}
		}

		// Token: 0x06000880 RID: 2176
		private protected abstract ConverterStrategy GetDefaultConverterStrategy();

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00025DC6 File Offset: 0x00023FC6
		internal virtual bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00025DC9 File Offset: 0x00023FC9
		internal virtual bool CanPopulate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00025DCC File Offset: 0x00023FCC
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00025DD4 File Offset: 0x00023FD4
		internal bool CanUseDirectReadOrWrite { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00025DDD File Offset: 0x00023FDD
		internal virtual bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00025DE0 File Offset: 0x00023FE0
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x00025DE8 File Offset: 0x00023FE8
		internal bool CanBePolymorphic { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00025DF1 File Offset: 0x00023FF1
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00025DF9 File Offset: 0x00023FF9
		internal bool RequiresReadAhead { get; set; }

		// Token: 0x0600088A RID: 2186 RVA: 0x00025E02 File Offset: 0x00024002
		internal virtual void ReadElementAndSetProperty(object obj, string propertyName, ref Utf8JsonReader reader, JsonSerializerOptions options, [ScopedRef] ref ReadStack state)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00025E09 File Offset: 0x00024009
		internal virtual JsonTypeInfo CreateJsonTypeInfo(JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00025E10 File Offset: 0x00024010
		internal JsonConverter<TTarget> CreateCastingConverter<TTarget>()
		{
			JsonConverter<TTarget> jsonConverter = this as JsonConverter<TTarget>;
			if (jsonConverter != null)
			{
				return jsonConverter;
			}
			JsonSerializerOptions.CheckConverterNullabilityIsSameAsPropertyType(this, typeof(TTarget));
			JsonConverter sourceConverterForCastingConverter = this.SourceConverterForCastingConverter;
			return ((sourceConverterForCastingConverter != null) ? sourceConverterForCastingConverter.CreateCastingConverter<TTarget>() : null) ?? new CastingConverter<TTarget>(this);
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00025E55 File Offset: 0x00024055
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00025E5D File Offset: 0x0002405D
		protected internal bool UsesDefaultHandleNull { internal get; private protected set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00025E66 File Offset: 0x00024066
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00025E6E File Offset: 0x0002406E
		protected internal bool HandleNullOnRead { internal get; private protected set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00025E77 File Offset: 0x00024077
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00025E7F File Offset: 0x0002407F
		protected internal bool HandleNullOnWrite { internal get; private protected set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00025E88 File Offset: 0x00024088
		internal virtual JsonConverter SourceConverterForCastingConverter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000894 RID: 2196
		internal abstract Type ElementType { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000895 RID: 2197
		internal abstract Type KeyType { get; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00025E8B File Offset: 0x0002408B
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x00025E93 File Offset: 0x00024093
		internal bool IsValueType { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00025E9C File Offset: 0x0002409C
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x00025EA4 File Offset: 0x000240A4
		internal bool IsInternalConverter { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00025EAD File Offset: 0x000240AD
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x00025EB5 File Offset: 0x000240B5
		internal bool IsInternalConverterForNumberType { get; set; }

		// Token: 0x0600089C RID: 2204 RVA: 0x00025EBE File Offset: 0x000240BE
		internal static bool ShouldFlush(Utf8JsonWriter writer, ref WriteStack state)
		{
			return state.FlushThreshold > 0 && writer.BytesPending > state.FlushThreshold;
		}

		// Token: 0x0600089D RID: 2205
		internal abstract object ReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);

		// Token: 0x0600089E RID: 2206
		internal abstract bool OnTryReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value);

		// Token: 0x0600089F RID: 2207
		internal abstract bool TryReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value);

		// Token: 0x060008A0 RID: 2208
		internal abstract object ReadAsPropertyNameAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);

		// Token: 0x060008A1 RID: 2209
		internal abstract object ReadAsPropertyNameCoreAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);

		// Token: 0x060008A2 RID: 2210
		internal abstract object ReadNumberWithCustomHandlingAsObject(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options);

		// Token: 0x060008A3 RID: 2211
		internal abstract void WriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options);

		// Token: 0x060008A4 RID: 2212
		internal abstract bool OnTryWriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, ref WriteStack state);

		// Token: 0x060008A5 RID: 2213
		internal abstract bool TryWriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, ref WriteStack state);

		// Token: 0x060008A6 RID: 2214
		internal abstract void WriteAsPropertyNameAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options);

		// Token: 0x060008A7 RID: 2215
		internal abstract void WriteAsPropertyNameCoreAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, bool isWritingExtensionDataProperty);

		// Token: 0x060008A8 RID: 2216
		internal abstract void WriteNumberWithCustomHandlingAsObject(Utf8JsonWriter writer, object value, JsonNumberHandling handling);

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00025ED9 File Offset: 0x000240D9
		internal virtual bool ConstructorIsParameterized { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00025EE1 File Offset: 0x000240E1
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00025EE9 File Offset: 0x000240E9
		internal ConstructorInfo ConstructorInfo { get; set; }

		// Token: 0x060008AC RID: 2220 RVA: 0x00025EF2 File Offset: 0x000240F2
		internal virtual void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00025EF4 File Offset: 0x000240F4
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal virtual void ConfigureJsonTypeInfoUsingReflection(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00025EF8 File Offset: 0x000240F8
		internal JsonConverter ResolvePolymorphicConverter(JsonTypeInfo jsonTypeInfo, ref ReadStack state)
		{
			JsonConverter jsonConverter = null;
			switch (state.Current.PolymorphicSerializationState)
			{
			case PolymorphicSerializationState.None:
			{
				PolymorphicTypeResolver polymorphicTypeResolver = jsonTypeInfo.PolymorphicTypeResolver;
				JsonTypeInfo jsonTypeInfo2;
				if (polymorphicTypeResolver.TryGetDerivedJsonTypeInfo(state.PolymorphicTypeDiscriminator, out jsonTypeInfo2))
				{
					jsonConverter = state.InitializePolymorphicReEntry(jsonTypeInfo2);
					if (!jsonConverter.CanHaveMetadata)
					{
						ThrowHelper.ThrowNotSupportedException_DerivedConverterDoesNotSupportMetadata(jsonTypeInfo2.Type);
					}
				}
				else
				{
					state.Current.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryNotFound;
				}
				state.PolymorphicTypeDiscriminator = null;
				break;
			}
			case PolymorphicSerializationState.PolymorphicReEntrySuspended:
				jsonConverter = state.ResumePolymorphicReEntry();
				break;
			}
			return jsonConverter;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00025F7C File Offset: 0x0002417C
		internal JsonConverter ResolvePolymorphicConverter(object value, JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options, ref WriteStack state)
		{
			JsonConverter jsonConverter = null;
			switch (state.Current.PolymorphicSerializationState)
			{
			case PolymorphicSerializationState.None:
			{
				Type type = value.GetType();
				if (this.CanBePolymorphic && type != this.Type)
				{
					jsonTypeInfo = state.Current.InitializePolymorphicReEntry(type, options);
					jsonConverter = jsonTypeInfo.Converter;
				}
				PolymorphicTypeResolver polymorphicTypeResolver = jsonTypeInfo.PolymorphicTypeResolver;
				JsonTypeInfo jsonTypeInfo2;
				object obj;
				if (polymorphicTypeResolver != null && polymorphicTypeResolver.TryGetDerivedJsonTypeInfo(type, out jsonTypeInfo2, out obj))
				{
					jsonConverter = state.Current.InitializePolymorphicReEntry(jsonTypeInfo2);
					if (obj != null)
					{
						if (!jsonConverter.CanHaveMetadata)
						{
							ThrowHelper.ThrowNotSupportedException_DerivedConverterDoesNotSupportMetadata(jsonTypeInfo2.Type);
						}
						state.PolymorphicTypeDiscriminator = obj;
						state.PolymorphicTypeResolver = polymorphicTypeResolver;
					}
				}
				if (jsonConverter == null)
				{
					state.Current.PolymorphicSerializationState = PolymorphicSerializationState.PolymorphicReEntryNotFound;
				}
				break;
			}
			case PolymorphicSerializationState.PolymorphicReEntrySuspended:
				jsonConverter = state.Current.ResumePolymorphicReEntry();
				break;
			}
			return jsonConverter;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00026058 File Offset: 0x00024258
		internal bool TryHandleSerializedObjectReference(Utf8JsonWriter writer, object value, JsonSerializerOptions options, JsonConverter polymorphicConverter, ref WriteStack state)
		{
			ReferenceHandlingStrategy referenceHandlingStrategy = options.ReferenceHandlingStrategy;
			if (referenceHandlingStrategy != ReferenceHandlingStrategy.Preserve)
			{
				if (referenceHandlingStrategy == ReferenceHandlingStrategy.IgnoreCycles)
				{
					ReferenceResolver referenceResolver = state.ReferenceResolver;
					if (referenceResolver.ContainsReferenceForCycleDetection(value))
					{
						writer.WriteNullValue();
						return true;
					}
					referenceResolver.PushReferenceForCycleDetection(value);
					state.Current.IsPushedReferenceForCycleDetection = state.CurrentDepth > 0;
				}
			}
			else
			{
				bool flag = ((polymorphicConverter != null) ? polymorphicConverter.CanHaveMetadata : this.CanHaveMetadata);
				if (flag && JsonSerializer.TryGetReferenceForValue(value, ref state, writer))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000260D4 File Offset: 0x000242D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool SingleValueReadWithReadAhead(bool requiresReadAhead, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			if (!requiresReadAhead || !state.ReadAhead)
			{
				return reader.Read();
			}
			return JsonConverter.DoSingleValueReadWithReadAhead(ref reader);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00026100 File Offset: 0x00024300
		internal static bool DoSingleValueReadWithReadAhead(ref Utf8JsonReader reader)
		{
			Utf8JsonReader utf8JsonReader = reader;
			if (!reader.Read())
			{
				return false;
			}
			JsonTokenType tokenType = reader.TokenType;
			bool flag = tokenType == JsonTokenType.StartObject || tokenType == JsonTokenType.StartArray;
			if (flag)
			{
				bool flag2 = reader.TrySkip();
				reader = utf8JsonReader;
				if (!flag2)
				{
					return false;
				}
				(ref reader).ReadWithVerify();
			}
			return true;
		}

		// Token: 0x040002EE RID: 750
		private ConverterStrategy _converterStrategy;
	}
}
