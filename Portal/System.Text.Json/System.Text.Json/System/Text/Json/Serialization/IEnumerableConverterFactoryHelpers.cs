using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Reflection;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000088 RID: 136
	internal static class IEnumerableConverterFactoryHelpers
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00024F38 File Offset: 0x00023138
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public static MethodInfo GetImmutableEnumerableCreateRangeMethod(this Type type, Type elementType)
		{
			Type immutableEnumerableConstructingType = IEnumerableConverterFactoryHelpers.GetImmutableEnumerableConstructingType(type);
			if (immutableEnumerableConstructingType != null)
			{
				MethodInfo[] methods = immutableEnumerableConstructingType.GetMethods();
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.Name == "CreateRange" && methodInfo.GetParameters().Length == 1 && methodInfo.IsGenericMethod && methodInfo.GetGenericArguments().Length == 1)
					{
						return methodInfo.MakeGenericMethod(new Type[] { elementType });
					}
				}
			}
			ThrowHelper.ThrowNotSupportedException_SerializationNotSupported(type);
			return null;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00024FC0 File Offset: 0x000231C0
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public static MethodInfo GetImmutableDictionaryCreateRangeMethod(this Type type, Type keyType, Type valueType)
		{
			Type immutableDictionaryConstructingType = IEnumerableConverterFactoryHelpers.GetImmutableDictionaryConstructingType(type);
			if (immutableDictionaryConstructingType != null)
			{
				MethodInfo[] methods = immutableDictionaryConstructingType.GetMethods();
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.Name == "CreateRange" && methodInfo.GetParameters().Length == 1 && methodInfo.IsGenericMethod && methodInfo.GetGenericArguments().Length == 2)
					{
						return methodInfo.MakeGenericMethod(new Type[] { keyType, valueType });
					}
				}
			}
			ThrowHelper.ThrowNotSupportedException_SerializationNotSupported(type);
			return null;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002504C File Offset: 0x0002324C
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		private static Type GetImmutableEnumerableConstructingType(Type type)
		{
			string immutableEnumerableConstructingTypeName = type.GetImmutableEnumerableConstructingTypeName();
			if (immutableEnumerableConstructingTypeName != null)
			{
				return type.Assembly.GetType(immutableEnumerableConstructingTypeName);
			}
			return null;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00025074 File Offset: 0x00023274
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		private static Type GetImmutableDictionaryConstructingType(Type type)
		{
			string immutableDictionaryConstructingTypeName = type.GetImmutableDictionaryConstructingTypeName();
			if (immutableDictionaryConstructingTypeName != null)
			{
				return type.Assembly.GetType(immutableDictionaryConstructingTypeName);
			}
			return null;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002509C File Offset: 0x0002329C
		public static bool IsNonGenericStackOrQueue(this Type type)
		{
			Type typeIfExists = IEnumerableConverterFactoryHelpers.GetTypeIfExists("System.Collections.Stack, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			if (typeIfExists != null && typeIfExists.IsAssignableFrom(type))
			{
				return true;
			}
			Type typeIfExists2 = IEnumerableConverterFactoryHelpers.GetTypeIfExists("System.Collections.Queue, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			return typeIfExists2 != null && typeIfExists2.IsAssignableFrom(type);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000250DC File Offset: 0x000232DC
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2057:TypeGetType", Justification = "This method exists to allow for 'weak references' to the Stack and Queue types. If those types are used in the app, they will be preserved by the app and Type.GetType will return them. If those types are not used in the app, we don't want to preserve them here.")]
		private static Type GetTypeIfExists(string name)
		{
			return Type.GetType(name, false);
		}

		// Token: 0x040002EB RID: 747
		internal const string ImmutableConvertersUnreferencedCodeMessage = "System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.";
	}
}
