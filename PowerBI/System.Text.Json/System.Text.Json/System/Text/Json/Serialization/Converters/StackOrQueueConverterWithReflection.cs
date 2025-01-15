using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000BD RID: 189
	internal sealed class StackOrQueueConverterWithReflection<TCollection> : StackOrQueueConverter<TCollection> where TCollection : IEnumerable
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0002DDEA File Offset: 0x0002BFEA
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		public StackOrQueueConverterWithReflection()
		{
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002DDF2 File Offset: 0x0002BFF2
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal override void ConfigureJsonTypeInfoUsingReflection(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			jsonTypeInfo.AddMethodDelegate = DefaultJsonTypeInfoResolver.MemberAccessor.CreateAddMethodDelegate<TCollection>();
		}
	}
}
