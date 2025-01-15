using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Reflection;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E9 RID: 233
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class ObjectConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x0003066E File Offset: 0x0002E86E
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		public ObjectConverterFactory(bool useDefaultConstructorInUnannotatedStructs = true)
		{
			this._useDefaultConstructorInUnannotatedStructs = useDefaultConstructorInUnannotatedStructs;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0003067D File Offset: 0x0002E87D
		public override bool CanConvert(Type typeToConvert)
		{
			return true;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00030680 File Offset: 0x0002E880
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2067:UnrecognizedReflectionPattern", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			bool flag = this._useDefaultConstructorInUnannotatedStructs && !typeToConvert.IsKeyValuePair();
			ConstructorInfo constructorInfo;
			if (!typeToConvert.TryGetDeserializationConstructor(flag, out constructorInfo))
			{
				ThrowHelper.ThrowInvalidOperationException_SerializationDuplicateTypeAttribute<JsonConstructorAttribute>(typeToConvert);
			}
			ParameterInfo[] array = ((constructorInfo != null) ? constructorInfo.GetParameters() : null);
			Type type;
			if (constructorInfo == null || typeToConvert.IsAbstract || array.Length == 0)
			{
				type = typeof(ObjectDefaultConverter<>).MakeGenericType(new Type[] { typeToConvert });
			}
			else
			{
				int num = array.Length;
				if (num <= 4)
				{
					Type objectType = JsonTypeInfo.ObjectType;
					Type[] array2 = new Type[5];
					array2[0] = typeToConvert;
					for (int i = 0; i < 4; i++)
					{
						if (i < num)
						{
							array2[i + 1] = array[i].ParameterType;
						}
						else
						{
							array2[i + 1] = objectType;
						}
					}
					type = typeof(SmallObjectWithParameterizedConstructorConverter<, , , , >).MakeGenericType(array2);
				}
				else
				{
					type = typeof(LargeObjectWithParameterizedConstructorConverterWithReflection<>).MakeGenericType(new Type[] { typeToConvert });
				}
			}
			JsonConverter jsonConverter = (JsonConverter)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public, null, null, null);
			jsonConverter.ConstructorInfo = constructorInfo;
			return jsonConverter;
		}

		// Token: 0x04000409 RID: 1033
		private readonly bool _useDefaultConstructorInUnannotatedStructs;
	}
}
