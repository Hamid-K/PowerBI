using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F7 RID: 247
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class EnumConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x000328DB File Offset: 0x00030ADB
		public override bool CanConvert(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000328E3 File Offset: 0x00030AE3
		public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
		{
			return EnumConverterFactory.Create(type, EnumConverterOptions.AllowNumbers, null, options);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000328EE File Offset: 0x00030AEE
		internal static JsonConverter Create(Type enumType, EnumConverterOptions converterOptions, JsonNamingPolicy namingPolicy, JsonSerializerOptions options)
		{
			return (JsonConverter)Activator.CreateInstance(EnumConverterFactory.GetEnumConverterType(enumType), new object[] { converterOptions, namingPolicy, options });
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00032917 File Offset: 0x00030B17
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2070:UnrecognizedReflectionPattern", Justification = "'EnumConverter<T> where T : struct' implies 'T : new()', so the trimmer is warning calling MakeGenericType here because enumType's constructors are not annotated. But EnumConverter doesn't call new T(), so this is safe.")]
		[return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
		private static Type GetEnumConverterType(Type enumType)
		{
			return typeof(EnumConverter<>).MakeGenericType(new Type[] { enumType });
		}
	}
}
