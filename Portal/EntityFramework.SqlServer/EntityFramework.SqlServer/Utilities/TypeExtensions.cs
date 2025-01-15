using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000025 RID: 37
	internal static class TypeExtensions
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
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

		// Token: 0x060003D9 RID: 985 RVA: 0x0000F46C File Offset: 0x0000D66C
		public static bool IsCollection(this Type type)
		{
			return type.IsCollection(out type);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F476 File Offset: 0x0000D676
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

		// Token: 0x060003DB RID: 987 RVA: 0x0000F4A2 File Offset: 0x0000D6A2
		public static IEnumerable<PropertyInfo> GetNonIndexerProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
				where p.IsPublic() && !p.GetIndexParameters().Any<ParameterInfo>()
				select p;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
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

		// Token: 0x060003DD RID: 989 RVA: 0x0000F510 File Offset: 0x0000D710
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

		// Token: 0x060003DE RID: 990 RVA: 0x0000F575 File Offset: 0x0000D775
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

		// Token: 0x060003DF RID: 991 RVA: 0x0000F588 File Offset: 0x0000D788
		public static Type GetTargetType(this Type type)
		{
			Type type2;
			if (!type.IsCollection(out type2))
			{
				type2 = type;
			}
			return type2;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000F5A2 File Offset: 0x0000D7A2
		public static bool TryUnwrapNullableType(this Type type, out Type underlyingType)
		{
			underlyingType = Nullable.GetUnderlyingType(type) ?? type;
			return underlyingType != type;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000F5B9 File Offset: 0x0000D7B9
		public static bool IsNullable(this Type type)
		{
			return !type.IsValueType() || Nullable.GetUnderlyingType(type) != null;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		public static bool IsValidStructuralType(this Type type)
		{
			return !type.IsGenericType() && !type.IsValueType() && !type.IsPrimitive() && !type.IsInterface() && !type.IsArray && !(type == typeof(string)) && !(type == typeof(DbGeography)) && !(type == typeof(DbGeometry)) && !(type == typeof(HierarchyId)) && type.IsValidStructuralPropertyType();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F65C File Offset: 0x0000D85C
		public static bool IsValidStructuralPropertyType(this Type type)
		{
			return !type.IsGenericTypeDefinition() && !type.IsPointer && !(type == typeof(object)) && !typeof(ComplexObject).IsAssignableFrom(type) && !typeof(EntityObject).IsAssignableFrom(type) && !typeof(StructuralObject).IsAssignableFrom(type) && !typeof(EntityKey).IsAssignableFrom(type) && !typeof(EntityReference).IsAssignableFrom(type);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		public static bool IsPrimitiveType(this Type type, out PrimitiveType primitiveType)
		{
			return TypeExtensions._primitiveTypesMap.TryGetValue(type, out primitiveType);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public static bool IsValidEdmScalarType(this Type type)
		{
			type.TryUnwrapNullableType(out type);
			PrimitiveType primitiveType;
			return type.IsPrimitiveType(out primitiveType) || type.IsEnum();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F720 File Offset: 0x0000D920
		public static string NestingNamespace(this Type type)
		{
			if (!type.IsNested)
			{
				return type.Namespace;
			}
			string fullName = type.FullName;
			return fullName.Substring(0, fullName.Length - type.Name.Length - 1).Replace('+', '.');
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F767 File Offset: 0x0000D967
		public static string FullNameWithNesting(this Type type)
		{
			if (!type.IsNested)
			{
				return type.FullName;
			}
			return type.FullName.Replace('+', '.');
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F788 File Offset: 0x0000D988
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public static bool IsPublic(this Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			return typeInfo.IsPublic || (typeInfo.IsNestedPublic && type.DeclaringType.IsPublic());
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000F813 File Offset: 0x0000DA13
		public static bool IsNotPublic(this Type type)
		{
			return !type.IsPublic();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000F81E File Offset: 0x0000DA1E
		public static MethodInfo GetOnlyDeclaredMethod(this Type type, string name)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault<MethodInfo>();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public static MethodInfo GetDeclaredMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetDeclaredMethods(name).SingleOrDefault((MethodInfo m) => (from p in m.GetParameters()
				select p.ParameterType).SequenceEqual(parameterTypes));
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000F85E File Offset: 0x0000DA5E
		public static MethodInfo GetPublicInstanceMethod(this Type type, string name, params Type[] parameterTypes)
		{
			return type.GetRuntimeMethod(name, (MethodInfo m) => m.IsPublic && !m.IsStatic, parameterTypes);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000F888 File Offset: 0x0000DA88
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Func<MethodInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return parameterTypes.Select((Type[] t) => type.GetRuntimeMethod(name, predicate, t)).FirstOrDefault((MethodInfo m) => m != null);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
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

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000F95D File Offset: 0x0000DB5D
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			return type.GetTypeInfo().DeclaredMethods;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F96A File Offset: 0x0000DB6A
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredMethods(name);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F978 File Offset: 0x0000DB78
		public static PropertyInfo GetDeclaredProperty(this Type type, string name)
		{
			return type.GetTypeInfo().GetDeclaredProperty(name);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F986 File Offset: 0x0000DB86
		public static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
		{
			return type.GetTypeInfo().DeclaredProperties;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F993 File Offset: 0x0000DB93
		public static IEnumerable<PropertyInfo> GetInstanceProperties(this Type type)
		{
			return from p in type.GetRuntimeProperties()
				where !p.IsStatic()
				select p;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public static IEnumerable<PropertyInfo> GetNonHiddenProperties(this Type type)
		{
			return from property in type.GetRuntimeProperties()
				group property by property.Name into propertyGroup
				select TypeExtensions.MostDerived(propertyGroup);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000FA1C File Offset: 0x0000DC1C
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

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000FA8C File Offset: 0x0000DC8C
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
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

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000FB24 File Offset: 0x0000DD24
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

		// Token: 0x060003FA RID: 1018 RVA: 0x0000FB70 File Offset: 0x0000DD70
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

		// Token: 0x060003FB RID: 1019 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		public static Assembly Assembly(this Type type)
		{
			return type.GetTypeInfo().Assembly;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000FBCD File Offset: 0x0000DDCD
		public static Type BaseType(this Type type)
		{
			return type.GetTypeInfo().BaseType;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000FBDA File Offset: 0x0000DDDA
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000FBF4 File Offset: 0x0000DDF4
		public static TypeAttributes Attributes(this Type type)
		{
			return type.GetTypeInfo().Attributes;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000FC01 File Offset: 0x0000DE01
		public static bool IsClass(this Type type)
		{
			return type.GetTypeInfo().IsClass;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000FC0E File Offset: 0x0000DE0E
		public static bool IsInterface(this Type type)
		{
			return type.GetTypeInfo().IsInterface;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000FC1B File Offset: 0x0000DE1B
		public static bool IsValueType(this Type type)
		{
			return type.GetTypeInfo().IsValueType;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000FC28 File Offset: 0x0000DE28
		public static bool IsAbstract(this Type type)
		{
			return type.GetTypeInfo().IsAbstract;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000FC35 File Offset: 0x0000DE35
		public static bool IsSealed(this Type type)
		{
			return type.GetTypeInfo().IsSealed;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000FC42 File Offset: 0x0000DE42
		public static bool IsEnum(this Type type)
		{
			return type.GetTypeInfo().IsEnum;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000FC4F File Offset: 0x0000DE4F
		public static bool IsSerializable(this Type type)
		{
			return type.GetTypeInfo().IsSerializable;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		public static bool IsGenericParameter(this Type type)
		{
			return type.GetTypeInfo().IsGenericParameter;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000FC69 File Offset: 0x0000DE69
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.GetTypeInfo().ContainsGenericParameters;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000FC76 File Offset: 0x0000DE76
		public static bool IsPrimitive(this Type type)
		{
			return type.GetTypeInfo().IsPrimitive;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000FC83 File Offset: 0x0000DE83
		public static IEnumerable<ConstructorInfo> GetDeclaredConstructors(this Type type)
		{
			return type.GetTypeInfo().DeclaredConstructors;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000FC90 File Offset: 0x0000DE90
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

		// Token: 0x0600040C RID: 1036 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		public static ConstructorInfo GetPublicConstructor(this Type type, params Type[] parameterTypes)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(parameterTypes);
			if (!(declaredConstructor != null) || !declaredConstructor.IsPublic)
			{
				return null;
			}
			return declaredConstructor;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public static ConstructorInfo GetDeclaredConstructor(this Type type, Func<ConstructorInfo, bool> predicate, params Type[][] parameterTypes)
		{
			return parameterTypes.Select((Type[] p) => type.GetDeclaredConstructor(p)).FirstOrDefault((ConstructorInfo c) => c != null && predicate(c));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000FD34 File Offset: 0x0000DF34
		public static bool IsSubclassOf(this Type type, Type otherType)
		{
			return type.GetTypeInfo().IsSubclassOf(otherType);
		}

		// Token: 0x040000CD RID: 205
		private static readonly Dictionary<Type, PrimitiveType> _primitiveTypesMap = new Dictionary<Type, PrimitiveType>();
	}
}
