using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000BF RID: 191
	internal sealed class LargeObjectWithParameterizedConstructorConverterWithReflection<T> : LargeObjectWithParameterizedConstructorConverter<T>
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x0002DF62 File Offset: 0x0002C162
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public LargeObjectWithParameterizedConstructorConverterWithReflection()
		{
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002DF6A File Offset: 0x0002C16A
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal override void ConfigureJsonTypeInfoUsingReflection(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			jsonTypeInfo.CreateObjectWithArgs = DefaultJsonTypeInfoResolver.MemberAccessor.CreateParameterizedConstructor<T>(base.ConstructorInfo);
		}
	}
}
