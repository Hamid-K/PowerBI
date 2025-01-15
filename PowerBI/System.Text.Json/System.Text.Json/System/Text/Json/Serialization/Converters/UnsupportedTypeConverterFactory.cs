using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000109 RID: 265
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class UnsupportedTypeConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000D1A RID: 3354 RVA: 0x000331B4 File Offset: 0x000313B4
		public override bool CanConvert(Type type)
		{
			return typeof(MemberInfo).IsAssignableFrom(type) || type == typeof(SerializationInfo) || type == typeof(IntPtr) || type == typeof(UIntPtr) || typeof(Delegate).IsAssignableFrom(type);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003321B File Offset: 0x0003141B
		public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
		{
			return UnsupportedTypeConverterFactory.CreateUnsupportedConverterForType(type, null);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00033224 File Offset: 0x00031424
		internal static JsonConverter CreateUnsupportedConverterForType(Type type, string errorMessage = null)
		{
			return (JsonConverter)Activator.CreateInstance(typeof(UnsupportedTypeConverter<>).MakeGenericType(new Type[] { type }), BindingFlags.Instance | BindingFlags.Public, null, new object[] { errorMessage }, null);
		}
	}
}
