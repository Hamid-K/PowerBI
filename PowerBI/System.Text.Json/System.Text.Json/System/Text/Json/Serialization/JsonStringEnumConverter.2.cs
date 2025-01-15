using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000093 RID: 147
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresDynamicCode("JsonStringEnumConverter cannot be statically analyzed and requires runtime code generation. Applications should use the generic JsonStringEnumConverter<TEnum> instead.")]
	public class JsonStringEnumConverter : JsonConverterFactory
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x00026F44 File Offset: 0x00025144
		public JsonStringEnumConverter()
			: this(null, true)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00026F4E File Offset: 0x0002514E
		[NullableContext(2)]
		public JsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
		{
			this._namingPolicy = namingPolicy;
			this._converterOptions = (allowIntegerValues ? (EnumConverterOptions.AllowStrings | EnumConverterOptions.AllowNumbers) : EnumConverterOptions.AllowStrings);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00026F6A File Offset: 0x0002516A
		public sealed override bool CanConvert(Type typeToConvert)
		{
			return typeToConvert.IsEnum;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00026F72 File Offset: 0x00025172
		public sealed override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			if (!typeToConvert.IsEnum)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_JsonConverterFactory_TypeNotSupported(typeToConvert);
			}
			return EnumConverterFactory.Create(typeToConvert, this._converterOptions, this._namingPolicy, options);
		}

		// Token: 0x040002FE RID: 766
		private readonly JsonNamingPolicy _namingPolicy;

		// Token: 0x040002FF RID: 767
		private readonly EnumConverterOptions _converterOptions;
	}
}
