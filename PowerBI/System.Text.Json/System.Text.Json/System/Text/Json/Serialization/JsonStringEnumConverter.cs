using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000092 RID: 146
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonStringEnumConverter<[Nullable(0)] TEnum> : JsonConverterFactory where TEnum : struct, Enum
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x00026EE0 File Offset: 0x000250E0
		public JsonStringEnumConverter()
			: this(null, true)
		{
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00026EEA File Offset: 0x000250EA
		[NullableContext(2)]
		public JsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
		{
			this._namingPolicy = namingPolicy;
			this._converterOptions = (allowIntegerValues ? (EnumConverterOptions.AllowStrings | EnumConverterOptions.AllowNumbers) : EnumConverterOptions.AllowStrings);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00026F06 File Offset: 0x00025106
		public sealed override bool CanConvert(Type typeToConvert)
		{
			return typeToConvert == typeof(TEnum);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00026F18 File Offset: 0x00025118
		[return: Nullable(2)]
		public sealed override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			if (typeToConvert != typeof(TEnum))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_JsonConverterFactory_TypeNotSupported(typeToConvert);
			}
			return new EnumConverter<TEnum>(this._converterOptions, this._namingPolicy, options);
		}

		// Token: 0x040002FC RID: 764
		private readonly JsonNamingPolicy _namingPolicy;

		// Token: 0x040002FD RID: 765
		private readonly EnumConverterOptions _converterOptions;
	}
}
