using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000BC RID: 188
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class MemoryConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0002DD38 File Offset: 0x0002BF38
		public override bool CanConvert(Type typeToConvert)
		{
			if (!typeToConvert.IsGenericType || !typeToConvert.IsValueType)
			{
				return false;
			}
			Type genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(Memory<>) || genericTypeDefinition == typeof(ReadOnlyMemory<>);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002DD84 File Offset: 0x0002BF84
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type type = ((typeToConvert.GetGenericTypeDefinition() == typeof(Memory<>)) ? typeof(MemoryConverter<>) : typeof(ReadOnlyMemoryConverter<>));
			Type type2 = typeToConvert.GetGenericArguments()[0];
			return (JsonConverter)Activator.CreateInstance(type.MakeGenericType(new Type[] { type2 }));
		}
	}
}
