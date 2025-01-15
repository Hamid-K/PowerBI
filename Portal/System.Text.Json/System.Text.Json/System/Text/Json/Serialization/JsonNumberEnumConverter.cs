using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Converters;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JsonNumberEnumConverter<[Nullable(0)] TEnum> : JsonConverterFactory where TEnum : struct, Enum
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x00024BA5 File Offset: 0x00022DA5
		public override bool CanConvert(Type typeToConvert)
		{
			return typeToConvert == typeof(TEnum);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00024BB7 File Offset: 0x00022DB7
		[return: Nullable(2)]
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			if (typeToConvert != typeof(TEnum))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_JsonConverterFactory_TypeNotSupported(typeToConvert);
			}
			return new EnumConverter<TEnum>(EnumConverterOptions.AllowNumbers, options);
		}
	}
}
