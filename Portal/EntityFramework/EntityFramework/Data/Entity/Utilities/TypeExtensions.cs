using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000090 RID: 144
	internal static class TypeExtensions
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00010F30 File Offset: 0x0000F130
		static TypeExtensions()
		{
			foreach (PrimitiveType primitiveType in PrimitiveType.GetEdmPrimitiveTypes())
			{
				if (!TypeExtensions._primitiveTypesMap.ContainsKey(primitiveType.ClrEquivalentType))
				{
					TypeExtensions._primitiveTypesMap.Add(primitiveType.ClrEquivalentType, primitiveType);
				}
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public static bool IsCollection(this Type type)
		{
			return type.IsCollection(out type);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00010FAE File Offset: 0x0000F1AE
		public static bool IsCollection(this Type type, out Type elementType)
		{
			elementType = type.TryGetElementType(typeof(ICollection<>));
			if (elementType == null || type.IsArray)
			{
				elementType = type;
				return false;
			}
			return true;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00010FDA File Offset: 0x0000F1DA
		public static IEnumerable<PropertyInfo> GetNonIndexerProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
				where p.IsPublic() && !p.GetIndexParameters().Any<ParameterInfo>()
				select p;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00011008 File Offset: 0x0000F208
		public static Type TryGetElementType(this Type type, Type interfaceOrBaseType)
		{
			if (type.IsGenericTypeDefinition())
			{
				return null;
			}
			List<Type> list = type.GetGenericTypeImplementations(interfaceOrBaseType).ToList<Type>();
			if (list.Count != 1)
			{
				return null;
			}
			return list[0].GetGenericArguments().FirstOrDefault<Type>();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00011048 File Offset: 0x0000F248
		public static IEnumerable<Type> GetGenericTypeImplementations(this Type type, Type interfaceOrBaseType)
		{
			if (!type.IsGenericTypeDefinition())
			{
				IEnumerable<Type> enumerable;
				if (!interfaceOrBaseType.IsInterface())
				{
					enumerable = type.GetBaseTypes();
				}
				else
				{
					IEnumerable<Type> interfaces = type.GetInterfaces();
					enumerable = interfaces;
				}
				return from t in enumerable.Union(new Type[] { type })
					where t.IsGenericType() && t.GetGenericTypeDefinition() == interfaceOrBaseType
					select t;
			}
			return Enumerable.Empty<Type>();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000110AD File Offset: 0x0000F2AD
		public static IEnumerable<Type> GetBaseTypes(this Type type)
		{
			type = type.BaseType();
			while (type != null)
			{
				yield return type;
				type = type.BaseType();
			}
			yield break;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000110C0 File Offset: 0x0000F2C0
		public static Type GetTargetType(this Type type)
		{
			Type type2;
			if (!type.IsCollection(out type2))
			{
				type2 = type;
			}
			return type2;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000110DA File Offset: 0x0000F2DA
		public static bool TryUnwrapNullableType(this Type type, out Type underlyingType)
		{
			underlyingType = Nullable.GetUnderlyingType(type) ?? type;
			return underlyingType != type;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000110F1 File Offset: 0x0000F2F1
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType() || Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001110C File Offset: 0x0000F30C
		public static bool IsValidStructuralType(this Type type)
		{
			return !type.IsGenericType() && !type.IsValueType() && !type.IsPrimitive() && !type.IsInterface() && !type.IsArray && !(type == typeof(string)) && !(type == typeof(DbGeography)) && !(type == typeof(DbGeometry)) && !(type == typeof(HierarchyId)) && type.IsValidStructuralPropertyType();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011194 File Offset: 0x0000F394
		public static bool IsValidStructuralPropertyType(this Type type)
		{
			return !type.IsGenericTypeDefinition() && !type.IsPointer && !(type == typeof(object)) && !typeof(ComplexObject).IsAssignableFrom(type) && !typeof(EntityObject).IsAssignableFrom(type) && !typeof(StructuralObject).IsAssignableFrom(type) && !typeof(EntityKey).IsAssignableFrom(type) && !typeof(EntityReference).IsAssignableFrom(type);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011220 File Offset: 0x0000F420
		public static bool IsPrimitiveType(this Type type, out PrimitiveType primitiveType)
		{
			return TypeExtensions._primitiveTypesMap.TryGetValue(type, out primitiveType);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00011230 File Offset: 0x0000F430
		public static T CreateInstance<T>(this Type type, Func<string, string, string> typeMessageFactory, Func<string, Exception> exceptionFactory = null)
		{
			Func<string, Exception> func;
			if ((func = exceptionFactory) == null && (func = TypeExtensions.<>c__14<T>.<>9__14_0) == null)
			{
				func = (TypeExtensions.<>c__14<T>.<>9__14_0 = (string s) => new InvalidOperationException(s));
			}
			exceptionFactory = func;
			if (!typeof(T).IsAssignableFrom(type))
			{
				throw exceptionFactory(typeMessageFactory(type.ToString(), typeof(T).ToString()));
			}
			return type.CreateInstance(exceptionFactory);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public static T CreateInstance<T>(this Type type, Func<string, Exception> exceptionFactory = null)
		{
			Func<string, Exception> func;
			if ((func = exceptionFactory) == null && (func = TypeExtensions.<>c__15<T>.<>9__15_0) == null)
			{
				func = (TypeExtensions.<>c__15<T>.<>9__15_0 = (string s) => new InvalidOperationException(s));
			}
			exceptionFactory = func;
			if (type.GetDeclaredConstructor(new Type[0]) == null)
			{
				throw exceptionFactory(Strings.CreateInstance_NoParameterlessConstructor(type));
			}
			if (type.IsAbstract())
			{
				throw exceptionFactory(Strings.CreateInstance_AbstractType(type));
			}
			if (type.IsGenericType())
			{
				throw exceptionFactory(Strings.CreateInstance_GenericType(type));
			}
			return (T)((object)Activator.CreateInstance(type, true));
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001132C File Offset: 0x0000F52C
		public static bool IsValidEdmScalarType(this Type type)
		{
			type.TryUnwrapNullableType(out type);
			PrimitiveType primitiveType;
			return type.IsPrimitiveType(out primitiveType) || type.IsEnum();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011354 File Offset: 0x0000F554
		public static string NestingNamespace(this Type type)
		{
			if (!type.IsNested)
			{
				return type.Namespace;
			}
			string fullName = type.FullName;
			return fullName.Substring(0, fullName.Length - type.Name.Length - 1).Replace('+', '.');
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001139B File Offset: 0x0000F59B
		public static string FullNameWithNesting(this Type type)
		{
			if (!type.IsNested)
			{
				return type.FullName;
			}
			return type.FullName.Replace('+', '.');
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000113BC File Offset: 0x0000F5BC
		public static bool OverridesEqualsOrGetHashCode(this Type type)
		{
			while (type != typeof(object))
			{
				if (type.GetDeclaredMethods().Any((MethodInfo m) => (m.Name == "Equals" || m.Name == "GetHashCode") && m.DeclaringType != typeof(object) && m.GetBaseDefinition().DeclaringType == typeof(object)))
				{
					return true;
				}
				type = type.BaseType();
			}
			return false;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00011414 File Offset: 0x0000F614
		public static bool IsPublic(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsPublic || (typeInfo.IsNestedPublic && type.DeclaringType.IsPublic());
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00011447 File Offset: 0x0000F647
		public static bool IsNotPublic(this Type type)
		{
			return !type.IsPublic();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00011452 File Offset: 0x0000F652
		public static MethodInfo GetOnlyDeclaredMethod(this Type type, string name)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011460 File Offset: 0x0000F660
		public static MethodInfo GetDeclaredMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault((MethodInfo m) => (from p in m.GetParameters()
				select p.ParameterType).SequenceEqual(parameterTypes));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00011492 File Offset: 0x0000F692
		public static MethodInfo GetPublicInstanceMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetRuntimeMethod(name, (MethodInfo m) => m.IsPublic && !m.IsStatic, parameterTypes);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000114BC File Offset: 0x0000F6BC
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Func<MethodInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return parameterTypes.Select((Type[] t) => type.GetRuntimeMethod(name, predicate, t)).FirstOrDefault((MethodInfo m) => m != null);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001151C File Offset: 0x0000F71C
		private static MethodInfo GetRuntimeMethod(this Type type, string name, Func<MethodInfo, bool> predicate, Type[] parameterTypes)
		{
			MethodInfo[] methods = type.GetRuntimeMethods().Where(delegate(MethodInfo m)
			{
				if (name == m.Name && predicate(m))
				{
					return (from p in m.GetParameters()
						select p.ParameterType).SequenceEqual(parameterTypes);
				}
				return false;
			}).ToArray<MethodInfo>();
			if (methods.Length == 1)
			{
				return methods[0];
			}
			return methods.SingleOrDefault((MethodInfo m) => !methods.Any((MethodInfo m2) => m2.DeclaringType.IsSubclassOf(m.DeclaringType)));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00011591 File Offset: 0x0000F791
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return type.GetTypeInfo().DeclaredMethods;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001159E File Offset: 0x0000F79E
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredMethods(name);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000115AC File Offset: 0x0000F7AC
		public static PropertyInfo GetDeclaredProperty(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredProperty(name);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000115BA File Offset: 0x0000F7BA
		public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
		{
			return type.GetTypeInfo().DeclaredProperties;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000115C7 File Offset: 0x0000F7C7
		public static IEnumerable<PropertyInfo> GetInstanceProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
				where !p.IsStatic()
				select p;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public static IEnumerable<PropertyInfo> GetNonHiddenProperties(this Type type)
		{
			return from property in type.GetRuntimeProperties()
				group property by property.Name into propertyGroup
				select TypeExtensions.MostDerived(propertyGroup);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00011650 File Offset: 0x0000F850
		private static PropertyInfo MostDerived(IEnumerable<PropertyInfo> properties)
		{
			PropertyInfo propertyInfo = null;
			foreach (PropertyInfo propertyInfo2 in properties)
			{
				if (propertyInfo == null || (propertyInfo.DeclaringType != null && propertyInfo.DeclaringType.IsAssignableFrom(propertyInfo2.DeclaringType)))
				{
					propertyInfo = propertyInfo2;
				}
			}
			return propertyInfo;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public static PropertyInfo GetAnyProperty(this Type type, string name)
		{
			List<PropertyInfo> list = (from p in type.GetRuntimeProperties()
				where p.Name == name
				select p).ToList<PropertyInfo>();
			if (list.Count<PropertyInfo>() > 1)
			{
				throw new AmbiguousMatchException();
			}
			return list.SingleOrDefault<PropertyInfo>();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001170C File Offset: 0x0000F90C
		public static PropertyInfo GetInstanceProperty(this Type type, string name)
		{
			List<PropertyInfo> list = (from p in type.GetRuntimeProperties()
				where p.Name == name && !p.IsStatic()
				select p).ToList<PropertyInfo>();
			if (list.Count<PropertyInfo>() > 1)
			{
				throw new AmbiguousMatchException();
			}
			return list.SingleOrDefault<PropertyInfo>();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00011758 File Offset: 0x0000F958
		public static PropertyInfo GetStaticProperty(this Type type, string name)
		{
			List<PropertyInfo> list = (from p in type.GetRuntimeProperties()
				where p.Name == name && p.IsStatic()
				select p).ToList<PropertyInfo>();
			if (list.Count<PropertyInfo>() > 1)
			{
				throw new AmbiguousMatchException();
			}
			return list.SingleOrDefault<PropertyInfo>();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public static PropertyInfo GetTopProperty(this Type type, string name)
		{
			PropertyInfo declaredProperty;
			for (;;)
			{
				TypeInfo typeInfo = type.GetTypeInfo();
				declaredProperty = typeInfo.GetDeclaredProperty(name);
				if (declaredProperty != null && !(declaredProperty.GetMethod ?? declaredProperty.SetMethod).IsStatic)
				{
					break;
				}
				type = typeInfo.BaseType;
				if (!(type != null))
				{
					goto Block_3;
				}
			}
			return declaredProperty;
			Block_3:
			return null;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00011801 File Offset: 0x0000FA01
		public static Type BaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001180E File Offset: 0x0000FA0E
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001181B File Offset: 0x0000FA1B
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00011828 File Offset: 0x0000FA28
		public static TypeAttributes Attributes(this Type type)
		{
			return type.GetTypeInfo().Attributes;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00011835 File Offset: 0x0000FA35
		public static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00011842 File Offset: 0x0000FA42
		public static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001184F File Offset: 0x0000FA4F
		public static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001185C File Offset: 0x0000FA5C
		public static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00011869 File Offset: 0x0000FA69
		public static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00011876 File Offset: 0x0000FA76
		public static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00011883 File Offset: 0x0000FA83
		public static bool IsSerializable(this Type type)
		{
			return type.GetTypeInfo().IsSerializable;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00011890 File Offset: 0x0000FA90
		public static bool IsGenericParameter(this Type type)
		{
			return type.GetTypeInfo().IsGenericParameter;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001189D File Offset: 0x0000FA9D
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000118AA File Offset: 0x0000FAAA
		public static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000118B7 File Offset: 0x0000FAB7
		public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000118C4 File Offset: 0x0000FAC4
		public static ConstructorInfo GetDeclaredConstructor(this Type type, params Type[] parameterTypes)
		{
			return type.GetDeclaredConstructors().SingleOrDefault(delegate(ConstructorInfo c)
			{
				if (!c.IsStatic)
				{
					return (from p in c.GetParameters()
						select p.ParameterType).SequenceEqual(parameterTypes);
				}
				return false;
			});
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public static ConstructorInfo GetPublicConstructor(this Type type, params Type[] parameterTypes)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(parameterTypes);
			if (!(declaredConstructor != null) || !declaredConstructor.IsPublic)
			{
				return null;
			}
			return declaredConstructor;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011924 File Offset: 0x0000FB24
		public static ConstructorInfo GetDeclaredConstructor(this Type type, Func<ConstructorInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return parameterTypes.Select((Type[] p) => type.GetDeclaredConstructor(p)).FirstOrDefault((ConstructorInfo c) => c != null && predicate(c));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00011968 File Offset: 0x0000FB68
		public static bool IsSubclassOf(this Type type, Type otherType)
		{
			return type.GetTypeInfo().IsSubclassOf(otherType);
		}

		// Token: 0x0400011E RID: 286
		private static readonly Dictionary<Type, PrimitiveType> _primitiveTypesMap = new Dictionary<Type, PrimitiveType>();
	}
}
