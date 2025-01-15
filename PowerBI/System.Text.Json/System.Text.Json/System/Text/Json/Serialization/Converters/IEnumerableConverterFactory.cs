using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Reflection;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000CE RID: 206
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class IEnumerableConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002EC91 File Offset: 0x0002CE91
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		public IEnumerableConverterFactory()
		{
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002EC99 File Offset: 0x0002CE99
		public override bool CanConvert(Type typeToConvert)
		{
			return typeof(IEnumerable).IsAssignableFrom(typeToConvert);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002ECAC File Offset: 0x0002CEAC
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The ctor is marked RequiresUnreferencedCode.")]
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type type = null;
			Type type2 = null;
			Type type3;
			Type type4;
			if (typeToConvert.IsArray)
			{
				if (typeToConvert.GetArrayRank() > 1)
				{
					return UnsupportedTypeConverterFactory.CreateUnsupportedConverterForType(typeToConvert, null);
				}
				type3 = typeof(ArrayConverter<, >);
				type = typeToConvert.GetElementType();
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericBaseClass(typeof(List<>))) != null)
			{
				type3 = typeof(ListOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericBaseClass(typeof(Dictionary<, >))) != null)
			{
				Type[] array = type4.GetGenericArguments();
				type3 = typeof(DictionaryOfTKeyTValueConverter<, , >);
				type2 = array[0];
				type = array[1];
			}
			else if (typeToConvert.IsImmutableDictionaryType())
			{
				Type[] array = typeToConvert.GetGenericArguments();
				type3 = typeof(ImmutableDictionaryOfTKeyTValueConverterWithReflection<, , >);
				type2 = array[0];
				type = array[1];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericInterface(typeof(IDictionary<, >))) != null)
			{
				Type[] array = type4.GetGenericArguments();
				type3 = typeof(IDictionaryOfTKeyTValueConverter<, , >);
				type2 = array[0];
				type = array[1];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericInterface(typeof(IReadOnlyDictionary<, >))) != null)
			{
				Type[] array = type4.GetGenericArguments();
				type3 = typeof(IReadOnlyDictionaryOfTKeyTValueConverter<, , >);
				type2 = array[0];
				type = array[1];
			}
			else if (typeToConvert.IsImmutableEnumerableType())
			{
				type3 = typeof(ImmutableEnumerableOfTConverterWithReflection<, >);
				type = typeToConvert.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericInterface(typeof(IList<>))) != null)
			{
				type3 = typeof(IListOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericInterface(typeof(ISet<>))) != null)
			{
				type3 = typeof(ISetOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericInterface(typeof(ICollection<>))) != null)
			{
				type3 = typeof(ICollectionOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericBaseClass(typeof(Stack<>))) != null)
			{
				type3 = typeof(StackOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericBaseClass(typeof(Queue<>))) != null)
			{
				type3 = typeof(QueueOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericBaseClass(typeof(ConcurrentStack<>))) != null)
			{
				type3 = typeof(ConcurrentStackOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericBaseClass(typeof(ConcurrentQueue<>))) != null)
			{
				type3 = typeof(ConcurrentQueueOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if ((type4 = typeToConvert.GetCompatibleGenericInterface(typeof(IEnumerable<>))) != null)
			{
				type3 = typeof(IEnumerableOfTConverter<, >);
				type = type4.GetGenericArguments()[0];
			}
			else if (typeof(IDictionary).IsAssignableFrom(typeToConvert))
			{
				if (typeToConvert == typeof(IDictionary))
				{
					return IEnumerableConverterFactory.s_converterForIDictionary;
				}
				type3 = typeof(IDictionaryConverter<>);
			}
			else if (typeof(IList).IsAssignableFrom(typeToConvert))
			{
				if (typeToConvert == typeof(IList))
				{
					return IEnumerableConverterFactory.s_converterForIList;
				}
				type3 = typeof(IListConverter<>);
			}
			else if (typeToConvert.IsNonGenericStackOrQueue())
			{
				type3 = typeof(StackOrQueueConverterWithReflection<>);
			}
			else
			{
				if (typeToConvert == typeof(IEnumerable))
				{
					return IEnumerableConverterFactory.s_converterForIEnumerable;
				}
				type3 = typeof(IEnumerableConverter<>);
			}
			int num = type3.GetGenericArguments().Length;
			Type type5;
			if (num == 1)
			{
				type5 = type3.MakeGenericType(new Type[] { typeToConvert });
			}
			else if (num == 2)
			{
				type5 = type3.MakeGenericType(new Type[] { typeToConvert, type });
			}
			else
			{
				type5 = type3.MakeGenericType(new Type[] { typeToConvert, type2, type });
			}
			return (JsonConverter)Activator.CreateInstance(type5, BindingFlags.Instance | BindingFlags.Public, null, null, null);
		}

		// Token: 0x040003F5 RID: 1013
		private static readonly IDictionaryConverter<IDictionary> s_converterForIDictionary = new IDictionaryConverter<IDictionary>();

		// Token: 0x040003F6 RID: 1014
		private static readonly IEnumerableConverter<IEnumerable> s_converterForIEnumerable = new IEnumerableConverter<IEnumerable>();

		// Token: 0x040003F7 RID: 1015
		private static readonly IListConverter<IList> s_converterForIList = new IListConverter<IList>();
	}
}
