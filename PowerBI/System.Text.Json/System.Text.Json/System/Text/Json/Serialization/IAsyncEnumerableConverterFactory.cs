using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Reflection;
using System.Text.Json.Serialization.Converters;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000087 RID: 135
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class IAsyncEnumerableConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x00024ECF File Offset: 0x000230CF
		public override bool CanConvert(Type typeToConvert)
		{
			return IAsyncEnumerableConverterFactory.GetAsyncEnumerableInterface(typeToConvert) != null;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00024EE0 File Offset: 0x000230E0
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type asyncEnumerableInterface = IAsyncEnumerableConverterFactory.GetAsyncEnumerableInterface(typeToConvert);
			Type type = asyncEnumerableInterface.GetGenericArguments()[0];
			Type type2 = typeof(IAsyncEnumerableOfTConverter<, >).MakeGenericType(new Type[] { typeToConvert, type });
			return (JsonConverter)Activator.CreateInstance(type2);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00024F26 File Offset: 0x00023126
		private static Type GetAsyncEnumerableInterface(Type type)
		{
			return type.GetCompatibleGenericInterface(typeof(IAsyncEnumerable<>));
		}
	}
}
