using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B7 RID: 183
	internal sealed class ReflectionMemberAccessor : MemberAccessor
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x0002D794 File Offset: 0x0002B994
		public override Func<object> CreateParameterlessConstructor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type, ConstructorInfo ctorInfo)
		{
			if (type.IsAbstract)
			{
				return null;
			}
			if (ctorInfo != null)
			{
				return () => ctorInfo.Invoke(null);
			}
			if (!type.IsValueType)
			{
				return null;
			}
			return () => Activator.CreateInstance(type, false);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
		public override Func<object[], T> CreateParameterizedConstructor<T>(ConstructorInfo constructor)
		{
			Type typeFromHandle = typeof(T);
			int parameterCount = constructor.GetParameters().Length;
			return delegate(object[] arguments)
			{
				object[] array = new object[parameterCount];
				for (int i = 0; i < parameterCount; i++)
				{
					array[i] = arguments[i];
				}
				T t;
				try
				{
					t = (T)((object)constructor.Invoke(array));
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException ?? ex;
				}
				return t;
			};
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002D838 File Offset: 0x0002BA38
		public override JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3> CreateParameterizedConstructor<T, TArg0, TArg1, TArg2, TArg3>(ConstructorInfo constructor)
		{
			Type typeFromHandle = typeof(T);
			int parameterCount = constructor.GetParameters().Length;
			return delegate(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3)
			{
				object[] array = new object[parameterCount];
				for (int i = 0; i < parameterCount; i++)
				{
					switch (i)
					{
					case 0:
						array[0] = arg0;
						break;
					case 1:
						array[1] = arg1;
						break;
					case 2:
						array[2] = arg2;
						break;
					case 3:
						array[3] = arg3;
						break;
					default:
						throw new InvalidOperationException();
					}
				}
				return (T)((object)constructor.Invoke(array));
			};
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002D87C File Offset: 0x0002BA7C
		public override Action<TCollection, object> CreateAddMethodDelegate<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TCollection>()
		{
			Type typeFromHandle = typeof(TCollection);
			Type objectType = JsonTypeInfo.ObjectType;
			MethodInfo addMethod = typeFromHandle.GetMethod("Push") ?? typeFromHandle.GetMethod("Enqueue");
			return delegate(TCollection collection, object element)
			{
				addMethod.Invoke(collection, new object[] { element });
			};
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002D8CC File Offset: 0x0002BACC
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public override Func<IEnumerable<TElement>, TCollection> CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>()
		{
			MethodInfo immutableEnumerableCreateRangeMethod = typeof(TCollection).GetImmutableEnumerableCreateRangeMethod(typeof(TElement));
			return (Func<IEnumerable<TElement>, TCollection>)immutableEnumerableCreateRangeMethod.CreateDelegate(typeof(Func<IEnumerable<TElement>, TCollection>));
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002D908 File Offset: 0x0002BB08
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public override Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection> CreateImmutableDictionaryCreateRangeDelegate<TCollection, TKey, TValue>()
		{
			MethodInfo immutableDictionaryCreateRangeMethod = typeof(TCollection).GetImmutableDictionaryCreateRangeMethod(typeof(TKey), typeof(TValue));
			return (Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection>)immutableDictionaryCreateRangeMethod.CreateDelegate(typeof(Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection>));
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002D950 File Offset: 0x0002BB50
		public override Func<object, TProperty> CreatePropertyGetter<TProperty>(PropertyInfo propertyInfo)
		{
			MethodInfo getMethodInfo = propertyInfo.GetMethod;
			return (object obj) => (TProperty)((object)getMethodInfo.Invoke(obj, null));
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002D97C File Offset: 0x0002BB7C
		public override Action<object, TProperty> CreatePropertySetter<TProperty>(PropertyInfo propertyInfo)
		{
			MethodInfo setMethodInfo = propertyInfo.SetMethod;
			return delegate(object obj, TProperty value)
			{
				setMethodInfo.Invoke(obj, new object[] { value });
			};
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002D9A8 File Offset: 0x0002BBA8
		public override Func<object, TProperty> CreateFieldGetter<TProperty>(FieldInfo fieldInfo)
		{
			return (object obj) => (TProperty)((object)fieldInfo.GetValue(obj));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002D9D0 File Offset: 0x0002BBD0
		public override Action<object, TProperty> CreateFieldSetter<TProperty>(FieldInfo fieldInfo)
		{
			return delegate(object obj, TProperty value)
			{
				fieldInfo.SetValue(obj, value);
			};
		}
	}
}
