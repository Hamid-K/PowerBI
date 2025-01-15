using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B6 RID: 182
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class ReflectionEmitMemberAccessor : MemberAccessor
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		public override Func<object> CreateParameterlessConstructor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type, ConstructorInfo constructorInfo)
		{
			if (type.IsAbstract)
			{
				return null;
			}
			if (constructorInfo == null && !type.IsValueType)
			{
				return null;
			}
			DynamicMethod dynamicMethod = new DynamicMethod(ConstructorInfo.ConstructorName, JsonTypeInfo.ObjectType, Type.EmptyTypes, typeof(ReflectionEmitMemberAccessor).Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			if (constructorInfo == null)
			{
				LocalBuilder localBuilder = ilgenerator.DeclareLocal(type);
				ilgenerator.Emit(OpCodes.Ldloca_S, localBuilder);
				ilgenerator.Emit(OpCodes.Initobj, type);
				ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
				ilgenerator.Emit(OpCodes.Box, type);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Newobj, constructorInfo);
				if (type.IsValueType)
				{
					ilgenerator.Emit(OpCodes.Box, type);
				}
			}
			ilgenerator.Emit(OpCodes.Ret);
			return ReflectionEmitMemberAccessor.CreateDelegate<Func<object>>(dynamicMethod);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002D06A File Offset: 0x0002B26A
		public override Func<object[], T> CreateParameterizedConstructor<T>(ConstructorInfo constructor)
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Func<object[], T>>(ReflectionEmitMemberAccessor.CreateParameterizedConstructor(constructor));
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002D078 File Offset: 0x0002B278
		private static DynamicMethod CreateParameterizedConstructor(ConstructorInfo constructor)
		{
			Type declaringType = constructor.DeclaringType;
			ParameterInfo[] parameters = constructor.GetParameters();
			int num = parameters.Length;
			DynamicMethod dynamicMethod = new DynamicMethod(ConstructorInfo.ConstructorName, declaringType, new Type[] { typeof(object[]) }, typeof(ReflectionEmitMemberAccessor).Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			for (int i = 0; i < num; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldc_I4, i);
				ilgenerator.Emit(OpCodes.Ldelem_Ref);
				ilgenerator.Emit(OpCodes.Unbox_Any, parameterType);
			}
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002D137 File Offset: 0x0002B337
		public override JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3> CreateParameterizedConstructor<T, TArg0, TArg1, TArg2, TArg3>(ConstructorInfo constructor)
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3>>(ReflectionEmitMemberAccessor.CreateParameterizedConstructor(constructor, typeof(TArg0), typeof(TArg1), typeof(TArg2), typeof(TArg3)));
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002D16C File Offset: 0x0002B36C
		private static DynamicMethod CreateParameterizedConstructor(ConstructorInfo constructor, Type parameterType1, Type parameterType2, Type parameterType3, Type parameterType4)
		{
			Type declaringType = constructor.DeclaringType;
			ParameterInfo[] parameters = constructor.GetParameters();
			int num = parameters.Length;
			DynamicMethod dynamicMethod = new DynamicMethod(ConstructorInfo.ConstructorName, declaringType, new Type[] { parameterType1, parameterType2, parameterType3, parameterType4 }, typeof(ReflectionEmitMemberAccessor).Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			for (int i = 0; i < num; i++)
			{
				ILGenerator ilgenerator2 = ilgenerator;
				OpCode opCode;
				switch (i)
				{
				case 0:
					opCode = OpCodes.Ldarg_0;
					break;
				case 1:
					opCode = OpCodes.Ldarg_1;
					break;
				case 2:
					opCode = OpCodes.Ldarg_2;
					break;
				case 3:
					opCode = OpCodes.Ldarg_3;
					break;
				default:
					throw new InvalidOperationException();
				}
				ilgenerator2.Emit(opCode);
			}
			ilgenerator.Emit(OpCodes.Newobj, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002D240 File Offset: 0x0002B440
		public override Action<TCollection, object> CreateAddMethodDelegate<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TCollection>()
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Action<TCollection, object>>(ReflectionEmitMemberAccessor.CreateAddMethodDelegate(typeof(TCollection)));
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002D258 File Offset: 0x0002B458
		private static DynamicMethod CreateAddMethodDelegate([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type collectionType)
		{
			MethodInfo methodInfo = collectionType.GetMethod("Push") ?? collectionType.GetMethod("Enqueue");
			DynamicMethod dynamicMethod = new DynamicMethod(methodInfo.Name, typeof(void), new Type[]
			{
				collectionType,
				JsonTypeInfo.ObjectType
			}, typeof(ReflectionEmitMemberAccessor).Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Callvirt, methodInfo);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002D2ED File Offset: 0x0002B4ED
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public override Func<IEnumerable<TElement>, TCollection> CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>()
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Func<IEnumerable<TElement>, TCollection>>(ReflectionEmitMemberAccessor.CreateImmutableEnumerableCreateRangeDelegate(typeof(TCollection), typeof(TElement), typeof(IEnumerable<TElement>)));
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002D318 File Offset: 0x0002B518
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		private static DynamicMethod CreateImmutableEnumerableCreateRangeDelegate(Type collectionType, Type elementType, Type enumerableType)
		{
			MethodInfo immutableEnumerableCreateRangeMethod = collectionType.GetImmutableEnumerableCreateRangeMethod(elementType);
			DynamicMethod dynamicMethod = new DynamicMethod(immutableEnumerableCreateRangeMethod.Name, collectionType, new Type[] { enumerableType }, typeof(ReflectionEmitMemberAccessor).Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, immutableEnumerableCreateRangeMethod);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002D37E File Offset: 0x0002B57E
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		[RequiresDynamicCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public override Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection> CreateImmutableDictionaryCreateRangeDelegate<TCollection, TKey, TValue>()
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection>>(ReflectionEmitMemberAccessor.CreateImmutableDictionaryCreateRangeDelegate(typeof(TCollection), typeof(TKey), typeof(TValue), typeof(IEnumerable<KeyValuePair<TKey, TValue>>)));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002D3B4 File Offset: 0x0002B5B4
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		private static DynamicMethod CreateImmutableDictionaryCreateRangeDelegate(Type collectionType, Type keyType, Type valueType, Type enumerableType)
		{
			MethodInfo immutableDictionaryCreateRangeMethod = collectionType.GetImmutableDictionaryCreateRangeMethod(keyType, valueType);
			DynamicMethod dynamicMethod = new DynamicMethod(immutableDictionaryCreateRangeMethod.Name, collectionType, new Type[] { enumerableType }, typeof(ReflectionEmitMemberAccessor).Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, immutableDictionaryCreateRangeMethod);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002D41B File Offset: 0x0002B61B
		public override Func<object, TProperty> CreatePropertyGetter<TProperty>(PropertyInfo propertyInfo)
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Func<object, TProperty>>(ReflectionEmitMemberAccessor.CreatePropertyGetter(propertyInfo, typeof(TProperty)));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002D434 File Offset: 0x0002B634
		private static DynamicMethod CreatePropertyGetter(PropertyInfo propertyInfo, Type runtimePropertyType)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			Type declaringType = propertyInfo.DeclaringType;
			Type propertyType = propertyInfo.PropertyType;
			DynamicMethod dynamicMethod = ReflectionEmitMemberAccessor.CreateGetterMethod(propertyInfo.Name, runtimePropertyType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			if (declaringType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox, declaringType);
				ilgenerator.Emit(OpCodes.Call, getMethod);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Castclass, declaringType);
				ilgenerator.Emit(OpCodes.Callvirt, getMethod);
			}
			if (propertyType != runtimePropertyType && propertyType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Box, propertyType);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002D4E0 File Offset: 0x0002B6E0
		public override Action<object, TProperty> CreatePropertySetter<TProperty>(PropertyInfo propertyInfo)
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Action<object, TProperty>>(ReflectionEmitMemberAccessor.CreatePropertySetter(propertyInfo, typeof(TProperty)));
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
		private static DynamicMethod CreatePropertySetter(PropertyInfo propertyInfo, Type runtimePropertyType)
		{
			MethodInfo setMethod = propertyInfo.SetMethod;
			Type declaringType = propertyInfo.DeclaringType;
			Type propertyType = propertyInfo.PropertyType;
			DynamicMethod dynamicMethod = ReflectionEmitMemberAccessor.CreateSetterMethod(propertyInfo.Name, runtimePropertyType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(declaringType.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, declaringType);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			if (propertyType != runtimePropertyType && propertyType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox_Any, propertyType);
			}
			ilgenerator.Emit(declaringType.IsValueType ? OpCodes.Call : OpCodes.Callvirt, setMethod);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002D5AA File Offset: 0x0002B7AA
		public override Func<object, TProperty> CreateFieldGetter<TProperty>(FieldInfo fieldInfo)
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Func<object, TProperty>>(ReflectionEmitMemberAccessor.CreateFieldGetter(fieldInfo, typeof(TProperty)));
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
		private static DynamicMethod CreateFieldGetter(FieldInfo fieldInfo, Type runtimeFieldType)
		{
			Type declaringType = fieldInfo.DeclaringType;
			Type fieldType = fieldInfo.FieldType;
			DynamicMethod dynamicMethod = ReflectionEmitMemberAccessor.CreateGetterMethod(fieldInfo.Name, runtimeFieldType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(declaringType.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, declaringType);
			ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
			if (fieldType.IsValueType && fieldType != runtimeFieldType)
			{
				ilgenerator.Emit(OpCodes.Box, fieldType);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002D64E File Offset: 0x0002B84E
		public override Action<object, TProperty> CreateFieldSetter<TProperty>(FieldInfo fieldInfo)
		{
			return ReflectionEmitMemberAccessor.CreateDelegate<Action<object, TProperty>>(ReflectionEmitMemberAccessor.CreateFieldSetter(fieldInfo, typeof(TProperty)));
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002D668 File Offset: 0x0002B868
		private static DynamicMethod CreateFieldSetter(FieldInfo fieldInfo, Type runtimeFieldType)
		{
			Type declaringType = fieldInfo.DeclaringType;
			Type fieldType = fieldInfo.FieldType;
			DynamicMethod dynamicMethod = ReflectionEmitMemberAccessor.CreateSetterMethod(fieldInfo.Name, runtimeFieldType);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(declaringType.IsValueType ? OpCodes.Unbox : OpCodes.Castclass, declaringType);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			if (fieldType != runtimeFieldType && fieldType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox_Any, fieldType);
			}
			ilgenerator.Emit(OpCodes.Stfld, fieldInfo);
			ilgenerator.Emit(OpCodes.Ret);
			return dynamicMethod;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002D6FD File Offset: 0x0002B8FD
		private static DynamicMethod CreateGetterMethod(string memberName, Type memberType)
		{
			return new DynamicMethod(memberName + "Getter", memberType, new Type[] { JsonTypeInfo.ObjectType }, typeof(ReflectionEmitMemberAccessor).Module, true);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0002D72E File Offset: 0x0002B92E
		private static DynamicMethod CreateSetterMethod(string memberName, Type memberType)
		{
			return new DynamicMethod(memberName + "Setter", typeof(void), new Type[]
			{
				JsonTypeInfo.ObjectType,
				memberType
			}, typeof(ReflectionEmitMemberAccessor).Module, true);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002D76C File Offset: 0x0002B96C
		[return: NotNullIfNotNull("method")]
		private static T CreateDelegate<T>(DynamicMethod method) where T : Delegate
		{
			return (T)((object)((method != null) ? method.CreateDelegate(typeof(T)) : null));
		}
	}
}
